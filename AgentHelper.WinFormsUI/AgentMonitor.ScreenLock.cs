using System;
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
                this.Records.Add(new AgentStatusChangeEventArgs(AgentStatus.Undetermined,
                    AgentStatus.Undetermined,
                    DateTime.Now,
                    "Screen Lock"));

                // On screen lock
                if (Settings.Default.ChangeStatusOnScreenLock)
                {
                    if (Settings.Default.ChangeStatusOnScreenLockTo == AgentStatus.NotReady)
                    {
                        this.Records.Add(new AgentStatusChangeEventArgs(
                            AgentStatus.Undetermined,
                            AgentStatus.Undetermined,
                            DateTime.Now,
                            "Switching to not ready because of screen lock."));
                        this.SwitchToNotReady();
                    }
                    else if (Settings.Default.ChangeStatusOnScreenLockTo ==
                        AgentStatus.LoggedOut)
                    {
                        this.Records.Add(new AgentStatusChangeEventArgs(
                            AgentStatus.Undetermined,
                            AgentStatus.Undetermined,
                            DateTime.Now,
                            "Logging out because of screen lock."));
                        this.LogOut();
                    }
                }
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                this.Records.Add(new AgentStatusChangeEventArgs(AgentStatus.Undetermined,
                    AgentStatus.Undetermined,
                    DateTime.Now,
                    "Screen Unlock"));

                // On screen unlock
                if (Settings.Default.ChangeStatusOnScreenUnlock)
                {
                    if (Settings.Default.ChangeStatusOnScreenUnlockTo == AgentStatus.NotReady)
                    {
                        this.Records.Add(new AgentStatusChangeEventArgs(
                            AgentStatus.Undetermined,
                            AgentStatus.Undetermined,
                            DateTime.Now,
                            "Switching to Not Ready because of screen unlock."));
                        this.SwitchToNotReady();
                    }
                    else if (Settings.Default.ChangeStatusOnScreenUnlockTo == AgentStatus.Ready)
                    {
                        this.Records.Add(new AgentStatusChangeEventArgs(
                            AgentStatus.Undetermined,
                            AgentStatus.Undetermined,
                            DateTime.Now,
                            "Switching to Ready because of screen unlock."));
                        this.SwitchToReady();
                    }
                }
            }
        }
        #endregion
    }
}