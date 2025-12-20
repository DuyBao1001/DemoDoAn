using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewsApp.Data;
using NewsApp.BLL;

namespace NewsApp.UI
{
    public partial class ArticleForm : Form
    {
        private readonly Article _article;
        private readonly ArticleServices _articleServices;
        private readonly CommentServices _commentServices;
        private readonly User _user;

        public ArticleForm(Article article, ArticleServices articleServices, CommentServices commentServices, User user)
        {
            InitializeComponent();
            _article = article;
            _articleServices = articleServices;
            _commentServices = commentServices;
            _user = user;
            DisplayArticle(article);

            btnSendComment.Click += BtnSendComment_Click;
            _commentServices.DataChanged += CommentServices_DataChanged;
            _commentServices.GetCommentsResult += CommentServices_GetCommentsResult;

            // Load comments
            _commentServices.GetComments(_article.ArticleID);
        }

        private void CommentServices_GetCommentsResult(List<Comment> comments)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateCommentList(comments)));
            }
            else
            {
                UpdateCommentList(comments);
            }
        }

        private void UpdateCommentList(List<Comment> comments)
        {
            flpComments.Controls.Clear();
            foreach (var comment in comments)
            {
                CommentControl commentControl = new CommentControl();
                commentControl.SetComment(comment);
                flpComments.Controls.Add(commentControl);
            }

        }

        private void CommentServices_DataChanged(bool success, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    if (success)
                    {
                        txtComment.Clear();
                        _commentServices.GetComments(_article.ArticleID);
                    }
                }));
            }
            else
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (success)
                {
                    txtComment.Clear();
                    _commentServices.GetComments(_article.ArticleID); // Refresh comments
                }
            }
        }

        private void BtnSendComment_Click(object? sender, EventArgs e)
        {
            string content = txtComment.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Vui lòng nhập nội dung bình luận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _commentServices.PostComment(_article.ArticleID, _user.Id, content);
        }

        private void DisplayArticle(Article article)
        {
            lblTitle.Text = article.Title;
            lblCategory.Text = article.CategoryName;
            lblAuthor.Text = $"Tác giả: {article.AuthorName}";
            lblDate.Text = article.PublishDate.ToString("dd/MM/yyyy HH:mm");
            txtContent.Text = article.Content;

            if (article.Image != null && article.Image.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(article.Image))
                {
                    pbImage.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pbImage.Image = null;
            }
        }

        private void pbImage_Click(object sender, EventArgs e)
        {

        }
    }
}