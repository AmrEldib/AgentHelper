using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            // This fixes a bug
            // When the application is updated, the script files are overwritten with new version.
            // The new version of the file has [PASSCODE] not the password even though the user hasn't changed the password.
            SettingsForm.UpdateAhkScriptsWithPassword(Settings.Default.CiscoPassword);

            // Set initial agent status
            this.Status = AgentStatus.Undetermined;

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

            // Set up monitoring of agent status every 1 second.
            this.StatusTimer = new Timer();
            this.StatusTimer.Interval = 1000;
            this.StatusTimer.Tick += StatusTimer_Tick;

            // Detect status for the first time
            this.DetectAgentStatus();

            // Hook up form's event handlers
            this.Resize += AgentMonitor_Resize;
            this.FormClosing += AgentMonitor_FormClosing;
            this.FormClosed += AgentMonitor_FormClosed;

            // Settings button
            this.btnSettings.Click += btnSettings_Click;

            // About button
            this.lnkAbout.Click += lnkAbout_Click;

            // Set up watch for screen lock
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            // Records
            this.Records = new List<AgentStatusChangeEventArgs>();
            this.AgentStatusChange += AgentMonitor_AgentStatusChange;
            this.lnkShowRecords.Click += lnkShowRecords_Click;

            // Change status if User indicates that in Settings
            if (Settings.Default.LogInOnStartup)
            {
                if (Settings.Default.LogInOnStartupTo == AgentStatus.NotReady)
                {
                    this.SwitchToNotReady();
                }
                else if (Settings.Default.LogInOnStartupTo == AgentStatus.Ready)
                {
                    this.SwitchToReady();
                }
            }

            // Start monitoring status
            this.StatusTimer.Start();
        }

        /// <summary>
        /// Handler for the lnkAbout's Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void lnkAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        /// <summary>
        /// Handler for the AgentMonitor's AgentStatusChange event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void AgentMonitor_AgentStatusChange(object sender, AgentStatusChangeEventArgs e)
        {
            this.Records.Add(e);
        }

        /// <summary>
        /// Handler for the lnkShowRecords's Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void lnkShowRecords_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                // Easter egg to keep things interesting.
                Process.Start("http://www.youtube.com/watch?v=oHg5SJYRHA0");
            }
            else
            {
                // Display records form.
                AgentRecords agentRecordsForm = new AgentRecords();
                agentRecordsForm.Records = this.Records;
                agentRecordsForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Agent's records.
        /// </summary>
        public List<AgentStatusChangeEventArgs> Records { get; set; }

        /// <summary>
        /// Handler for the form's FormClosing event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void AgentMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close agent if User indicates in Settings
            if (Settings.Default.LogOutAndCloseAgentOnShutDown)
            {
                this.CloseAgent();
            }
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
            settingsForm.FormClosed += settingsForm_FormClosed;
            settingsForm.Show(this);
        }

        /// <summary>
        /// Handler for the Settings Form's FormClosed event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void settingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.SetupIdleMonitoringTimer();
        }
        #endregion

    }
}
