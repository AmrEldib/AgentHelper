namespace AgentHelper.WinFormsUI
{
    partial class AgentMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lnkShowRecords = new System.Windows.Forms.LinkLabel();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(26, 39);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(133, 13);
            this.lblCurrentStatus.TabIndex = 1;
            this.lblCurrentStatus.Text = "Agent is <CurrentStatus>";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(233, 34);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // lnkShowRecords
            // 
            this.lnkShowRecords.AutoSize = true;
            this.lnkShowRecords.Location = new System.Drawing.Point(26, 71);
            this.lnkShowRecords.Name = "lnkShowRecords";
            this.lnkShowRecords.Size = new System.Drawing.Size(75, 13);
            this.lnkShowRecords.TabIndex = 3;
            this.lnkShowRecords.TabStop = true;
            this.lnkShowRecords.Text = "Show Records";
            // 
            // lnkAbout
            // 
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.Location = new System.Drawing.Point(272, 71);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(36, 13);
            this.lnkAbout.TabIndex = 4;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "About";
            // 
            // AgentMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 103);
            this.Controls.Add(this.lnkAbout);
            this.Controls.Add(this.lnkShowRecords);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblCurrentStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AgentMonitor";
            this.Text = "Agent Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.LinkLabel lnkShowRecords;
        private System.Windows.Forms.LinkLabel lnkAbout;
    }
}

