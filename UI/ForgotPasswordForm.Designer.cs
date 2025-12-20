namespace NewsApp.UI
{
    partial class ForgotPasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            btnGetCode = new Guna.UI2.WinForms.Guna2Button();
            txtNewPass = new Guna.UI2.WinForms.Guna2TextBox();
            txtOTP = new Guna.UI2.WinForms.Guna2TextBox();
            lblOtp = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblNewPass = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnConfirm = new Guna.UI2.WinForms.Guna2Button();
            llbBack = new LinkLabel();
            lblEmail = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = false;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.MenuHighlight;
            lblTitle.Location = new Point(293, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(437, 79);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quên mật khẩu";
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 10;
            txtEmail.CustomizableEdges = customizableEdges1;
            txtEmail.DefaultText = "";
            txtEmail.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtEmail.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtEmail.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtEmail.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtEmail.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtEmail.Location = new Point(79, 112);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "";
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtEmail.Size = new Size(544, 52);
            txtEmail.TabIndex = 1;
            // 
            // btnGetCode
            // 
            btnGetCode.BorderRadius = 10;
            btnGetCode.CustomizableEdges = customizableEdges3;
            btnGetCode.DisabledState.BorderColor = Color.DarkGray;
            btnGetCode.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGetCode.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGetCode.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGetCode.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGetCode.ForeColor = Color.White;
            btnGetCode.Location = new Point(653, 108);
            btnGetCode.Name = "btnGetCode";
            btnGetCode.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnGetCode.Size = new Size(184, 56);
            btnGetCode.TabIndex = 2;
            btnGetCode.Text = "Gửi mã xác nhận";
            btnGetCode.Click += btnGetCode_Click;
            // 
            // txtNewPass
            // 
            txtNewPass.BorderRadius = 10;
            txtNewPass.CustomizableEdges = customizableEdges5;
            txtNewPass.DefaultText = "";
            txtNewPass.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNewPass.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNewPass.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNewPass.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNewPass.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNewPass.Font = new Font("Segoe UI", 9F);
            txtNewPass.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNewPass.Location = new Point(293, 228);
            txtNewPass.Margin = new Padding(3, 4, 3, 4);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PlaceholderText = "";
            txtNewPass.SelectedText = "";
            txtNewPass.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtNewPass.Size = new Size(544, 52);
            txtNewPass.TabIndex = 3;
            // 
            // txtOTP
            // 
            txtOTP.BorderRadius = 10;
            txtOTP.CustomizableEdges = customizableEdges7;
            txtOTP.DefaultText = "";
            txtOTP.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtOTP.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtOTP.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtOTP.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtOTP.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtOTP.Font = new Font("Segoe UI", 9F);
            txtOTP.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtOTP.Location = new Point(79, 228);
            txtOTP.Margin = new Padding(3, 4, 3, 4);
            txtOTP.Name = "txtOTP";
            txtOTP.PlaceholderText = "";
            txtOTP.SelectedText = "";
            txtOTP.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtOTP.Size = new Size(195, 52);
            txtOTP.TabIndex = 4;
            // 
            // lblOtp
            // 
            lblOtp.AutoSize = false;
            lblOtp.BackColor = Color.Transparent;
            lblOtp.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOtp.ForeColor = SystemColors.MenuHighlight;
            lblOtp.Location = new Point(88, 192);
            lblOtp.Name = "lblOtp";
            lblOtp.Size = new Size(186, 62);
            lblOtp.TabIndex = 5;
            lblOtp.Text = "Mã OTP";
            // 
            // lblNewPass
            // 
            lblNewPass.AutoSize = false;
            lblNewPass.BackColor = Color.Transparent;
            lblNewPass.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNewPass.ForeColor = SystemColors.MenuHighlight;
            lblNewPass.Location = new Point(302, 192);
            lblNewPass.Name = "lblNewPass";
            lblNewPass.Size = new Size(507, 62);
            lblNewPass.TabIndex = 6;
            lblNewPass.Text = "Mật khẩu mới";
            // 
            // btnConfirm
            // 
            btnConfirm.BorderRadius = 10;
            btnConfirm.CustomizableEdges = customizableEdges9;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(332, 320);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnConfirm.Size = new Size(184, 56);
            btnConfirm.TabIndex = 7;
            btnConfirm.Text = "Xác nhận đổi";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // llbBack
            // 
            llbBack.AutoSize = true;
            llbBack.Location = new Point(350, 395);
            llbBack.Name = "llbBack";
            llbBack.Size = new Size(138, 20);
            llbBack.TabIndex = 8;
            llbBack.TabStop = true;
            llbBack.Text = "Quay lại đăng nhập";
            llbBack.LinkClicked += llbBack_LinkClicked;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = false;
            lblEmail.BackColor = Color.Transparent;
            lblEmail.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = SystemColors.MenuHighlight;
            lblEmail.Location = new Point(88, 74);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(507, 62);
            lblEmail.TabIndex = 9;
            lblEmail.Text = "Nhập Email đã đăng ký";
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 450);
            Controls.Add(llbBack);
            Controls.Add(btnConfirm);
            Controls.Add(txtOTP);
            Controls.Add(txtNewPass);
            Controls.Add(btnGetCode);
            Controls.Add(txtEmail);
            Controls.Add(lblNewPass);
            Controls.Add(lblOtp);
            Controls.Add(lblEmail);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ForgotPasswordForm";
            Text = "ForgotPasswordForm";
            FormClosed += ForgotPasswordForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2Button btnGetCode;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPass;
        private Guna.UI2.WinForms.Guna2TextBox txtOTP;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblOtp;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNewPass;
        private Guna.UI2.WinForms.Guna2Button btnConfirm;
        private LinkLabel llbBack;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEmail;
    }
}