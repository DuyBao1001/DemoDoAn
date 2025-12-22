namespace NewsApp.UI
{
    partial class ChangePasswordForm
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

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();

            this.txtOldPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNewPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtConfirmPass = new Guna.UI2.WinForms.Guna2TextBox();

            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();

            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();

            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.DimGray;
            this.labelTitle.Location = new System.Drawing.Point(130, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(180, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "ĐỔI MẬT KHẨU";

            // 
            // label1 (Mật khẩu cũ)
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(40, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mật khẩu cũ:";

            // 
            // txtOldPass
            // 
            this.txtOldPass.BorderRadius = 5;
            this.txtOldPass.DefaultText = "";
            this.txtOldPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtOldPass.Location = new System.Drawing.Point(40, 100);
            this.txtOldPass.PasswordChar = '●';
            this.txtOldPass.PlaceholderText = "Nhập mật khẩu hiện tại";
            this.txtOldPass.Size = new System.Drawing.Size(360, 45);
            this.txtOldPass.TabIndex = 2;
            this.txtOldPass.UseSystemPasswordChar = true;

            // 
            // label2 (Mật khẩu mới)
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(40, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mật khẩu mới:";

            // 
            // txtNewPass
            // 
            this.txtNewPass.BorderRadius = 5;
            this.txtNewPass.DefaultText = "";
            this.txtNewPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPass.Location = new System.Drawing.Point(40, 190);
            this.txtNewPass.PasswordChar = '●';
            this.txtNewPass.PlaceholderText = "Nhập mật khẩu mới";
            this.txtNewPass.Size = new System.Drawing.Size(360, 45);
            this.txtNewPass.TabIndex = 4;
            this.txtNewPass.UseSystemPasswordChar = true;

            // 
            // label3 (Nhập lại mật khẩu)
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(40, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nhập lại mật khẩu mới:";

            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.BorderRadius = 5;
            this.txtConfirmPass.DefaultText = "";
            this.txtConfirmPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPass.Location = new System.Drawing.Point(40, 280);
            this.txtConfirmPass.PasswordChar = '●';
            this.txtConfirmPass.PlaceholderText = "Xác nhận mật khẩu mới";
            this.txtConfirmPass.Size = new System.Drawing.Size(360, 45);
            this.txtConfirmPass.TabIndex = 6;
            this.txtConfirmPass.UseSystemPasswordChar = true;

            // 
            // btnSave (Lưu thay đổi)
            // 
            this.btnSave.BorderRadius = 5;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(0, 192, 0);
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(230, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnClose (Hủy bỏ)
            // 
            this.btnClose.BorderRadius = 5;
            this.btnClose.FillColor = System.Drawing.Color.Gray;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(80, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 45);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Hủy";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.labelTitle);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.txtOldPass);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.txtNewPass);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.txtConfirmPass);
            this.guna2Panel1.Controls.Add(this.btnSave);
            this.guna2Panel1.Controls.Add(this.btnClose);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(450, 450);
            this.guna2Panel1.TabIndex = 0;

            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "ChangePasswordForm";
            this.Text = "Đổi mật khẩu";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtOldPass;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPass;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPass;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}