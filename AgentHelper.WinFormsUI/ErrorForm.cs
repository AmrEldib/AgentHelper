using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace AgentHelper.WinFormsUI
{
    public partial class ErrorForm : Form
    {
        /// <summary>
        /// Initialize form.
        /// </summary>
        private void Initialize()
        {
            InitializeComponent();

            this.lblErrorTitle.Text = "See, this is what happens when you code while watching funny videos on YouTube. My bad. However, if you e-mail me the error details, maybe I can fix it. You'll get a chance to read what exactly you're sending me and maybe add a comment and links to other funny videos. Thank you!";

            this.btnCancel.Click += btnCancel_Click;
            this.btnSend.Click += btnSend_Click;
        }

        /// <summary>
        /// Error form.
        /// </summary>
        public ErrorForm()
        {
            this.Initialize();
        }

        /// <summary>
        /// Error form.
        /// </summary>
        /// <param name="errorDetails">Details of error.</param>
        public ErrorForm(Exception errorDetails)
        {
            this.Initialize();
            this.ErrorDetails = errorDetails;
        }

        /// <summary>
        /// Error details.
        /// </summary>
        private Exception _ErrorDetails;

        /// <summary>
        /// Error details.
        /// </summary>
        public Exception ErrorDetails
        {
            get { return _ErrorDetails; }
            set { _ErrorDetails = value; }
        }

        /// <summary>
        /// Prepare the content of the error email that will be sent the developer.
        /// </summary>
        /// <returns>The content of the error email.</returns>
        private string PrepareErrorEmail()
        {
            StringBuilder errorEmail = new StringBuilder();

            // User comment
            errorEmail.AppendLine("Agent helper broke down when I was: ");
            errorEmail.AppendLine();

            // Environment details
            var osName = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                          select x.GetPropertyValue("Caption")).First();
            errorEmail.Append("I'm using Agent Handler version ");
            errorEmail.Append(Application.ProductVersion);
            errorEmail.AppendLine();
            errorEmail.Append("On Operating System: ");
            errorEmail.Append((osName != null) ? osName.ToString() : "Unknown");
            errorEmail.AppendLine();

            if (this.ErrorDetails != null)
            {
                errorEmail.Append("Exception Message: ");
                errorEmail.Append(this.ErrorDetails.Message);
                errorEmail.AppendLine();
                errorEmail.Append("Source: ");
                errorEmail.Append(this.ErrorDetails.Source);
                errorEmail.AppendLine();
                errorEmail.Append("Stacktrace: ");
                errorEmail.Append(this.ErrorDetails.StackTrace);
                errorEmail.AppendLine();
            }
            else
            {
                errorEmail.AppendLine("No Exception details was provided.");
            }

            return errorEmail.ToString();
        }

        /// <summary>
        /// Handler for btnSend Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string errorEmail = this.PrepareErrorEmail();
            Process.Start(Uri.EscapeUriString("mailto:aeldib@esri.ca?subject=Error Occurred with Agent Helper&body=" + errorEmail));
        }

        /// <summary>
        /// Handler for btnCancel Click event.
        /// </summary>
        /// <param name="sender">Object raising event.</param>
        /// <param name="e">Event Arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
