
using NewsApp.Data;

namespace NewsApp.DAL
{
    public interface IAccountRepository
    {
        public bool Login(Account account);
        public bool CheckUserNameExists(string userName);
        public bool Register(Account account);
        public Account? GetAccountByUsername(string userName);
    }
}
