using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient; 

namespace NewsApp.DAL
{
    internal class DatabaseHepper
    {
        private static readonly IConfigurationRoot _configuration;

        private const string DEFAULT_CONNECTION = "Server=localhost;Database=QLBB;User Id=sa;Password=123456;TrustServerCertificate=True;Trust_Connection=true";

        static DatabaseHepper()
        {
            try
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(AppContext.BaseDirectory)
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
            }
            catch
            {

            }
        }

        public static string ConnectionString
        {
            get
            {
                if (_configuration != null)
                {
                    return _configuration.GetConnectionString("DefaultConnection") ?? DEFAULT_CONNECTION;
                }
                return DEFAULT_CONNECTION;
            }
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null) command.Parameters.AddRange(parameters);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LỖI DATABASE RỒI: " + ex.Message);
                return -1; 
            }
        }
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }
        public static DataTable GetDataTable(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }
    }
}
