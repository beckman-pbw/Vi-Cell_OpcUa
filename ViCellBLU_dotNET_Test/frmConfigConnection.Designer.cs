
namespace ViCellBLU_dotNET_Test
{
    partial class frmConfigConnection
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIPAddr = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnSaveSample = new System.Windows.Forms.Button();
            this.bttnLoad = new System.Windows.Forms.Button();
            this.bttnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(76, 65);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 20);
            this.label9.TabIndex = 26;
            this.label9.Text = "Port:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPort
            // 
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(128, 63);
            this.txtPort.Margin = new System.Windows.Forms.Padding(5);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(113, 26);
            this.txtPort.TabIndex = 27;
            this.txtPort.Text = "62641";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(52, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 24;
            this.label8.Text = "IP Addr:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIPAddr
            // 
            this.txtIPAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIPAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIPAddr.Location = new System.Drawing.Point(128, 23);
            this.txtIPAddr.Margin = new System.Windows.Forms.Padding(5);
            this.txtIPAddr.Name = "txtIPAddr";
            this.txtIPAddr.Size = new System.Drawing.Size(145, 26);
            this.txtIPAddr.TabIndex = 25;
            this.txtIPAddr.Text = "127.0.0.1";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(128, 142);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(225, 26);
            this.txtPassword.TabIndex = 23;
            this.txtPassword.Text = "Vi-CELL#01";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "User Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(128, 106);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(5);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(225, 26);
            this.txtUserName.TabIndex = 21;
            this.txtUserName.Text = "factory_admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 144);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Password:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bttnSaveSample
            // 
            this.bttnSaveSample.BackColor = System.Drawing.SystemColors.Control;
            this.bttnSaveSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnSaveSample.Location = new System.Drawing.Point(192, 193);
            this.bttnSaveSample.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnSaveSample.Name = "bttnSaveSample";
            this.bttnSaveSample.Size = new System.Drawing.Size(140, 40);
            this.bttnSaveSample.TabIndex = 48;
            this.bttnSaveSample.Text = "Save";
            this.bttnSaveSample.UseVisualStyleBackColor = false;
            this.bttnSaveSample.Click += new System.EventHandler(this.bttnSaveSample_Click);
            // 
            // bttnLoad
            // 
            this.bttnLoad.BackColor = System.Drawing.SystemColors.Control;
            this.bttnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnLoad.Location = new System.Drawing.Point(32, 193);
            this.bttnLoad.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnLoad.Name = "bttnLoad";
            this.bttnLoad.Size = new System.Drawing.Size(140, 40);
            this.bttnLoad.TabIndex = 49;
            this.bttnLoad.Text = "Load";
            this.bttnLoad.UseVisualStyleBackColor = false;
            this.bttnLoad.Click += new System.EventHandler(this.bttnLoad_Click);
            // 
            // bttnDone
            // 
            this.bttnDone.BackColor = System.Drawing.SystemColors.Control;
            this.bttnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnDone.Location = new System.Drawing.Point(99, 245);
            this.bttnDone.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnDone.Name = "bttnDone";
            this.bttnDone.Size = new System.Drawing.Size(140, 35);
            this.bttnDone.TabIndex = 50;
            this.bttnDone.Text = "Done";
            this.bttnDone.UseVisualStyleBackColor = false;
            this.bttnDone.Click += new System.EventHandler(this.bttnDone_Click);
            // 
            // frmConfigConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 308);
            this.Controls.Add(this.bttnSaveSample);
            this.Controls.Add(this.bttnLoad);
            this.Controls.Add(this.bttnDone);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIPAddr);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Configuration";
            this.Load += new System.EventHandler(this.frmConfigConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIPAddr;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button bttnSaveSample;
        internal System.Windows.Forms.Button bttnLoad;
        internal System.Windows.Forms.Button bttnDone;
    }
}