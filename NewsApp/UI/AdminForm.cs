using NewsApp.BLL;
using NewsApp.Common;
using NewsApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsApp.UI
{
    public partial class AdminForm : Form
    {
        private readonly AdminServices _adminServices;
        private readonly ArticleServices _articleServices;
        private readonly CategoryServices _categoryServices;
        private List<User> _users = new();
        private List<Article> _articles = new();
        private List<Category> _categories = new();

        public AdminForm(AdminServices adminServices)
        {
            InitializeComponent();
            _adminServices = adminServices;
            _articleServices = new ArticleServices();
            _categoryServices = new CategoryServices();

            InitializeEvents();
            this.Load += AdminForm_Load;
        }


        private void InitializeEvents()
        {
            _adminServices.GetAllUsersResult += OnGetAllUsersResult;
            _adminServices.DeleteUserResult += OnDeleteUserResult;
            _adminServices.DeleteArticleResult += OnDeleteArticleResult;
            _adminServices.AddCategoryResult += OnAddCategoryResult;
            _adminServices.UpdateCategoryResult += OnUpdateCategoryResult;
            _adminServices.DeleteCategoryResult += OnDeleteCategoryResult;

            _articleServices.GetLatestArticlesResult += OnGetArticlesResult;
            _categoryServices.GetCategoriesResult += OnGetCategoriesResult;

            _adminServices.GetPendingArticlesResult += OnGetPendingArticlesResult;
            _adminServices.ApproveArticleResult += OnApproveArticleResult;



            // Wire up button events
            btnDeleteUser.Click += BtnDeleteUser_Click;
            btnRefreshUsers.Click += BtnRefreshUsers_Click;
            btnDeleteArticle.Click += BtnDeleteArticle_Click;
            btnRefreshArticles.Click += BtnRefreshArticles_Click;
            btnNewCategory.Click += BtnNewCategory_Click;
            btnSaveCategory.Click += BtnSaveCategory_Click;
            btnDeleteCategory.Click += BtnDeleteCategory_Click;
            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;
            btnApprove.Click += btnApprove_Click;
            btnRefreshPending.Click += btnRefreshPending_Click;
        }

        private void AdminForm_Load(object? sender, EventArgs e)
        {
            LoadUsers();
            LoadCategories();
            LoadArticles();
            LoadPendingArticles();
        }

        private void LoadUsers() => _adminServices.GetAllUsers();
        private void LoadCategories() => _categoryServices.GetCategories();
        private void LoadArticles() => _articleServices.GetLatestArticles();


        private void LoadPendingArticles()
        {
            _adminServices.GetPendingArticles();
        }

        private void btnRefreshPending_Click(object? sender, EventArgs e)
        {
            LoadPendingArticles();
        }

        private void btnApprove_Click(object? sender, EventArgs e)
        {
            if (dgvPendingArticles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bài viết để duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Article selectedArticle = (Article)dgvPendingArticles.SelectedRows[0].DataBoundItem;

            if (selectedArticle != null)
            {
                _adminServices.ApproveArticle(selectedArticle.ArticleID);
            }
        }

        private void OnGetAllUsersResult(List<User> users)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<User>>(OnGetAllUsersResult), users);
                return;
            }
            _users = users;
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = _users;
        }

        private void OnDeleteUserResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnDeleteUserResult), success, message);
                return;
            }
            MessageBox.Show(message);
            if (success) LoadUsers();
        }

        private void OnGetArticlesResult(List<Article> articles)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<Article>>(OnGetArticlesResult), articles);
                return;
            }
            _articles = articles;
            dgvArticles.DataSource = null;
            dgvArticles.DataSource = _articles;
        }

        private void OnDeleteArticleResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnDeleteArticleResult), success, message);
                return;
            }
            MessageBox.Show(message);
            if (success) LoadArticles();
        }

        private void OnAddCategoryResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnAddCategoryResult), success, message);
                return;
            }
            MessageBox.Show(message);
            if (success) LoadCategories();
        }

        private void OnUpdateCategoryResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnUpdateCategoryResult), success, message);
                return;
            }
            MessageBox.Show(message);
            if (success) LoadCategories();
        }

        private void OnDeleteCategoryResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnDeleteCategoryResult), success, message);
                return;
            }
            MessageBox.Show(message);
            if (success) LoadCategories();
        }

        private void OnGetCategoriesResult(List<Category> categories)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<Category>>(OnGetCategoriesResult), categories);
                return;
            }
            _categories = categories;
            dgvCategories.DataSource = null;
            dgvCategories.DataSource = _categories;
        }

        private void OnGetPendingArticlesResult(List<Article> articles)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<Article>>(OnGetPendingArticlesResult), articles);
                return;
            }

            dgvPendingArticles.DataSource = null;
            dgvPendingArticles.DataSource = articles;

            if (dgvPendingArticles.Columns["Content"] != null)
                dgvPendingArticles.Columns["Content"].Visible = false;
            if (dgvPendingArticles.Columns["Image"] != null)
                dgvPendingArticles.Columns["Image"].Visible = false;
        }

        private void OnApproveArticleResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnApproveArticleResult), success, message);
                return;
            }

            MessageBox.Show(message);

            if (success)
            {
                LoadPendingArticles();
                LoadArticles();
            }
        }

        private void BtnDeleteUser_Click(object? sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var user = dgvUsers.SelectedRows[0].DataBoundItem as User;
                if (user != null)
                {
                    var result = MessageBox.Show($"Bạn có chắc muốn xóa user '{user.FullName}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        _adminServices.DeleteUser(user.Id);
                    }
                }
            }
        }

        private void BtnRefreshUsers_Click(object? sender, EventArgs e)
        {
            LoadUsers();
        }

        private void BtnDeleteArticle_Click(object? sender, EventArgs e)
        {
            if (dgvArticles.SelectedRows.Count > 0)
            {
                var article = dgvArticles.SelectedRows[0].DataBoundItem as Article;
                if (article != null)
                {
                    var result = MessageBox.Show($"Bạn có chắc muốn xóa bài viết '{article.Title}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        _adminServices.DeleteArticle(article.ArticleID);
                    }
                }
            }
        }

        private void BtnRefreshArticles_Click(object? sender, EventArgs e)
        {
            LoadArticles();
        }

        private void BtnNewCategory_Click(object? sender, EventArgs e)
        {
            txtCategoryName.Text = "";
            dgvCategories.ClearSelection();
        }

        private void BtnSaveCategory_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chuyên mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string categoryName = txtCategoryName.Text.Trim();

            if (dgvCategories.SelectedRows.Count > 0)
            {
                var selectedCategory = dgvCategories.SelectedRows[0].DataBoundItem as Category;
                if (selectedCategory != null)
                {
                    _adminServices.UpdateCategory(selectedCategory.CategoryID, categoryName);
                }
            }
            else
            {
                _adminServices.AddCategory(categoryName);
            }
        }

        private void BtnDeleteCategory_Click(object? sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                var category = dgvCategories.SelectedRows[0].DataBoundItem as Category;
                if (category != null)
                {
                    var result = MessageBox.Show($"Bạn có chắc muốn xóa chuyên mục '{category.Name}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        _adminServices.DeleteCategory(category.CategoryID);
                    }
                }
            }
        }

        private void DgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                var category = dgvCategories.SelectedRows[0].DataBoundItem as Category;
                if (category != null)
                {
                    txtCategoryName.Text = category.Name;
                }
            }
        }
    }
}