using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        private Timer StatusTimer { get; set; }

        private Timer IdleTimer { get; set; }

        private NotifyIcon TrayIcon { get; set; }
        private ContextMenu TrayMenu { get; set; }

        public bool LoggedOutBecauseIdle { get; set; }

        private bool _IsLoggedIn;

        public bool IsLoggedIn
        {
            get { return _IsLoggedIn; }
            set
            {
                _IsLoggedIn = value;
                if (value)
                {
                    this.lblCurrentStatus.Text = "Agent is Logged In";
                    this.TrayIcon.Icon = Resources.GreenPhone;
                    this.Icon = Resources.GreenPhone;
                }
                else
                {
                    this.lblCurrentStatus.Text = "Agent is Logged Out";
                    this.TrayIcon.Icon = Resources.RedPhone;
                    this.Icon = Resources.RedPhone;
                }
            }
        }

        public AgentMonitor()
        {
            InitializeComponent();

            // Check that log file exists
            this.CheckLogFilePath();

            // Settings button
            this.btnSettings.Click += btnSettings_Click;

            // Set up Idle monitoring
            this.SetupIdleMonitoring();

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

        private void SetupIdleMonitoring()
        {
            // Set up monitoring of idle time
            this.IdleTimer = new Timer();
            if (Settings.Default.IdleMinsBeforeLoggingOut != 0)
            {
                this.IdleTimer.Interval = (Settings.Default.IdleMinsBeforeLoggingOut * 60 * 1000) / 2; // IdleMintues * 60 seconds/min * 1000 millisec/sec
            }
            else
            {
                this.IdleTimer.Interval = 10000000;
            }
            this.IdleTimer.Tick += IdleTimer_Tick;

            // Start monitoring idle time
            if (Settings.Default.IdleMinsBeforeLoggingOut != 0)
            {
                this.IdleTimer.Start();
            }
            this.LoggedOutBecauseIdle = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.ShowSettingsForm();

            // Set up Idle monitoring
            this.SetupIdleMonitoring();
        }

        private void ShowSettingsForm()
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show(this);
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            if (Settings.Default.IdleMinsBeforeLoggingOut != 0)
            {
                if (GetLastInputTime() > Settings.Default.IdleMinsBeforeLoggingOut & this.IsLoggedIn)
                {
                    this.LogOut();
                    this.LoggedOutBecauseIdle = true;
                }
                else
                {
                    if (this.LoggedOutBecauseIdle & !this.IsLoggedIn & Settings.Default.LogInAfterIdle)
                    {
                        this.LogIn();
                        this.LoggedOutBecauseIdle = false;
                    }
                }
            }
        }

        private void LogIn()
        {
            Process.Start("LogInAgent.ahk");

            // Change status
            this.IsLoggedIn = true;
        }

        private void LogOut()
        {
            Process.Start("LogOutAgent.ahk");

            // Change status
            this.IsLoggedIn = false;
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
                    else
                    {
                        this.ChooseLogFilePath();
                    }
                }
                else
                {
                    this.ChooseLogFilePath();
                }
            }
        }

        private void ChooseLogFilePath()
        {
            MessageBox.Show("Can't find Cisco's log file. Please choose the path to the file.", "Log File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            bool fileReplaced = false;
            while (!fileReplaced)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Log File|*.log";
                ofd.SupportMultiDottedExtensions = true;
                ofd.Title = "Select Cisco Log File";
                DialogResult result = ofd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Settings.Default.AgentLogFilePath = ofd.FileName;
                    Settings.Default.Save();
                    fileReplaced = true;
                }
                else
                {
                    DialogResult errorResult = MessageBox.Show("Can't run Agent Helper without the location of the Cisco log file. Exit now?", "File Path Required", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (errorResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        fileReplaced = true;
                        Environment.Exit(0);
                    }
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
            this.IsLoggedIn = this.IsAgentLoggedIn(logContent);
        }

        private bool IsAgentLoggedIn(string logContent)
        {
            int loggedInIndex = logContent.LastIndexOf(Resources.LoggedInMessage);
            int loggedOutIndex = logContent.LastIndexOf(Resources.LoggedOutMessage);

            return (loggedInIndex > loggedOutIndex);
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        static int GetLastInputTime()
        {
            int idleTime = 0;
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            int envTicks = Environment.TickCount;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                int lastInputTick = Convert.ToInt32(lastInputInfo.dwTime);

                idleTime = envTicks - lastInputTick;
            }

            return ((idleTime > 0) ? (idleTime / 1000) : idleTime);
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }
    }
}
