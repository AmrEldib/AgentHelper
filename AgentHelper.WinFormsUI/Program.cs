using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AgentHelper.WinFormsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Idle += Application_Idle;
            Application.ApplicationExit += Application_ApplicationExit;

            Application.Run(new AgentMonitor());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;
        }

        static void Application_Idle(object sender, EventArgs e)
        {

        }
    }
}
