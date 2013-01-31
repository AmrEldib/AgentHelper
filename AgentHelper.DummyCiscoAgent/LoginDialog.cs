using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AgentHelper.DummyCiscoAgent
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();

            this.Countdown = 5;
            t = new Timer();
            t.Interval = 1000;
            t.Tick += t_Tick;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            this.Countdown -= 1;
            if (this.Countdown == -1)
            {
                // Stop timer
                t.Enabled = false;

                Program.waitForm.Hide();
                if (Program.agentForm == null)
                {
                    Program.agentForm = new AgentForm();
                }
                Program.agentForm.Login();
                Program.agentForm.Show();

                this.Countdown = 10;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int Countdown { get; set; }
        private Timer t { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Program.waitForm == null)
            {
                Program.waitForm = new WaitForm();
            }
            Program.waitForm.Show();

            t.Enabled = true;

            // Enable all controls
            this.txtID.Enabled = true;
            this.txtExtension.Enabled = true;
        }

        public void LoginUsingSameProfile()
        {
            this.txtID.Enabled = false;
            this.txtExtension.Enabled = false;
            this.txtPassword.Focus();
        }
    }
}
