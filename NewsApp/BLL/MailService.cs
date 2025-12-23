using System;
using System.Net;
using System.Net.Mail;

public class MailService
{
    // Cấu hình Email gửi đi
    private static readonly string _fromEmail = "24520372@gm.uit.edu.vn";
    private static readonly string _appPassword = "ratc kpmf dxsg ivdu";

    public static bool SendOTP(string toEmail, string otpCode)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_fromEmail, "NewsApp");
            mail.To.Add(toEmail);
            mail.Subject = "Mã xác nhận quên mật khẩu";
            mail.Body = $"<h1>Mã OTP của bạn là: <b style='color:red; font-size: 20px;'>{otpCode}</b></h1>" +
                        "<p>Vui lòng không chia sẻ mã này cho ai.</p>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_fromEmail, _appPassword);

            smtp.Send(mail);
            Console.WriteLine($"[MAIL SUCCESS] Đã gửi OTP đến {toEmail}");
            return true;
        }
        catch (Exception ex)
        {

            Console.WriteLine($"[MAIL ERROR] Lỗi gửi mail: {ex.Message}");
            return false;
        }
    }
}