using System;
using System.Windows.Forms;
using NewsApp.BLL;
using NewsApp.Data;

namespace NewsApp.UI
{
    public partial class RegisterForm : Form
    {
        private readonly AccountServices _accountServices;

        public RegisterForm()
        {
            InitializeComponent();

            // Cài đặt mặc định
            tbPassword.PasswordChar = '●';
            tbConfirmPass.PasswordChar = '●';

            // Mặc định chọn role là Reader
            cboxReader.Checked = true;
            cboxWriter.Checked = false;

            // Khởi tạo AccountServices
            _accountServices = new AccountServices();

            // Subscribe events
            _accountServices.ConnectionStatusChanged += (status) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => MessageBox.Show(status, "Thông tin kết nối", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                }
                else
                {
                    MessageBox.Show(status, "Thông tin kết nối", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            _accountServices.RegisterResult += (success, message) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ProcessRegisterResult(success, message)));
                }
                else
                {
                    ProcessRegisterResult(success, message);
                }
            };

            this.btnRegister.Click += new EventHandler(BtnRegister_Click);
            this.linkLabelLogin.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabelLogin_LinkClicked);


            this.cboxReader.CheckedChanged += new EventHandler(cboxReader_CheckedChanged);
            this.cboxWriter.CheckedChanged += new EventHandler(cboxWriter_CheckedChanged);
        }

        private void ProcessRegisterResult(bool success, string message)
        {
            if (success)
            {
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboxReader_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu Reader được chọn, thì bỏ chọn Writer
            if (cboxReader.Checked)
            {
                cboxWriter.Checked = false;
            }
            else if (!cboxWriter.Checked)
            {
                cboxReader.Checked = true;
            }
        }

        private void cboxWriter_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu Writer được chọn, thì bỏ chọn Reader
            if (cboxWriter.Checked)
            {
                cboxReader.Checked = false;
            }
            // Tương tự, không cho phép bỏ chọn cả hai
            else if (!cboxReader.Checked)
            {
                cboxWriter.Checked = true;
            }
        }

        // --- XỬ LÝ ĐĂNG KÝ ---

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullName = tbFullName.Text.Trim();
            string username = tbUsername.Text.Trim();
            string password = tbPassword.Text;
            string confirmPassword = tbConfirmPass.Text;
            string email = tbEmail.Text.Trim();
            DateTime birthDay = dtpickerBirth.Value;

            // Validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedRole = cboxWriter.Checked ? "Writer" : "Reader";

            // Kiểm tra kết nối
            if (!_accountServices.IsConnected)
            {
                MessageBox.Show("Không có kết nối đến server! Vui lòng đảm bảo server đang chạy.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gửi request đăng ký thông qua AccountServices
            _accountServices.Register(username, password, email, birthDay, fullName, selectedRole);
        }

        private void linkLabelLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}