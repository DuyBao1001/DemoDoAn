using System.Collections.Generic;

namespace NewsApp.BLL
{
    public static class OtpManager
    {
        private static Dictionary<string, string> _otpStore = new Dictionary<string, string>();

        public static void SaveOtp(string email, string otp)
        {
            if (_otpStore.ContainsKey(email))
            {
                _otpStore[email] = otp;
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
                if (_otpStore[email] == otpInput)
                {
                    _otpStore.Remove(email);
                    return true;
                }
            }
            return false;
        }
    }
}