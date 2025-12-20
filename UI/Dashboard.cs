using System;
using System.Windows.Forms;

namespace NewsApp.UI
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            btnRunClient.Enabled = false;
        }

        private void btnRunServer_Click(object sender, EventArgs e)
        {
            // Mở form Server
            ServerForm serverForm = new ServerForm();
            serverForm.Show();
            btnRunServer.Enabled = false;
            btnRunClient.Enabled=true;
            
        }

        private void btnRunClient_Click(object sender, EventArgs e)
        {
            // Mở form Login (Client)
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}