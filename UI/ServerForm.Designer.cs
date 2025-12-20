namespace NewsApp.UI
{
    partial class ServerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            rtbLog = new RichTextBox();
            btnStart = new Guna.UI2.WinForms.Guna2Button();
            btnStop = new Guna.UI2.WinForms.Guna2Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // rtbLog
            // 
            rtbLog.BackColor = Color.Black;
            rtbLog.Font = new Font("Consolas", 10F);
            rtbLog.ForeColor = Color.Lime;
            rtbLog.Location = new Point(12, 70);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(760, 591);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "";
            // 
            // btnStart
            // 
            btnStart.CustomizableEdges = customizableEdges1;
            btnStart.FillColor = Color.Green;
            btnStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(12, 12);
            btnStart.Name = "btnStart";
            btnStart.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnStart.Size = new Size(120, 45);
            btnStart.TabIndex = 2;
            btnStart.Text = "START SERVER";
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.CustomizableEdges = customizableEdges3;
            btnStop.Enabled = false;
            btnStop.FillColor = Color.Red;
            btnStop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(140, 12);
            btnStop.Name = "btnStop";
            btnStop.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnStop.Size = new Size(120, 45);
            btnStop.TabIndex = 1;
            btnStop.Text = "STOP SERVER";
            btnStop.Click += btnStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(280, 22);
            label1.Name = "label1";
            label1.Size = new Size(385, 45);
            label1.TabIndex = 0;
            label1.Text = "Server Logs & Monitoring";
            // 
            // ServerForm
            // 
            ClientSize = new Size(784, 673);
            Controls.Add(label1);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(rtbLog);
            Name = "ServerForm";
            Text = "NewsApp Server Control";
            FormClosing += ServerForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.RichTextBox rtbLog;
        private Guna.UI2.WinForms.Guna2Button btnStart;
        private Guna.UI2.WinForms.Guna2Button btnStop;
        private System.Windows.Forms.Label label1;
    }
}