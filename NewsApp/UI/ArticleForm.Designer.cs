namespace NewsApp.UI
{
    partial class ArticleForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pbImage = new Guna.UI2.WinForms.Guna2PictureBox();
            lblTitle = new Label();
            lblCategory = new Label();
            lblAuthor = new Label();
            lblDate = new Label();
            txtContent = new Guna.UI2.WinForms.Guna2TextBox();
            flpComments = new FlowLayoutPanel();
            pnComment = new Guna.UI2.WinForms.Guna2Panel();
            btnSendComment = new Guna.UI2.WinForms.Guna2Button();
            txtComment = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            pnComment.SuspendLayout();
            SuspendLayout();
            // 
            // pbImage
            // 
            pbImage.BorderRadius = 10;
            pbImage.CustomizableEdges = customizableEdges1;
            pbImage.FillColor = Color.LightGray;
            pbImage.ImageRotate = 0F;
            pbImage.Location = new Point(10, 9);
            pbImage.Margin = new Padding(3, 2, 3, 2);
            pbImage.Name = "pbImage";
            pbImage.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pbImage.Size = new Size(679, 225);
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbImage.TabIndex = 0;
            pbImage.TabStop = false;
            pbImage.Click += pbImage_Click;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 244);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(679, 52);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Tiêu đề bài viết";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategory.ForeColor = Color.DodgerBlue;
            lblCategory.Location = new Point(10, 300);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(75, 19);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Danh mục";
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI", 9F);
            lblAuthor.ForeColor = Color.Gray;
            lblAuthor.Location = new Point(105, 302);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(44, 15);
            lblAuthor.TabIndex = 3;
            lblAuthor.Text = "Tác giả";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F);
            lblDate.ForeColor = Color.Gray;
            lblDate.Location = new Point(595, 302);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(83, 15);
            lblDate.TabIndex = 4;
            lblDate.Text = "DD/MM/YYYY";
            lblDate.TextAlign = ContentAlignment.TopRight;
            // 
            // txtContent
            // 
            txtContent.BorderThickness = 0;
            txtContent.CustomizableEdges = customizableEdges3;
            txtContent.DefaultText = "";
            txtContent.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtContent.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtContent.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtContent.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtContent.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtContent.Font = new Font("Segoe UI", 11F);
            txtContent.ForeColor = Color.Black;
            txtContent.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtContent.Location = new Point(10, 330);
            txtContent.Margin = new Padding(4, 3, 4, 3);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.PlaceholderText = "";
            txtContent.ReadOnly = true;
            txtContent.ScrollBars = ScrollBars.Vertical;
            txtContent.SelectedText = "";
            txtContent.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtContent.Size = new Size(679, 240);
            txtContent.TabIndex = 5;
            // 
            // flpComments
            // 
            flpComments.AutoScroll = true;
            flpComments.AutoSize = true;
            flpComments.FlowDirection = FlowDirection.TopDown;
            flpComments.Location = new Point(9, 674);
            flpComments.Margin = new Padding(3, 2, 3, 2);
            flpComments.MaximumSize = new Size(679, 0);
            flpComments.MinimumSize = new Size(679, 8);
            flpComments.Name = "flpComments";
            flpComments.Size = new Size(679, 8);
            flpComments.TabIndex = 6;
            flpComments.WrapContents = false;
            // 
            // pnComment
            // 
            pnComment.Controls.Add(btnSendComment);
            pnComment.Controls.Add(txtComment);
            pnComment.CustomizableEdges = customizableEdges9;
            pnComment.FillColor = Color.WhiteSmoke;
            pnComment.Location = new Point(9, 585);
            pnComment.Margin = new Padding(3, 2, 3, 2);
            pnComment.Name = "pnComment";
            pnComment.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnComment.Size = new Size(679, 75);
            pnComment.TabIndex = 7;
            // 
            // btnSendComment
            // 
            btnSendComment.BorderRadius = 5;
            btnSendComment.CustomizableEdges = customizableEdges5;
            btnSendComment.DisabledState.BorderColor = Color.DarkGray;
            btnSendComment.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSendComment.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSendComment.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSendComment.Font = new Font("Segoe UI", 9F);
            btnSendComment.ForeColor = Color.White;
            btnSendComment.Location = new Point(578, 46);
            btnSendComment.Margin = new Padding(3, 2, 3, 2);
            btnSendComment.Name = "btnSendComment";
            btnSendComment.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnSendComment.Size = new Size(88, 26);
            btnSendComment.TabIndex = 1;
            btnSendComment.Text = "Gửi";
            // 
            // txtComment
            // 
            txtComment.BorderRadius = 5;
            txtComment.CustomizableEdges = customizableEdges7;
            txtComment.DefaultText = "";
            txtComment.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtComment.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtComment.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtComment.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtComment.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtComment.Font = new Font("Segoe UI", 9F);
            txtComment.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtComment.Location = new Point(13, 11);
            txtComment.Name = "txtComment";
            txtComment.PlaceholderText = "Viết bình luận...";
            txtComment.SelectedText = "";
            txtComment.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtComment.Size = new Size(653, 30);
            txtComment.TabIndex = 0;
            // 
            // ArticleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(700, 701);
            Controls.Add(pnComment);
            Controls.Add(flpComments);
            Controls.Add(txtContent);
            Controls.Add(lblDate);
            Controls.Add(lblAuthor);
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            Controls.Add(pbImage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "ArticleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết bài viết";
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            pnComment.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox pbImage;
        private Label lblTitle;
        private Label lblCategory;
        private Label lblAuthor;
        private Label lblDate;
        private Guna.UI2.WinForms.Guna2TextBox txtContent;
        private FlowLayoutPanel flpComments;
        private Guna.UI2.WinForms.Guna2Panel pnComment;
        private Guna.UI2.WinForms.Guna2TextBox txtComment;
        private Guna.UI2.WinForms.Guna2Button btnSendComment;
    }
}