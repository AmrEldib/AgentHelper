namespace AgentHelper.WinFormsUI
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.btnLogPathBrowse = new System.Windows.Forms.Button();
            this.chkLogOutWhenIdle = new System.Windows.Forms.CheckBox();
            this.numLogOutWhenIdle = new System.Windows.Forms.NumericUpDown();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.chkLogInAfterIdle = new System.Windows.Forms.CheckBox();
            this.lblCiscoPassword = new System.Windows.Forms.Label();
            this.txtCiscoPassword = new System.Windows.Forms.TextBox();
            this.lblCiscoPasswordNote = new System.Windows.Forms.Label();
            this.ofdLogFilePath = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numLogOutWhenIdle)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(419, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(338, 224);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path to Cisco Log File";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogPath.Location = new System.Drawing.Point(29, 47);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(431, 20);
            this.txtLogPath.TabIndex = 3;
            // 
            // btnLogPathBrowse
            // 
            this.btnLogPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogPathBrowse.Location = new System.Drawing.Point(466, 45);
            this.btnLogPathBrowse.Name = "btnLogPathBrowse";
            this.btnLogPathBrowse.Size = new System.Drawing.Size(28, 23);
            this.btnLogPathBrowse.TabIndex = 4;
            this.btnLogPathBrowse.Text = "...";
            this.btnLogPathBrowse.UseVisualStyleBackColor = true;
            // 
            // chkLogOutWhenIdle
            // 
            this.chkLogOutWhenIdle.AutoSize = true;
            this.chkLogOutWhenIdle.Location = new System.Drawing.Point(29, 82);
            this.chkLogOutWhenIdle.Name = "chkLogOutWhenIdle";
            this.chkLogOutWhenIdle.Size = new System.Drawing.Size(209, 17);
            this.chkLogOutWhenIdle.TabIndex = 5;
            this.chkLogOutWhenIdle.Text = "Log out Agent when System is Idle for";
            this.chkLogOutWhenIdle.UseVisualStyleBackColor = true;
            // 
            // numLogOutWhenIdle
            // 
            this.numLogOutWhenIdle.Location = new System.Drawing.Point(244, 81);
            this.numLogOutWhenIdle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLogOutWhenIdle.Name = "numLogOutWhenIdle";
            this.numLogOutWhenIdle.Size = new System.Drawing.Size(65, 20);
            this.numLogOutWhenIdle.TabIndex = 6;
            this.numLogOutWhenIdle.ThousandsSeparator = true;
            this.numLogOutWhenIdle.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(324, 83);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(48, 13);
            this.lblMinutes.TabIndex = 7;
            this.lblMinutes.Text = "minutes.";
            // 
            // chkLogInAfterIdle
            // 
            this.chkLogInAfterIdle.AutoSize = true;
            this.chkLogInAfterIdle.Location = new System.Drawing.Point(29, 115);
            this.chkLogInAfterIdle.Name = "chkLogInAfterIdle";
            this.chkLogInAfterIdle.Size = new System.Drawing.Size(217, 17);
            this.chkLogInAfterIdle.TabIndex = 8;
            this.chkLogInAfterIdle.Text = "Log back in after coming back from Idle.";
            this.chkLogInAfterIdle.UseVisualStyleBackColor = true;
            // 
            // lblCiscoPassword
            // 
            this.lblCiscoPassword.AutoSize = true;
            this.lblCiscoPassword.Location = new System.Drawing.Point(26, 147);
            this.lblCiscoPassword.Name = "lblCiscoPassword";
            this.lblCiscoPassword.Size = new System.Drawing.Size(126, 13);
            this.lblCiscoPassword.TabIndex = 9;
            this.lblCiscoPassword.Text = "Password to Cisco Agent";
            // 
            // txtCiscoPassword
            // 
            this.txtCiscoPassword.Location = new System.Drawing.Point(158, 144);
            this.txtCiscoPassword.Name = "txtCiscoPassword";
            this.txtCiscoPassword.Size = new System.Drawing.Size(336, 20);
            this.txtCiscoPassword.TabIndex = 10;
            // 
            // lblCiscoPasswordNote
            // 
            this.lblCiscoPasswordNote.AutoSize = true;
            this.lblCiscoPasswordNote.Location = new System.Drawing.Point(35, 182);
            this.lblCiscoPasswordNote.Name = "lblCiscoPasswordNote";
            this.lblCiscoPasswordNote.Size = new System.Drawing.Size(337, 26);
            this.lblCiscoPasswordNote.TabIndex = 11;
            this.lblCiscoPasswordNote.Text = "Note: this password will be stored as plain text.\r\nYou have to have AutoHotKey in" +
    "stalled for loggin out and in to work.";
            // 
            // ofdLogFilePath
            // 
            this.ofdLogFilePath.Filter = "Log File|*.log";
            this.ofdLogFilePath.RestoreDirectory = true;
            this.ofdLogFilePath.SupportMultiDottedExtensions = true;
            this.ofdLogFilePath.Title = "Select Cisco Log File";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 259);
            this.Controls.Add(this.lblCiscoPasswordNote);
            this.Controls.Add(this.txtCiscoPassword);
            this.Controls.Add(this.lblCiscoPassword);
            this.Controls.Add(this.chkLogInAfterIdle);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.numLogOutWhenIdle);
            this.Controls.Add(this.chkLogOutWhenIdle);
            this.Controls.Add(this.btnLogPathBrowse);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numLogOutWhenIdle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Button btnLogPathBrowse;
        private System.Windows.Forms.CheckBox chkLogOutWhenIdle;
        private System.Windows.Forms.NumericUpDown numLogOutWhenIdle;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.CheckBox chkLogInAfterIdle;
        private System.Windows.Forms.Label lblCiscoPassword;
        private System.Windows.Forms.TextBox txtCiscoPassword;
        private System.Windows.Forms.Label lblCiscoPasswordNote;
        private System.Windows.Forms.OpenFileDialog ofdLogFilePath;
    }
}