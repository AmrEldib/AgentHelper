using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace AgentHelper.WinFormsUI
{
    public partial class AboutForm : Form
    {
        /// <summary>
        /// About Form.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            this.btnClose.Click += btnClose_Click;
            this.lnkAskQuestion.Click += lnkAskQuestion_Click;
            this.lnkReportProblem.Click += lnkReportProblem_Click;

            // Display version
            this.lblVersion.Text = "Version " + Application.ProductVersion;
        }

        /// <summary>
        /// Handler for lnkReportProblem Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void lnkReportProblem_Click(object sender, EventArgs e)
        {
            StringBuilder envDescription = new StringBuilder();

            var osName = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                          select x.GetPropertyValue("Caption")).First();
            envDescription.Append("I'm using Agent Helper version ");
            envDescription.Append(Application.ProductVersion);
            envDescription.AppendLine();
            envDescription.Append("On Operating System: ");
            envDescription.Append((osName != null) ? osName.ToString() : "Unknown");

            Process.Start(Uri.EscapeUriString("mailto:aeldib@esri.ca?subject=Problem with Agent Helper&body=" + envDescription.ToString()));
        }

        /// <summary>
        /// Handler for lnkAskQuestion Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void lnkAskQuestion_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:aeldib@esri.ca?subject=Question about Agent Helper");
        }

        /// <summary>
        /// Handler for btnClose Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
