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
    public partial class AgentForm : Form
    {
        public AgentForm()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.FormClosed += AgentForm_FormClosed;

            this.btnLogin.Click += btnLogin_Click;
            this.rdbNotReady.CheckedChanged += rdbNotReady_CheckedChanged;
            this.rdbReady.CheckedChanged += rdbReady_CheckedChanged;
        }

        void rdbReady_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbReady.Checked)
            {
                this.Text = "Ready - Cisco Agent Desktop";
            }
        }

        void rdbNotReady_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbNotReady.Checked)
            {
                this.Text = "Not Ready - Cisco Agent Desktop";
            }
        }

        private bool _LoggedIn;

        public bool LoggedIn
        {
            get { return _LoggedIn; }
            set
            {
                _LoggedIn = value;
                if (value)
                {
                    this.txtLoginStatus.Text = "Logged In";
                }
                else
                {
                    this.txtLoginStatus.Text = "Logged Out";
                    this.Text = "Logout - Cisco Agent Desktop";
                }
                this.rdbNotReady.Enabled = value;
                this.rdbReady.Enabled = value;
            }
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.LoggedIn)
            {
                this.LoggedIn = false;
                return;
            }
            else
            {
                this.Enabled = false;
                Program.loginDialog.Show();
                Program.loginDialog.LoginUsingSameProfile();
            }
        }

        public void Login()
        {
            this.LoggedIn = true;
            this.rdbNotReady.Checked = true;
            this.rdbReady_CheckedChanged(null, null);
            this.Enabled = true;
        }

        private void AgentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ready
            if (keyData == (Keys.Control | Keys.W))
            {
                this.rdbReady.Checked = true;
                return true;
            }
            // Not Ready
            if (keyData == (Keys.Control | Keys.O))
            {
                this.rdbNotReady.Checked = true;
                return true;
            }
            // Login 
            if (keyData == (Keys.Control | Keys.L))
            {
                this.btnLogin_Click(null, null);
                return true;
            }
            // Close
            if (keyData == (Keys.Alt | Keys.F4))
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
