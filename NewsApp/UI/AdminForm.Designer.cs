namespace NewsApp.UI
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tabControlAdmin = new Guna.UI2.WinForms.Guna2TabControl();
            tabArticles = new TabPage();
            dgvArticles = new DataGridView();
            panelArticlesTop = new Guna.UI2.WinForms.Guna2Panel();
            btnRefreshArticles = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteArticle = new Guna.UI2.WinForms.Guna2Button();
            tabUsers = new TabPage();
            dgvUsers = new DataGridView();
            panelUsersTop = new Guna.UI2.WinForms.Guna2Panel();
            btnRefreshUsers = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteUser = new Guna.UI2.WinForms.Guna2Button();
            tabCategories = new TabPage();
            splitContainer1 = new SplitContainer();
            dgvCategories = new DataGridView();
            gbCategoryDetails = new Guna.UI2.WinForms.Guna2GroupBox();
            btnNewCategory = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteCategory = new Guna.UI2.WinForms.Guna2Button();
            btnSaveCategory = new Guna.UI2.WinForms.Guna2Button();
            txtCategoryName = new Guna.UI2.WinForms.Guna2TextBox();
            label2 = new Label();
            tabControlAdmin.SuspendLayout();
            tabArticles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvArticles).BeginInit();
            panelArticlesTop.SuspendLayout();
            tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panelUsersTop.SuspendLayout();
            tabCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            gbCategoryDetails.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlAdmin
            // 
            tabControlAdmin.Controls.Add(tabArticles);
            tabControlAdmin.Controls.Add(tabUsers);
            tabControlAdmin.Controls.Add(tabCategories);
            tabControlAdmin.Dock = DockStyle.Fill;
            tabControlAdmin.ItemSize = new Size(180, 40);
            tabControlAdmin.Location = new Point(0, 0);
            tabControlAdmin.Margin = new Padding(2);
            tabControlAdmin.Name = "tabControlAdmin";
            tabControlAdmin.SelectedIndex = 0;
            tabControlAdmin.Size = new Size(1010, 556);
            tabControlAdmin.TabButtonHoverState.BorderColor = Color.Empty;
            tabControlAdmin.TabButtonHoverState.FillColor = Color.FromArgb(40, 52, 70);
            tabControlAdmin.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F);
            tabControlAdmin.TabButtonHoverState.ForeColor = Color.White;
            tabControlAdmin.TabButtonHoverState.InnerColor = Color.FromArgb(40, 52, 70);
            tabControlAdmin.TabButtonIdleState.BorderColor = Color.Empty;
            tabControlAdmin.TabButtonIdleState.FillColor = Color.FromArgb(33, 42, 57);
            tabControlAdmin.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F);
            tabControlAdmin.TabButtonIdleState.ForeColor = Color.FromArgb(156, 160, 167);
            tabControlAdmin.TabButtonIdleState.InnerColor = Color.FromArgb(33, 42, 57);
            tabControlAdmin.TabButtonSelectedState.BorderColor = Color.Empty;
            tabControlAdmin.TabButtonSelectedState.FillColor = Color.FromArgb(29, 37, 49);
            tabControlAdmin.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F);
            tabControlAdmin.TabButtonSelectedState.ForeColor = Color.White;
            tabControlAdmin.TabButtonSelectedState.InnerColor = Color.FromArgb(76, 132, 255);
            tabControlAdmin.TabButtonSize = new Size(180, 40);
            tabControlAdmin.TabIndex = 0;
            tabControlAdmin.TabMenuBackColor = Color.FromArgb(33, 42, 57);
            tabControlAdmin.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tabArticles
            // 
            tabArticles.Controls.Add(dgvArticles);
            tabArticles.Controls.Add(panelArticlesTop);
            tabArticles.Location = new Point(4, 44);
            tabArticles.Margin = new Padding(2);
            tabArticles.Name = "tabArticles";
            tabArticles.Padding = new Padding(2);
            tabArticles.Size = new Size(1002, 508);
            tabArticles.TabIndex = 0;
            tabArticles.Text = "Quản lý Bài viết";
            // 
            // dgvArticles
            // 
            dgvArticles.AllowUserToAddRows = false;
            dgvArticles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvArticles.BackgroundColor = Color.White;
            dgvArticles.ColumnHeadersHeight = 29;
            dgvArticles.Dock = DockStyle.Fill;
            dgvArticles.Location = new Point(2, 50);
            dgvArticles.Margin = new Padding(2);
            dgvArticles.Name = "dgvArticles";
            dgvArticles.ReadOnly = true;
            dgvArticles.RowHeadersWidth = 51;
            dgvArticles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArticles.Size = new Size(998, 456);
            dgvArticles.TabIndex = 0;
            // 
            // panelArticlesTop
            // 
            panelArticlesTop.Controls.Add(btnRefreshArticles);
            panelArticlesTop.Controls.Add(btnDeleteArticle);
            panelArticlesTop.CustomizableEdges = customizableEdges5;
            panelArticlesTop.Dock = DockStyle.Top;
            panelArticlesTop.Location = new Point(2, 2);
            panelArticlesTop.Margin = new Padding(2);
            panelArticlesTop.Name = "panelArticlesTop";
            panelArticlesTop.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelArticlesTop.Size = new Size(998, 48);
            panelArticlesTop.TabIndex = 1;
            // 
            // btnRefreshArticles
            // 
            btnRefreshArticles.BorderRadius = 5;
            btnRefreshArticles.CustomizableEdges = customizableEdges1;
            btnRefreshArticles.FillColor = Color.Gray;
            btnRefreshArticles.Font = new Font("Segoe UI", 9F);
            btnRefreshArticles.ForeColor = Color.White;
            btnRefreshArticles.Location = new Point(143, 8);
            btnRefreshArticles.Margin = new Padding(2);
            btnRefreshArticles.Name = "btnRefreshArticles";
            btnRefreshArticles.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnRefreshArticles.Size = new Size(112, 32);
            btnRefreshArticles.TabIndex = 0;
            btnRefreshArticles.Text = "Làm mới";
            // 
            // btnDeleteArticle
            // 
            btnDeleteArticle.BorderRadius = 5;
            btnDeleteArticle.CustomizableEdges = customizableEdges3;
            btnDeleteArticle.FillColor = Color.Red;
            btnDeleteArticle.Font = new Font("Segoe UI", 9F);
            btnDeleteArticle.ForeColor = Color.White;
            btnDeleteArticle.Location = new Point(12, 8);
            btnDeleteArticle.Margin = new Padding(2);
            btnDeleteArticle.Name = "btnDeleteArticle";
            btnDeleteArticle.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnDeleteArticle.Size = new Size(112, 32);
            btnDeleteArticle.TabIndex = 1;
            btnDeleteArticle.Text = "Xóa bài viết";
            // 
            // tabUsers
            // 
            tabUsers.Controls.Add(dgvUsers);
            tabUsers.Controls.Add(panelUsersTop);
            tabUsers.Location = new Point(4, 44);
            tabUsers.Margin = new Padding(2);
            tabUsers.Name = "tabUsers";
            tabUsers.Padding = new Padding(2);
            tabUsers.Size = new Size(1002, 508);
            tabUsers.TabIndex = 1;
            tabUsers.Text = "Quản lý Người dùng";
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.ColumnHeadersHeight = 29;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(2, 50);
            dgvUsers.Margin = new Padding(2);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(998, 456);
            dgvUsers.TabIndex = 0;
            // 
            // panelUsersTop
            // 
            panelUsersTop.Controls.Add(btnRefreshUsers);
            panelUsersTop.Controls.Add(btnDeleteUser);
            panelUsersTop.CustomizableEdges = customizableEdges11;
            panelUsersTop.Dock = DockStyle.Top;
            panelUsersTop.Location = new Point(2, 2);
            panelUsersTop.Margin = new Padding(2);
            panelUsersTop.Name = "panelUsersTop";
            panelUsersTop.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelUsersTop.Size = new Size(998, 48);
            panelUsersTop.TabIndex = 1;
            // 
            // btnRefreshUsers
            // 
            btnRefreshUsers.BorderRadius = 5;
            btnRefreshUsers.CustomizableEdges = customizableEdges7;
            btnRefreshUsers.FillColor = Color.Gray;
            btnRefreshUsers.Font = new Font("Segoe UI", 9F);
            btnRefreshUsers.ForeColor = Color.White;
            btnRefreshUsers.Location = new Point(171, 8);
            btnRefreshUsers.Margin = new Padding(2);
            btnRefreshUsers.Name = "btnRefreshUsers";
            btnRefreshUsers.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnRefreshUsers.Size = new Size(112, 32);
            btnRefreshUsers.TabIndex = 0;
            btnRefreshUsers.Text = "Làm mới";
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.BorderRadius = 5;
            btnDeleteUser.CustomizableEdges = customizableEdges9;
            btnDeleteUser.FillColor = Color.Red;
            btnDeleteUser.Font = new Font("Segoe UI", 9F);
            btnDeleteUser.ForeColor = Color.White;
            btnDeleteUser.Location = new Point(12, 8);
            btnDeleteUser.Margin = new Padding(2);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnDeleteUser.Size = new Size(140, 32);
            btnDeleteUser.TabIndex = 1;
            btnDeleteUser.Text = "Xóa người dùng";
            // 
            // tabCategories
            // 
            tabCategories.Controls.Add(splitContainer1);
            tabCategories.Location = new Point(4, 44);
            tabCategories.Margin = new Padding(2);
            tabCategories.Name = "tabCategories";
            tabCategories.Padding = new Padding(2);
            tabCategories.Size = new Size(1002, 508);
            tabCategories.TabIndex = 2;
            tabCategories.Text = "Quản lý Chuyên mục";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(2, 2);
            splitContainer1.Margin = new Padding(2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvCategories);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(gbCategoryDetails);
            splitContainer1.Size = new Size(998, 504);
            splitContainer1.SplitterDistance = 506;
            splitContainer1.SplitterWidth = 3;
            splitContainer1.TabIndex = 0;
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.ColumnHeadersHeight = 29;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.Location = new Point(0, 0);
            dgvCategories.Margin = new Padding(2);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(506, 504);
            dgvCategories.TabIndex = 0;
            // 
            // gbCategoryDetails
            // 
            gbCategoryDetails.Controls.Add(btnNewCategory);
            gbCategoryDetails.Controls.Add(btnDeleteCategory);
            gbCategoryDetails.Controls.Add(btnSaveCategory);
            gbCategoryDetails.Controls.Add(txtCategoryName);
            gbCategoryDetails.Controls.Add(label2);
            gbCategoryDetails.CustomizableEdges = customizableEdges21;
            gbCategoryDetails.Dock = DockStyle.Fill;
            gbCategoryDetails.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbCategoryDetails.ForeColor = Color.Black;
            gbCategoryDetails.Location = new Point(0, 0);
            gbCategoryDetails.Margin = new Padding(2);
            gbCategoryDetails.Name = "gbCategoryDetails";
            gbCategoryDetails.ShadowDecoration.CustomizableEdges = customizableEdges22;
            gbCategoryDetails.Size = new Size(489, 504);
            gbCategoryDetails.TabIndex = 0;
            gbCategoryDetails.Text = "Chi tiết Chuyên mục";
            // 
            // btnNewCategory
            // 
            btnNewCategory.BorderRadius = 5;
            btnNewCategory.CustomizableEdges = customizableEdges13;
            btnNewCategory.FillColor = Color.WhiteSmoke;
            btnNewCategory.Font = new Font("Segoe UI", 9F);
            btnNewCategory.ForeColor = Color.Black;
            btnNewCategory.Location = new Point(16, 451);
            btnNewCategory.Margin = new Padding(2);
            btnNewCategory.Name = "btnNewCategory";
            btnNewCategory.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnNewCategory.Size = new Size(112, 32);
            btnNewCategory.TabIndex = 0;
            btnNewCategory.Text = "Tạo mới";
            // 
            // btnDeleteCategory
            // 
            btnDeleteCategory.BorderRadius = 5;
            btnDeleteCategory.CustomizableEdges = customizableEdges15;
            btnDeleteCategory.FillColor = Color.Red;
            btnDeleteCategory.Font = new Font("Segoe UI", 9F);
            btnDeleteCategory.ForeColor = Color.White;
            btnDeleteCategory.Location = new Point(147, 451);
            btnDeleteCategory.Margin = new Padding(2);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnDeleteCategory.Size = new Size(80, 32);
            btnDeleteCategory.TabIndex = 1;
            btnDeleteCategory.Text = "Xóa";
            // 
            // btnSaveCategory
            // 
            btnSaveCategory.BorderRadius = 5;
            btnSaveCategory.CustomizableEdges = customizableEdges17;
            btnSaveCategory.FillColor = Color.SteelBlue;
            btnSaveCategory.Font = new Font("Segoe UI", 9F);
            btnSaveCategory.ForeColor = Color.White;
            btnSaveCategory.Location = new Point(243, 451);
            btnSaveCategory.Margin = new Padding(2);
            btnSaveCategory.Name = "btnSaveCategory";
            btnSaveCategory.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnSaveCategory.Size = new Size(112, 32);
            btnSaveCategory.TabIndex = 2;
            btnSaveCategory.Text = "Lưu thay đổi";
            // 
            // txtCategoryName
            // 
            txtCategoryName.CustomizableEdges = customizableEdges19;
            txtCategoryName.DefaultText = "";
            txtCategoryName.Font = new Font("Segoe UI", 9F);
            txtCategoryName.Location = new Point(169, 62);
            txtCategoryName.Margin = new Padding(2, 3, 2, 3);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.PlaceholderText = "";
            txtCategoryName.SelectedText = "";
            txtCategoryName.ShadowDecoration.CustomizableEdges = customizableEdges20;
            txtCategoryName.Size = new Size(224, 29);
            txtCategoryName.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Location = new Point(16, 71);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(128, 20);
            label2.TabIndex = 4;
            label2.Text = "Tên Chuyên mục:";
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1010, 556);
            Controls.Add(tabControlAdmin);
            Margin = new Padding(2);
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bảng điều khiển Admin";
            tabControlAdmin.ResumeLayout(false);
            tabArticles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvArticles).EndInit();
            panelArticlesTop.ResumeLayout(false);
            tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panelUsersTop.ResumeLayout(false);
            tabCategories.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            gbCategoryDetails.ResumeLayout(false);
            gbCategoryDetails.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabArticles;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabCategories;
        private Guna.UI2.WinForms.Guna2Panel panelArticlesTop;
        private Guna.UI2.WinForms.Guna2Button btnRefreshArticles;
        private Guna.UI2.WinForms.Guna2Button btnDeleteArticle;
        private System.Windows.Forms.DataGridView dgvArticles;
        private System.Windows.Forms.DataGridView dgvUsers;
        private Guna.UI2.WinForms.Guna2Panel panelUsersTop;
        private Guna.UI2.WinForms.Guna2Button btnRefreshUsers;
        private Guna.UI2.WinForms.Guna2Button btnDeleteUser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvCategories;
        private Guna.UI2.WinForms.Guna2GroupBox gbCategoryDetails;

        private Guna.UI2.WinForms.Guna2TextBox txtCategoryName;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnSaveCategory;
        private Guna.UI2.WinForms.Guna2Button btnNewCategory;
        private Guna.UI2.WinForms.Guna2Button btnDeleteCategory;
    }
}