using Microsoft.Data.SqlClient;
using NewsApp.Data;

namespace NewsApp.DAL
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository()
            : base() { }

        public override bool Save(Article obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = @"INSERT INTO Article (Title, Content, Image, PublishDate, AuthorID, CategoryID, Status)
                         VALUES (@title, @content, @image, @publishDate, @authorId, @categoryId, @status)";

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@title", obj.Title);
                command.Parameters.AddWithValue("@content", obj.Content);
                command.Parameters.AddWithValue("@image", (object?)obj.Image ?? DBNull.Value);
                command.Parameters.AddWithValue("@publishDate", obj.PublishDate);
                command.Parameters.AddWithValue("@authorId", obj.AuthorID);
                command.Parameters.AddWithValue("@categoryId", obj.CategoryID);
                command.Parameters.AddWithValue("@status", 0); // khi đăng lên thì cho 0 để chờ duyệt

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine($"SQL Error: {sqle.Message}");
                Console.WriteLine($"AuthorID: {obj.AuthorID}, CategoryID: {obj.CategoryID}");
                LastError = sqle.Message;
                return false;
            }
        }

        public override bool Update(Article obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query =
                    @"UPDATE Article
                                SET Title = @title, Content = @content, Image = @image,
                                    PublishDate = @publishDate, AuthorID = @authorId, CategoryID = @categoryId
                                WHERE ArticleID = @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@title", obj.Title);
                command.Parameters.AddWithValue("@content", obj.Content);
                command.Parameters.AddWithValue("@image", (object?)obj.Image ?? DBNull.Value);
                command.Parameters.AddWithValue("@publishDate", obj.PublishDate);
                command.Parameters.AddWithValue("@authorId", obj.AuthorID);
                command.Parameters.AddWithValue("@categoryId", obj.CategoryID);
                command.Parameters.AddWithValue("@id", obj.ArticleID);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return false;
            }
        }

        public override IList<Article> GetAll()
        {
            List<Article> articles = new();
            try
            {
                using SqlConnection connection = GetConnection();
                string query =
                    @"SELECT A.ArticleID, A.Title, A.Content, A.Image, A.PublishDate, 
                                        A.AuthorID, U.FullName as AuthorName, 
                                        A.CategoryID, C.CategoryName as CategoryName
                                 FROM Article A 
                                 JOIN [User] U ON A.AuthorID = U.UserID 
                                 JOIN Category C ON A.CategoryID = C.CategoryID
                                 ORDER BY A.PublishDate DESC";
                SqlCommand command = new(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Article article = new()
                    {
                        ArticleID = reader.GetInt32(reader.GetOrdinal("ArticleID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        Image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"],
                        PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                        AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                    };
                    articles.Add(article);
                }

                reader.Close();
                return articles;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return articles;
            }
        }

        public override Article? GetByID(int id)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query =
                    @"SELECT A.ArticleID, A.Title, A.Content, A.Image, A.PublishDate, 
                                        A.AuthorID, U.FullName as AuthorName, 
                                        A.CategoryID, C.CategoryName as CategoryName
                                 FROM Article A 
                                 JOIN [User] U ON A.AuthorID = U.UserID 
                                 JOIN Category C ON A.CategoryID = C.CategoryID
                                 WHERE A.ArticleID = @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Article article = new()
                    {
                        ArticleID = reader.GetInt32(reader.GetOrdinal("ArticleID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        Image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"],
                        PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                        AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                    };
                    reader.Close();
                    return article;
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

        public IList<Article> GetLatestArticles(int count)
        {
            List<Article> articles = new();
            try
            {
                using SqlConnection connection = GetConnection();
                string query =
                    @"SELECT TOP (@count) A.ArticleID, A.Title, A.Content, A.Image, A.PublishDate, 
                                        A.AuthorID, U.FullName as AuthorName, 
                                        A.CategoryID, C.CategoryName as CategoryName
                                 FROM Article A 
                                 JOIN [User] U ON A.AuthorID = U.UserID 
                                 JOIN Category C ON A.CategoryID = C.CategoryID
                                 WHERE a.Status = 1
                                 ORDER BY A.PublishDate DESC";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@count", count);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Article article = new()
                    {
                        ArticleID = reader.GetInt32(reader.GetOrdinal("ArticleID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        Image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"],
                        PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                        AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                    };
                    articles.Add(article);
                }

                reader.Close();
                return articles;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return articles;
            }
        }

        public IList<Article> GetArticlesByCategory(int categoryId)
        {
            List<Article> articles = new();
            try
            {
                using SqlConnection connection = GetConnection();
                string query =
                    @"SELECT A.ArticleID, A.Title, A.Content, A.Image, A.PublishDate, 
                                        A.AuthorID, U.FullName as AuthorName, 
                                        A.CategoryID, C.CategoryName as CategoryName
                                 FROM Article A 
                                 JOIN [User] U ON A.AuthorID = U.UserID 
                                 JOIN Category C ON A.CategoryID = C.CategoryID
                                 WHERE A.CategoryID = @categoryId
                                 ORDER BY A.PublishDate DESC";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Article article = new()
                    {
                        ArticleID = reader.GetInt32(reader.GetOrdinal("ArticleID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        Image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"],
                        PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                        AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                    };
                    articles.Add(article);
                }

                reader.Close();
                return articles;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return articles;
            }
        }

        public IList<Article> SearchArticles(string keyword)
        {
            List<Article> articles = new();
            try
            {
                using SqlConnection connection = GetConnection();
                string query =
                    @"SELECT A.ArticleID, A.Title, A.Content, A.Image, A.PublishDate, 
                                        A.AuthorID, U.FullName as AuthorName, 
                                        A.CategoryID, C.CategoryName as CategoryName
                                 FROM Article A 
                                 JOIN [User] U ON A.AuthorID = U.UserID 
                                 JOIN Category C ON A.CategoryID = C.CategoryID
                                 WHERE A.Title LIKE @keyword
                                 ORDER BY A.PublishDate DESC";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Article article = new()
                    {
                        ArticleID = reader.GetInt32(reader.GetOrdinal("ArticleID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        Image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"],
                        PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                        AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                    };
                    articles.Add(article);
                }

                reader.Close();
                return articles;
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message);
                return articles;
            }
        }

        public override bool Delete(Article obj)
        {
            try
            {
                using SqlConnection connection = GetConnection();
                string query = "DELETE FROM Article WHERE ArticleID = @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", obj.ArticleID);

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

        public List<Article> GetPendingArticles()
        {
            List<Article> articles = new List<Article>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    // Lấy các bài có Status = 0
                    string query = @"SELECT a.*, c.CategoryName, u.FullName as AuthorName 
                                     FROM Article a 
                                     JOIN Category c ON a.CategoryID = c.CategoryID
                                     JOIN [User] u ON a.AuthorID = u.UserID
                                     WHERE a.Status = 0
                                     ORDER BY a.PublishDate ASC";

                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Article article = new()
                            {
                                ArticleID = reader.GetInt32(reader.GetOrdinal("ArticleID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"],
                                PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                                AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                                AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                            };
                            articles.Add(article);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetPendingArticles: " + ex.Message);
            }
            return articles;
        }

        public bool ApproveArticle(int articleId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE Article SET Status = 1 WHERE ArticleID = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", articleId);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
