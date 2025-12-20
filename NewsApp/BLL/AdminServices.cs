using NewsApp.Common;
using NewsApp.Data;
using NewsApp.Network;
using System.Text.Json;

namespace NewsApp.BLL
{
    public class AdminServices
    {
        private readonly ClientSocket _clientSocket;

        public bool IsConnected { get { return _clientSocket != null && _clientSocket.IsConnected; } }

        public event Action<List<User>>? GetAllUsersResult;
        public event Action<bool, string>? DeleteUserResult;
        public event Action<bool, string>? DeleteArticleResult;
        public event Action<bool, string>? AddCategoryResult;
        public event Action<bool, string>? UpdateCategoryResult;
        public event Action<bool, string>? DeleteCategoryResult;
        public event Action<string>? ResponseError;
        public event Action<string>? ConnectionStatusChanged;
        public event Action<string>? ErrorData;

        public AdminServices()
        {
            _clientSocket = new ClientSocket();
            _clientSocket.Start();

            _clientSocket.DataReceive += HandleDataReceive;
            _clientSocket.ConnectionStatusChanged += HandleConnectionStatusChanged;
            _clientSocket.ErrorData += HandleErrorData;
        }

        private void HandleDataReceive(Packet? packet)
        {
            if (packet == null)
            {
                ResponseError?.Invoke("Không nhận được phản hồi từ server");
                return;
            }

            string command = packet.Command;
            string payload = packet.Payload;

            switch (command)
            {
                case MessageProtocol.ResponseCommand.GET_ALL_USERS_SUCCESS:
                    try
                    {
                        var users = JsonSerializer.Deserialize<List<User>>(payload);
                        GetAllUsersResult?.Invoke(users ?? []);
                    }
                    catch
                    {
                        GetAllUsersResult?.Invoke([]);
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_ALL_USERS_FAIL:
                    GetAllUsersResult?.Invoke([]);
                    break;

                case MessageProtocol.ResponseCommand.DELETE_USER_SUCCESS:
                    DeleteUserResult?.Invoke(true, "Xóa người dùng thành công");
                    break;
                case MessageProtocol.ResponseCommand.DELETE_USER_FAIL:
                    DeleteUserResult?.Invoke(false, "Xóa người dùng thất bại");
                    break;

                case MessageProtocol.ResponseCommand.DELETE_ARTICLE_SUCCESS:
                    DeleteArticleResult?.Invoke(true, "Xóa bài viết thành công");
                    break;
                case MessageProtocol.ResponseCommand.DELETE_ARTICLE_FAIL:
                    DeleteArticleResult?.Invoke(false, "Xóa bài viết thất bại");
                    break;

                case MessageProtocol.ResponseCommand.ADD_CATEGORY_SUCCESS:
                    AddCategoryResult?.Invoke(true, "Thêm chuyên mục thành công");
                    break;
                case MessageProtocol.ResponseCommand.ADD_CATEGORY_FAIL:
                    AddCategoryResult?.Invoke(false, "Thêm chuyên mục thất bại");
                    break;

                case MessageProtocol.ResponseCommand.UPDATE_CATEGORY_SUCCESS:
                    UpdateCategoryResult?.Invoke(true, "Cập nhật chuyên mục thành công");
                    break;
                case MessageProtocol.ResponseCommand.UPDATE_CATEGORY_FAIL:
                    UpdateCategoryResult?.Invoke(false, "Cập nhật chuyên mục thất bại");
                    break;

                case MessageProtocol.ResponseCommand.DELETE_CATEGORY_SUCCESS:
                    DeleteCategoryResult?.Invoke(true, "Xóa chuyên mục thành công");
                    break;
                case MessageProtocol.ResponseCommand.DELETE_CATEGORY_FAIL:
                    DeleteCategoryResult?.Invoke(false, "Xóa chuyên mục thất bại");
                    break;

                default:
                    break;
            }
        }

        public void GetAllUsers()
        {
            if (IsConnected)
            {
                Packet requestPacket = new(MessageProtocol.RequestCommand.GET_ALL_USERS, null);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void DeleteUser(int userId)
        {
            if (IsConnected)
            {
                var data = new { UserID = userId };
                Packet requestPacket = new(MessageProtocol.RequestCommand.DELETE_USER, data);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void DeleteArticle(int articleId)
        {
            if (IsConnected)
            {
                var data = new { ArticleID = articleId };
                Packet requestPacket = new(MessageProtocol.RequestCommand.DELETE_ARTICLE, data);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void AddCategory(string categoryName)
        {
            if (IsConnected)
            {
                var data = new { CategoryName = categoryName };
                Packet requestPacket = new(MessageProtocol.RequestCommand.ADD_CATEGORY, data);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void UpdateCategory(int categoryId, string categoryName)
        {
            if (IsConnected)
            {
                var data = new { CategoryID = categoryId, CategoryName = categoryName };
                Packet requestPacket = new(MessageProtocol.RequestCommand.UPDATE_CATEGORY, data);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            if (IsConnected)
            {
                var data = new { CategoryID = categoryId };
                Packet requestPacket = new(MessageProtocol.RequestCommand.DELETE_CATEGORY, data);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        private void HandleConnectionStatusChanged(string message)
        {
            ConnectionStatusChanged?.Invoke(message);
        }

        private void HandleErrorData(string message)
        {
            ErrorData?.Invoke(message);
        }
    }
}
