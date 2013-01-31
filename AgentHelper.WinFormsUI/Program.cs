using System;
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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new AgentMonitor());
            }
            catch (Exception e)
            {
                ErrorForm errorForm = new ErrorForm(e);
                errorForm.Show();
            }
        }

    }
}
