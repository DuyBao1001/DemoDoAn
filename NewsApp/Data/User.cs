
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Data
{
    public class User
    {
        public int Id { get; set;}
        public required string FullName { get; set; }
        public required string Email { get; set;}
        public DateTime BirthDay { get; set; }
        public int AccountID { get; set; }
        public required string UserName { get; set;}
        public string Role { get; set; } = "Reader";
    }
}
