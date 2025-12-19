
using NewsApp.Common;
using NewsApp.Data;
using NewsApp.Network;
using System.Text.Json;
using System.Collections.Generic;

namespace NewsApp.BLL
{

    public class AccountServices
    {
        private readonly ClientSocket _socketClient;

        // Kết quả đăng ký
        public bool RegisterProcessing { get; set; }

        // User đã đăng nhập
        public User? UserAuthenticated { get; private set; }

        // Khai báo các sự kiện
        public event Action<User?, string>? LoginResult;
        public event Action<bool, string>? RegisterResult;
        public event Action<string>? ConnectionStatusChanged;
        public event Action<Packet, string>? ReceivedData;
        public event Action<string>? ErrorData;
        public event Action<bool, string>? UpdateProfileResult;
        public event Action<bool, string>? ChangePasswordResult;

        public bool IsConnected { get { return _socketClient != null && _socketClient.IsConnected; } }



        public AccountServices()
        {
            _socketClient = new ClientSocket();
            _socketClient.DataReceive += HandleDataReceive;
            _socketClient.Start();
        }

        private void HandleDataReceive(Packet? packet)
        {
            if (packet == null)
            {
                ErrorData?.Invoke("Dữ liệu nhận được không hợp lệ");
                return;
            }

            string command = packet.Command;
            string payload = packet.Payload;

            switch (command)
            {
                case MessageProtocol.ResponseCommand.LOGIN_SUCCESS:
                    {
                        User? user = JsonSerializer.Deserialize<User>(payload);
                        UserAuthenticated = user;
                        LoginResult?.Invoke(user, "Đăng nhập thành công");
                    }
                    break;
                case MessageProtocol.ResponseCommand.LOGIN_FAIL:
                    {
                        string errorMessage = JsonSerializer.Deserialize<string>(payload) ?? "Đăng nhập thất bại";
                        LoginResult?.Invoke(null, errorMessage);
                    }
                    break;
                case MessageProtocol.ResponseCommand.REGISTER_SUCCESS:
                    {
                        RegisterResult?.Invoke(true, "Đăng ký thành công");
                    }
                    break;
                case MessageProtocol.ResponseCommand.REGISTER_FAIL:
                    {
                        RegisterResult?.Invoke(false, "Đăng ký thất bại");
                    }
                    break;
                case MessageProtocol.ResponseCommand.UPDATE_PROFILE_SUCCESS:
                    UpdateProfileResult?.Invoke(true, "Cập nhật thông tin thành công!");
                    break;

                case MessageProtocol.ResponseCommand.UPDATE_PROFILE_FAIL:
                    UpdateProfileResult?.Invoke(false, "Cập nhật thất bại: " + packet.Payload);
                    break;
                case MessageProtocol.ResponseCommand.CHANGE_PASSWORD_SUCCESS:
                    ChangePasswordResult?.Invoke(true, "Đổi mật khẩu thành công!");
                    break;

                case MessageProtocol.ResponseCommand.CHANGE_PASSWORD_FAIL:
                    string msg = JsonSerializer.Deserialize<string>(payload) ?? "Đổi mật khẩu thất bại";
                    ChangePasswordResult?.Invoke(false, msg);
                    break;
                default:
                    break;
            }
        }

        private void HandleLoginResult(User? packet, string command)
        {
            LoginResult?.Invoke(packet, command);
        }

        private void HandleRegisterResult(bool result, string command)
        {
            RegisterResult?.Invoke(result, command);
        }

        private void HandleErrorData(string message)
        {
            ErrorData?.Invoke(message);
        }

        private void HandleConnectionStatusChanged(string message)
        {
            ConnectionStatusChanged?.Invoke(message);
        }

        public void Login(string userName, string password)
        {
            if (IsConnected)
            {
                Account account = new()
                {
                    Username = userName,
                    Password = password,
                };
                Packet requestPacket = new(MessageProtocol.RequestCommand.LOGIN, account);
                _socketClient.SendRequest(requestPacket);
            }
        }

        public void Register(string userName, string password, string email, DateTime birthDay, string fullName, string role)
        {
            if (IsConnected)
            {
                RegisterModel registerModel = new()
                {
                    Username = userName,
                    Password = password,
                    Email = email,
                    BirthDay = birthDay,
                    FullName = fullName,
                    Role = role
                };
                Packet requestPacket = new(MessageProtocol.RequestCommand.REGISTER, registerModel);
                _socketClient.SendRequest(requestPacket);
            }
        }

        public void Logout()
        {
            if (IsConnected)
            {
                Packet requestPacket = new(MessageProtocol.RequestCommand.LOGOUT, "");
                _socketClient.SendRequest(requestPacket);
            }
        }

        public void UpdateProfile(User user)
        {
            if (IsConnected)
            {
                // Serialize User thành JSON (bao gồm cả Avatar byte[])
                string payload = JsonSerializer.Serialize(user);

                Packet request = new Packet(
                    MessageProtocol.RequestCommand.UPDATE_PROFILE,
                    payload
                );

                _socketClient.SendRequest(request);
            }
        }

        public void ChangePassword(string username, string oldPass, string newPass)
        {
            if (IsConnected)
            {

                ChangePasswordModel model = new ChangePasswordModel
                {
                    Username = username,
                    OldPassword = oldPass,
                    NewPassword = newPass
                };

                string payload = JsonSerializer.Serialize(model);
                Packet request = new Packet(MessageProtocol.RequestCommand.CHANGE_PASSWORD, payload);

                _socketClient.SendRequest(request);
            }
        }
    }
}