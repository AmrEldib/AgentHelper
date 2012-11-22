using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        private Timer StatusTimer { get; set; }

        private NotifyIcon TrayIcon { get; set; }
        private ContextMenu TrayMenu { get; set; }

        public Icon PhoneIcon
        {
            set
            {
                this.TrayIcon.Icon = value;
                this.Icon = value;
            }
        }

        public AgentMonitor()
        {
            InitializeComponent();

            // Check that log file exists
            this.CheckLogFilePath();

            // Set up monitoring of agent status every 10 seconds
            this.StatusTimer = new Timer();
            this.StatusTimer.Interval = 10000;
            this.StatusTimer.Tick += StatusTimer_Tick;

            // Create a simple tray menu.
            TrayMenu = new ContextMenu();
            TrayMenu.MenuItems.Add("Exit", OnExit);
            TrayMenu.MenuItems.Add("Restore", OnRestore);

            // Create tray icon.
            this.TrayIcon = new NotifyIcon();
            this.TrayIcon.Text = "Agent Helper";
            this.TrayIcon.ContextMenu = TrayMenu;
            this.TrayIcon.Visible = true;
            this.TrayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
            this.TrayIcon.DoubleClick += TrayIcon_DoubleClick;

            // Detect status for the first time
            this.DetectAgentStatus();

            this.Resize += AgentMonitor_Resize;
            this.FormClosed += AgentMonitor_FormClosed;

            // Start monitoring status
            this.StatusTimer.Start();
        }

        private void AgentMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TrayIcon.Visible = false;
            this.TrayIcon.Dispose();
        }

        private void CheckLogFilePath()
        {
            if (!File.Exists(Settings.Default.AgentLogFilePath))
            {
                // Check if "Program Files (x86)" exists. If not, it's a x86 OS
                if (!Directory.Exists(@"C:\Program Files (x86)\"))
                {
                    // If x86 log file is found, switch to it.
                    if (File.Exists(Resources.AgentLogFilePath86))
                    {
                        Settings.Default.AgentLogFilePath = Resources.AgentLogFilePath86;
                        Settings.Default.Save();
                    }
                }
                else
                {
                    // TODO: Other cases where "Agent0001.log" is not the log file, but "Agent0002.log" or other number.
                }
            }
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Settings.Default.ShowMinimizeNotification = false;
            Settings.Default.Save();
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void AgentMonitor_Resize(object sender, EventArgs e)
        {
            TrayIcon.BalloonTipTitle = "Agent Helper Minimized to Tray";
            TrayIcon.BalloonTipText = "Click here to stop showing this message.";

            if (FormWindowState.Minimized == this.WindowState)
            {
                this.TrayIcon.Visible = true;
                if (Settings.Default.ShowMinimizeNotification)
                {
                    this.TrayIcon.ShowBalloonTip(500);
                }
                this.Hide();
            }
        }

        private void OnRestore(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            this.DetectAgentStatus();
        }

        private string GetContentOfAgentLog(string filepath)
        {
            string content;
            FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                // get file length
                int length = (int)fileStream.Length;
                // create buffer
                var buffer = new byte[length];
                // actual number of bytes read
                int count;
                // total number of bytes read          
                int sum = 0;

                // read until Read method returns 0 
                // (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                {
                    sum += count;  // sum is a buffer offset for next reading
                }

                content = Encoding.UTF8.GetString(buffer);
            }
            finally
            {
                fileStream.Close();
            }

            return content;
        }

        private void DetectAgentStatus()
        {
            // Read log file
            string logContent = this.GetContentOfAgentLog(Settings.Default.AgentLogFilePath);

            // Detect status based on log content
            bool loggedIn = this.IsAgentLoggedIn(logContent);

            // Update visuals
            if (loggedIn)
            {
                this.lblCurrentStatus.Text = "Logged In";
                this.PhoneIcon = Resources.GreenPhone;
            }
            else
            {
                this.lblCurrentStatus.Text = "Logged Out";
                this.PhoneIcon = Resources.RedPhone;
            }
        }

        private bool IsAgentLoggedIn(string logContent)
        {
            int loggedInIndex = logContent.LastIndexOf(Resources.LoggedInMessage);
            int loggedOutIndex = logContent.LastIndexOf(Resources.LoggedOutMessage);

            return (loggedInIndex > loggedOutIndex);
        }
    }
}
