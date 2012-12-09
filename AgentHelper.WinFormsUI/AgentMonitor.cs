using System;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;
using Microsoft.Win32;

namespace AgentHelper.WinFormsUI
{
    /// <summary>
    /// Main form for the AgentHelper application.
    /// </summary>
    public partial class AgentMonitor : Form
    {
        #region Form Event Handlers
        /// <summary>
        /// Agent Monitor default constructor.
        /// </summary>
        public AgentMonitor()
        {
            // Initialize form's components.
            InitializeComponent();

            // Validate stored settings
            SettingsForm.ValidateStoredSettings();

            // Create a simple tray menu.
            this.TrayMenu = new ContextMenu();
            this.TrayMenu.MenuItems.Add("Ready", TrayMenu_Ready);
            this.TrayMenu.MenuItems.Add("Not Ready", TrayMenu_NotReady);
            this.TrayMenu.MenuItems.Add("Log out", TrayMenu_LogOut);
            this.TrayMenu.MenuItems.Add("Close Agent", TrayMenu_CloseAgent);
            this.TrayMenu.MenuItems.Add("-");
            this.TrayMenu.MenuItems.Add("Restore", TrayIcon_OnRestore);
            this.TrayMenu.MenuItems.Add("Exit", TrayIcon_OnExit);

            // Create tray icon.
            this.TrayIcon = new NotifyIcon();
            this.TrayIcon.Text = "Agent Helper";
            this.TrayIcon.ContextMenu = TrayMenu;
            this.TrayIcon.Visible = true;
            this.TrayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
            this.TrayIcon.DoubleClick += TrayIcon_DoubleClick;

            // Set up Idle monitoring
            this.SetupIdleMonitoring();

            // Set up monitoring of agent status every 10 seconds
            this.StatusTimer = new Timer();
            this.StatusTimer.Interval = 10000;
            this.StatusTimer.Tick += StatusTimer_Tick;

            // Detect status for the first time
            this.DetectAgentStatus();

            // Hook up form's event handlers
            this.Resize += AgentMonitor_Resize;
            this.FormClosed += AgentMonitor_FormClosed;

            // Settings button
            this.btnSettings.Click += btnSettings_Click;

            // Set up watch for screen lock
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            // Start monitoring status
            this.StatusTimer.Start();
        }

        /// <summary>
        /// Handler for the form's FormClosed event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void AgentMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TrayIcon.Visible = false;
            this.TrayIcon.Dispose();
        }

        /// <summary>
        /// Handler for the form's Resize event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
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
        #endregion

        #region Settings Form
        /// <summary>
        /// Handler for the 'Settings' button's Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.ShowSettingsForm();

            // Set up Idle monitoring
            this.SetupIdleMonitoring();
        }

        /// <summary>
        /// Shows Settings form.
        /// </summary>
        private void ShowSettingsForm()
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show(this);
        }
        #endregion

    }
}
