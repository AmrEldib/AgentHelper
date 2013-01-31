namespace AgentHelper.DummyCiscoAgent
{
    partial class AgentForm
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
            this.rdbReady = new System.Windows.Forms.RadioButton();
            this.rdbNotReady = new System.Windows.Forms.RadioButton();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtLoginStatus = new System.Windows.Forms.Label();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbReady
            // 
            this.rdbReady.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbReady.AutoSize = true;
            this.rdbReady.Location = new System.Drawing.Point(27, 33);
            this.rdbReady.Name = "rdbReady";
            this.rdbReady.Size = new System.Drawing.Size(48, 23);
            this.rdbReady.TabIndex = 1;
            this.rdbReady.Text = "Ready";
            this.rdbReady.UseVisualStyleBackColor = true;
            // 
            // rdbNotReady
            // 
            this.rdbNotReady.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbNotReady.AutoSize = true;
            this.rdbNotReady.Checked = true;
            this.rdbNotReady.Location = new System.Drawing.Point(90, 33);
            this.rdbNotReady.Name = "rdbNotReady";
            this.rdbNotReady.Size = new System.Drawing.Size(68, 23);
            this.rdbNotReady.TabIndex = 1;
            this.rdbNotReady.TabStop = true;
            this.rdbNotReady.Text = "Not Ready";
            this.rdbNotReady.UseVisualStyleBackColor = true;
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.rdbReady);
            this.grpStatus.Controls.Add(this.rdbNotReady);
            this.grpStatus.Location = new System.Drawing.Point(136, 12);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(200, 81);
            this.grpStatus.TabIndex = 2;
            this.grpStatus.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(35, 45);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // txtLoginStatus
            // 
            this.txtLoginStatus.Location = new System.Drawing.Point(32, 19);
            this.txtLoginStatus.Name = "txtLoginStatus";
            this.txtLoginStatus.Size = new System.Drawing.Size(78, 23);
            this.txtLoginStatus.TabIndex = 4;
            this.txtLoginStatus.Text = "Logged In";
            this.txtLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 109);
            this.Controls.Add(this.txtLoginStatus);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.grpStatus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgentForm";
            this.ShowIcon = false;
            this.Text = "Not Ready - Cisco Agent Desktop";
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbReady;
        private System.Windows.Forms.RadioButton rdbNotReady;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label txtLoginStatus;
    }
}

