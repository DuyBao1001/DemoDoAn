using System.Net.Sockets;
using System.Text.Json;
using NewsApp.BLL.Network;
using NewsApp.Common;
using NewsApp.DAL;
using NewsApp.Data;

namespace NewsApp.BLL
{
    public class HandlerClientRequest
    {
        private readonly TcpClient _client;
        private readonly ServerSocket _serverSocket;
        private readonly NetworkStream _networkStream;
        private readonly StreamReader _streamReader;
        private readonly StreamWriter _streamWriter;

        private bool isConnected = true;

        // Các Repository làm việc với database
        private readonly IAccountRepository _accountRepository;
        private readonly ArticleRepository _articleRepository;

        private readonly UserRepository _userRepository;

        private readonly CategoryRepository _categoryRepository;
        private readonly CommentRepository _commentRepository;

        public HandlerClientRequest(TcpClient client, ServerSocket serverSocket)
        {
            _client = client;
            _serverSocket = serverSocket;
            _networkStream = _client.GetStream();
            _streamReader = new StreamReader(_networkStream);
            _streamWriter = new StreamWriter(_networkStream) { AutoFlush = true };
            _accountRepository = new AccountRepository();
            _articleRepository = new();
            _userRepository = new();
            _categoryRepository = new();
            _commentRepository = new();
        }

        // Hàm xử lý request của client
        public void HandleRequest()
        {
            try
            {
                while (isConnected && _client.Connected)
                {
                    string? request = _streamReader.ReadLine();
                    if (request == null)
                    {
                        Disconnect();
                        _serverSocket.Log($"[Client - INFO] Client {_client.Client.RemoteEndPoint} đã ngắt kết nối.");
                        break;
                    }

                    Packet? packet = JsonSerializer.Deserialize<Packet>(request);
                    if (packet == null)
                    {
                        _serverSocket.Log("[Client - ERROR] Request không hợp lệ.");
                        continue;
                    }

                    // --- BẮT ĐẦU XỬ LÝ HIỂN THỊ TÊN USER CHO MONITORING ---

                    string clientEndPoint = _client.Client.RemoteEndPoint?.ToString() ?? "Unknown";
                    string displayUser = "Guest"; // Mặc định là khách

                    // TRƯỜNG HỢP 1: User đã đăng nhập thành công từ trước
                    // Biến _currentUsername đã được set trong lần xử lý LOGIN trước đó
                    if (!string.IsNullOrEmpty(_currentUsername))
                    {
                        displayUser = _currentUsername;
                    }
                    // TRƯỜNG HỢP 2: User CHƯA đăng nhập, nhưng đang gửi yêu cầu LOGIN
                    // Chúng ta "nhìn trộm" gói tin để lấy Username họ đang nhập
                    else if (packet.Command == MessageProtocol.RequestCommand.LOGIN)
                    {
                        try
                        {
                            var accLogin = JsonSerializer.Deserialize<Account>(packet.Payload);
                            // Hiển thị dạng: "admin (Đang đăng nhập)"
                            displayUser = $"{accLogin?.Username} (Login Request)";
                        }
                        catch { }
                    }
                    // TRƯỜNG HỢP 3: User đang gửi yêu cầu ĐĂNG KÝ
                    else if (packet.Command == MessageProtocol.RequestCommand.REGISTER)
                    {
                        try
                        {
                            var accReg = JsonSerializer.Deserialize<RegisterModel>(packet.Payload);
                            displayUser = $"{accReg?.Username} (Register Request)";
                        }
                        catch { }
                    }

                    // Tạo nội dung log
                    // Ví dụ: [Request] [127.0.0.1:5544] User: admin -> Command: GET_LATEST_ARTICLES
                    string logMessage = $"[Request] [{clientEndPoint}] User: {displayUser} -> Command: {packet.Command}";

                    // Gửi log ra màn hình
                    _serverSocket.Log(logMessage);

                    // --- KẾT THÚC XỬ LÝ LOG ---

                    // Sau khi log xong mới đưa vào xử lý logic
                    ProcessRequest(packet);
                }
            }
            catch (IOException ioe)
            {
                _serverSocket.Log($"[Client - ERROR] {ioe.Message}");
                Disconnect();
            }
        }

