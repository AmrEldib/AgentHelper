using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;
using Microsoft.Win32;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        #region Detecting Screen Lock
        /// <summary>
        /// Handler for SessionSwitch event of the System Events.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                // On screen lock
                if (Settings.Default.ChangeStatusOnScreenLock)
                {
                    if (Settings.Default.ChangeStatusOnScreenLockTo == AgentStatus.NotReady)
                    {
                        this.SwitchToNotReady();
                    }
                    else if (Settings.Default.ChangeStatusOnScreenLockTo == AgentStatus.LoggedOut)
                    {
                        this.LogOut();
                    }
                }
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                // On screen unlock
                if (Settings.Default.ChangeStatusOnScreenUnlock)
                {
                    if (Settings.Default.ChangeStatusOnScreenUnlockTo == AgentStatus.NotReady)
                    {
                        this.SwitchToNotReady();
                    }
                    else if (Settings.Default.ChangeStatusOnScreenUnlockTo == AgentStatus.Ready)
                    {
                        this.SwitchToReady();
                    }
                }
            }
        }
        #endregion
    }
}