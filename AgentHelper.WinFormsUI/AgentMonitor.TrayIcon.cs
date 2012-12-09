using System;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        /// <summary>
        /// Tray icon of the application that shows in the System Notification Area.
        /// </summary>
        private NotifyIcon TrayIcon { get; set; }

        /// <summary>
        /// Menu of the tray icon.
        /// </summary>
        private ContextMenu TrayMenu { get; set; }

        /// <summary>
        /// Handler for the tray icon's Restore event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayIcon_OnRestore(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Handler for the tray icon's Exit event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayIcon_OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handler for the tray icon's BalloonTipClicked event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Settings.Default.ShowMinimizeNotification = false;
            Settings.Default.Save();
        }

        /// <summary>
        /// Handler for the tray icon's DoubleClick event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Handler for the tray menu's 'Close Agent' item.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayMenu_CloseAgent(object sender, EventArgs e)
        {
            this.CloseAgent();
        }

        /// <summary>
        /// Handler for the tray menu's 'Not Ready' item.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayMenu_NotReady(object sender, EventArgs e)
        {
            this.SwitchToNotReady();
        }

        /// <summary>
        /// Handler for the tray menu's 'Ready' item.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayMenu_Ready(object sender, EventArgs e)
        {
            this.SwitchToReady();
        }

        /// <summary>
        /// Handler for the tray menu's 'Log Out' item.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void TrayMenu_LogOut(object sender, EventArgs e)
        {
            this.LogOut();
        }

    }
}