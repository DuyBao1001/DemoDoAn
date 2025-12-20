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

        // --- CÁC THUỘC TÍNH (PROPERTIES) ---

        // Cờ đánh dấu đang xử lý đăng ký (nếu cần dùng cho UI loading)
        public bool RegisterProcessing { get; set; }

        // Lưu thông tin User sau khi đăng nhập thành công
        public User? UserAuthenticated { get; private set; }

        // Kiểm tra xem socket còn kết nối không
        public bool IsConnected => _socketClient != null && _socketClient.IsConnected;

        // --- CÁC SỰ KIỆN (EVENTS) - UI sẽ lắng nghe các sự kiện này ---
        public event Action<User?, string>? LoginResult;           // Kết quả đăng nhập
        public event Action<bool, string>? RegisterResult;         // Kết quả đăng ký
        public event Action<string>? ConnectionStatusChanged;      // Trạng thái kết nối thay đổi
        public event Action<Packet, string>? ReceivedData;         // Dữ liệu chung khác
        public event Action<string>? ErrorData;                    // Báo lỗi chung
        public event Action<bool, string>? UpdateProfileResult;    // Kết quả cập nhật hồ sơ

        // Đã thêm sự kiện cho tính năng Quên/Đặt lại mật khẩu
        public event Action<bool, string>? ForgotPasswordResult;
        public event Action<bool, string>? ResetPasswordResult;

        // --- KHỞI TẠO ---
        public AccountServices()
        {
            // LƯU Ý: Nếu app có nhiều màn hình, nên dùng Singleton cho Socket để tránh tạo nhiều kết nối.
            _socketClient = new ClientSocket();
            _socketClient.DataReceive += HandleDataReceive;
            _socketClient.Start();
        }

        // --- 1. XỬ LÝ DỮ LIỆU NHẬN TỪ SERVER ---
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
                // --- ĐĂNG NHẬP ---
                case MessageProtocol.ResponseCommand.LOGIN_SUCCESS:
                    var user = JsonSerializer.Deserialize<User>(payload);
                    UserAuthenticated = user; // Lưu user vào biến local
                    LoginResult?.Invoke(user, "Đăng nhập thành công");
                    break;

                case MessageProtocol.ResponseCommand.LOGIN_FAIL:
                    // Nhận thông báo lỗi từ server (nếu có), hoặc dùng thông báo mặc định
                    string loginErr = JsonSerializer.Deserialize<string>(payload) ?? "Đăng nhập thất bại";
                    UserAuthenticated = null;
                    LoginResult?.Invoke(null, loginErr);
                    break;

                // --- ĐĂNG KÝ ---
                case MessageProtocol.ResponseCommand.REGISTER_SUCCESS:
                    RegisterResult?.Invoke(true, "Đăng ký thành công");
                    break;

                case MessageProtocol.ResponseCommand.REGISTER_FAIL:
                    RegisterResult?.Invoke(false, "Đăng ký thất bại");
                    break;

                // --- CẬP NHẬT HỒ SƠ ---
                case MessageProtocol.ResponseCommand.UPDATE_PROFILE_SUCCESS:
                    // Có thể cập nhật lại UserAuthenticated tại đây nếu server gửi về data mới
                    UpdateProfileResult?.Invoke(true, "Cập nhật thông tin thành công!");
                    break;

                case MessageProtocol.ResponseCommand.UPDATE_PROFILE_FAIL:
                    UpdateProfileResult?.Invoke(false, "Cập nhật thất bại: " + payload);
                    break;

                // --- QUÊN MẬT KHẨU (Đã bổ sung) ---
                case MessageProtocol.ResponseCommand.FORGOT_PASSWORD_SUCCESS:
                    ForgotPasswordResult?.Invoke(true, "Mã OTP đã được gửi đến email của bạn.");
                    break;

                case MessageProtocol.ResponseCommand.FORGOT_PASSWORD_FAIL:
                    // Payload chứa lý do lỗi (ví dụ: Email không tồn tại)
                    ForgotPasswordResult?.Invoke(false, payload);
                    break;

                // --- ĐẶT LẠI MẬT KHẨU (Đã bổ sung) ---
                case MessageProtocol.ResponseCommand.RESET_PASSWORD_SUCCESS:
                    ResetPasswordResult?.Invoke(true, "Đổi mật khẩu thành công. Vui lòng đăng nhập lại.");
                    break;

                case MessageProtocol.ResponseCommand.RESET_PASSWORD_FAIL:
                    ResetPasswordResult?.Invoke(false, payload);
                    break;

                default:
                    // Các gói tin khác không thuộc Account thì đẩy ra event chung
                    ReceivedData?.Invoke(packet, "Unhandled command");
                    break;
            }
        }

        // --- 2. CÁC HÀM GỬI YÊU CẦU (REQUEST) ---

        public void Login(string userName, string password)
        {
            if (!IsConnected) return;

            Account account = new() { Username = userName, Password = password };

            // Tạo gói tin và gửi đi
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

            // Xóa thông tin user đang lưu ở client
            UserAuthenticated = null;

            Packet requestPacket = new(MessageProtocol.RequestCommand.LOGOUT, "");
            _socketClient.SendRequest(requestPacket);
        }

        public void UpdateProfile(User user)
        {
            if (!IsConnected) return;

            // Serialize user thành JSON string để gửi đi
            string payload = JsonSerializer.Serialize(user);
            Packet request = new Packet(MessageProtocol.RequestCommand.UPDATE_PROFILE, payload);

            _socketClient.SendRequest(request);
        }

        public void RequestForgotPassword(string email)
        {
            if (!IsConnected) return;

            // Đã đổi từ Send() sang SendRequest() để đồng bộ
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

            // Serialize model thành chuỗi JSON trước khi đóng gói
            string payload = JsonSerializer.Serialize(data);

            var packet = new Packet(MessageProtocol.RequestCommand.RESET_PASSWORD, payload);
            _socketClient.SendRequest(packet);
        }
    }
}