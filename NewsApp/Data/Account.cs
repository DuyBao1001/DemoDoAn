using System;

namespace NewsApp.Data
{
    public class Account
    {
        public int AccountID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}