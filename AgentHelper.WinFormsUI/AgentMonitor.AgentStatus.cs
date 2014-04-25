using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;
using System.Threading;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        #region Properties
        /// <summary>
        /// Timer for monitoring agent's status.
        /// </summary>
        private System.Windows.Forms.Timer StatusTimer { get; set; }

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

        /// <summary>
        /// Checks if agent is logged in based on current status.
        /// </summary>
        /// <returns>True if agent is logged in, False if not.</returns>
        private bool IsLoggedIn
        {
            get { return (this.Status == AgentStatus.NotReady | this.Status == AgentStatus.Ready); }
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
        /// Logs in agent to 'Not Ready' status.
        /// </summary>
        /// <remarks>
        /// This method uses an AutoHotKey script.
        /// Application is launched if it's not running.
        /// </remarks>
        private void LogIn()
        {
            // If already logged in, just switch to 'Not Ready'.
            if (this.IsLoggedIn)
            {
                this.SwitchToNotReady();
            }
            else if (this.Status == AgentStatus.Closed)
            {
                // Run agent if it's not already running.
                this.RunAgent();

                //// Hook up event handler for switching status from 'Closed' to 'LoggedOut'.
                //// This event handler will find the Login Dialog and type in password to log in.
                //this.AgentLoggedOut += AgentStartupEventHandler;
            }
            else if (this.Status == AgentStatus.LoggedOut)
            {
                string title = this.GetMainWindowTitle(Path.GetFileNameWithoutExtension(Settings.Default.CiscoAgentExeFileLocation));

                if (title == Resources.CiscoWindowTitleLoginDialog)
                {
                    // Type in password to log in
                    this.TypeInPasswordToLogIn(Settings.Default.CiscoPassword);
                }
                else if (title == Resources.CiscoWindowTitleLoggedOut)
                {
                    // TODO: Check if script is deployed. if not, deploy it.

                    // Bring up login dialog
                    Process.Start(Resources.AhkFileNameLoggedOutToLoggedIn);

                    #region UI Automation with White Library
                    //// Activate the agent's window
                    //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
                    //var application = White.Core.Application.AttachOrLaunch(psi);
                    //var mainWindow = application.GetWindow(SearchCriteria.ByText(Resources.CiscoWindowTitleLoggedOut), InitializeOption.NoCache);

                    //// Type in keyboard shortcut to bring up login dialog (Ctrl+L)
                    //Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
                    //Keyboard.Instance.Enter("L");
                    //Keyboard.Instance.LeaveKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL); 
                    #endregion
                }
            }
        }

        /// <summary>
        /// Handler for agent's startup event. This occurs after this application launches the Cisco agent and waits until the login dialog shows up.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event arguments.</param>
        private void AgentStartupEventHandler(object sender, AgentStatusChangeEventArgs e)
        {
            // This event handler will find the Login Dialog and type in password to log in.
            // Type in password to log in
            this.TypeInPasswordToLogIn(Settings.Default.CiscoPassword);

            // Unhook the event handler 
            this.AgentLoggedOut -= AgentStartupEventHandler;
        }

        /// <summary>
        /// Types in Cisco's agent password in the login dialog.s
        /// </summary>
        /// <param name="password">Cisco's agent password.</param>
        private void TypeInPasswordToLogIn(string password)
        {
            // TODO: Check if script is deployed. if not, deploy it.

            // Type in password to login
            Process.Start(Resources.AhkFileNameTypeInPasswordToLogIn);

            #region UI Automation with White Library
            //// Activate the agent's window
            //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
            //var application = White.Core.Application.AttachOrLaunch(psi);
            //var mainWindow = application.GetWindow(SearchCriteria.ByText(Resources.CiscoWindowTitleLoginDialog), InitializeOption.NoCache);

            //// Find the password text box
            //var passwordTextbox = mainWindow.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("4003"));

            //// Type in password
            //passwordTextbox.Enter(password);

            //// Press Enter
            //White.Core.UIItems.Button loginButton = mainWindow.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Ok"));
            //loginButton.Click(); 
            #endregion
        }

        /// <summary>
        /// Starts Cisco agent application.
        /// </summary>
        private void RunAgent()
        {
            // TODO: Check if script is deployed. if not, deploy it.

            // Start script to run agent and type in password.
            Process.Start(Resources.AhkFileNameRunAgent);
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
            if (this.Status == AgentStatus.NotReady)
            {
                // Do Nothing
            }
            else if (this.Status == AgentStatus.Ready)
            {
                // TODO: Check if script is deployed. if not, deploy it.

                // Switch from Not Ready to Ready
                Process.Start(Resources.AhkFileNameReadyToNotReady);

                #region UI Automation with White Library
                //// Activate Cisco's agent window
                //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
                //var application = White.Core.Application.Attach(Path.GetFileNameWithoutExtension(psi.FileName));
                //var mainWindow = application.GetWindow(Resources.CiscoWindowTitleReady);

                //// Type in keyboard shortcut to switch to Not Ready (Ctrl+O)
                //Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
                //Keyboard.Instance.Enter("O");
                //Keyboard.Instance.LeaveKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL); 
                #endregion
            }
            else if (this.Status == AgentStatus.LoggedOut)
            {
                // Just log in (it will switch to 'Not Ready').
                this.LogIn();
            }
            else if (this.Status == AgentStatus.Closed)
            {
                // If agent is closed, just log in (will launch agent, then log in to 'Not Ready' status)
                this.LogIn();
            }
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
            if (this.Status == AgentStatus.Ready)
            {
                // Do Nothing
            }
            else if (this.Status == AgentStatus.NotReady)
            {
                // TODO: Check if script is deployed. if not, deploy it.

                // Switch from Ready to Not Ready
                Process.Start(Resources.AhkFileNameNotReadyToReady);

                #region UI Automation using White Library
                //// Activate Cisco's agent window
                //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
                //var application = White.Core.Application.AttachOrLaunch(psi);
                //var mainWindow = application.GetWindow(SearchCriteria.ByText(Resources.CiscoWindowTitleNotReady), InitializeOption.NoCache);

                //// Type in keyboard shortcut to switch to Ready (Ctrl+W)
                //Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
                //Keyboard.Instance.Enter("W");
                //Keyboard.Instance.LeaveKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL); 
                #endregion
            }
            else if (this.Status == AgentStatus.LoggedOut)
            {
                // Set up an event for when the status turns to Not Ready
                // This event handler will switch from Not Ready to Ready
                this.AgentNotReady += SwitchFromNotReadyToReadyAfterLoggingIn;

                // Just log in (it will switch to 'Not Ready').
                this.LogIn();
            }
            else if (this.Status == AgentStatus.Closed)
            {
                // Set up an event for when the status turns to Not Ready
                // This event handler will switch from Not Ready to Ready
                this.AgentNotReady += SwitchFromNotReadyToReadyAfterLoggingIn;

                // If agent is closed, just log in (will launch agent, then log in to 'Not Ready' status)
                this.LogIn();
            }
        }

        private void SwitchFromNotReadyToReadyAfterLoggingIn(object sender, AgentStatusChangeEventArgs e)
        {
            // Wait for 2 seconds
            Thread.Sleep(2000);
            // Switch to Ready
            this.SwitchToReady();
            // Unhook handler
            this.AgentNotReady -= SwitchFromNotReadyToReadyAfterLoggingIn;
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
            if (this.IsLoggedIn)
            {
                // TODO: Check if script is deployed. if not, deploy it.

                // Switch from Logged In to Logged Out
                Process.Start(Resources.AhkFileNameLoggedInToLoggedOut);

                #region UI Automation with White Library
                //// Activate Cisco's agent window
                //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
                //var application = White.Core.Application.AttachOrLaunch(psi);
                //var mainWindow = application.GetWindow(SearchCriteria.ByText(Resources.CiscoWindowTitleNotReady), InitializeOption.NoCache);

                //// Type in keyboard shortcut to log out (Ctrl+L)
                //Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
                //Keyboard.Instance.Enter("L");
                //Keyboard.Instance.LeaveKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL); 
                #endregion
            }
            else if (this.Status == AgentStatus.LoggedOut)
            {
                // Do Nothing
            }
            else if (this.Status == AgentStatus.Closed)
            {
                // Do Nothing
            }
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
            // Proper closing is to log out first, then close agent.

            // In all cases, agent need to log out first.

            if (this.IsLoggedIn)
            {
                // If agent is logged in, logging out can take sometime
                // So, we'll hook up an event to detect when logout is complete
                // then the event handler will close the agent 
                this.AgentLoggedOut += AgentLoggedOutBeforeClosing;

                // Then, Log out
                this.LogOut();
            }
            else
            {
                // Close agent
                this.CloseAgentWithKeyboardShortcut();
            }
        }

        /// <summary>
        /// Handler for Logging Out event before closing the agent window.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void AgentLoggedOutBeforeClosing(object sender, AgentStatusChangeEventArgs e)
        {
            // Close agent
            this.CloseAgentWithKeyboardShortcut();

            // Unhook event handler
            this.AgentLoggedOut -= AgentLoggedOutBeforeClosing;
        }

        /// <summary>
        /// Closes agent by activating agent's window and pressing Ctrl+F4
        /// </summary>
        private void CloseAgentWithKeyboardShortcut()
        {
            // TODO: Check if script is deployed. if not, deploy it.

            // Close Agent
            Process.Start(Resources.AhkFileNameCloseAgent);

            #region UI Automation with White Library
            //// Activate Cisco's agent window
            //var psi = new ProcessStartInfo(Settings.Default.CiscoAgentExeFileLocation);
            //var application = White.Core.Application.AttachOrLaunch(psi);
            //var mainWindow = application.GetWindow(Resources.CiscoWindowTitleLoggedOut);

            //// Type in keyboard shortcut to log out (Alt+F4)
            //Keyboard.Instance.HoldKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.ALT);
            //Keyboard.Instance.PressSpecialKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.F4);
            //Keyboard.Instance.LeaveKey(White.Core.WindowsAPI.KeyboardInput.SpecialKeys.ALT); 
            #endregion
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
                string titleOfAgentWindow = this.GetMainWindowTitle(Path.GetFileNameWithoutExtension(Settings.Default.CiscoAgentExeFileLocation));

                if (titleOfAgentWindow == Resources.CiscoWindowTitleLoggedOut | titleOfAgentWindow == Resources.CiscoWindowTitleLoginDialog)
                {
                    if (this.Status != AgentStatus.LoggedOut)
                    {
                        if (this.AgentLoggedOut != null)
                            AgentLoggedOut(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.LoggedOut, DateTime.Now));
                        if (this.AgentStatusChange != null)
                            AgentStatusChange(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.LoggedOut, DateTime.Now));
                        this.Status = AgentStatus.LoggedOut;
                    }
                }
                else if (titleOfAgentWindow == Resources.CiscoWindowTitleNotReady)
                {
                    if (this.Status != AgentStatus.NotReady)
                    {
                        if (this.AgentNotReady != null)
                            AgentNotReady(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.NotReady, DateTime.Now));
                        if (this.AgentStatusChange != null)
                            AgentStatusChange(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.NotReady, DateTime.Now));
                        this.Status = AgentStatus.NotReady;
                    }
                }
                else if (titleOfAgentWindow == Resources.CiscoWindowTitleReady)
                {
                    if (this.Status != AgentStatus.Ready)
                    {
                        if (this.AgentReady != null)
                            AgentReady(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.Ready, DateTime.Now));
                        if (this.AgentStatusChange != null)
                            AgentStatusChange(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.Ready, DateTime.Now));
                        this.Status = AgentStatus.Ready;
                    }
                }
            }
            catch (ArgumentException)
            {
                if (this.Status != AgentStatus.Closed)
                {
                    if (this.AgentClosed != null)
                        AgentClosed(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.Closed, DateTime.Now));
                    if (this.AgentStatusChange != null)
                        AgentStatusChange(this, new AgentStatusChangeEventArgs(this.Status, AgentStatus.Closed, DateTime.Now));
                    this.Status = AgentStatus.Closed;
                }
            }

            return this.Status;
        }

        #region Events
        /// <summary>
        /// Event for Agent status turned to Logged In.
        /// </summary>
        public event AgentLoggedInHandler AgentLoggedIn;

        /// <summary>
        /// Event for Agent status turned to Logged Out.
        /// </summary>
        public event AgentLoggedOutHandler AgentLoggedOut;

        /// <summary>
        /// Event for Agent status turned to Not Ready.
        /// </summary>
        public event AgentNotReadyHandler AgentNotReady;

        /// <summary>
        /// Event for Agent status turned to Ready.
        /// </summary>
        public event AgentReadyHandler AgentReady;

        /// <summary>
        /// Event for Agent status turned to Closed.
        /// </summary>
        public event AgentClosedHandler AgentClosed;

        /// <summary>
        /// Event for changing of Agent status.
        /// </summary>
        public event AgentStatusChangeHandler AgentStatusChange;
        #endregion
    }

    /// <summary>
    /// Delegate of event for agent status turned to Logged In.
    /// </summary>
    /// <param name="sender">Object raising event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void AgentLoggedInHandler(object sender, AgentStatusChangeEventArgs e);

    /// <summary>
    /// Delegate of event for agent status turned to Logged Out.
    /// </summary>
    /// <param name="sender">Object raising event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void AgentLoggedOutHandler(object sender, AgentStatusChangeEventArgs e);

    /// <summary>
    /// Delegate of event for agent status turned to Ready.
    /// </summary>
    /// <param name="sender">Object raising event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void AgentReadyHandler(object sender, AgentStatusChangeEventArgs e);

    /// <summary>
    /// Delegate of event for agent status turned to Not Ready.
    /// </summary>
    /// <param name="sender">Object raising event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void AgentNotReadyHandler(object sender, AgentStatusChangeEventArgs e);

    /// <summary>
    /// Delegate of event for agent status turned to Closed.
    /// </summary>
    /// <param name="sender">Object raising event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void AgentClosedHandler(object sender, AgentStatusChangeEventArgs e);

    /// <summary>
    /// Delegate of event for changing of Agent status.
    /// </summary>
    /// <param name="sender">Object raising event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void AgentStatusChangeHandler(object sender, AgentStatusChangeEventArgs e);
}