using System;
using System.Diagnostics;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;
using White.Core.Factory;
using White.Core.UIItems.Finders;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        #region Properties
        /// <summary>
        /// Timer for monitoring agent's status.
        /// </summary>
        private Timer StatusTimer { get; set; }

        /// <summary>
        /// Agent's current status.
        /// </summary>
        private AgentStatus _Status;

        /// <summary>
        /// Agent's current status.
        /// </summary>
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
                        this.lblCurrentStatus.Text = "Agent is Closed";
                        this.TrayIcon.Icon = Resources.GrayPhone;
                        this.Icon = Resources.GrayPhone;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        /// <summary>
        /// Gets the title of the main window of an application given its process's name.
        /// </summary>
        /// <param name="processName">Process name.</param>
        /// <returns>Title of application's main window.</returns>
        /// <exception cref="System.ArgumentException">No process can be found with this name.</exception>
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

        /// <summary>
        /// Checks if agent is logged in based on current status.
        /// </summary>
        /// <returns>True if agent is logged in, False if not.</returns>
        private bool IsLoggedIn()
        {
            return (this.Status == AgentStatus.NotReady | this.Status == AgentStatus.Ready);
        }

        /// <summary>
        /// Logs in agent to 'Not Ready' status.
        /// </summary>
        /// <remarks>
        /// This method uses an AutoHotKey script.
        /// Application is launched if it's not running.
        /// </remarks>
        private void LogIn()
        {
            Process.Start("LogInAgent.ahk");

            //// Attach to process or start process if it hasn't already started.
            //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
            //var application = White.Core.Application.AttachOrLaunch(psi);

            //var mainWindow = application.GetWindow(SearchCriteria.ByText("Calculator"), InitializeOption.NoCache);

            // Change status
            this.Status = AgentStatus.NotReady;
        }

        /// <summary>
        /// Switches agent to 'Not Ready' status.
        /// </summary>
        /// <remarks>
        /// This method uses an AutoHotKey script.
        /// Application is launched and agent is logged in if they're not.
        /// </remarks>
        private void SwitchToNotReady()
        {
            Process.Start("SwitchToNotReady.ahk");

            // Change status
            this.Status = AgentStatus.NotReady;
        }

        /// <summary>
        /// Switches agent to 'Ready' status.
        /// </summary>
        /// <remarks>
        /// This method uses an AutoHotKey script.
        /// Application is launched and agent is logged in if they're not.
        /// </remarks>
        private void SwitchToReady()
        {
            Process.Start("SwitchToReady.ahk");

            // Change status
            this.Status = AgentStatus.Ready;
        }

        /// <summary>
        /// Switches agent to 'Ready' status.
        /// </summary>
        /// <remarks>
        /// This method uses an AutoHotKey script.
        /// Application is launched and left at logged out if it's not running.
        /// </remarks>
        private void LogOut()
        {
            Process.Start("LogOutAgent.ahk");

            // Change status
            this.Status = AgentStatus.LoggedOut;
        }

        /// <summary>
        /// Switches agent to 'Ready' status.
        /// </summary>
        /// <remarks>
        /// This method uses an AutoHotKey script.
        /// If agent is logged in, then it's logged out first, then application is closed.
        /// </remarks>
        private void CloseAgent()
        {
            Process.Start("CloseAgent.ahk");

            // Change status
            this.Status = AgentStatus.Closed;
        }

        /// <summary>
        /// Handler for the StatusTimer's Tick event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            this.DetectAgentStatus();
        }

        /// <summary>
        /// Detects current status of agent.
        /// </summary>
        /// <returns>Current status of agent.</returns>
        private AgentStatus DetectAgentStatus()
        {
            try
            {
                // Get title of Cisco Agent window
                string titleOfAgentWindow = this.GetMainWindowTitle(Settings.Default.CiscoAgentExeFileLocation);

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
    }
}