using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgentHelper.WinFormsUI
{
    public partial class AgentRecords : Form
    {
        /// <summary>
        /// List of agent status changes.
        /// </summary>
        private List<AgentStatusChangeEventArgs> _Records;

        /// <summary>
        /// List of agent status changes.
        /// </summary>
        public List<AgentStatusChangeEventArgs> Records
        {
            get { return _Records; }
            set
            {
                _Records = value;
                this.DisplayRecords(value);
            }
        }

        /// <summary>
        /// Formats DateTime into a certain string.
        /// </summary>
        /// <param name="recordTime">DateTime to be formatted.</param>
        /// <returns>Formatted string.</returns>
        private string FormatRecordTime(DateTime recordTime)
        {
            string time = string.Empty;

            // Date
            if (recordTime.Date == DateTime.Today)
            {
                time += "Today, ";
            }
            else if (DateTime.Today == recordTime.Date.AddDays(1))
            {
                time += "Yesterday, ";
            }
            else if (DateTime.Today.Year == recordTime.Date.Year)
            {
                time += string.Format("{0:m}", recordTime);
            }
            else
            {
                time += string.Format("{0:MMM d, yyyy}", recordTime);
            }

            // Time
            time += " " + recordTime.ToLongTimeString();

            return time;
        }

        /// <summary>
        /// Takes a list of agent status changes and display it on form.
        /// </summary>
        /// <param name="records">List of agent status changes.</param>
        private void DisplayRecords(List<AgentStatusChangeEventArgs> records)
        {
            this.lsvRecords.Items.Clear();

            foreach (var record in records)
            {
                ListViewItem item = new ListViewItem(this.FormatRecordTime(record.Time));

                switch (record.To)
                {
                    case AgentStatus.Undetermined:
                        // Write detailed message
                        item.SubItems.Add(record.Message);
                        break;
                    case AgentStatus.Ready:
                        // Agent is ready.
                        item.SubItems.Add("Agent is ready. " + record.Message);
                        break;
                    case AgentStatus.NotReady:
                        // Agent is not ready.
                        item.SubItems.Add("Agent is not ready. " + record.Message);
                        break;
                    case AgentStatus.LoggedOut:
                        // Agent logged out.
                        item.SubItems.Add("Agent logged out. " + record.Message);
                        break;
                    case AgentStatus.Closed:
                        // Agent is closed.
                        item.SubItems.Add("Agent is closed. " + record.Message);
                        break;
                    default:
                        break;
                }

                this.lsvRecords.Items.Add(item);
            }
        }

        /// <summary>
        /// Form that displays records of agent's activity during the current session.
        /// </summary>
        public AgentRecords()
        {
            InitializeComponent();
        }
    }
}
