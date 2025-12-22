using Microsoft.Data.SqlClient;
using NewsApp.Data;
using System.Data;

namespace NewsApp.DAL
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base()
        {
        }

        public override bool Save(User obj)
        {
            try
            {
                if (CheckEmailExists(obj.Email))
                {
                    Console.WriteLine("Email này đã có người sử dụng!");
                    return false; // Trả về false để báo cho Server biết không đăng ký được
                }

                using SqlConnection connection = GetConnection();
                // Thêm cột Avatar vào câu lệnh INSERT
                string query = "INSERT INTO [User] (FullName, Email, BirthDay, Role, AccountID, Avatar) VALUES (@fullName, @email, @birthDay, @role, @accountId, @avatar)";

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@fullName", obj.FullName);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@birthDay", obj.BirthDay);
                command.Parameters.AddWithValue("@role", obj.Role);
                command.Parameters.AddWithValue("@accountId", obj.AccountID);

                // Xử lý tham số Avatar (có thể null)
                if (obj.Avatar == null)
                {
                    command.Parameters.Add("@avatar", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.AddWithValue("@avatar", obj.Avatar);
                }

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }

        // Đã hiện thực hàm Update để cập nhật thông tin và Avatar
        public override bool Update(User obj)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = @"UPDATE [User] 
                                     SET FullName = @fullName, 
                                         Email = @email, 
                                         BirthDay = @birthDay,
                                         Avatar = @avatar
                                     WHERE UserID = @id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@fullName", obj.FullName);
                    command.Parameters.AddWithValue("@email", obj.Email);
                    command.Parameters.AddWithValue("@birthDay", obj.BirthDay);
                    command.Parameters.AddWithValue("@id", obj.Id);

                    // Xử lý tham số Avatar
                    if (obj.Avatar == null)
                    {
                        command.Parameters.Add("@avatar", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@avatar", obj.Avatar);
                    }

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi Update User: " + ex.Message);
                return false;
            }
        }

        public override bool Delete(User obj)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    int accountIdToDelete = obj.AccountID;

                    if (accountIdToDelete == 0)
                    {
                        string findAccountQuery = "SELECT AccountID FROM [User] WHERE UserID = @userId";
                        using (SqlCommand findCmd = new SqlCommand(findAccountQuery, connection))
                        {
                            findCmd.Parameters.AddWithValue("@userId", obj.Id);
                            object result = findCmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                accountIdToDelete = Convert.ToInt32(result);
                            }
                        }
                    }

                    if (accountIdToDelete == 0) return false;

                    using (SqlTransaction tran = connection.BeginTransaction())
                    {
                        try
                        {
                            // Bước 1: Xóa tất cả Comment của User này trước
                            string queryDelComment = "DELETE FROM [Comment] WHERE UserID = @userId";
                            SqlCommand commandDelComment = new SqlCommand(queryDelComment, connection, tran);
                            commandDelComment.Parameters.AddWithValue("@userId", obj.Id);
                            commandDelComment.ExecuteNonQuery();

                            // Bước 2: Sau đó mới xóa User
                            string queryUser = "DELETE FROM [User] WHERE UserID = @userId";
                            SqlCommand commandUser = new SqlCommand(queryUser, connection, tran);
                            commandUser.Parameters.AddWithValue("@userId", obj.Id);
                            commandUser.ExecuteNonQuery();

                            // Bước 3: Cuối cùng xóa Account
                            string queryAcc = "DELETE FROM Account WHERE AccountID = @accountId";
                            SqlCommand commandAcc = new SqlCommand(queryAcc, connection, tran);
                            commandAcc.Parameters.AddWithValue("@accountId", accountIdToDelete);
                            int rows = commandAcc.ExecuteNonQuery();

                            tran.Commit();
                            return rows > 0;
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                LastError = sqle.Message;
                return false;
            }
        }

        public override IList<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                using SqlConnection connection = GetConnection();
                // Thêm U.Avatar vào SELECT
                string query = "SELECT U.UserID, FullName, Email, BirthDay, Role, U.AccountID, A.UserName, U.Avatar FROM [User] as U JOIN Account as A ON U.AccountID = A.AccountID";
                SqlCommand command = new(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Email = reader.GetString(2),
                        BirthDay = reader.GetDateTime(3),
                        Role = reader.GetString(4),
                        AccountID = reader.GetInt32(5),
                        UserName = reader.GetString(6),
                        // Đọc Avatar (cột thứ 7 - index 7 vì bắt đầu từ 0)
                        Avatar = reader.IsDBNull(7) ? null : (byte[])reader[7]
                    });
                }
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                LastError = sqle.Message;
            }
            return users;
        }

        public User? GetByAccountID(int accountId)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                // Thêm U.Avatar vào SELECT
                string query = "SELECT U.UserID, FullName, Email, BirthDay, Role, U.AccountID, A.UserName, U.Avatar FROM [User] as U JOIN Account as A ON U.AccountID = A.AccountID WHERE U.AccountID = @accountId";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@accountId", accountId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Email = reader.GetString(2),
                        BirthDay = reader.GetDateTime(3),
                        Role = reader.GetString(4),
                        AccountID = reader.GetInt32(5),
                        UserName = reader.GetString(6),
                        // Đọc Avatar
                        Avatar = reader.IsDBNull(7) ? null : (byte[])reader[7]
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

        public bool CheckEmailExists(string email)
        {
            
            string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";

            SqlParameter[] parameters = new SqlParameter[]
            {
        
        new SqlParameter("@Email", System.Data.SqlDbType.NVarChar) { Value = email.Trim() }
            };

            
            object result = DatabaseHepper.ExecuteScalar(query, parameters);

            int count = Convert.ToInt32(result);
            Console.WriteLine($"[SQL DEBUG] Tim email: {email} -> Ket qua: {count} dong");

            return count > 0;
        }
        public override User? GetByID(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    // Thêm U.Avatar vào SELECT
                    String query = "SELECT U.UserID, FullName, Email, BirthDay, Role, U.AccountID, A.UserName, U.Avatar FROM [User] as U JOIN Account as A ON U.AccountID = A.AccountID WHERE U.UserID = @userId";
                    SqlCommand command = new(query, connection);
                    command.Parameters.AddWithValue("@userId", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        User user = new()
                        {
                            Id = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Email = reader.GetString(2),
                            BirthDay = reader.GetDateTime(3),
                            Role = reader.GetString(4),
                            AccountID = reader.GetInt32(5),
                            UserName = reader.GetString(6),
                            // Đọc Avatar
                            Avatar = reader.IsDBNull(7) ? null : (byte[])reader[7]
                        };
                        return user;
                    }
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