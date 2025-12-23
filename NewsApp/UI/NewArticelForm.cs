using NewsApp.BLL;
using NewsApp.Data;

namespace NewsApp.UI
{
    public partial class NewArticleForm : Form
    {
        private readonly ArticleServices _articleServices;
        private readonly CategoryServices _categoryServices;
        private readonly User _currentUser;
        private byte[]? _selectedImage = null;

        public NewArticleForm(
            ArticleServices articleServices,
            CategoryServices categoryServices,
            User currentUser
        )
        {
            InitializeComponent();
            _articleServices = articleServices;
            _categoryServices = categoryServices;
            _currentUser = currentUser;

            guna2Button1.Click += BtnPost_Click;
            buttonUploadPic.Click += BtnUploadPic_Click;
            buttonDelete.Click += BtnDeletePic_Click;
            _articleServices.DataChanged += OnArticleDataChanged;
            _categoryServices.GetCategoriesResult += OnGetCategoriesResult;
            _categoryServices.GetCategories();
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void OnGetCategoriesResult(System.Collections.Generic.List<Category> categories)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new Action<System.Collections.Generic.List<Category>>(OnGetCategoriesResult),
                    categories
                );
                return;
            }

            guna2ComboBox1.DataSource = categories;
            guna2ComboBox1.DisplayMember = "Name";
            guna2ComboBox1.ValueMember = "CategoryID";
        }

        private void BtnUploadPic_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                ofd.Title = "Chọn hình ảnh";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load image to PictureBox
                        guna2PictureBox1.Image = Image.FromFile(ofd.FileName);

                        // Convert image to byte array
                        using (
                            FileStream fs = new FileStream(
                                ofd.FileName,
                                FileMode.Open,
                                FileAccess.Read
                            )
                        )
                        {
                            _selectedImage = new byte[fs.Length];
                            fs.Read(_selectedImage, 0, (int)fs.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Lỗi khi tải ảnh: {ex.Message}",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        private void BtnDeletePic_Click(object? sender, EventArgs e)
        {
            guna2PictureBox1.Image = null;
            _selectedImage = null;
        }

        private void BtnPost_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tiêu đề bài viết",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (guna2ComboBox1.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn chuyên mục",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập nội dung bài viết",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string title = textBoxTitle.Text.Trim();
            int categoryId = (int)guna2ComboBox1.SelectedValue;
            string categoryName = guna2ComboBox1.Text;
            string content = guna2TextBox1.Text.Trim();

            _articleServices.PostArticle(
                title,
                categoryId,
                categoryName,
                content,
                _selectedImage,
                _currentUser.Id,
                _currentUser.FullName
            );
        }

        private void OnArticleDataChanged(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnArticleDataChanged), success, message);
                return;
            }

            MessageBox.Show(
                message,
                "Thông báo",
                MessageBoxButtons.OK,
                success ? MessageBoxIcon.Information : MessageBoxIcon.Error
            );

            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
