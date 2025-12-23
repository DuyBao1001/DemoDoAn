using System.Windows.Forms;
using NewsApp.Data;
using System.Drawing;
using System.IO;
using NewsApp.BLL;

namespace NewsApp.UI
{
    public partial class ArticleControl : UserControl
    {
        public ArticleControl()
        {
            InitializeComponent();
        }

        private Article? _article;
        private ArticleServices? _articleServices;
        private CommentServices? _commentServices;
        private User? _user;

        public void SetArticle(Article article)
        {
            _article = article;
            lblTitle.Text = article.Title;
            lblCategory.Text = article.CategoryName;
            lblAuthor.Text = $"Tác giả: {article.AuthorName}";
            lblDate.Text = article.PublishDate.ToString("dd/MM/yyyy");

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

            this.Click += ArticleControl_Click;
            lblTitle.Click += ArticleControl_Click;
            pbImage.Click += ArticleControl_Click;
        }

        public void SetContext(ArticleServices articleServices, CommentServices commentServices, User user)
        {
            _articleServices = articleServices;
            _commentServices = commentServices;
            _user = user;
        }

        private void ArticleControl_Click(object? sender, EventArgs e)
        {
            if (_article != null && _articleServices != null && _commentServices != null && _user != null)
            {
                ArticleForm articleForm = new ArticleForm(_article, _articleServices, _commentServices, _user);
                articleForm.ShowDialog();
            }
        }
    }
}
