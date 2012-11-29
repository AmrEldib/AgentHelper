using System;
using System.IO;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;

namespace AgentHelper.WinFormsUI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            this.chkLogInAfterIdle.CheckedChanged += chkLogInAfterIdle_CheckedChanged;
            this.chkLogOutWhenIdle.CheckedChanged += chkLogOutWhenIdle_CheckedChanged;
            this.btnLogPathBrowse.Click += btnLogPathBrowse_Click;
            this.btnCancel.Click += btnCancel_Click;
            this.btnSave.Click += btnSave_Click;

            this.LoadSettings();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveSettings();
                this.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(this, "The log file specified can't be found, or is invalid.", "Can't Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogPathBrowse_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.txtLogPath.Text))
            {
                ofdLogFilePath.InitialDirectory = Path.GetDirectoryName(this.txtLogPath.Text);
                ofdLogFilePath.FileName = Path.GetFileName(this.txtLogPath.Text);
            }
            DialogResult result = ofdLogFilePath.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtLogPath.Text = ofdLogFilePath.FileName;
            }
        }

        private void chkLogOutWhenIdle_CheckedChanged(object sender, EventArgs e)
        {
            this.numLogOutWhenIdle.Enabled = this.chkLogOutWhenIdle.Checked;
        }

        private void chkLogInAfterIdle_CheckedChanged(object sender, EventArgs e)
        {
            this.txtCiscoPassword.Enabled = this.chkLogInAfterIdle.Checked;
        }

        private void LoadSettings()
        {
            // AgentLogFilePath
            this.txtLogPath.Text = Settings.Default.AgentLogFilePath;

            // IdleMinsBeforeLoggingOut
            if (Settings.Default.IdleMinsBeforeLoggingOut == 0)
            {
                this.chkLogOutWhenIdle.Checked = false;
                this.numLogOutWhenIdle.Value = 10;
            }
            else
            {
                this.chkLogOutWhenIdle.Checked = true;
                this.numLogOutWhenIdle.Value = Settings.Default.IdleMinsBeforeLoggingOut;
            }
            this.chkLogOutWhenIdle_CheckedChanged(null, null);

            // LogInAfterIdle
            this.chkLogInAfterIdle.Checked = Settings.Default.LogInAfterIdle;
            this.chkLogInAfterIdle_CheckedChanged(null, null);

            // CiscoPassword
            this.txtCiscoPassword.Text = Settings.Default.CiscoPassword;
        }

        private void SaveSettings()
        {
            // AgendLogFilePath
            if (!File.Exists(this.txtLogPath.Text))
            {
                throw new FileNotFoundException("Log file not found.", this.txtLogPath.Text);
            }
            else
            {
                Settings.Default.AgentLogFilePath = this.txtLogPath.Text;
            }

            // IdleMinsBeforeLoggingOut
            if (!this.chkLogOutWhenIdle.Checked)
            {
                Settings.Default.IdleMinsBeforeLoggingOut = 0;
            }
            else
            {
                Settings.Default.IdleMinsBeforeLoggingOut = (int)this.numLogOutWhenIdle.Value;
            }

            // LogInAfterIdle
            Settings.Default.LogInAfterIdle = this.chkLogInAfterIdle.Checked;

            // CiscoPassword
            if (this.chkLogInAfterIdle.Checked)
            {
                Settings.Default.CiscoPassword = this.txtCiscoPassword.Text;
            }
            else
            {
                Settings.Default.CiscoPassword = "123456";
            }
            UpdateAhkLogInScript();
        }

        private void UpdateAhkLogInScript()
        {
            string loginScriptContent = Resources.AhkLoginScript.Replace("[PASSCODE]", Settings.Default.CiscoPassword);

            // Write script to file
            using (TextWriter tw = new StreamWriter(Resources.AhkLoginScriptName))
            {
                tw.Write(loginScriptContent);
            }
        }
    }
}
