namespace NewsApp.UI
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnRunServer = new Guna.UI2.WinForms.Guna2Button();
            this.btnRunClient = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(100, 40);
            this.label1.Text = "NewsApp Dashboard";

            // btnRunServer
            this.btnRunServer.FillColor = System.Drawing.Color.DarkSlateBlue;
            this.btnRunServer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRunServer.ForeColor = System.Drawing.Color.White;
            this.btnRunServer.Location = new System.Drawing.Point(80, 100);
            this.btnRunServer.Size = new System.Drawing.Size(240, 60);
            this.btnRunServer.Text = "Chạy Server";
            this.btnRunServer.Click += new System.EventHandler(this.btnRunServer_Click);

            // btnRunClient
            this.btnRunClient.FillColor = System.Drawing.Color.Teal;
            this.btnRunClient.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRunClient.ForeColor = System.Drawing.Color.White;
            this.btnRunClient.Location = new System.Drawing.Point(80, 180);
            this.btnRunClient.Size = new System.Drawing.Size(240, 60);
            this.btnRunClient.Text = "Chạy News App";
            this.btnRunClient.Click += new System.EventHandler(this.btnRunClient_Click);

            // Dashboard
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnRunClient);
            this.Controls.Add(this.btnRunServer);
            this.Controls.Add(this.label1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2Button btnRunServer;
        private Guna.UI2.WinForms.Guna2Button btnRunClient;
        private System.Windows.Forms.Label label1;
    }
}