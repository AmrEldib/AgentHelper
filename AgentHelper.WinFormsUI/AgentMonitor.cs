using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;
using System.Linq;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        private Timer StatusTimer { get; set; }

        private Timer IdleTimer { get; set; }

        private NotifyIcon TrayIcon { get; set; }
        private ContextMenu TrayMenu { get; set; }

        public bool LoggedOutBecauseIdle { get; set; }

        private AgentStatus _Status;

        public AgentStatus Status
        {
            get { return _Status; }
            set
            {
                _Status = value;

                switch (value)
                {
                    case AgentStatus.Ready:
                        this.lblCurrentStatus.Text = "Agent is Ready";
                        this.TrayIcon.Icon = Resources.GreenPhone;
                        this.Icon = Resources.GreenPhone;
                        break;
                    case AgentStatus.NotReady:
                        this.lblCurrentStatus.Text = "Agent is Not Ready";
                        this.TrayIcon.Icon = Resources.OrangePhone;
                        this.Icon = Resources.OrangePhone;
                        break;
                    case AgentStatus.LoggedOut:
                        this.lblCurrentStatus.Text = "Agent is Logged Out";
                        this.TrayIcon.Icon = Resources.RedPhone;
                        this.Icon = Resources.RedPhone;
                        break;
                    case AgentStatus.Closed:
                        // TODO: add gray icon for closed agent.
                        break;
                    default:
                        break;
                }
            }
        }

        public AgentMonitor()
        {
            InitializeComponent();

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

        private string GetMainWindowTitle(string processName)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName.ToLower() == processName.ToLower())
                {
                    return process.MainWindowTitle;
                }
            }

            // If no process is found with this name, throw an exception
            throw new ArgumentException("No process can be found with this name", "processName");
        }

        private void SetupIdleMonitoring()
        {
            // Set up monitoring of idle time
            this.IdleTimer = new Timer();
            if (Settings.Default.IdleMinsBeforeLoggingOut != 0)
            {
                // IdleMintues * 60 seconds/min * 1000 millisecond/sec
                this.IdleTimer.Interval = (Settings.Default.IdleMinsBeforeLoggingOut * 60 * 1000) / 2;
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

        private bool IsLoggedIn()
        {
            return (this.Status == AgentStatus.NotReady | this.Status == AgentStatus.Ready);
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            if (Settings.Default.IdleMinsBeforeLoggingOut != 0)
            {
                if (GetLastInputTime() > Settings.Default.IdleMinsBeforeLoggingOut & this.IsLoggedIn())
                {
                    this.LogOut();
                    this.LoggedOutBecauseIdle = true;
                }
                else
                {
                    if (this.LoggedOutBecauseIdle & !this.IsLoggedIn() & Settings.Default.LogInAfterIdle)
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
            this.Status = AgentStatus.NotReady;
        }

        private void SwitchToNotReady()
        {
            Process.Start("SwitchToNotReady.ahk");

            // Change status
            this.Status = AgentStatus.NotReady;
        }

        private void SwitchToReady()
        {
            Process.Start("SwitchToReady.ahk");

            // Change status
            this.Status = AgentStatus.Ready;
        }

        private void LogOut()
        {
            Process.Start("LogOutAgent.ahk");

            // Change status
            this.Status = AgentStatus.LoggedOut;
        }

        private void CloseAgent()
        {
            Process.Start("CloseAgent.ahk");

            // Change status
            this.Status = AgentStatus.Closed;
        }

        private void AgentMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TrayIcon.Visible = false;
            this.TrayIcon.Dispose();
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

        private AgentStatus DetectAgentStatus()
        {
            try
            {
                // Get title of Cisco Agent window
                string titleOfAgentWindow = this.GetMainWindowTitle(Settings.Default.CiscoAgentProcessName);

                if (titleOfAgentWindow == Resources.CiscoWindowTitleLoggedOut)
                {
                    this.Status = AgentStatus.LoggedOut;
                }
                else if (titleOfAgentWindow == Resources.CiscoWindowTitleNotReady)
                {
                    this.Status = AgentStatus.NotReady;
                }
                else if (titleOfAgentWindow == Resources.CiscoWindowTitleReady)
                {
                    this.Status = AgentStatus.Ready;
                }
            }
            catch (ArgumentException)
            {
                this.Status = AgentStatus.Closed;
            }

            return this.Status;
        }

        #region Interop Methods for Detecting Idle Time
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
        #endregion
    }
}
