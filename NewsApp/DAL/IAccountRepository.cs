
using NewsApp.Data;

namespace NewsApp.DAL
{
    public interface IAccountRepository
    {
        public bool Login(Account account);
        public bool CheckUserNameExists(string userName);
        public bool Register(Account account);
        public Account? GetAccountByUsername(string userName);
        public bool Update(Account account);
        bool VerifyPassword(string username, string password);
        bool UpdatePassword(string username, string newPassword);
        bool CheckEmailExists(string email);
        bool ResetPassword(string email, string newPassword);
    }
}
