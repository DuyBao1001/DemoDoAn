using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewsApp.Data;
using NewsApp.Network;

namespace NewsApp.UI
{
    public partial class ProfileForm : Form
    {
        private readonly User User;

        public ProfileForm(User user)
        {
            InitializeComponent();
            User = user;
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
                LoadDefaultAvatar();
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
    }
}