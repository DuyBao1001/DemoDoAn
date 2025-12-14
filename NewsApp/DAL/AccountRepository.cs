using Microsoft.Data.SqlClient;
using NewsApp.Data;

namespace NewsApp.DAL
{
    internal class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository() : base()
        {
        }

        public bool CheckUserNameExists(string userName)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                connection.Open();
                string query = "SELECT UserName FROM Account WHERE UserName = @userName";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@userName", userName);
                SqlDataReader reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (SqlException sqle)
            {
                throw new NotImplementedException(sqle.Message);
            }
        }



        public override bool Delete(Account obj)
        {
            //try
            //{
            //    using SqlConnection connection = GetConnection();
            //    connection.Open();
            //    string query = "DELETE FROM Account WHERE AccountID = @accountId";
            //    SqlCommand command = new(query, connection);
            //    command.Parameters.AddWithValue("@accountId", obj.AccountID);
            //    return command.ExecuteNonQuery() > 0;
            //}
            //catch (SqlException sqle)
            //{
            //    Console.WriteLine(sqle.Message);
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public override IList<Account> GetAll()
        {
            try
            {
                using SqlConnection connection = GetConnection();
                connection.Open();
                string query = "SELECT * FROM Account";
                SqlCommand command = new(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                return [];
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return [];
            }
        }

        public override Account? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool Login(Account account)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                String query = "SELECT UserName FROM Account WHERE UserName = @userName AND Password = @password";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@userName", account.Username);
                command.Parameters.AddWithValue("@password", account.Password);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }

        public bool Register(Account account)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                String query = "INSERT INTO Account (UserName, Password) VALUES (@userName, @password)";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@userName", account.Username);
                command.Parameters.AddWithValue("@password", account.Password);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }

        public override bool Save(Account obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                String query = "INSERT INTO Account (UserName, Password) VALUES (@userName, @password)";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@userName", obj.Username);
                command.Parameters.AddWithValue("@password", obj.Password);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }

        public override bool Update(Account obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                String query = "UPDATE Account SET UserName = @userName, Password = @password WHERE AccountID = @accountId";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@userName", obj.Username);
                command.Parameters.AddWithValue("@password", obj.Password);
                command.Parameters.AddWithValue("@accountId", obj.AccountID);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }
        public Account? GetAccountByUsername(string userName)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "SELECT * FROM Account WHERE UserName = @userName";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@userName", userName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Account
                    {
                        AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                        Username = reader.GetString(reader.GetOrdinal("UserName")),
                        Password = reader.GetString(reader.GetOrdinal("Password"))
                    };
                }
                return null;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return null;
            }
        }
    }
}
