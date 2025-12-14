using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewsApp.BLL;
using NewsApp.Data;
using NewsApp.Network;

namespace NewsApp.UI
{
    public partial class ProfileForm : Form
    {
        private readonly User User;
        private readonly AccountServices _accountServices;

        public ProfileForm(User user)
        {
            InitializeComponent();
            User = user;

            // 2. KHỞI TẠO SERVICE VÀ ĐĂNG KÝ SỰ KIỆN
            _accountServices = new AccountServices();
            _accountServices.UpdateProfileResult += OnUpdateProfileResult;
        }

        private void OnUpdateProfileResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnUpdateProfileResult), success, message);
                return;
            }

            MessageBox.Show(message, success ? "Thông báo" : "Lỗi", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
            {
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
        }

        // Event handler khi form load
        private void ProfileForm_Load(object sender, EventArgs e)
        {
            if (User == null)
            {
                MessageBox.Show("User object is null!", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                labelUserName.Text = $"@{User.UserName ?? "unknown"}";
                txtFullName.DefaultText = User.FullName ?? "";
                txtRole.DefaultText = User.Role ?? "";
                txtEmail.DefaultText = User.Email ?? "";
                LoadAvatar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDefaultAvatar()
        {
            picBoxProfileAvatar.Image = null;
            picBoxProfileAvatar.FillColor = Color.LightGray;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void LoadAvatar()
        {
            if (User.Avatar != null && User.Avatar.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(User.Avatar))
                    {
                        // SỬA LỖI: Phải tạo new Bitmap để copy dữ liệu ra khỏi stream
                        picBoxProfileAvatar.Image = new Bitmap(Image.FromStream(ms));
                    }
                }
                catch
                {
                    LoadDefaultAvatar();
                }
            }
            else
            {
                LoadDefaultAvatar();
            }
        }

        private void btnUploadAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. Tạo Bitmap mới từ file để không bị lock file gốc
                    Bitmap newAvatar;
                    using (var stream = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                    {
                        newAvatar = new Bitmap(stream);
                    }

                    // 2. Gán vào PictureBox
                    picBoxProfileAvatar.Image = newAvatar;

                    // 3. Lưu vào User Object (Chuyển thành byte[])
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Lưu dưới dạng PNG hoặc JPEG để tối ưu dung lượng
                        newAvatar.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        User.Avatar = ms.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Cập nhật các thông tin khác từ Textbox
            User.FullName = txtFullName.Text;
            User.Email = txtEmail.Text;
            // User.Avatar đã được cập nhật ở nút Upload

            // Gửi lệnh lên Server (Giả sử AccountServices có hàm UpdateProfile)
            // Nếu chưa có, bạn có thể gọi trực tiếp socket gửi Packet UPDATE_PROFILE
            _accountServices.UpdateProfile(User);

            MessageBox.Show("Đã gửi yêu cầu cập nhật!");
        }
    }
}