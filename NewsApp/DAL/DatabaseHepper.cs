using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL
{
    internal class DatabaseHepper
    {
        private static readonly IConfigurationRoot _configuration;

        private const string DEFAULT_CONNECTION = "Server=localhost;Database=QLBB;User Id=sa;Password=123456;TrustServerCertificate=True;Trust_Connection=true";

        static DatabaseHepper()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppContext.BaseDirectory).
                AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public static string ConnectionString => _configuration.GetConnectionString("DefaultConnection") ?? DEFAULT_CONNECTION;
    }
}
