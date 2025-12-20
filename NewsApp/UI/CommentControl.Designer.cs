namespace NewsApp.UI
{
    partial class CommentControl
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
            pbAvatar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            lblAuthor = new Label();
            lblContent = new Label();
            lblDate = new Label();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            SuspendLayout();
            // 
            // pbAvatar
            // 
            pbAvatar.FillColor = Color.LightGray;
            pbAvatar.ImageRotate = 0F;
            pbAvatar.Location = new Point(10, 10);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            pbAvatar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pbAvatar.Size = new Size(40, 40);
            pbAvatar.TabIndex = 0;
            pbAvatar.TabStop = false;
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAuthor.Location = new Point(60, 10);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(60, 20);
            lblAuthor.TabIndex = 1;
            lblAuthor.Text = "Author";
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Font = new Font("Segoe UI", 9F);
            lblContent.Location = new Point(60, 35);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(61, 20);
            lblContent.TabIndex = 2;
            lblContent.Text = "Content";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 8F);
            lblDate.ForeColor = Color.Gray;
            lblDate.Location = new Point(60, 60);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(38, 19);
            lblDate.TabIndex = 3;
            lblDate.Text = "Date";
            // 
            // CommentControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblDate);
            Controls.Add(lblContent);
            Controls.Add(lblAuthor);
            Controls.Add(pbAvatar);
            Name = "CommentControl";
            Size = new Size(750, 90);
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox pbAvatar;
        private Label lblAuthor;
        private Label lblContent;
        private Label lblDate;
    }
}
