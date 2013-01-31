using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AgentHelper.DummyCiscoAgent
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
            Application.Run(new SplashForm());
        }

        public static AgentForm agentForm { get; set; }

        public static WaitForm waitForm { get; set; }

        public static LoginDialog loginDialog { get; set; }
    }
}
