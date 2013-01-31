using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentHelper.WinFormsUI
{
    public class AgentStatusChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Status from which agent is changing.
        /// </summary>
        public AgentStatus From { get; set; }

        /// <summary>
        /// Status to which agent is changing.
        /// </summary>
        public AgentStatus To { get; set; }

        /// <summary>
        /// Time stamp of status change.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Detailed Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Event arguments for changing Agent's Status.
        /// </summary>
        /// <param name="from">Status from which agent is changing.</param>
        /// <param name="to">Status to which agent is changing.</param>
        /// <param name="time">Time stamp of status change.</param>
        /// <param name="message">Detailed message.</param>
        public AgentStatusChangeEventArgs(AgentStatus from, AgentStatus to, DateTime time, string message = "")
        {
            this.From = from;
            this.To = to;
            this.Time = time;
            this.Message = message;
        }

        /// <summary>
        /// Event arguments for changing Agent's Status.
        /// </summary>
        public AgentStatusChangeEventArgs()
        {
            // Do Nothing.
        }
    }
}
