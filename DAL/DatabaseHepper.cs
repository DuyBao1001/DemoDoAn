using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient; // Thư viện quan trọng để kết nối SQL

namespace NewsApp.DAL
{
    internal class DatabaseHepper
    {
        private static readonly IConfigurationRoot _configuration;

        // Chuỗi kết nối mặc định (dự phòng)
        private const string DEFAULT_CONNECTION = "Server=DuyDuy;Database=QuanLyTinTuc;TrustServerCertificate=True;Trust_Connection=true";

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
                // Nếu lỗi đọc file config thì bỏ qua, sẽ dùng DEFAULT_CONNECTION
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

        // ============================================================
        // CÁC HÀM THÊM MỚI ĐỂ THỰC THI SQL
        // ============================================================

        /// <summary>
        /// Dùng cho các lệnh UPDATE, INSERT, DELETE (trả về số dòng bị ảnh hưởng)
        /// </summary>
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
                // Dòng này sẽ hiện ra "Hung thủ" thực sự trong cửa sổ Output/Console của Visual Studio
                System.Diagnostics.Debug.WriteLine("LỖI DATABASE RỒI: " + ex.Message);
                return -1; // Trả về -1 để báo hiệu thất bại
            }
        }

        /// <summary>
        /// Dùng cho các lệnh lấy 1 giá trị duy nhất (VD: SELECT COUNT(*), SELECT Id...)
        /// </summary>
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

        /// <summary>
        /// Dùng cho các lệnh SELECT trả về bảng dữ liệu (VD: Lấy danh sách bài viết)
        /// </summary>
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