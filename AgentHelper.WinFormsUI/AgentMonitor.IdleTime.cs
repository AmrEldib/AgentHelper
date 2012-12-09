using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AgentHelper.WinFormsUI.Properties;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentMonitor : Form
    {
        /// <summary>
        /// Timer for checking system's idle status.
        /// </summary>
        private Timer IdleTimer { get; set; }

        /// <summary>
        /// Flag for checking if agent was logged out because application switched to idle status, and not because the user logged out.
        /// </summary>
        public bool LoggedOutBecauseIdle { get; set; }

        /// <summary>
        /// Sets up monitoring for system's idle status.
        /// </summary>
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

        /// <summary>
        /// Handler for the IdleTimer's Tick event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
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

        #region Interop Methods for Detecting Idle Time
        /// <summary>
        /// External method for getting last time the user made an input.
        /// </summary>
        /// <param name="plii">Last input info.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        /// <summary>
        /// Gets the last time the user made an input (mouse move or keyboard input).
        /// </summary>
        /// <returns>Last time an input was made.</returns>
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

        /// <summary>
        /// Structure for last input info.
        /// </summary>
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