namespace NewsApp.UI
{
    partial class ArticleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pbImage = new Guna.UI2.WinForms.Guna2PictureBox();
            lblTitle = new System.Windows.Forms.Label();
            lblCategory = new System.Windows.Forms.Label();
            lblAuthor = new System.Windows.Forms.Label();
            lblDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            SuspendLayout();
            // 
            // pbImage
            // 
            pbImage.BorderRadius = 10;
            pbImage.Cursor = System.Windows.Forms.Cursors.Hand;
            pbImage.CustomizableEdges = customizableEdges1;
            pbImage.FillColor = System.Drawing.Color.LightGray;
            pbImage.ImageRotate = 0F;
            pbImage.Location = new System.Drawing.Point(13, 13);
            pbImage.Name = "pbImage";
            pbImage.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pbImage.Size = new System.Drawing.Size(254, 150);
            pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pbImage.TabIndex = 0;
            pbImage.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitle.Location = new System.Drawing.Point(13, 175);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(254, 56);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Tiêu đề bài viết";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblCategory.ForeColor = System.Drawing.Color.DodgerBlue;
            lblCategory.Location = new System.Drawing.Point(13, 240);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new System.Drawing.Size(80, 20);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Danh mục";
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblAuthor.ForeColor = System.Drawing.Color.Gray;
            lblAuthor.Location = new System.Drawing.Point(13, 270);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new System.Drawing.Size(51, 19);
            lblAuthor.TabIndex = 3;
            lblAuthor.Text = "Tác giả";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblDate.ForeColor = System.Drawing.Color.Gray;
            lblDate.Location = new System.Drawing.Point(170, 270);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(97, 19);
            lblDate.TabIndex = 4;
            lblDate.Text = "DD/MM/YYYY";
            lblDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ArticleControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(lblDate);
            Controls.Add(lblAuthor);
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            Controls.Add(pbImage);
            Name = "ArticleControl";
            Size = new System.Drawing.Size(280, 300);
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox pbImage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblDate;
    }
}
