using System;
using System.Drawing;
using System.Windows.Forms;
using NewsApp.Data;

namespace NewsApp.UI
{
    public partial class CommentControl : UserControl
    {
        public CommentControl()
        {
            InitializeComponent();
        }

        public void SetComment(Comment comment)
        {
            lblAuthor.Text = comment.UserName ?? $"User {comment.UserID}";
            lblContent.Text = comment.Content;
            lblDate.Text = comment.Timestamp.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
