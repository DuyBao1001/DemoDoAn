using System;
using System.Threading;
using System.Windows.Forms;
using NewsApp.BLL.Network;

namespace NewsApp.UI
{
    public partial class ServerForm : Form
    {
        private readonly ServerSocket _server;
        private Thread _serverThread;

        public ServerForm()
        {
            InitializeComponent();
            _server = new ServerSocket();

            // Đăng ký nhận log từ Server
            _server.OnLog += (msg) =>
            {
                // Vì Server chạy luồng khác, phải dùng Invoke để vẽ lên Form
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        rtbLog.AppendText(msg + Environment.NewLine);
                        rtbLog.ScrollToCaret();
                    }));
                }
            };
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            // Chạy Server trên luồng riêng để không đơ Form
            _serverThread = new Thread(() =>
            {
                _server.Start();
            })
            {
                IsBackground = true
            };
            _serverThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _server.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.Stop();
        }
    }
}