using System;
using System.ComponentModel;
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
            this.IdleTimer.Interval = 3000;
            SetupIdleMonitoringTimer();
            this.IdleTimer.Tick += IdleTimer_Tick;

            // Start monitoring idle time
            if (Settings.Default.IdleMinsBeforeLoggingOut != 0)
            {
                this.IdleTimer.Start();
            }
            this.LoggedOutBecauseIdle = false;
        }

        /// <summary>
        /// Sets up the timer interval for monitoring Idle status.
        /// </summary>
        private void SetupIdleMonitoringTimer()
        {
            this.IdleTimer.Enabled = (Settings.Default.IdleMinsBeforeLoggingOut != 0);
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
                bool inactive = User32Interop.GetLastInput() > TimeSpan.FromMinutes(Settings.Default.IdleMinsBeforeLoggingOut);

                if (inactive &
                    Settings.Default.LogOutOnIdle &
                    this.IsLoggedIn)
                {
                    this.Records.Add(new AgentStatusChangeEventArgs(
                        AgentStatus.Undetermined,
                        AgentStatus.Undetermined,
                        DateTime.Now,
                        "Logging out because of user going into idle status."));

                    this.LogOut();
                    this.LoggedOutBecauseIdle = true;
                    return;
                }
                else
                {
                    if (!this.IsLoggedIn &
                        Settings.Default.LogInAfterIdle &
                        this.LoggedOutBecauseIdle)
                    {
                        this.Records.Add(new AgentStatusChangeEventArgs(
                            AgentStatus.Undetermined,
                            AgentStatus.Undetermined,
                            DateTime.Now,
                            "Logging back in because of user coming back from idle status."));

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

        public static class User32Interop
        {
            public static TimeSpan GetLastInput()
            {
                var plii = new LASTINPUTINFO();
                plii.cbSize = (uint)Marshal.SizeOf(plii);

                if (GetLastInputInfo(ref plii))
                    return TimeSpan.FromMilliseconds(Environment.TickCount - plii.dwTime);
                else
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            [DllImport("user32.dll", SetLastError = true)]
            static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

            struct LASTINPUTINFO
            {
                public uint cbSize;
                public uint dwTime;
            }
        }
    }
}