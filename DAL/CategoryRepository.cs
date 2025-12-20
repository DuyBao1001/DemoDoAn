using Microsoft.Data.SqlClient;
using NewsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL
{
    internal class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository() : base() { }

        public override bool Save(Category obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "INSERT INTO Category (CategoryName) VALUES (@name)";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@name", obj.Name);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }

        public override bool Update(Category obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "UPDATE Category SET CategoryName = @name WHERE CategoryID = @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@id", obj.CategoryID);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                LastError = sqle.Message;
                return false;
            }
        }

        public override bool Delete(Category obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "DELETE FROM Category WHERE CategoryID = @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", obj.CategoryID);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                LastError = sqle.Message;
                return false;
            }
        }

        public override IList<Category> GetAll()
        {
            List<Category> categories = new();
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "SELECT CategoryID, CategoryName FROM Category ORDER BY CategoryName";
                SqlCommand command = new(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new()
                    {
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        Name = reader.GetString(reader.GetOrdinal("CategoryName"))
                    };
                    categories.Add(category);
                }

                reader.Close();
                return categories;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return categories;
            }
        }

        public override Category? GetByID(int id)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "SELECT CategoryID, Name FROM Category WHERE CategoryID = @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Category category = new()
                    {
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };
                    reader.Close();
                    return category;
                }

                reader.Close();
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