        private string? _currentUsername;

        // Hàm xử lý request dựa trên command được yêu cầu
        private void ProcessRequest(Packet packet)
        {
            string command = packet.Command;
            string payload = packet.Payload;
            switch (command)
            {
                case MessageProtocol.RequestCommand.LOGIN:
                    {
                        Account? account = JsonSerializer.Deserialize<Account>(payload);

                        // --- BẮT ĐẦU SỬA ĐỔI ---
                        if (account != null)
                        {
                            // Băm mật khẩu người dùng vừa nhập thành mã hóa (ví dụ: "123" -> "a665a4...")
                            // Sau đó hàm Login bên dưới sẽ so sánh chuỗi mã hóa này với chuỗi trong Database
                            account.Password = HashHelper.Hash(account.Password);

                            if (_accountRepository.Login(account))
                            {
                                // Kiểm tra xem user đã online chưa
                                if (_serverSocket.IsUserOnline(account.Username))
                                {
                                    Packet failResponse = new(
                                        MessageProtocol.ResponseCommand.LOGIN_FAIL,
                                        (object)"Tài khoản này đang được đăng nhập ở nơi khác!"
                                    );
                                    _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                                    return;
                                }

                                Account? accDetails = _accountRepository.GetAccountByUsername(
                                    account.Username
                                );
                                if (accDetails != null)
                                {
                                    User? user = _userRepository.GetByAccountID(accDetails.AccountID);
                                    if (user != null)
                                    {
                                        // Thêm user vào danh sách online
                                        _serverSocket.AddUser(account.Username, _client);
                                        _currentUsername = account.Username;

                                        Packet response = new(
                                            MessageProtocol.ResponseCommand.LOGIN_SUCCESS,
                                            user
                                        );
                                        string jsonResponse = JsonSerializer.Serialize(response);
                                        _streamWriter.WriteLine(jsonResponse);
                                        return;
                                    }
                                }
                            }
                        }
                        // --- KẾT THÚC SỬA ĐỔI ---

                        // Nếu code chạy xuống đây nghĩa là: account bị null HOẶC Login thất bại
                        Packet loginFailResponse = new(
                            MessageProtocol.ResponseCommand.LOGIN_FAIL,
                            (object)"Sai tài khoản hoặc mật khẩu"
                        );
                        _streamWriter.WriteLine(JsonSerializer.Serialize(loginFailResponse));
                    }
                    break;
                case MessageProtocol.RequestCommand.REGISTER:
                    {
                        RegisterModel? regModel = JsonSerializer.Deserialize<RegisterModel>(
                            payload
                        );
                        if (regModel != null)
                        {
                            if (_accountRepository.CheckUserNameExists(regModel.Username))
                            {
                                Packet failResponse = new(
                                    MessageProtocol.ResponseCommand.REGISTER_FAIL,
                                    (object)"Tên đăng nhập đã tồn tại"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                                return;
                            }

                            Account newAccount = new()
                            {
                                Username = regModel.Username,
                                Password = HashHelper.Hash(regModel.Password),
                            };
                            if (_accountRepository.Register(newAccount))
                            {
                                Account? createdAcc = _accountRepository.GetAccountByUsername(
                                    regModel.Username
                                );
                                if (createdAcc != null)
                                {
                                    User newUser = new()
                                    {
                                        FullName = regModel.FullName,
                                        Email = regModel.Email,
                                        BirthDay = regModel.BirthDay,
                                        Role = regModel.Role,
                                        AccountID = createdAcc.AccountID,
                                        UserName = createdAcc.Username,
                                    };

                                    if (_userRepository.Save(newUser))
                                    {
                                        Packet successResponse = new(
                                            MessageProtocol.ResponseCommand.REGISTER_SUCCESS,
                                            (object)"Đăng ký thành công"
                                        );
                                        _streamWriter.WriteLine(
                                            JsonSerializer.Serialize(successResponse)
                                        );
                                        return;
                                    }
                                }
                            }
                        }
                        Packet errorResponse = new(
                            MessageProtocol.ResponseCommand.REGISTER_FAIL,
                            (object)"Đăng ký thất bại"
                        );
                        _streamWriter.WriteLine(JsonSerializer.Serialize(errorResponse));
                    }
                    break;
                case MessageProtocol.RequestCommand.LOGOUT:
                    {
                        if (!string.IsNullOrEmpty(_currentUsername))
                        {
                            _serverSocket.RemoveUser(_currentUsername);
                            _currentUsername = null;
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.GET_CATEGORIES:
                    {
                        IList<Category> categories = _categoryRepository.GetAll();
                        if (categories != null)
                        {
                            Packet successResponse = new(
                                MessageProtocol.ResponseCommand.GET_CATEGORIES_SUCCESS,
                                categories
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(successResponse));
                        }
                        else
                        {
                            Packet failResponse = new(
                                MessageProtocol.ResponseCommand.GET_CATEGORIES_FAIL,
                                (object)"Lỗi lấy danh sách danh mục"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.GET_LATEST_ARTICLES:
                    {
                        IList<Article> articles = _articleRepository.GetLatestArticles(100);
                        if (articles != null)
                        {
                            Packet successResponse = new(
                                MessageProtocol.ResponseCommand.GET_LATEST_ARTICLES_SUCCESS,
                                articles
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(successResponse));
                        }
                        else
                        {
                            Packet failResponse = new(
                                MessageProtocol.ResponseCommand.GET_LATEST_ARTICLES_FAIL,
                                (object)"Lỗi lấy danh sách bài viết"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.GET_ARTICLES_BY_CATEGORY:
                    {
                        int categoryId = int.Parse(packet.Payload.ToString());
                        IList<Article> articles = _articleRepository.GetArticlesByCategory(
                            categoryId
                        );
                        if (articles != null)
                        {
                            Packet successResponse = new(
                                MessageProtocol.ResponseCommand.GET_ARTICLES_BY_CATEGORY_SUCCESS,
                                articles
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(successResponse));
                        }
                        else
                        {
                            Packet failResponse = new(
                                MessageProtocol.ResponseCommand.GET_ARTICLES_BY_CATEGORY_FAIL,
                                (object)"Lỗi lấy danh sách bài viết theo danh mục"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.SEARCH_ARTICLES:
                    {
                        string keyword = packet.Payload.ToString();
                        IList<Article> articles = _articleRepository.SearchArticles(keyword);
                        if (articles != null)
                        {
                            Packet successResponse = new(
                                MessageProtocol.ResponseCommand.SEARCH_ARTICLES_SUCCESS,
                                articles
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(successResponse));
                        }
                        else
                        {
                            Packet failResponse = new(
                                MessageProtocol.ResponseCommand.SEARCH_ARTICLES_FAIL,
                                (object)"Lỗi tìm kiếm bài viết"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.GET_COMMENTS:
                    {
                        var data = JsonSerializer.Deserialize<Dictionary<string, int>>(payload);
                        if (data != null && data.ContainsKey("ArticleID"))
                        {
                            int articleId = data["ArticleID"];
                            List<Comment> comments = _commentRepository.GetCommentsByArticleId(
                                articleId
                            );
                            Packet successResponse = new(
                                MessageProtocol.ResponseCommand.GET_COMMENTS_SUCCESS,
                                comments
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(successResponse));
                        }
                        else
                        {
                            Packet failResponse = new(
                                MessageProtocol.ResponseCommand.GET_COMMENTS_FAIL,
                                (object)"Lỗi lấy danh sách bình luận"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.POST_COMMENT:
                    {
                        try
                        {
                            Comment? comment = JsonSerializer.Deserialize<Comment>(payload);
                            if (comment != null)
                            {
                                comment.Timestamp = DateTime.Now;
                                if (_commentRepository.Save(comment))
                                {
                                    Packet successResponse = new(
                                        MessageProtocol.ResponseCommand.POST_COMMENT_SUCCESS,
                                        (object)"Bình luận thành công"
                                    );
                                    _streamWriter.WriteLine(
                                        JsonSerializer.Serialize(successResponse)
                                    );
                                }
                                else
                                {
                                    string errorMessage =
                                        _commentRepository.LastError ?? "Lỗi lưu bình luận";
                                    Packet failResponse = new(
                                        MessageProtocol.ResponseCommand.POST_COMMENT_FAIL,
                                        (object)errorMessage
                                    );
                                    _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                                }
                            }
                            else
                            {
                                Packet failResponse = new(
                                    MessageProtocol.ResponseCommand.POST_COMMENT_FAIL,
                                    (object)"Dữ liệu bình luận không hợp lệ"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                            }
                        }
                        catch
                        {
                            Packet failResponse = new(
                                MessageProtocol.ResponseCommand.POST_COMMENT_FAIL,
                                (object)"Lỗi dữ liệu bình luận"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
                        }
                    }
                    break;

                // Admin Handlers
                case MessageProtocol.RequestCommand.GET_ALL_USERS:
                    var users = _userRepository.GetAll();
                    Packet usersResponse = new(
                        MessageProtocol.ResponseCommand.GET_ALL_USERS_SUCCESS,
                        users
                    );
                    _streamWriter.WriteLine(JsonSerializer.Serialize(usersResponse));
                    break;

                case MessageProtocol.RequestCommand.DELETE_USER:
                    try
                    {
                        var deleteUserRequest = JsonSerializer.Deserialize<Dictionary<string, int>>(
                            payload
                        );
                        if (deleteUserRequest != null && deleteUserRequest.ContainsKey("UserID"))
                        {
                            int userIdToDelete = deleteUserRequest["UserID"];
                            // Create dummy user with required fields to satisfy compiler/runtime
                            User userToDelete = new User
                            {
                                Id = userIdToDelete,
                                FullName = "",
                                Email = "",
                                UserName = "",
                                Role = "",
                                BirthDay = DateTime.MinValue,
                            };

                            if (_userRepository.Delete(userToDelete))
                            {
                                Packet success = new(
                                    MessageProtocol.ResponseCommand.DELETE_USER_SUCCESS,
                                    "Xóa thành công"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                            }
                            else
                            {
                                Packet fail = new(
                                    MessageProtocol.ResponseCommand.DELETE_USER_FAIL,
                                    _userRepository.LastError ?? "Xóa thất bại"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                            }
                        }
                    }
                    catch { }
                    break;

                case MessageProtocol.RequestCommand.DELETE_ARTICLE:
                    try
                    {
                        var deleteArticleRequest = JsonSerializer.Deserialize<
                            Dictionary<string, int>
                        >(payload);
                        if (
                            deleteArticleRequest != null
                            && deleteArticleRequest.ContainsKey("ArticleID")
                        )
                        {
                            int articleIdToDelete = deleteArticleRequest["ArticleID"];
                            Article articleToDelete = new Article
                            {
                                ArticleID = articleIdToDelete,
                                Title = "",
                                Content = "",
                                AuthorName = "",
                                CategoryName = "",
                                PublishDate = DateTime.MinValue,
                            };

                            if (_articleRepository.Delete(articleToDelete))
                            {
                                Packet success = new(
                                    MessageProtocol.ResponseCommand.DELETE_ARTICLE_SUCCESS,
                                    "Xóa thành công"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                            }
                            else
                            {
                                Packet fail = new(
                                    MessageProtocol.ResponseCommand.DELETE_ARTICLE_FAIL,
                                    _articleRepository.LastError ?? "Xóa thất bại"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                            }
                        }
                    }
                    catch { }
                    break;

                case MessageProtocol.RequestCommand.ADD_CATEGORY:
                    try
                    {
                        var addCategoryRequest = JsonSerializer.Deserialize<
                            Dictionary<string, string>
                        >(payload);
                        if (
                            addCategoryRequest != null
                            && addCategoryRequest.ContainsKey("CategoryName")
                        )
                        {
                            string categoryName = addCategoryRequest["CategoryName"];
                            if (_categoryRepository.Save(new Category { Name = categoryName }))
                            {
                                Packet success = new(
                                    MessageProtocol.ResponseCommand.ADD_CATEGORY_SUCCESS,
                                    "Thêm thành công"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                            }
                            else
                            {
                                Packet fail = new(
                                    MessageProtocol.ResponseCommand.ADD_CATEGORY_FAIL,
                                    _categoryRepository.LastError ?? "Thêm thất bại"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                            }
                        }
                    }
                    catch { }
                    break;

                case MessageProtocol.RequestCommand.UPDATE_CATEGORY:
                    try
                    {
                        var updateCategoryRequest = JsonSerializer.Deserialize<JsonElement>(
                            payload
                        );
                        if (
                            updateCategoryRequest.TryGetProperty(
                                "CategoryID",
                                out JsonElement idElement
                            )
                            && updateCategoryRequest.TryGetProperty(
                                "CategoryName",
                                out JsonElement nameElement
                            )
                        )
                        {
                            int catId = idElement.GetInt32();
                            string catName = nameElement.GetString() ?? "";
                            if (
                                _categoryRepository.Update(
                                    new Category { CategoryID = catId, Name = catName }
                                )
                            )
                            {
                                Packet success = new(
                                    MessageProtocol.ResponseCommand.UPDATE_CATEGORY_SUCCESS,
                                    "Cập nhật thành công"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                            }
                            else
                            {
                                Packet fail = new(
                                    MessageProtocol.ResponseCommand.UPDATE_CATEGORY_FAIL,
                                    _categoryRepository.LastError ?? "Cập nhật thất bại"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                            }
                        }
                    }
                    catch { }
                    break;

                case MessageProtocol.RequestCommand.DELETE_CATEGORY:
                    try
                    {
                        var deleteCategoryRequest = JsonSerializer.Deserialize<
                            Dictionary<string, int>
                        >(payload);
                        if (
                            deleteCategoryRequest != null
                            && deleteCategoryRequest.ContainsKey("CategoryID")
                        )
                        {
                            int catId = deleteCategoryRequest["CategoryID"];
                            if (
                                _categoryRepository.Delete(
                                    new Category { CategoryID = catId, Name = "" }
                                )
                            )
                            {
                                Packet success = new(
                                    MessageProtocol.ResponseCommand.DELETE_CATEGORY_SUCCESS,
                                    "Xóa thành công"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                            }
                            else
                            {
                                Packet fail = new(
                                    MessageProtocol.ResponseCommand.DELETE_CATEGORY_FAIL,
                                    _categoryRepository.LastError ?? "Xóa thất bại"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                            }
                        }
                    }
                    catch { }
                    break;

                case MessageProtocol.RequestCommand.POST_ARTICLE:
                    try
                    {
                        var articleData = JsonSerializer.Deserialize<JsonElement>(payload);
                        Article newArticle = new()
                        {
                            Title = articleData.GetProperty("Title").GetString() ?? "",
                            CategoryID = articleData.GetProperty("CategoryID").GetInt32(),
                            CategoryName =
                                articleData.GetProperty("CategoryName").GetString() ?? "",
                            Content = articleData.GetProperty("Content").GetString() ?? "",
                            Image =
                                articleData.GetProperty("Image").ValueKind == JsonValueKind.Null
                                    ? null
                                    : JsonSerializer.Deserialize<byte[]>(
                                        articleData.GetProperty("Image").GetRawText()
                                    ),
                            AuthorID = articleData.GetProperty("AuthorID").GetInt32(),
                            AuthorName = articleData.GetProperty("AuthorName").GetString() ?? "",
                            PublishDate = DateTime.Now,
                        };

                        if (_articleRepository.Save(newArticle))
                        {
                            Packet success = new(
                                MessageProtocol.ResponseCommand.POST_ARTICLE_SUCCESS,
                                "Đăng bài thành công, đang chờ duyệt"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                        }
                        else
                        {
                            Packet fail = new(
                                MessageProtocol.ResponseCommand.POST_ARTICLE_FAIL,
                                _articleRepository.LastError ?? "Đăng bài thất bại"
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error posting article: {ex.Message}");
                        Packet fail = new(
                            MessageProtocol.ResponseCommand.POST_ARTICLE_FAIL,
                            $"Lỗi: {ex.Message}"
                        );
                        _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                    }
                    break;
                case MessageProtocol.RequestCommand.GET_PENDING_ARTICLES:
                    {
                        try
                        {
                            // 1. Gọi Repository để lấy danh sách bài có Status = 0
                            List<Article> pendingList = _articleRepository.GetPendingArticles();

                            // 2. Gửi phản hồi thành công kèm dữ liệu
                            Packet response = new(
                                MessageProtocol.ResponseCommand.GET_PENDING_ARTICLES_SUCCESS,
                                pendingList
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(response));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error getting pending articles: " + ex.Message);
                            Packet errorResponse = new(
                                MessageProtocol.ResponseCommand.GET_PENDING_ARTICLES_FAIL,
                                "Lỗi server: " + ex.Message
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(errorResponse));
                        }
                    }
                    break;

                // --- CASE: DUYỆT BÀI VIẾT (ĐỔI STATUS TỪ 0 -> 1) ---
                case MessageProtocol.RequestCommand.APPROVE_ARTICLE:
                    {
                        try
                        {
                            // 1. Giải mã Payload để lấy ArticleID (dạng int)
                            int articleId = JsonSerializer.Deserialize<int>(payload);

                            // 2. Gọi Repository để thực hiện update
                            bool isApproved = _articleRepository.ApproveArticle(articleId);

                            if (isApproved)
                            {
                                Packet response = new(
                                    MessageProtocol.ResponseCommand.APPROVE_ARTICLE_SUCCESS,
                                    "Duyệt bài viết thành công!"
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(response));
                            }
                            else
                            {
                                Packet response = new(
                                    MessageProtocol.ResponseCommand.APPROVE_ARTICLE_FAIL,
                                    "Không tìm thấy bài viết hoặc lỗi DB."
                                );
                                _streamWriter.WriteLine(JsonSerializer.Serialize(response));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error approving article: " + ex.Message);
                            Packet errorResponse = new(
                                MessageProtocol.ResponseCommand.APPROVE_ARTICLE_FAIL,
                                "Lỗi xử lý: " + ex.Message
                            );
                            _streamWriter.WriteLine(JsonSerializer.Serialize(errorResponse));
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.UPDATE_PROFILE:
                    {
                        try
                        {
                            User? userToUpdate = JsonSerializer.Deserialize<User>(payload);
                            if (userToUpdate != null)
                            {
                                // Gọi Repository để update vào DB
                                if (_userRepository.Update(userToUpdate))
                                {
                                    Packet success = new(
                                        MessageProtocol.ResponseCommand.UPDATE_PROFILE_SUCCESS,
                                        "Cập nhật thành công"
                                    );
                                    _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                                }
                                else
                                {
                                    Packet fail = new Packet(
                                        MessageProtocol.ResponseCommand.UPDATE_PROFILE_FAIL,
                                        "Lỗi Database"
                                    );
                                    _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Update Profile Error: " + ex.Message);
                        }
                    }
                    break;


                default:
                    break;
            }
        }

        private void Disconnect()
        {
            if (!string.IsNullOrEmpty(_currentUsername))
            {
                _serverSocket.RemoveUser(_currentUsername);
            }

            _streamReader.Close();
            _streamWriter.Close();
            _networkStream.Close();
            _client.Close();
            isConnected = false;
        }
    }
}
