using System;
using System.Drawing;
using System.Windows.Forms;
using NewsApp.BLL;
using NewsApp.Data;

namespace NewsApp.UI
{
    public partial class ChangePasswordForm : Form
    {
        private readonly User _user;
        private readonly AccountServices _accountServices;

        public ChangePasswordForm(User user, AccountServices accountServices)
        {
            InitializeComponent();
            _user = user;
            _accountServices = accountServices;

            _accountServices.ChangePasswordResult += OnChangePasswordResult;
        }

        private void OnChangePasswordResult(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool, string>(OnChangePasswordResult), success, message);
                return;
            }

            if (success)
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOldPass.Text = "";
                txtOldPass.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPass.Text.Trim();
            string newPass = txtNewPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPass == oldPass)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _accountServices.ChangePassword(_user.UserName, oldPass, newPass);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _accountServices.ChangePasswordResult -= OnChangePasswordResult;
        }
    }
}