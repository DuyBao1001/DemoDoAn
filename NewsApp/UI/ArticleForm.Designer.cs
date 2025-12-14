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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pbImage = new Guna.UI2.WinForms.Guna2PictureBox();
            lblTitle = new Label();
            lblCategory = new Label();
            lblAuthor = new Label();
            lblDate = new Label();
            txtContent = new Guna.UI2.WinForms.Guna2TextBox();
            flpComments = new FlowLayoutPanel();
            pnComment = new Guna.UI2.WinForms.Guna2Panel();
            txtComment = new Guna.UI2.WinForms.Guna2TextBox();
            btnSendComment = new Guna.UI2.WinForms.Guna2Button();
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
            pbImage.Location = new Point(12, 12);
            pbImage.Name = "pbImage";
            pbImage.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pbImage.Size = new Size(776, 300);
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbImage.TabIndex = 0;
            pbImage.TabStop = false;
            pbImage.Click += pbImage_Click;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 325);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(776, 70);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Tiêu đề bài viết";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategory.ForeColor = Color.DodgerBlue;
            lblCategory.Location = new Point(12, 400);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(91, 23);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Danh mục";
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI", 9F);
            lblAuthor.ForeColor = Color.Gray;
            lblAuthor.Location = new Point(120, 402);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(55, 20);
            lblAuthor.TabIndex = 3;
            lblAuthor.Text = "Tác giả";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F);
            lblDate.ForeColor = Color.Gray;
            lblDate.Location = new Point(680, 402);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(101, 20);
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
            txtContent.Location = new Point(12, 440);
            txtContent.Margin = new Padding(4);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.PlaceholderText = "";
            txtContent.ReadOnly = true;
            txtContent.ScrollBars = ScrollBars.Vertical;
            txtContent.SelectedText = "";
            txtContent.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtContent.Size = new Size(776, 300);
            txtContent.TabIndex = 5;
            // 
            // flpComments
            // 
            flpComments.AutoScroll = true;
            flpComments.AutoSize = true;
            flpComments.FlowDirection = FlowDirection.TopDown;
            flpComments.Location = new Point(12, 750);
            flpComments.MaximumSize = new Size(776, 0);
            flpComments.MinimumSize = new Size(776, 10);
            flpComments.Name = "flpComments";
            flpComments.Size = new Size(776, 10);
            flpComments.TabIndex = 6;
            flpComments.WrapContents = false;
            // 
            // pnComment
            // 
            pnComment.Controls.Add(btnSendComment);
            pnComment.Controls.Add(txtComment);
            pnComment.CustomizableEdges = customizableEdges5;
            pnComment.FillColor = Color.WhiteSmoke;
            pnComment.Location = new Point(12, 770);
            pnComment.Name = "pnComment";
            pnComment.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnComment.Size = new Size(776, 100);
            pnComment.TabIndex = 7;
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
            txtComment.Location = new Point(15, 15);
            txtComment.Margin = new Padding(3, 4, 3, 4);
            txtComment.Name = "txtComment";
            txtComment.PasswordChar = '\0';
            txtComment.PlaceholderText = "Viết bình luận...";
            txtComment.SelectedText = "";
            txtComment.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtComment.Size = new Size(746, 40);
            txtComment.TabIndex = 0;
            // 
            // btnSendComment
            // 
            btnSendComment.BorderRadius = 5;
            btnSendComment.CustomizableEdges = customizableEdges9;
            btnSendComment.DisabledState.BorderColor = Color.DarkGray;
            btnSendComment.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSendComment.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSendComment.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSendComment.Font = new Font("Segoe UI", 9F);
            btnSendComment.ForeColor = Color.White;
            btnSendComment.Location = new Point(661, 62);
            btnSendComment.Name = "btnSendComment";
            btnSendComment.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSendComment.Size = new Size(100, 35);
            btnSendComment.TabIndex = 1;
            btnSendComment.Text = "Gửi";
            // 
            // ArticleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(800, 860);
            Controls.Add(pnComment);
            Controls.Add(flpComments);
            Controls.Add(txtContent);
            Controls.Add(lblDate);
            Controls.Add(lblAuthor);
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            Controls.Add(pbImage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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