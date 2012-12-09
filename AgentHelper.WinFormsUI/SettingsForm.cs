using System;
using System.IO;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;

namespace AgentHelper.WinFormsUI
{
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// SettingsForm default constructor.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();

            this.chkLogOutWhenIdle.CheckedChanged += chkLogOutWhenIdle_CheckedChanged;
            this.btnCiscoExeLocation.Click += btnCiscoExeLocation_Click;
            this.btnCancel.Click += btnCancel_Click;
            this.btnSave.Click += btnSave_Click;
            this.txtCiscoExeLocation.TextChanged += textBoxCanNotBeEmpty_TextChanged;
            this.txtCiscoPassword.TextChanged += textBoxCanNotBeEmpty_TextChanged;

            this.LoadSettings();
        }

        /// <summary>
        /// Handler for the text boxes that can't be empty TextChanged event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void textBoxCanNotBeEmpty_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            this.btnSave.Enabled = (textBox.Text.Length != 0);
        }

        /// <summary>
        /// Handler for the 'Log Out When Idle' checkbox's CheckedChanged event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void chkLogOutWhenIdle_CheckedChanged(object sender, EventArgs e)
        {
            this.numLogOutWhenIdle.Enabled = this.chkLogOutWhenIdle.Checked;
        }

        /// <summary>
        /// Handler for the 'Save' button's Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveSettings();
                this.Close();
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show(this,
                    exp.Message,
                    "Can't Save",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handler for the 'Cancel' button's Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handler for the form's Resize event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnCiscoExeLocation_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.txtCiscoExeLocation.Text))
            {
                ofdCiscoExeLocation.InitialDirectory = Path.GetDirectoryName(this.txtCiscoExeLocation.Text);
                ofdCiscoExeLocation.FileName = Path.GetFileName(this.txtCiscoExeLocation.Text);
            }
            DialogResult result = ofdCiscoExeLocation.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtCiscoExeLocation.Text = ofdCiscoExeLocation.FileName;
            }
        }

        /// <summary>
        /// In case the application file of the Cisco Agent is not found, this method helps the user choose the location of the file.
        /// </summary>
        private static void ChooseCiscoApplicationLocation()
        {
            MessageBox.Show("Can't find Cisco Agent Application file. Please choose the path to the file.",
                "Application File Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            bool fileReplaced = false;
            while (!fileReplaced)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Application|*.exe";
                ofd.SupportMultiDottedExtensions = true;
                ofd.Title = "Select Cisco Application File";
                DialogResult result = ofd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Settings.Default.CiscoAgentExeFileLocation = ofd.FileName;
                    Settings.Default.Save();
                    fileReplaced = true;
                }
                else
                {
                    DialogResult errorResult = MessageBox.Show("Can't run Agent Helper without the location of the Cisco Agent Application file. Exit now?",
                        "File Location Required",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (errorResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        fileReplaced = true;
                        Environment.Exit(0);
                    }
                }
            }
        }

        /// <summary>
        /// Validates stored settings and corrects them if their invalid.
        /// </summary>
        public static void ValidateStoredSettings()
        {
            // CiscoAgentExeFileLocation must exist
            if (!File.Exists(Settings.Default.CiscoAgentExeFileLocation))
            {
                if (File.Exists(Resources.AgentExeLocation86))
                {
                    Settings.Default.CiscoAgentExeFileLocation = Resources.AgentExeLocation86;
                    Settings.Default.Save();
                }
                else
                {
                    SettingsForm.ChooseCiscoApplicationLocation();
                }
            }

            // IdleMinsBeforeLoggingOut can't be less than 1
            if (Settings.Default.IdleMinsBeforeLoggingOut < 1)
            {
                Settings.Default.IdleMinsBeforeLoggingOut = 1;
                Settings.Default.Save();
            }

            // LogInAfterIdleToStatus can't be outside NotReady or Ready
            if (Settings.Default.LogInAfterIdleToStatus != AgentStatus.Ready &
                Settings.Default.LogInAfterIdleToStatus != AgentStatus.NotReady)
            {
                Settings.Default.LogInAfterIdleToStatus = AgentStatus.NotReady;
                Settings.Default.Save();
            }

            // ChangeStatusOnScreenLockTo can't be outside LogOut or NotReady
            if (Settings.Default.ChangeStatusOnScreenLockTo != AgentStatus.LoggedOut &
                Settings.Default.ChangeStatusOnScreenLockTo != AgentStatus.NotReady)
            {
                Settings.Default.ChangeStatusOnScreenLockTo = AgentStatus.NotReady;
                Settings.Default.Save();
            }

            // ChangeStatusOnScreenUnlockTo can't be outside NotReady or Ready
            if (Settings.Default.ChangeStatusOnScreenUnlockTo != AgentStatus.Ready &
                Settings.Default.ChangeStatusOnScreenUnlockTo != AgentStatus.NotReady)
            {
                Settings.Default.ChangeStatusOnScreenUnlockTo = AgentStatus.NotReady;
                Settings.Default.Save();
            }

            // LogInOnStartupTo can't be outside NotReady or Ready
            if (Settings.Default.LogInOnStartupTo != AgentStatus.Ready &
                Settings.Default.LogInOnStartupTo != AgentStatus.NotReady)
            {
                Settings.Default.LogInOnStartupTo = AgentStatus.NotReady;
                Settings.Default.Save();
            }

            // CiscoPassword can't be empty
            if (String.IsNullOrWhiteSpace(Settings.Default.CiscoPassword))
            {
                Settings.Default.CiscoPassword = "123456";
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Load settings to form.
        /// </summary>
        private void LoadSettings()
        {
            // Verify that settings are valid
            // In case the user changes the settings file manually, this should fix it.
            ValidateStoredSettings();

            // CiscoAgentProcessName
            this.txtCiscoExeLocation.Text = Settings.Default.CiscoAgentExeFileLocation;

            // ============ Idle Time ==============
            // Log out Agent when System is Idle
            this.chkLogOutWhenIdle.Checked = Settings.Default.LogOutOnIdle;
            this.numLogOutWhenIdle.Value = Settings.Default.IdleMinsBeforeLoggingOut;
            this.chkLogOutWhenIdle_CheckedChanged(null, null);

            // Log in after coming back from Idle
            this.chkLogInAfterIdle.Checked = Settings.Default.LogInAfterIdle;
            if (Settings.Default.LogInAfterIdleToStatus == AgentStatus.Ready)
            {
                this.rdbIdleReady.Checked = true;
            }
            else
            {
                this.rdbIdleNotReady.Checked = true;
            }

            // On screen lock, switch to
            this.chkScreenLock.Checked = Settings.Default.ChangeStatusOnScreenLock;
            if (Settings.Default.ChangeStatusOnScreenLockTo == AgentStatus.LoggedOut)
            {
                this.rdbScreenLockLogOut.Checked = true;
            }
            else
            {
                this.rdbScreenLockNotReady.Checked = true;
            }

            // Log in after coming back from screen lock to
            this.chkScreenUnlock.Checked = Settings.Default.ChangeStatusOnScreenUnlock;
            if (Settings.Default.ChangeStatusOnScreenUnlockTo == AgentStatus.Ready)
            {
                this.rdbScreenUnlockReady.Checked = true;
            }
            else
            {
                this.rdbScreenUnlockNotReady.Checked = true;
            }

            // ============ Startup & Shutdown ==============
            this.chkStartup.Checked = Settings.Default.LogInOnStartup;
            if (Settings.Default.LogInOnStartupTo == AgentStatus.Ready)
            {
                this.rdbStartupReady.Checked = true;
            }
            else
            {
                this.rdbStartupNotReady.Checked = true;
            }

            // CiscoPassword
            this.txtCiscoPassword.Text = Settings.Default.CiscoPassword;
        }

        /// <summary>
        /// Save settings from form to user's settings.
        /// </summary>
        private void SaveSettings()
        {
            // CiscoAgentProcessName
            if (!string.IsNullOrWhiteSpace(this.txtCiscoExeLocation.Text))
            {
                throw new ArgumentException("Cisco Application Name is missing.", "CiscoAgentProcessName");
            }
            else
            {
                Settings.Default.CiscoAgentExeFileLocation = this.txtCiscoExeLocation.Text;
            }

            // ============ Idle Time ==============
            // Log out Agent when System is Idle
            Settings.Default.LogOutOnIdle = this.chkLogOutWhenIdle.Checked;
            Settings.Default.IdleMinsBeforeLoggingOut = (int)this.numLogOutWhenIdle.Value;

            // Log in after coming back from Idle
            Settings.Default.LogInAfterIdle = this.chkLogInAfterIdle.Checked;
            if (this.rdbIdleReady.Checked == true)
            {
                Settings.Default.LogInAfterIdleToStatus = AgentStatus.Ready;
            }
            else
            {
                Settings.Default.LogInAfterIdleToStatus = AgentStatus.NotReady;
            }

            // On screen lock, switch to
            Settings.Default.ChangeStatusOnScreenLock = this.chkScreenLock.Checked;
            if (this.rdbScreenLockLogOut.Checked == true)
            {
                Settings.Default.ChangeStatusOnScreenLockTo = AgentStatus.LoggedOut;
            }
            else
            {
                Settings.Default.ChangeStatusOnScreenLockTo = AgentStatus.NotReady;
            }

            // Log in after coming back from screen lock to
            Settings.Default.ChangeStatusOnScreenUnlock = this.chkScreenUnlock.Checked;
            if (this.rdbScreenUnlockReady.Checked == true)
            {
                Settings.Default.ChangeStatusOnScreenUnlockTo = AgentStatus.Ready;
            }
            else
            {
                Settings.Default.ChangeStatusOnScreenUnlockTo = AgentStatus.NotReady;
            }

            // ============ Startup & Shutdown ==============
            Settings.Default.LogInOnStartup = this.chkStartup.Checked;
            if (this.rdbStartupReady.Checked == true)
            {
                Settings.Default.LogInOnStartupTo = AgentStatus.Ready;
            }
            else
            {
                Settings.Default.LogInOnStartupTo = AgentStatus.NotReady;
            }

            // CiscoPassword
            if (string.IsNullOrWhiteSpace(this.txtCiscoPassword.Text))
            {
                throw new ArgumentException("Cisco Password is missing.", "CiscoPassword");
            }
            else
            {
                Settings.Default.CiscoPassword = this.txtCiscoPassword.Text;
                this.UpdateAhkScriptsWithPassword(this.txtCiscoPassword.Text);
            }

        }

        private void UpdateAhkScriptsWithPassword(string password)
        {
            string loginScriptContent = Resources.AhkLoginScript.Replace("[PASSCODE]", password);

            // Write script to file
            using (TextWriter tw = new StreamWriter(Resources.AhkScriptLogin))
            {
                tw.Write(loginScriptContent);
            }
        }
    }
}
