using System;

namespace NewsApp.Data
{
    public class Comment
    {
        public int ArticleID { get; set; }
        public int UserID { get; set; }
        public required string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string? UserName { get; set; }
        public byte[]? UserAvatar { get; set; }
    }
}