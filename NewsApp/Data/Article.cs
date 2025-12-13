using System;

namespace NewsApp.Data
{
    public class Article
    {
        public int ArticleID { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public byte[]? Image { get; set; } // Ảnh bìa
        public DateTime PublishDate { get; set; }

        public int AuthorID { get; set; }
        public required string AuthorName { get; set; }
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
    }
}
