
namespace ViCellBLU_dotNET_Test
{
    partial class frmConfigRunSet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bttnDone = new System.Windows.Forms.Button();
            this.bttnClearSet = new System.Windows.Forms.Button();
            this.bttnSaveSet = new System.Windows.Forms.Button();
            this.bttnLoadSet = new System.Windows.Forms.Button();
            this.dgvSet = new System.Windows.Forms.DataGridView();
            this.txtSetName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoSortColumn = new System.Windows.Forms.RadioButton();
            this.rdoSortRow = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSet)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttnDone
            // 
            this.bttnDone.BackColor = System.Drawing.SystemColors.Control;
            this.bttnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnDone.Location = new System.Drawing.Point(672, 7);
            this.bttnDone.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnDone.Name = "bttnDone";
            this.bttnDone.Size = new System.Drawing.Size(140, 35);
            this.bttnDone.TabIndex = 47;
            this.bttnDone.Text = "Done";
            this.bttnDone.UseVisualStyleBackColor = false;
            this.bttnDone.Click += new System.EventHandler(this.bttnDone_Click);
            // 
            // bttnClearSet
            // 
            this.bttnClearSet.BackColor = System.Drawing.SystemColors.Control;
            this.bttnClearSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnClearSet.Location = new System.Drawing.Point(176, 15);
            this.bttnClearSet.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnClearSet.Name = "bttnClearSet";
            this.bttnClearSet.Size = new System.Drawing.Size(140, 35);
            this.bttnClearSet.TabIndex = 52;
            this.bttnClearSet.Text = "Clear";
            this.bttnClearSet.UseVisualStyleBackColor = false;
            this.bttnClearSet.Click += new System.EventHandler(this.bttnClearSet_Click);
            // 
            // bttnSaveSet
            // 
            this.bttnSaveSet.BackColor = System.Drawing.SystemColors.Control;
            this.bttnSaveSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnSaveSet.Location = new System.Drawing.Point(18, 62);
            this.bttnSaveSet.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnSaveSet.Name = "bttnSaveSet";
            this.bttnSaveSet.Size = new System.Drawing.Size(140, 35);
            this.bttnSaveSet.TabIndex = 51;
            this.bttnSaveSet.Text = "Save";
            this.bttnSaveSet.UseVisualStyleBackColor = false;
            this.bttnSaveSet.Click += new System.EventHandler(this.bttnSaveSet_Click);
            // 
            // bttnLoadSet
            // 
            this.bttnLoadSet.BackColor = System.Drawing.SystemColors.Control;
            this.bttnLoadSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnLoadSet.Location = new System.Drawing.Point(18, 15);
            this.bttnLoadSet.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.bttnLoadSet.Name = "bttnLoadSet";
            this.bttnLoadSet.Size = new System.Drawing.Size(140, 35);
            this.bttnLoadSet.TabIndex = 50;
            this.bttnLoadSet.Text = "Load";
            this.bttnLoadSet.UseVisualStyleBackColor = false;
            this.bttnLoadSet.Click += new System.EventHandler(this.bttnLoadSet_Click);
            // 
            // dgvSet
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvSet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSet.Location = new System.Drawing.Point(18, 122);
            this.dgvSet.Name = "dgvSet";
            this.dgvSet.Size = new System.Drawing.Size(781, 527);
            this.dgvSet.TabIndex = 49;
            // 
            // txtSetName
            // 
            this.txtSetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetName.Location = new System.Drawing.Point(271, 69);
            this.txtSetName.Name = "txtSetName";
            this.txtSetName.Size = new System.Drawing.Size(209, 26);
            this.txtSetName.TabIndex = 48;
            this.txtSetName.Text = "My_Set";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(205, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 47;
            this.label2.Text = "Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoSortColumn);
            this.groupBox4.Controls.Add(this.rdoSortRow);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(495, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(101, 90);
            this.groupBox4.TabIndex = 53;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sort Order";
            // 
            // rdoSortColumn
            // 
            this.rdoSortColumn.AutoSize = true;
            this.rdoSortColumn.Location = new System.Drawing.Point(15, 59);
            this.rdoSortColumn.Name = "rdoSortColumn";
            this.rdoSortColumn.Size = new System.Drawing.Size(77, 20);
            this.rdoSortColumn.TabIndex = 1;
            this.rdoSortColumn.Text = "Column";
            this.rdoSortColumn.UseVisualStyleBackColor = true;
            // 
            // rdoSortRow
            // 
            this.rdoSortRow.AutoSize = true;
            this.rdoSortRow.Checked = true;
            this.rdoSortRow.Location = new System.Drawing.Point(15, 25);
            this.rdoSortRow.Name = "rdoSortRow";
            this.rdoSortRow.Size = new System.Drawing.Size(56, 20);
            this.rdoSortRow.TabIndex = 0;
            this.rdoSortRow.TabStop = true;
            this.rdoSortRow.Text = "Row";
            this.rdoSortRow.UseVisualStyleBackColor = true;
            // 
            // frmConfigRunSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 661);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.bttnLoadSet);
            this.Controls.Add(this.bttnSaveSet);
            this.Controls.Add(this.bttnClearSet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSetName);
            this.Controls.Add(this.bttnDone);
            this.Controls.Add(this.dgvSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigRunSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample Set Config";
            this.Load += new System.EventHandler(this.frmConfigRunSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSet)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button bttnDone;
        internal System.Windows.Forms.Button bttnClearSet;
        internal System.Windows.Forms.Button bttnSaveSet;
        internal System.Windows.Forms.Button bttnLoadSet;
        private System.Windows.Forms.DataGridView dgvSet;
        private System.Windows.Forms.TextBox txtSetName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoSortColumn;
        private System.Windows.Forms.RadioButton rdoSortRow;
    }
}