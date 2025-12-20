using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using NewsApp.Data;

namespace NewsApp.DAL
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public override IList<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Comment? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Save(Comment obj)
        {
            string query = "INSERT INTO [Comment] (ArticleID, UserID, Content, [Timestamp]) VALUES (@ArticleID, @UserID, @Content, @Timestamp)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ArticleID", obj.ArticleID);
                command.Parameters.AddWithValue("@UserID", obj.UserID);
                command.Parameters.AddWithValue("@Content", obj.Content);
                command.Parameters.AddWithValue("@Timestamp", obj.Timestamp);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving comment: " + ex.Message);
                    LastError = ex.Message;
                    return false;
                }
            }
        }

        public override bool Update(Comment obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Comment obj)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByArticleId(int articleId)
        {
            List<Comment> comments = new List<Comment>();
            string query = @"
                SELECT c.ArticleID, c.UserID, c.Content, c.Timestamp, u.FullName
                FROM Comment c
                JOIN [User] u ON c.UserID = u.UserID
                WHERE c.ArticleID = @ArticleID
                ORDER BY c.Timestamp DESC";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ArticleID", articleId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comments.Add(new()
                        {
                            ArticleID = (int)reader["ArticleID"],
                            UserID = (int)reader["UserID"],
                            Content = reader["Content"]?.ToString() ?? "",
                            Timestamp = (DateTime)reader["Timestamp"],
                            UserName = reader["FullName"]?.ToString() ?? ""
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error getting comments: " + ex.Message);
                }
            }
            return comments;
        }
    }
}
