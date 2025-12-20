using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewsApp.BLL;
using NewsApp.Data;
using NewsApp.Network;

namespace NewsApp.UI
{
    public partial class MainForm : Form
    {
        private User user;
        private readonly ArticleServices _articleServices;
        private readonly CategoryServices _categoryServices;
        private readonly CommentServices _commentServices;

        public MainForm(
            User user,
            ArticleServices articleServices,
            CategoryServices categoryServices,
            CommentServices commentServices
        )
        {
            InitializeComponent();
            this.user = user;
            this._articleServices = articleServices;
            this._categoryServices = categoryServices;
            this._commentServices = commentServices;
            this.Text = $"NewsApp - Xin chào {user.FullName}";

            if (user.Role == "Admin")
            {
                btnAdminPanel.Visible = true;
            }

            if (user.Role != "Writer")
            {
                btnPostArticle.Visible = false;
            }

            _categoryServices.GetCategoriesResult += (categories) =>
            {
                categories.Insert(0, new Category { CategoryID = -1, Name = "Tất cả" });
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        cbCategories.DisplayMember = "Name";
                        cbCategories.ValueMember = "CategoryID";
                        cbCategories.DataSource = categories;
                        cbCategories.SelectedIndex = 0;
                    }));
                }
                else
                {
                    cbCategories.DisplayMember = "Name";
                    cbCategories.ValueMember = "CategoryID";
                    cbCategories.DataSource = categories;
                    cbCategories.SelectedIndex = 0;
                }
            };

            _articleServices.GetLatestArticlesResult += UpdateArticleList;

            _articleServices.GetArticlesByCategoryResult += (articles) =>
            {
                if (articles == null || articles.Count == 0)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Danh mục này chưa có bài viết nào. Đang hiển thị các bài viết mới nhất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cbCategories.SelectedIndex = 0;
                        }));
                    }
                    else
                    {
                        MessageBox.Show("Danh mục này chưa có bài viết nào. Đang hiển thị các bài viết mới nhất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbCategories.SelectedIndex = 0;
                    }
                }
                else
                {
                    UpdateArticleList(articles);
                }
            };

            _articleServices.SearchArticlesResult += UpdateArticleList;

            _categoryServices.GetCategories();
            _articleServices.GetLatestArticles();

            cbCategories.SelectedIndexChanged += CbCategories_SelectedIndexChanged;
            btnSearch.Click += BtnSearch_Click;
        }

        private void UpdateArticleList(List<Article> articles)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    flpArticles.Controls.Clear();
                    foreach (var article in articles)
                    {
                        ArticleControl articleControl = new ArticleControl();
                        articleControl.SetArticle(article);
                        articleControl.SetContext(_articleServices, _commentServices, user);
                        flpArticles.Controls.Add(articleControl);
                    }
                }));
            }
            else
            {
                flpArticles.Controls.Clear();
                foreach (var article in articles)
                {
                    ArticleControl articleControl = new ArticleControl();
                    articleControl.SetArticle(article);
                    articleControl.SetContext(_articleServices, _commentServices, user);
                    flpArticles.Controls.Add(articleControl);
                }
            }
        }

        private void CbCategories_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbCategories.SelectedValue != null)
            {
                if (int.TryParse(cbCategories.SelectedValue.ToString(), out int categoryId))
                {
                    if (categoryId == -1)
                    {
                        _articleServices.GetLatestArticles();
                    }
                    else
                    {
                        _articleServices.GetArticlesByCategory(categoryId);
                    }
                }
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                _articleServices.SearchArticles(keyword);
            }
            else
            {
                _articleServices.GetLatestArticles();
            }
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            AdminServices adminServices = new AdminServices();
            AdminForm adminForm = new(adminServices);
            adminForm.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profileFrom = new(user);
            DialogResult result = profileFrom.ShowDialog();
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnPostArticle_Click(object sender, EventArgs e)
        {
            NewArticleForm newArticleForm = new(_articleServices, _categoryServices, user);
            DialogResult result = newArticleForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                _articleServices.GetLatestArticles();
            }
        }
    }
}