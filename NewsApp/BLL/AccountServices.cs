using NewsApp.Common;
using NewsApp.Data;
using NewsApp.Network;
using System.Text.Json;
using System.Collections.Generic;
using System;

namespace NewsApp.BLL
{
    public class AccountServices
    {
        private readonly ClientSocket _socketClient;
        public bool RegisterProcessing { get; set; }
        public User? UserAuthenticated { get; private set; }
        public bool IsConnected => _socketClient != null && _socketClient.IsConnected;

        public event Action<User?, string>? LoginResult;           // Kết quả đăng nhập
        public event Action<bool, string>? RegisterResult;         // Kết quả đăng ký
        public event Action<string>? ConnectionStatusChanged;      // Trạng thái kết nối thay đổi
        public event Action<Packet, string>? ReceivedData;         // Dữ liệu chung khác
        public event Action<string>? ErrorData;                    // Báo lỗi chung
        public event Action<bool, string>? UpdateProfileResult;    // Kết quả cập nhật hồ sơ
        public event Action<bool, string>? ForgotPasswordResult;
        public event Action<bool, string>? ResetPasswordResult;
        public event Action<bool, string>? ChangePasswordResult;

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
                    var user = JsonSerializer.Deserialize<User>(payload);
                    UserAuthenticated = user;
                    LoginResult?.Invoke(user, "Đăng nhập thành công");
                    break;

                case MessageProtocol.ResponseCommand.LOGIN_FAIL:
                    string loginErr = JsonSerializer.Deserialize<string>(payload) ?? "Đăng nhập thất bại";
                    UserAuthenticated = null;
                    LoginResult?.Invoke(null, loginErr);
                    break;

                case MessageProtocol.ResponseCommand.REGISTER_SUCCESS:
                    RegisterResult?.Invoke(true, "Đăng ký thành công");
                    break;

                case MessageProtocol.ResponseCommand.REGISTER_FAIL:
                    RegisterResult?.Invoke(false, "Đăng ký thất bại");
                    break;

                case MessageProtocol.ResponseCommand.UPDATE_PROFILE_SUCCESS:
                    UpdateProfileResult?.Invoke(true, "Cập nhật thông tin thành công!");
                    break;

                case MessageProtocol.ResponseCommand.UPDATE_PROFILE_FAIL:
                    UpdateProfileResult?.Invoke(false, "Cập nhật thất bại: " + payload);
                    break;

                case MessageProtocol.ResponseCommand.FORGOT_PASSWORD_SUCCESS:
                    ForgotPasswordResult?.Invoke(true, "Mã OTP đã được gửi đến email của bạn.");
                    break;

                case MessageProtocol.ResponseCommand.FORGOT_PASSWORD_FAIL:
                    ForgotPasswordResult?.Invoke(false, payload);
                    break;

                case MessageProtocol.ResponseCommand.RESET_PASSWORD_SUCCESS:
                    ResetPasswordResult?.Invoke(true, "Đổi mật khẩu thành công. Vui lòng đăng nhập lại.");
                    break;

                case MessageProtocol.ResponseCommand.RESET_PASSWORD_FAIL:
                    ResetPasswordResult?.Invoke(false, payload);
                    break;

                case MessageProtocol.ResponseCommand.CHANGE_PASSWORD_SUCCESS:
                    ChangePasswordResult?.Invoke(true, "Đổi mật khẩu thành công!");
                    break;

                case MessageProtocol.ResponseCommand.CHANGE_PASSWORD_FAIL:
                    string msg = JsonSerializer.Deserialize<string>(payload) ?? "Đổi mật khẩu thất bại";
                    ChangePasswordResult?.Invoke(false, msg);
                    break;

                default:
                    ReceivedData?.Invoke(packet, "Unhandled command");
                    break;
            }
        }


        public void Login(string userName, string password)
        {
            if (!IsConnected) return;

            Account account = new() { Username = userName, Password = password };

            Packet requestPacket = new(MessageProtocol.RequestCommand.LOGIN, account);
            _socketClient.SendRequest(requestPacket);
        }

        public void Register(string userName, string password, string email, DateTime birthDay, string fullName, string role)
        {
            if (!IsConnected) return;

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

        public void Logout()
        {
            if (!IsConnected) return;

            UserAuthenticated = null;

            Packet requestPacket = new(MessageProtocol.RequestCommand.LOGOUT, "");
            _socketClient.SendRequest(requestPacket);
        }

        public void UpdateProfile(User user)
        {
            if (!IsConnected) return;

            string payload = JsonSerializer.Serialize(user);
            Packet request = new Packet(MessageProtocol.RequestCommand.UPDATE_PROFILE, payload);

            _socketClient.SendRequest(request);
        }

        public void RequestForgotPassword(string email)
        {
            if (!IsConnected) return;

            var packet = new Packet(MessageProtocol.RequestCommand.FORGOT_PASSWORD, email);
            _socketClient.SendRequest(packet);
        }

        public void RequestResetPassword(string email, string otp, string newPass)
        {
            if (!IsConnected) return;

            var data = new ResetPasswordModel
            {
                Email = email,
                OTP = otp,
                NewPassword = newPass
            };

            string payload = JsonSerializer.Serialize(data);

            var packet = new Packet(MessageProtocol.RequestCommand.RESET_PASSWORD, payload);
            _socketClient.SendRequest(packet);
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