using NewsApp.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;



namespace NewsApp.UI
{
    public partial class ForgotPasswordForm : Form
    {
        private readonly AccountServices _accountServices;
        private string _currentEmail = "";

        public ForgotPasswordForm(AccountServices accountServices)
        {
            InitializeComponent();
            _accountServices = accountServices;
            _accountServices.ForgotPasswordResult += OnForgotPasswordResult;
            _accountServices.ResetPasswordResult += OnResetPasswordResult;
            SetupInitialState();
        }
        private void SetupInitialState()
        {
            txtOTP.Visible = false;
            txtNewPass.Visible = false;
            lblOtp.Visible = false;
            lblNewPass.Visible = false;
            btnConfirm.Visible = false;
            btnGetCode.Visible = true;
            txtEmail.Enabled = true;
            txtNewPass.PasswordChar = '●';
        }
        private void OnForgotPasswordResult(bool isSuccess, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnForgotPasswordResult(isSuccess, message)));
                return;
            }
            btnGetCode.Enabled = true;
            btnGetCode.Text = "Gửi mã xác nhận";

            if (isSuccess)
            {
                MessageBox.Show("Mã OTP đã được gửi đến email của bạn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtEmail.Enabled = false; // Khóa email lại 
                btnGetCode.Visible = false; // Ẩn nút lấy mã 
                lblOtp.Visible = true;
                txtOTP.Visible = true;
                lblNewPass.Visible = true;
                txtNewPass.Visible = true;
                btnConfirm.Visible = true;
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OnResetPasswordResult(bool isSuccess, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnResetPasswordResult(isSuccess, message)));
                return;
            }
            if (isSuccess)
            {
                MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thất bại: " + message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGetCode_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập Email!");
                return;
            }
            _currentEmail = email;
            btnGetCode.Enabled = false;
            btnGetCode.Text = "Đang gửi...";
            _accountServices.RequestForgotPassword(email);
        }

        private void llbBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string otp = txtOTP.Text.Trim();
            string newPass = txtNewPass.Text.Trim();

            if (string.IsNullOrEmpty(otp) || string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ OTP và Mật khẩu mới!");
                return;
            }
            _accountServices.RequestResetPassword(_currentEmail, otp, newPass);
        }
        private void ForgotPasswordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_accountServices != null)
            {
                _accountServices.ForgotPasswordResult -= OnForgotPasswordResult;
                _accountServices.ResetPasswordResult -= OnResetPasswordResult;
            }
        }

    }

}