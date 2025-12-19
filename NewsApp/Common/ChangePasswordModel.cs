using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Common
{
    public class ChangePasswordModel
    {
        public string Username { get; set; }
        public string OldPassword { get; set; } // Mật khẩu cũ 
        public string NewPassword { get; set; } // Mật khẩu mới
    }
}