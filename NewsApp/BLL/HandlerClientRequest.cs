using System.Net.Sockets;
using System.Text.Json;
using NewsApp.BLL.Network;
using NewsApp.Common;
using NewsApp.DAL;
using NewsApp.Data;
using NewsApp.Network;

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
        private readonly IAccountRepository _accountRepository;
        private readonly ArticleRepository _articleRepository;
        private AccountRepository _accountRepo = new AccountRepository();
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


                    string clientEndPoint = _client.Client.RemoteEndPoint?.ToString() ?? "Unknown";
                    string displayUser = "Guest";


                    if (!string.IsNullOrEmpty(_currentUsername))
                    {
                        displayUser = _currentUsername;
                    }

                    else if (packet.Command == MessageProtocol.RequestCommand.LOGIN)
                    {
                        try
                        {
                            var accLogin = JsonSerializer.Deserialize<Account>(packet.Payload);
                            displayUser = $"{accLogin?.Username} (Login Request)";
                        }
                        catch { }
                    }
                    else if (packet.Command == MessageProtocol.RequestCommand.REGISTER)
                    {
                        try
                        {
                            var accReg = JsonSerializer.Deserialize<RegisterModel>(packet.Payload);
                            displayUser = $"{accReg?.Username} (Register Request)";
                        }
                        catch { }
                    }


                    string logMessage = $"[Request] [{clientEndPoint}] User: {displayUser} -> Command: {packet.Command}";

                    _serverSocket.Log(logMessage);

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

        private void ProcessRequest(Packet packet)
        {
            string command = packet.Command;
            string payload = packet.Payload;
            switch (command)
            {
                case MessageProtocol.RequestCommand.FORGOT_PASSWORD:
                    {
                        try
                        {
                            string rawPayload = packet.Payload ?? "";
                            string emailFromClient = "";

                            if (rawPayload.Trim().StartsWith("{"))
                            {
                                var data = JsonSerializer.Deserialize<Dictionary<string, string>>(rawPayload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                if (data != null && data.ContainsKey("Email"))
                                {
                                    emailFromClient = data["Email"].Trim();
                                }
                            }
                            else
                            {
                                emailFromClient = rawPayload.Trim().Replace("\"", "");
                            }

                            Console.WriteLine($"[DEBUG] Đang kiểm tra Email: '[{emailFromClient}]' - Độ dài: {emailFromClient.Length}");

                            if (string.IsNullOrEmpty(emailFromClient))
                            {
                                SendResponse(MessageProtocol.ResponseCommand.FORGOT_PASSWORD_FAIL, "Email không hợp lệ.");
                                break;
                            }

                            if (_accountRepo.CheckEmailExists(emailFromClient))
                            {
                                string otp = new Random().Next(100000, 999999).ToString();
                                OtpManager.SaveOtp(emailFromClient, otp);

                                if (MailService.SendOTP(emailFromClient, otp))
                                {
                                    SendResponse(MessageProtocol.ResponseCommand.FORGOT_PASSWORD_SUCCESS, "Mã OTP đã được gửi về Email của bạn.");
                                }
                                else
                                {
                                    SendResponse(MessageProtocol.ResponseCommand.FORGOT_PASSWORD_FAIL, "Hệ thống không thể gửi mail lúc này.");
                                }
                            }
                            else
                            {
                                SendResponse(MessageProtocol.ResponseCommand.FORGOT_PASSWORD_FAIL, "Email này chưa được đăng ký trên hệ thống.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Lỗi xử lý Quên mật khẩu: {ex.Message}");
                            SendResponse(MessageProtocol.ResponseCommand.FORGOT_PASSWORD_FAIL, "Lỗi xử lý dữ liệu.");
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.RESET_PASSWORD:
                    {
                        try
                        {
                            var resetData = JsonSerializer.Deserialize<ResetPasswordModel>(packet.Payload);
                            if (OtpManager.VerifyOtp(resetData.Email, resetData.OTP))
                            {
                                string hashedPassword = resetData.NewPassword;
                                _accountRepo.ResetPassword(resetData.Email, hashedPassword);

                                SendResponse(MessageProtocol.ResponseCommand.RESET_PASSWORD_SUCCESS, "Thành công.");
                            }
                            else
                            {
                                SendResponse(MessageProtocol.ResponseCommand.RESET_PASSWORD_FAIL, "Sai OTP.");
                            }
                        }
                        catch
                        {
                            SendResponse(MessageProtocol.ResponseCommand.RESET_PASSWORD_FAIL, "Lỗi dữ liệu.");
                        }
                    }
                    break;
                case MessageProtocol.RequestCommand.LOGIN:
                    {
                        Account? account = JsonSerializer.Deserialize<Account>(payload);

                        if (account != null)
                        {

                            account.Password = HashHelper.Hash(account.Password);

                            if (_accountRepository.Login(account))
                            {
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

                            if (_userRepository.CheckEmailExists(regModel.Email))
                            {
                                SendRegisterFail(payload, "Email này đã được sử dụng cho tài khoản khác");
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
                            List<Article> pendingList = _articleRepository.GetPendingArticles();

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

                case MessageProtocol.RequestCommand.APPROVE_ARTICLE:
                    {
                        try
                        {
                            int articleId = JsonSerializer.Deserialize<int>(payload);

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

                case MessageProtocol.RequestCommand.CHANGE_PASSWORD:
                    {
                        try
                        {
                            var changePassModel = JsonSerializer.Deserialize<ChangePasswordModel>(payload);
                            if (changePassModel != null)
                            {
                                string hashedOldPass = HashHelper.Hash(changePassModel.OldPassword);

                                if (_accountRepository.VerifyPassword(changePassModel.Username, hashedOldPass))
                                {
                                    string hashedNewPass = HashHelper.Hash(changePassModel.NewPassword);

                                    if (_accountRepository.UpdatePassword(changePassModel.Username, hashedNewPass))
                                    {
                                        Packet success = new(
                                            MessageProtocol.ResponseCommand.CHANGE_PASSWORD_SUCCESS,
                                            "Đổi mật khẩu thành công!"
                                        );
                                        _streamWriter.WriteLine(JsonSerializer.Serialize(success));
                                    }
                                    else
                                    {
                                        Packet fail = new(
                                            MessageProtocol.ResponseCommand.CHANGE_PASSWORD_FAIL,
                                            "Lỗi hệ thống: Không thể cập nhật mật khẩu."
                                        );
                                        _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                                    }
                                }
                                else
                                {
                                    Packet fail = new(
                                        MessageProtocol.ResponseCommand.CHANGE_PASSWORD_FAIL,
                                        "Mật khẩu cũ không chính xác."
                                    );
                                    _streamWriter.WriteLine(JsonSerializer.Serialize(fail));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _serverSocket.Log($"Error Change Password: {ex.Message}");
                            Packet error = new(MessageProtocol.ResponseCommand.CHANGE_PASSWORD_FAIL, "Lỗi xử lý tại Server");
                            _streamWriter.WriteLine(JsonSerializer.Serialize(error));
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void SendResponse(string command, string payload)
        {
            if (isConnected && _client.Connected)
            {
                try
                {
                    var responsePacket = new Packet { Command = command, Payload = payload };
                    string jsonString = JsonSerializer.Serialize(responsePacket);

                    _streamWriter.WriteLine(jsonString);
                    _streamWriter.Flush();
                }
                catch (Exception ex)
                {
                    _serverSocket.Log($"[Error Send] {ex.Message}");
                    isConnected = false;
                }
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

        private void SendRegisterFail(string payload, string message)
        {
            Packet failResponse = new(MessageProtocol.ResponseCommand.REGISTER_FAIL, message);
            _streamWriter.WriteLine(JsonSerializer.Serialize(failResponse));
        }
    }
}
