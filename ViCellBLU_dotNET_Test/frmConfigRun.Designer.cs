
namespace ViCellBLU_dotNET_Test
{
    partial class frmConfigRun
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
            this.numUD_NthImageSave = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.bttnSC_Load = new System.Windows.Forms.Button();
            this.bttnSC_Save = new System.Windows.Forms.Button();
            this.numUD_Dilution = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSampleCellType = new System.Windows.Forms.TextBox();
            this.txtSampleTag = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSampleName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bttnDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_NthImageSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Dilution)).BeginInit();
            this.SuspendLayout();
            // 
            // numUD_NthImageSave
            // 
            this.numUD_NthImageSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUD_NthImageSave.Location = new System.Drawing.Point(224, 156);
            this.numUD_NthImageSave.Name = "numUD_NthImageSave";
            this.numUD_NthImageSave.Size = new System.Drawing.Size(68, 26);
            this.numUD_NthImageSave.TabIndex = 44;
            this.numUD_NthImageSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUD_NthImageSave.ThousandsSeparator = true;
            this.numUD_NthImageSave.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numUD_NthImageSave.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(32, 156);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(186, 20);
            this.label14.TabIndex = 43;
            this.label14.Text = "Save Every Nth Image";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bttnSC_Load
            // 
            this.bttnSC_Load.BackColor = System.Drawing.SystemColors.Control;
            this.bttnSC_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSC_Load.Location = new System.Drawing.Point(24, 198);
            this.bttnSC_Load.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnSC_Load.Name = "bttnSC_Load";
            this.bttnSC_Load.Size = new System.Drawing.Size(140, 35);
            this.bttnSC_Load.TabIndex = 50;
            this.bttnSC_Load.Text = "Load";
            this.bttnSC_Load.UseVisualStyleBackColor = false;
            this.bttnSC_Load.Click += new System.EventHandler(this.bttnSC_Load_Click);
            // 
            // bttnSC_Save
            // 
            this.bttnSC_Save.BackColor = System.Drawing.SystemColors.Control;
            this.bttnSC_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSC_Save.Location = new System.Drawing.Point(199, 198);
            this.bttnSC_Save.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnSC_Save.Name = "bttnSC_Save";
            this.bttnSC_Save.Size = new System.Drawing.Size(140, 35);
            this.bttnSC_Save.TabIndex = 50;
            this.bttnSC_Save.Text = "Save";
            this.bttnSC_Save.UseVisualStyleBackColor = false;
            this.bttnSC_Save.Click += new System.EventHandler(this.bttnSC_Save_Click);
            // 
            // numUD_Dilution
            // 
            this.numUD_Dilution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numUD_Dilution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUD_Dilution.Location = new System.Drawing.Point(113, 127);
            this.numUD_Dilution.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numUD_Dilution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUD_Dilution.Name = "numUD_Dilution";
            this.numUD_Dilution.Size = new System.Drawing.Size(77, 26);
            this.numUD_Dilution.TabIndex = 36;
            this.numUD_Dilution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUD_Dilution.ThousandsSeparator = true;
            this.numUD_Dilution.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numUD_Dilution.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "Cell Type:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(32, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "Dilution:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleCellType
            // 
            this.txtSampleCellType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSampleCellType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleCellType.Location = new System.Drawing.Point(113, 63);
            this.txtSampleCellType.Name = "txtSampleCellType";
            this.txtSampleCellType.Size = new System.Drawing.Size(150, 26);
            this.txtSampleCellType.TabIndex = 28;
            this.txtSampleCellType.Text = "Yeast";
            // 
            // txtSampleTag
            // 
            this.txtSampleTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSampleTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleTag.Location = new System.Drawing.Point(113, 95);
            this.txtSampleTag.Name = "txtSampleTag";
            this.txtSampleTag.Size = new System.Drawing.Size(209, 26);
            this.txtSampleTag.TabIndex = 34;
            this.txtSampleTag.Text = "My Tag";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(63, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "Tag:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSampleName
            // 
            this.txtSampleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSampleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleName.Location = new System.Drawing.Point(113, 23);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.Size = new System.Drawing.Size(209, 26);
            this.txtSampleName.TabIndex = 32;
            this.txtSampleName.Text = "My_Sample";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 31;
            this.label2.Text = "Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bttnDone
            // 
            this.bttnDone.BackColor = System.Drawing.SystemColors.Control;
            this.bttnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnDone.Location = new System.Drawing.Point(113, 245);
            this.bttnDone.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnDone.Name = "bttnDone";
            this.bttnDone.Size = new System.Drawing.Size(140, 35);
            this.bttnDone.TabIndex = 51;
            this.bttnDone.Text = "Done";
            this.bttnDone.UseVisualStyleBackColor = false;
            this.bttnDone.Click += new System.EventHandler(this.bttnDone_Click);
            // 
            // frmConfigRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(356, 297);
            this.Controls.Add(this.bttnDone);
            this.Controls.Add(this.bttnSC_Load);
            this.Controls.Add(this.bttnSC_Save);
            this.Controls.Add(this.numUD_NthImageSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtSampleName);
            this.Controls.Add(this.numUD_Dilution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSampleCellType);
            this.Controls.Add(this.txtSampleTag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigRun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Single Sample Config";
            this.Load += new System.EventHandler(this.frmConfigRun_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUD_NthImageSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Dilution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numUD_NthImageSave;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numUD_Dilution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSampleCellType;
        private System.Windows.Forms.TextBox txtSampleTag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSampleName;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button bttnSC_Load;
        internal System.Windows.Forms.Button bttnSC_Save;
        internal System.Windows.Forms.Button bttnDone;
    }
}