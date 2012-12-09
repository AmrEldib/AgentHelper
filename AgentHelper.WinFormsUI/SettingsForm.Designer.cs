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
            this.txtCiscoExeLocation = new System.Windows.Forms.TextBox();
            this.btnCiscoExeLocation = new System.Windows.Forms.Button();
            this.chkLogOutWhenIdle = new System.Windows.Forms.CheckBox();
            this.numLogOutWhenIdle = new System.Windows.Forms.NumericUpDown();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.chkLogInAfterIdle = new System.Windows.Forms.CheckBox();
            this.lblCiscoPassword = new System.Windows.Forms.Label();
            this.txtCiscoPassword = new System.Windows.Forms.TextBox();
            this.lblCiscoPasswordNote = new System.Windows.Forms.Label();
            this.ofdCiscoExeLocation = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.label3 = new System.Windows.Forms.Label();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.rdbStartupReady = new System.Windows.Forms.RadioButton();
            this.rdbStartupNotReady = new System.Windows.Forms.RadioButton();
            this.chkShutdown = new System.Windows.Forms.CheckBox();
            this.chkScreenLock = new System.Windows.Forms.CheckBox();
            this.rdbIdleReady = new System.Windows.Forms.RadioButton();
            this.rdbIdleNotReady = new System.Windows.Forms.RadioButton();
            this.rdbScreenLockNotReady = new System.Windows.Forms.RadioButton();
            this.rdbScreenLockLogOut = new System.Windows.Forms.RadioButton();
            this.rdbScreenUnlockNotReady = new System.Windows.Forms.RadioButton();
            this.rdbScreenUnlockReady = new System.Windows.Forms.RadioButton();
            this.chkScreenUnlock = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numLogOutWhenIdle)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(419, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(338, 460);
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
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Location of Cisco Application File";
            // 
            // txtCiscoExeLocation
            // 
            this.txtCiscoExeLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCiscoExeLocation.Location = new System.Drawing.Point(29, 47);
            this.txtCiscoExeLocation.Name = "txtCiscoExeLocation";
            this.txtCiscoExeLocation.Size = new System.Drawing.Size(431, 20);
            this.txtCiscoExeLocation.TabIndex = 3;
            // 
            // btnCiscoExeLocation
            // 
            this.btnCiscoExeLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCiscoExeLocation.Location = new System.Drawing.Point(466, 45);
            this.btnCiscoExeLocation.Name = "btnCiscoExeLocation";
            this.btnCiscoExeLocation.Size = new System.Drawing.Size(28, 23);
            this.btnCiscoExeLocation.TabIndex = 4;
            this.btnCiscoExeLocation.Text = "...";
            this.btnCiscoExeLocation.UseVisualStyleBackColor = true;
            // 
            // chkLogOutWhenIdle
            // 
            this.chkLogOutWhenIdle.AutoSize = true;
            this.chkLogOutWhenIdle.Location = new System.Drawing.Point(29, 106);
            this.chkLogOutWhenIdle.Name = "chkLogOutWhenIdle";
            this.chkLogOutWhenIdle.Size = new System.Drawing.Size(209, 17);
            this.chkLogOutWhenIdle.TabIndex = 5;
            this.chkLogOutWhenIdle.Text = "Log out Agent when System is Idle for";
            this.chkLogOutWhenIdle.UseVisualStyleBackColor = true;
            // 
            // numLogOutWhenIdle
            // 
            this.numLogOutWhenIdle.Location = new System.Drawing.Point(244, 105);
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
            this.lblMinutes.Location = new System.Drawing.Point(324, 107);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(48, 13);
            this.lblMinutes.TabIndex = 7;
            this.lblMinutes.Text = "minutes.";
            // 
            // chkLogInAfterIdle
            // 
            this.chkLogInAfterIdle.AutoSize = true;
            this.chkLogInAfterIdle.Location = new System.Drawing.Point(29, 139);
            this.chkLogInAfterIdle.Name = "chkLogInAfterIdle";
            this.chkLogInAfterIdle.Size = new System.Drawing.Size(201, 17);
            this.chkLogInAfterIdle.TabIndex = 8;
            this.chkLogInAfterIdle.Text = "Log in after coming back from Idle to";
            this.chkLogInAfterIdle.UseVisualStyleBackColor = true;
            // 
            // lblCiscoPassword
            // 
            this.lblCiscoPassword.AutoSize = true;
            this.lblCiscoPassword.Location = new System.Drawing.Point(26, 360);
            this.lblCiscoPassword.Name = "lblCiscoPassword";
            this.lblCiscoPassword.Size = new System.Drawing.Size(126, 13);
            this.lblCiscoPassword.TabIndex = 9;
            this.lblCiscoPassword.Text = "Password to Cisco Agent";
            // 
            // txtCiscoPassword
            // 
            this.txtCiscoPassword.Location = new System.Drawing.Point(158, 357);
            this.txtCiscoPassword.Name = "txtCiscoPassword";
            this.txtCiscoPassword.Size = new System.Drawing.Size(336, 20);
            this.txtCiscoPassword.TabIndex = 10;
            // 
            // lblCiscoPasswordNote
            // 
            this.lblCiscoPasswordNote.AutoSize = true;
            this.lblCiscoPasswordNote.Location = new System.Drawing.Point(35, 392);
            this.lblCiscoPasswordNote.Name = "lblCiscoPasswordNote";
            this.lblCiscoPasswordNote.Size = new System.Drawing.Size(337, 26);
            this.lblCiscoPasswordNote.TabIndex = 11;
            this.lblCiscoPasswordNote.Text = "Note: this password will be stored as plain text.\r\nYou have to have AutoHotKey in" +
    "stalled for loggin out and in to work.";
            // 
            // ofdCiscoExeLocation
            // 
            this.ofdCiscoExeLocation.Filter = "Application|*.exe";
            this.ofdCiscoExeLocation.RestoreDirectory = true;
            this.ofdCiscoExeLocation.SupportMultiDottedExtensions = true;
            this.ofdCiscoExeLocation.Title = "Select Cisco Application File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Idle Time";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(510, 495);
            this.shapeContainer1.TabIndex = 13;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 63;
            this.lineShape2.X2 = 489;
            this.lineShape2.Y1 = 252;
            this.lineShape2.Y2 = 252;
            // 
            // lineShape1
            // 
            this.lineShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 63;
            this.lineShape1.X2 = 489;
            this.lineShape1.Y1 = 88;
            this.lineShape1.Y2 = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Startup && Shutdown";
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Location = new System.Drawing.Point(29, 278);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(175, 17);
            this.chkStartup.TabIndex = 15;
            this.chkStartup.Text = "Log in on Startup and switch to";
            this.chkStartup.UseVisualStyleBackColor = true;
            // 
            // rdbStartupReady
            // 
            this.rdbStartupReady.AutoSize = true;
            this.rdbStartupReady.Location = new System.Drawing.Point(35, 10);
            this.rdbStartupReady.Name = "rdbStartupReady";
            this.rdbStartupReady.Size = new System.Drawing.Size(56, 17);
            this.rdbStartupReady.TabIndex = 16;
            this.rdbStartupReady.Text = "Ready";
            this.rdbStartupReady.UseVisualStyleBackColor = true;
            // 
            // rdbStartupNotReady
            // 
            this.rdbStartupNotReady.AutoSize = true;
            this.rdbStartupNotReady.Checked = true;
            this.rdbStartupNotReady.Location = new System.Drawing.Point(142, 9);
            this.rdbStartupNotReady.Name = "rdbStartupNotReady";
            this.rdbStartupNotReady.Size = new System.Drawing.Size(76, 17);
            this.rdbStartupNotReady.TabIndex = 16;
            this.rdbStartupNotReady.TabStop = true;
            this.rdbStartupNotReady.Text = "Not Ready";
            this.rdbStartupNotReady.UseVisualStyleBackColor = true;
            // 
            // chkShutdown
            // 
            this.chkShutdown.AutoSize = true;
            this.chkShutdown.Location = new System.Drawing.Point(29, 312);
            this.chkShutdown.Name = "chkShutdown";
            this.chkShutdown.Size = new System.Drawing.Size(210, 17);
            this.chkShutdown.TabIndex = 15;
            this.chkShutdown.Text = "Log out and Close Agent on Shutdown";
            this.chkShutdown.UseVisualStyleBackColor = true;
            // 
            // chkScreenLock
            // 
            this.chkScreenLock.AutoSize = true;
            this.chkScreenLock.Location = new System.Drawing.Point(29, 171);
            this.chkScreenLock.Name = "chkScreenLock";
            this.chkScreenLock.Size = new System.Drawing.Size(146, 17);
            this.chkScreenLock.TabIndex = 8;
            this.chkScreenLock.Text = "On screen lock, switch to";
            this.chkScreenLock.UseVisualStyleBackColor = true;
            // 
            // rdbIdleReady
            // 
            this.rdbIdleReady.AutoSize = true;
            this.rdbIdleReady.Location = new System.Drawing.Point(29, 6);
            this.rdbIdleReady.Name = "rdbIdleReady";
            this.rdbIdleReady.Size = new System.Drawing.Size(56, 17);
            this.rdbIdleReady.TabIndex = 16;
            this.rdbIdleReady.Text = "Ready";
            this.rdbIdleReady.UseVisualStyleBackColor = true;
            // 
            // rdbIdleNotReady
            // 
            this.rdbIdleNotReady.AutoSize = true;
            this.rdbIdleNotReady.Checked = true;
            this.rdbIdleNotReady.Location = new System.Drawing.Point(137, 5);
            this.rdbIdleNotReady.Name = "rdbIdleNotReady";
            this.rdbIdleNotReady.Size = new System.Drawing.Size(76, 17);
            this.rdbIdleNotReady.TabIndex = 16;
            this.rdbIdleNotReady.TabStop = true;
            this.rdbIdleNotReady.Text = "Not Ready";
            this.rdbIdleNotReady.UseVisualStyleBackColor = true;
            // 
            // rdbScreenLockNotReady
            // 
            this.rdbScreenLockNotReady.AutoSize = true;
            this.rdbScreenLockNotReady.Checked = true;
            this.rdbScreenLockNotReady.Location = new System.Drawing.Point(137, 2);
            this.rdbScreenLockNotReady.Name = "rdbScreenLockNotReady";
            this.rdbScreenLockNotReady.Size = new System.Drawing.Size(76, 17);
            this.rdbScreenLockNotReady.TabIndex = 17;
            this.rdbScreenLockNotReady.TabStop = true;
            this.rdbScreenLockNotReady.Text = "Not Ready";
            this.rdbScreenLockNotReady.UseVisualStyleBackColor = true;
            // 
            // rdbScreenLockLogOut
            // 
            this.rdbScreenLockLogOut.AutoSize = true;
            this.rdbScreenLockLogOut.Location = new System.Drawing.Point(29, 2);
            this.rdbScreenLockLogOut.Name = "rdbScreenLockLogOut";
            this.rdbScreenLockLogOut.Size = new System.Drawing.Size(63, 17);
            this.rdbScreenLockLogOut.TabIndex = 18;
            this.rdbScreenLockLogOut.Text = "Log Out";
            this.rdbScreenLockLogOut.UseVisualStyleBackColor = true;
            // 
            // rdbScreenUnlockNotReady
            // 
            this.rdbScreenUnlockNotReady.AutoSize = true;
            this.rdbScreenUnlockNotReady.Checked = true;
            this.rdbScreenUnlockNotReady.Location = new System.Drawing.Point(129, -1);
            this.rdbScreenUnlockNotReady.Name = "rdbScreenUnlockNotReady";
            this.rdbScreenUnlockNotReady.Size = new System.Drawing.Size(76, 17);
            this.rdbScreenUnlockNotReady.TabIndex = 20;
            this.rdbScreenUnlockNotReady.TabStop = true;
            this.rdbScreenUnlockNotReady.Text = "Not Ready";
            this.rdbScreenUnlockNotReady.UseVisualStyleBackColor = true;
            // 
            // rdbScreenUnlockReady
            // 
            this.rdbScreenUnlockReady.AutoSize = true;
            this.rdbScreenUnlockReady.Location = new System.Drawing.Point(21, -1);
            this.rdbScreenUnlockReady.Name = "rdbScreenUnlockReady";
            this.rdbScreenUnlockReady.Size = new System.Drawing.Size(56, 17);
            this.rdbScreenUnlockReady.TabIndex = 21;
            this.rdbScreenUnlockReady.Text = "Ready";
            this.rdbScreenUnlockReady.UseVisualStyleBackColor = true;
            // 
            // chkScreenUnlock
            // 
            this.chkScreenUnlock.AutoSize = true;
            this.chkScreenUnlock.Location = new System.Drawing.Point(29, 205);
            this.chkScreenUnlock.Name = "chkScreenUnlock";
            this.chkScreenUnlock.Size = new System.Drawing.Size(236, 17);
            this.chkScreenUnlock.TabIndex = 19;
            this.chkScreenUnlock.Text = "Log in after coming back from screen lock to";
            this.chkScreenUnlock.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbIdleNotReady);
            this.panel1.Controls.Add(this.rdbIdleReady);
            this.panel1.Location = new System.Drawing.Point(258, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 30);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbScreenLockNotReady);
            this.panel2.Controls.Add(this.rdbScreenLockLogOut);
            this.panel2.Location = new System.Drawing.Point(258, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 22);
            this.panel2.TabIndex = 23;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdbScreenUnlockNotReady);
            this.panel3.Controls.Add(this.rdbScreenUnlockReady);
            this.panel3.Location = new System.Drawing.Point(266, 205);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(232, 22);
            this.panel3.TabIndex = 24;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rdbStartupNotReady);
            this.panel4.Controls.Add(this.rdbStartupReady);
            this.panel4.Location = new System.Drawing.Point(254, 267);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(239, 36);
            this.panel4.TabIndex = 25;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 495);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.chkScreenUnlock);
            this.Controls.Add(this.chkScreenLock);
            this.Controls.Add(this.chkShutdown);
            this.Controls.Add(this.chkStartup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCiscoPasswordNote);
            this.Controls.Add(this.txtCiscoPassword);
            this.Controls.Add(this.lblCiscoPassword);
            this.Controls.Add(this.chkLogInAfterIdle);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.numLogOutWhenIdle);
            this.Controls.Add(this.chkLogOutWhenIdle);
            this.Controls.Add(this.btnCiscoExeLocation);
            this.Controls.Add(this.txtCiscoExeLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numLogOutWhenIdle)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCiscoExeLocation;
        private System.Windows.Forms.Button btnCiscoExeLocation;
        private System.Windows.Forms.CheckBox chkLogOutWhenIdle;
        private System.Windows.Forms.NumericUpDown numLogOutWhenIdle;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.CheckBox chkLogInAfterIdle;
        private System.Windows.Forms.Label lblCiscoPassword;
        private System.Windows.Forms.TextBox txtCiscoPassword;
        private System.Windows.Forms.Label lblCiscoPasswordNote;
        private System.Windows.Forms.OpenFileDialog ofdCiscoExeLocation;
        private System.Windows.Forms.Label label2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkStartup;
        private System.Windows.Forms.RadioButton rdbStartupReady;
        private System.Windows.Forms.RadioButton rdbStartupNotReady;
        private System.Windows.Forms.CheckBox chkShutdown;
        private System.Windows.Forms.CheckBox chkScreenLock;
        private System.Windows.Forms.RadioButton rdbIdleReady;
        private System.Windows.Forms.RadioButton rdbIdleNotReady;
        private System.Windows.Forms.RadioButton rdbScreenLockNotReady;
        private System.Windows.Forms.RadioButton rdbScreenLockLogOut;
        private System.Windows.Forms.RadioButton rdbScreenUnlockNotReady;
        private System.Windows.Forms.RadioButton rdbScreenUnlockReady;
        private System.Windows.Forms.CheckBox chkScreenUnlock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}