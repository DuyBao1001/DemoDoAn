using System;
using System.Drawing;
using System.Windows.Forms;
using NewsApp.Data;
using System.IO;
using NewsApp.BLL;

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
            // 1. Kiểm tra an toàn
            if (comment == null) return;

            lblAuthor.Text = comment.UserName ?? $"User {comment.UserID}";
            lblContent.Text = comment.Content;
            lblDate.Text = comment.Timestamp.ToString("dd/MM/yyyy HH:mm");

            if (comment.UserAvatar != null && comment.UserAvatar.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(comment.UserAvatar))
                    {
                        Image tempImage = Image.FromStream(ms);
                        pbAvatar.Image = new Bitmap(tempImage);
                    }
                }
                catch
                {
                    pbAvatar.Image = null;
                }
            }
            else
            {
                pbAvatar.Image = null;
            }
        }
    }
}