namespace AgentHelper.WinFormsUI
{
    partial class AgentRecords
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentRecords));
            this.lsvRecords = new System.Windows.Forms.ListView();
            this.clmTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmStatusChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lsvRecords
            // 
            this.lsvRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvRecords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTime,
            this.clmStatusChange});
            this.lsvRecords.Location = new System.Drawing.Point(26, 21);
            this.lsvRecords.Name = "lsvRecords";
            this.lsvRecords.Size = new System.Drawing.Size(605, 357);
            this.lsvRecords.TabIndex = 0;
            this.lsvRecords.UseCompatibleStateImageBehavior = false;
            this.lsvRecords.View = System.Windows.Forms.View.Details;
            // 
            // clmTime
            // 
            this.clmTime.Text = "Time";
            this.clmTime.Width = 180;
            // 
            // clmStatusChange
            // 
            this.clmStatusChange.Text = "Status Change";
            this.clmStatusChange.Width = 370;
            // 
            // AgentRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 403);
            this.Controls.Add(this.lsvRecords);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AgentRecords";
            this.Text = "Agent Records";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvRecords;
        private System.Windows.Forms.ColumnHeader clmTime;
        private System.Windows.Forms.ColumnHeader clmStatusChange;
    }
}