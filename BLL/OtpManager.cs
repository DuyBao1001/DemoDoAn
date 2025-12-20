using System.Collections.Generic;

namespace NewsApp.BLL
{
    public static class OtpManager
    {
        // Dictionary lưu trữ: <Email, OTP>
        private static Dictionary<string, string> _otpStore = new Dictionary<string, string>();

        public static void SaveOtp(string email, string otp)
        {
            if (_otpStore.ContainsKey(email))
            {
                _otpStore[email] = otp; // Ghi đè nếu đã tồn tại
            }
            else
            {
                _otpStore.Add(email, otp);
            }
        }

        public static bool VerifyOtp(string email, string otpInput)
        {
            if (_otpStore.ContainsKey(email))
            {
                // So sánh OTP
                if (_otpStore[email] == otpInput)
                {
                    _otpStore.Remove(email); // Xóa OTP ngay sau khi dùng xong để bảo mật
                    return true;
                }
            }
            return false;
        }
    }
}