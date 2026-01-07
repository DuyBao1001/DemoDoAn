using NewsApp.BLL;
using NewsApp.Data;
using System;
using System.Windows.Forms;

namespace NewsApp.UI
{
    public partial class LoginForm : Form
    {
        private readonly AccountServices _accountServices;

        public LoginForm()
        {
            InitializeComponent();
            tbPassword.PasswordChar = '●';

            _accountServices = new AccountServices();
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

            _accountServices.LoginResult += (user, message) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ProcessLoginResult(user, message)));
                }
                else
                {
                    ProcessLoginResult(user, message);
                }
            };
        }

        private void ProcessLoginResult(User? user, string message)
        {
            if (user != null)
            {
                MessageBox.Show($"Đăng nhập thành công! Xin chào {user.FullName}");
                this.Hide();

                ArticleServices articleServices = new();
                CategoryServices categoryServices = new();
                CommentServices commentServices = new();

                MainForm mainForm = new(user, articleServices, categoryServices, commentServices);
                mainForm.FormClosed += (s, args) =>
                {
                    _accountServices.Logout();
                    this.Show();
                    tbPassword.Clear();
                };
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = tbUsername.Text.Trim();
            string pass = tbPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            _accountServices.Login(user, pass);
        }

        private void LinkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new();
            registerForm.ShowDialog();
            this.Show();
            tbPassword.Clear();
        }

        private void llbForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            this.Hide();

            ForgotPasswordForm forgotPassForm = new ForgotPasswordForm(_accountServices);

            forgotPassForm.ShowDialog();

            this.Show();
        }
    }
}