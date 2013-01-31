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
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();

            this.Countdown = 3;
            t = new Timer();
            t.Interval = 1000;
            t.Tick += t_Tick;

            t.Enabled = true;
        }

        private int Countdown { get; set; }
        private Timer t { get; set; }

        private void t_Tick(object sender, EventArgs e)
        {
            this.Countdown -= 1;
            if (this.Countdown == -1)
            {
                // Stop timer
                t.Enabled = false;

                if (Program.loginDialog == null)
                {
                    Program.loginDialog = new LoginDialog();
                }
                Program.loginDialog.Show();

                this.Countdown = 10;

                this.Hide();
            }
        }

    }
}
