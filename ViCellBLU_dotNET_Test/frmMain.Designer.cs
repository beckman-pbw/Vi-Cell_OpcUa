
namespace ViCellBLU_dotNET_Test
{
    partial class frmMain
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtIPAddr = new System.Windows.Forms.TextBox();
			this.bttnConnect = new System.Windows.Forms.Button();
			this.grpConnected = new System.Windows.Forms.GroupBox();
			this.label62 = new System.Windows.Forms.Label();
			this.lblFirmwareVersion = new System.Windows.Forms.Label();
			this.label63 = new System.Windows.Forms.Label();
			this.lblSoftwareVersion = new System.Windows.Forms.Label();
			this.lblWastTubeCapacity = new System.Windows.Forms.Label();
			this.lblReagentUses = new System.Windows.Forms.Label();
			this.lblInstrumentID = new System.Windows.Forms.Label();
			this.lblInstrumentStatus = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.grpInstrumentLocked = new System.Windows.Forms.GroupBox();
			this.diskSpaceLabel = new System.Windows.Forms.Label();
			this.label57 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.legendExport = new System.Windows.Forms.Label();
			this.rectangleExportDiskSpace = new System.Windows.Forms.Label();
			this.rectangleDataDiskSpace = new System.Windows.Forms.Label();
			this.rectangleOtherDiskSpace = new System.Windows.Forms.Label();
			this.bttnGetDiskSpace = new System.Windows.Forms.Button();
			this.progressBarDiskSpace = new System.Windows.Forms.ProgressBar();
			this.tabCtlrMain = new System.Windows.Forms.TabControl();
			this.tpgRunSamples = new System.Windows.Forms.TabPage();
			this.tabCtrlSamples = new System.Windows.Forms.TabControl();
			this.tpgSingleSampleSetup = new System.Windows.Forms.TabPage();
			this.cb_CellTypeorQC = new System.Windows.Forms.GroupBox();
			this.rbQualityControl = new System.Windows.Forms.RadioButton();
			this.rbCellType = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSampleCellTypeorQCName = new System.Windows.Forms.TextBox();
			this.gb_PostWashOption = new System.Windows.Forms.GroupBox();
			this.rb_PostWashFast = new System.Windows.Forms.RadioButton();
			this.rb_PostWashNormal = new System.Windows.Forms.RadioButton();
			this.numUD_NthImageSave = new System.Windows.Forms.NumericUpDown();
			this.bttnSaveSample = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.numUD_Dilution = new System.Windows.Forms.NumericUpDown();
			this.bttnLoadSample = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.txtSampleName = new System.Windows.Forms.TextBox();
			this.txtSampleTag = new System.Windows.Forms.TextBox();
			this.bttnStartSample = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tpgMultiSampleSetup = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.rdoSortColumn = new System.Windows.Forms.RadioButton();
			this.rdoSortRow = new System.Windows.Forms.RadioButton();
			this.bttnSampleSet_Clear = new System.Windows.Forms.Button();
			this.bttnSaveSet = new System.Windows.Forms.Button();
			this.bttnLoadSet = new System.Windows.Forms.Button();
			this.dgvSet = new System.Windows.Forms.DataGridView();
			this.txtSetName = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.bttnStartSet = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label38 = new System.Windows.Forms.Label();
			this.WorklistBox = new System.Windows.Forms.ListBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.lstSampleCompleteData = new System.Windows.Forms.ListBox();
			this.LastSampleTxt = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label55 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.label53 = new System.Windows.Forms.Label();
			this.CurrentSampleStatus = new System.Windows.Forms.Label();
			this.CurrentSamplePosition = new System.Windows.Forms.Label();
			this.CurrentSampleOwner = new System.Windows.Forms.Label();
			this.CurrentSampleId = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.bttnSampleResume = new System.Windows.Forms.Button();
			this.bttnSamplePause = new System.Windows.Forms.Button();
			this.bttnSampleStop = new System.Windows.Forms.Button();
			this.tpgCellTypes = new System.Windows.Forms.TabPage();
			this.bttnCT_Refresh = new System.Windows.Forms.Button();
			this.label30 = new System.Windows.Forms.Label();
			this.lstCellTypes = new System.Windows.Forms.ListBox();
			this.bttnCT_Delete = new System.Windows.Forms.Button();
			this.grpCT_Create = new System.Windows.Forms.GroupBox();
			this.bttnCTCreate_Save = new System.Windows.Forms.Button();
			this.bttnCTCreate_Load = new System.Windows.Forms.Button();
			this.numUDCT_ConcAdjFactor = new System.Windows.Forms.NumericUpDown();
			this.label27 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.numUDCT_ViaSpotArea = new System.Windows.Forms.NumericUpDown();
			this.label26 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.numUDCT_ViaSpotBright = new System.Windows.Forms.NumericUpDown();
			this.label23 = new System.Windows.Forms.Label();
			this.numUDCT_MixCycles = new System.Windows.Forms.NumericUpDown();
			this.label22 = new System.Windows.Forms.Label();
			this.numUDCT_AspCycles = new System.Windows.Forms.NumericUpDown();
			this.label21 = new System.Windows.Forms.Label();
			this.cbxCT_Decluster = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.numUDCT_MinCirc = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			this.numUDCT_Sharpness = new System.Windows.Forms.NumericUpDown();
			this.label19 = new System.Windows.Forms.Label();
			this.numUDCT_Images = new System.Windows.Forms.NumericUpDown();
			this.label18 = new System.Windows.Forms.Label();
			this.numUDCT_MaxDiam = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.numUDCT_MinDiam = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCTCreate_Name = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.bttnCT_Create = new System.Windows.Forms.Button();
			this.tpgQualityControls = new System.Windows.Forms.TabPage();
			this.bttnQC_Refresh = new System.Windows.Forms.Button();
			this.label31 = new System.Windows.Forms.Label();
			this.lstQualityControls = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.dtpQCExpires = new System.Windows.Forms.DateTimePicker();
			this.label29 = new System.Windows.Forms.Label();
			this.txtQCCreate_Comment = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.txtQCCreate_LotNumber = new System.Windows.Forms.TextBox();
			this.label39 = new System.Windows.Forms.Label();
			this.txtQCCreate_CellTypeName = new System.Windows.Forms.TextBox();
			this.bttnQC_Save = new System.Windows.Forms.Button();
			this.bttnQC_Load = new System.Windows.Forms.Button();
			this.label33 = new System.Windows.Forms.Label();
			this.cbxQC_AssayParam = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.numUDQC_AssayValue = new System.Windows.Forms.NumericUpDown();
			this.label35 = new System.Windows.Forms.Label();
			this.numUDQC_AcceptLimits = new System.Windows.Forms.NumericUpDown();
			this.txtQCCreate_Name = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.bttnQC_Create = new System.Windows.Forms.Button();
			this.tpgResultsConfig = new System.Windows.Forms.TabPage();
			this.bttnExportConfig = new System.Windows.Forms.Button();
			this.bttnImportConfig = new System.Windows.Forms.Button();
			this.lstResultsStatus = new System.Windows.Forms.ListBox();
			this.label58 = new System.Windows.Forms.Label();
			this.tabCtrlResults = new System.Windows.Forms.TabControl();
			this.tpgGetResults = new System.Windows.Forms.TabPage();
			this.SampleResults_TagLabel = new System.Windows.Forms.Label();
			this.SampleResults_TagBox = new System.Windows.Forms.TextBox();
			this.SampleFilterComboBox = new System.Windows.Forms.ComboBox();
			this.label52 = new System.Windows.Forms.Label();
			this.SampleResults_CTQCLabel = new System.Windows.Forms.Label();
			this.SampleResults_CTQCNameBox = new System.Windows.Forms.TextBox();
			this.lblFilterType = new System.Windows.Forms.Label();
			this.bttnGetResults = new System.Windows.Forms.Button();
			this.txtSampleResults_SearchString = new System.Windows.Forms.TextBox();
			this.dtpSampleResults_StartDate = new System.Windows.Forms.DateTimePicker();
			this.label48 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtSampleResults_Username = new System.Windows.Forms.TextBox();
			this.dtpSampleResults_EndDate = new System.Windows.Forms.DateTimePicker();
			this.label47 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnExportEncryptedResult = new System.Windows.Forms.Button();
			this.btnDeleteAllResults = new System.Windows.Forms.Button();
			this.listBoxCurrentSampleResult = new System.Windows.Forms.ListBox();
			this.lblSelectedSampleId = new System.Windows.Forms.Label();
			this.lblSampleResultCount = new System.Windows.Forms.Label();
			this.label51 = new System.Windows.Forms.Label();
			this.bttnDeleteResult = new System.Windows.Forms.Button();
			this.bttnExportResult = new System.Windows.Forms.Button();
			this.label50 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.lstResultIds = new System.Windows.Forms.ListBox();
			this.tpgMaintenance = new System.Windows.Forms.TabPage();
			this.tabControlReagents = new System.Windows.Forms.TabControl();
			this.tpgReagentVolume = new System.Windows.Forms.TabPage();
			this.txtAddReagentVolume = new System.Windows.Forms.TextBox();
			this.cbxSelectReagent = new System.Windows.Forms.ComboBox();
			this.txtSetReagentVolume = new System.Windows.Forms.TextBox();
			this.label61 = new System.Windows.Forms.Label();
			this.lblReagentVolume = new System.Windows.Forms.Label();
			this.btnGetReagentVolume = new System.Windows.Forms.Button();
			this.btnAddReagentVolume = new System.Windows.Forms.Button();
			this.btnSetReagentVolume = new System.Windows.Forms.Button();
			this.tpgCleanFluidics = new System.Windows.Forms.TabPage();
			this.btnCleanFluidics = new System.Windows.Forms.Button();
			this.lblCleanFluidicsStatus = new System.Windows.Forms.Label();
			this.tpgPrimeReagents = new System.Windows.Forms.TabPage();
			this.btnCancelPrimeReagents = new System.Windows.Forms.Button();
			this.btnPrimeReagents = new System.Windows.Forms.Button();
			this.lblPrimeReagentsStatus = new System.Windows.Forms.Label();
			this.tpgPurgeReagents = new System.Windows.Forms.TabPage();
			this.btnCancelPurgeReagents = new System.Windows.Forms.Button();
			this.btnPurgeReagents = new System.Windows.Forms.Button();
			this.lblPurgeReagentssStatus = new System.Windows.Forms.Label();
			this.tpgDecontaminate = new System.Windows.Forms.TabPage();
			this.btnCancelDecontaminate = new System.Windows.Forms.Button();
			this.btnDecontaminate = new System.Windows.Forms.Button();
			this.lblDecontaminateStatus = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.dtpConfigAudit_StartDate = new System.Windows.Forms.DateTimePicker();
			this.label59 = new System.Windows.Forms.Label();
			this.lstConfigAuditStatus = new System.Windows.Forms.ListBox();
			this.label49 = new System.Windows.Forms.Label();
			this.dtpConfigAudit_EndDate = new System.Windows.Forms.DateTimePicker();
			this.bttnStartLogDataExport = new System.Windows.Forms.Button();
			this.label60 = new System.Windows.Forms.Label();
			this.bttnDeleteCampaignData = new System.Windows.Forms.Button();
			this.btnReboot = new System.Windows.Forms.Button();
			this.btnShutdown = new System.Windows.Forms.Button();
			this.tabErrorStatus = new System.Windows.Forms.TabPage();
			this.btnClearErrorStatus = new System.Windows.Forms.Button();
			this.lstErrorStatus = new System.Windows.Forms.ListBox();
			this.bttnEjectStage = new System.Windows.Forms.Button();
			this.bttnUnlock = new System.Windows.Forms.Button();
			this.bttnRequestLock = new System.Windows.Forms.Button();
			this.label44 = new System.Windows.Forms.Label();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUtils = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCnxConfig = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUtils_CreateSingle = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUtils_CreateSet = new System.Windows.Forms.ToolStripMenuItem();
			this.lstCallResult = new System.Windows.Forms.ListBox();
			this.lblAppVersion = new System.Windows.Forms.Label();
			this.groupBox3.SuspendLayout();
			this.grpConnected.SuspendLayout();
			this.grpInstrumentLocked.SuspendLayout();
			this.tabCtlrMain.SuspendLayout();
			this.tpgRunSamples.SuspendLayout();
			this.tabCtrlSamples.SuspendLayout();
			this.tpgSingleSampleSetup.SuspendLayout();
			this.cb_CellTypeorQC.SuspendLayout();
			this.gb_PostWashOption.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUD_NthImageSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUD_Dilution)).BeginInit();
			this.tpgMultiSampleSetup.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSet)).BeginInit();
			this.tabPage1.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tpgCellTypes.SuspendLayout();
			this.grpCT_Create.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_ConcAdjFactor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_ViaSpotArea)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_ViaSpotBright)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MixCycles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_AspCycles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MinCirc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_Sharpness)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_Images)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MaxDiam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MinDiam)).BeginInit();
			this.tpgQualityControls.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUDQC_AssayValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDQC_AcceptLimits)).BeginInit();
			this.tpgResultsConfig.SuspendLayout();
			this.tabCtrlResults.SuspendLayout();
			this.tpgGetResults.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tpgMaintenance.SuspendLayout();
			this.tabControlReagents.SuspendLayout();
			this.tpgReagentVolume.SuspendLayout();
			this.tpgCleanFluidics.SuspendLayout();
			this.tpgPrimeReagents.SuspendLayout();
			this.tpgPurgeReagents.SuspendLayout();
			this.tpgDecontaminate.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabErrorStatus.SuspendLayout();
			this.menuStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox3.Controls.Add(this.txtPassword);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.txtUserName);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.txtPort);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.txtIPAddr);
			this.groupBox3.Controls.Add(this.bttnConnect);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(112, 27);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(512, 93);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Connection";
			// 
			// txtPassword
			// 
			this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(208, 56);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(169, 22);
			this.txtPassword.TabIndex = 34;
			this.txtPassword.Text = "Vi-CELL#01";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(260, 11);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 16);
			this.label10.TabIndex = 32;
			this.label10.Text = "Login";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtUserName
			// 
			this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUserName.Location = new System.Drawing.Point(208, 31);
			this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(169, 22);
			this.txtUserName.TabIndex = 33;
			this.txtUserName.Text = "factory_admin";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(66, 58);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 16);
			this.label9.TabIndex = 30;
			this.label9.Text = "Port:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPort
			// 
			this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPort.Location = new System.Drawing.Point(114, 56);
			this.txtPort.Margin = new System.Windows.Forms.Padding(4);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(85, 22);
			this.txtPort.TabIndex = 31;
			this.txtPort.Text = "62641";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(11, 28);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(63, 16);
			this.label8.TabIndex = 28;
			this.label8.Text = "IP Addr:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtIPAddr
			// 
			this.txtIPAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtIPAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIPAddr.Location = new System.Drawing.Point(82, 26);
			this.txtIPAddr.Margin = new System.Windows.Forms.Padding(4);
			this.txtIPAddr.Name = "txtIPAddr";
			this.txtIPAddr.Size = new System.Drawing.Size(117, 22);
			this.txtIPAddr.TabIndex = 29;
			this.txtIPAddr.Text = "127.0.0.1";
			// 
			// bttnConnect
			// 
			this.bttnConnect.BackColor = System.Drawing.SystemColors.Control;
			this.bttnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnConnect.Location = new System.Drawing.Point(384, 31);
			this.bttnConnect.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnConnect.Name = "bttnConnect";
			this.bttnConnect.Size = new System.Drawing.Size(120, 35);
			this.bttnConnect.TabIndex = 11;
			this.bttnConnect.Text = "Disconnect";
			this.bttnConnect.UseVisualStyleBackColor = false;
			this.bttnConnect.Click += new System.EventHandler(this.bttnConnect_Click);
			// 
			// grpConnected
			// 
			this.grpConnected.BackColor = System.Drawing.SystemColors.Control;
			this.grpConnected.Controls.Add(this.label62);
			this.grpConnected.Controls.Add(this.lblFirmwareVersion);
			this.grpConnected.Controls.Add(this.label63);
			this.grpConnected.Controls.Add(this.lblSoftwareVersion);
			this.grpConnected.Controls.Add(this.lblWastTubeCapacity);
			this.grpConnected.Controls.Add(this.lblReagentUses);
			this.grpConnected.Controls.Add(this.lblInstrumentID);
			this.grpConnected.Controls.Add(this.lblInstrumentStatus);
			this.grpConnected.Controls.Add(this.label41);
			this.grpConnected.Controls.Add(this.label37);
			this.grpConnected.Controls.Add(this.label36);
			this.grpConnected.Controls.Add(this.label32);
			this.grpConnected.Controls.Add(this.grpInstrumentLocked);
			this.grpConnected.Controls.Add(this.bttnRequestLock);
			this.grpConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpConnected.Location = new System.Drawing.Point(12, 126);
			this.grpConnected.Name = "grpConnected";
			this.grpConnected.Size = new System.Drawing.Size(940, 594);
			this.grpConnected.TabIndex = 16;
			this.grpConnected.TabStop = false;
			this.grpConnected.Text = "Disconnected";
			// 
			// label62
			// 
			this.label62.AutoSize = true;
			this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label62.Location = new System.Drawing.Point(676, 16);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(112, 16);
			this.label62.TabIndex = 62;
			this.label62.Text = "Firmware Version";
			this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFirmwareVersion
			// 
			this.lblFirmwareVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFirmwareVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFirmwareVersion.Location = new System.Drawing.Point(645, 38);
			this.lblFirmwareVersion.Name = "lblFirmwareVersion";
			this.lblFirmwareVersion.Size = new System.Drawing.Size(162, 25);
			this.lblFirmwareVersion.TabIndex = 61;
			this.lblFirmwareVersion.Text = "unknown";
			this.lblFirmwareVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label63
			// 
			this.label63.AutoSize = true;
			this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label63.Location = new System.Drawing.Point(504, 16);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(109, 16);
			this.label63.TabIndex = 60;
			this.label63.Text = "Software Version";
			this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblSoftwareVersion
			// 
			this.lblSoftwareVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSoftwareVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSoftwareVersion.Location = new System.Drawing.Point(473, 38);
			this.lblSoftwareVersion.Name = "lblSoftwareVersion";
			this.lblSoftwareVersion.Size = new System.Drawing.Size(162, 25);
			this.lblSoftwareVersion.TabIndex = 59;
			this.lblSoftwareVersion.Text = "unknown";
			this.lblSoftwareVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblWastTubeCapacity
			// 
			this.lblWastTubeCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblWastTubeCapacity.Enabled = false;
			this.lblWastTubeCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWastTubeCapacity.Location = new System.Drawing.Point(752, 90);
			this.lblWastTubeCapacity.Name = "lblWastTubeCapacity";
			this.lblWastTubeCapacity.Size = new System.Drawing.Size(50, 25);
			this.lblWastTubeCapacity.TabIndex = 58;
			this.lblWastTubeCapacity.Text = "---";
			this.lblWastTubeCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblWastTubeCapacity.Visible = false;
			// 
			// lblReagentUses
			// 
			this.lblReagentUses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblReagentUses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReagentUses.Location = new System.Drawing.Point(412, 86);
			this.lblReagentUses.Name = "lblReagentUses";
			this.lblReagentUses.Size = new System.Drawing.Size(50, 25);
			this.lblReagentUses.TabIndex = 57;
			this.lblReagentUses.Text = "---";
			this.lblReagentUses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblInstrumentID
			// 
			this.lblInstrumentID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInstrumentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInstrumentID.Location = new System.Drawing.Point(169, 38);
			this.lblInstrumentID.Name = "lblInstrumentID";
			this.lblInstrumentID.Size = new System.Drawing.Size(289, 25);
			this.lblInstrumentID.TabIndex = 56;
			this.lblInstrumentID.Text = "---";
			this.lblInstrumentID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblInstrumentStatus
			// 
			this.lblInstrumentStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInstrumentStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInstrumentStatus.Location = new System.Drawing.Point(132, 86);
			this.lblInstrumentStatus.Name = "lblInstrumentStatus";
			this.lblInstrumentStatus.Size = new System.Drawing.Size(162, 25);
			this.lblInstrumentStatus.TabIndex = 16;
			this.lblInstrumentStatus.Text = "---";
			this.lblInstrumentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label41
			// 
			this.label41.Enabled = false;
			this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label41.Location = new System.Drawing.Point(642, 86);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(104, 29);
			this.label41.TabIndex = 53;
			this.label41.Text = "Waste Tube Capacity Remaining";
			this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label41.Visible = false;
			// 
			// label37
			// 
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label37.Location = new System.Drawing.Point(312, 86);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(94, 29);
			this.label37.TabIndex = 51;
			this.label37.Text = "Reagent Uses Remaining";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label36.Location = new System.Drawing.Point(258, 15);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(84, 16);
			this.label36.TabIndex = 49;
			this.label36.Text = "Instrument ID";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label32.Location = new System.Drawing.Point(18, 91);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(108, 16);
			this.label32.TabIndex = 47;
			this.label32.Text = "Instrument Status";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpInstrumentLocked
			// 
			this.grpInstrumentLocked.BackColor = System.Drawing.SystemColors.Control;
			this.grpInstrumentLocked.Controls.Add(this.diskSpaceLabel);
			this.grpInstrumentLocked.Controls.Add(this.label57);
			this.grpInstrumentLocked.Controls.Add(this.label56);
			this.grpInstrumentLocked.Controls.Add(this.label46);
			this.grpInstrumentLocked.Controls.Add(this.label45);
			this.grpInstrumentLocked.Controls.Add(this.label42);
			this.grpInstrumentLocked.Controls.Add(this.legendExport);
			this.grpInstrumentLocked.Controls.Add(this.rectangleExportDiskSpace);
			this.grpInstrumentLocked.Controls.Add(this.rectangleDataDiskSpace);
			this.grpInstrumentLocked.Controls.Add(this.rectangleOtherDiskSpace);
			this.grpInstrumentLocked.Controls.Add(this.bttnGetDiskSpace);
			this.grpInstrumentLocked.Controls.Add(this.progressBarDiskSpace);
			this.grpInstrumentLocked.Controls.Add(this.tabCtlrMain);
			this.grpInstrumentLocked.Controls.Add(this.bttnEjectStage);
			this.grpInstrumentLocked.Controls.Add(this.bttnUnlock);
			this.grpInstrumentLocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpInstrumentLocked.Location = new System.Drawing.Point(6, 126);
			this.grpInstrumentLocked.Name = "grpInstrumentLocked";
			this.grpInstrumentLocked.Size = new System.Drawing.Size(928, 468);
			this.grpInstrumentLocked.TabIndex = 10;
			this.grpInstrumentLocked.TabStop = false;
			this.grpInstrumentLocked.Text = "Lock State Unkown";
			// 
			// diskSpaceLabel
			// 
			this.diskSpaceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.diskSpaceLabel.Location = new System.Drawing.Point(393, 46);
			this.diskSpaceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.diskSpaceLabel.Name = "diskSpaceLabel";
			this.diskSpaceLabel.Size = new System.Drawing.Size(225, 13);
			this.diskSpaceLabel.TabIndex = 60;
			this.diskSpaceLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label57.Location = new System.Drawing.Point(344, 42);
			this.label57.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(37, 13);
			this.label57.TabIndex = 59;
			this.label57.Text = "Export";
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label56.Location = new System.Drawing.Point(344, 28);
			this.label56.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(30, 13);
			this.label56.TabIndex = 58;
			this.label56.Text = "Data";
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label46.Location = new System.Drawing.Point(344, 13);
			this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(33, 13);
			this.label46.TabIndex = 57;
			this.label46.Text = "Other";
			// 
			// label45
			// 
			this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label45.Location = new System.Drawing.Point(327, 46);
			this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(8, 8);
			this.label45.TabIndex = 56;
			// 
			// label42
			// 
			this.label42.BackColor = System.Drawing.Color.Blue;
			this.label42.Location = new System.Drawing.Point(327, 31);
			this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(8, 8);
			this.label42.TabIndex = 55;
			// 
			// legendExport
			// 
			this.legendExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.legendExport.Location = new System.Drawing.Point(327, 16);
			this.legendExport.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.legendExport.Name = "legendExport";
			this.legendExport.Size = new System.Drawing.Size(8, 8);
			this.legendExport.TabIndex = 10;
			// 
			// rectangleExportDiskSpace
			// 
			this.rectangleExportDiskSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.rectangleExportDiskSpace.Location = new System.Drawing.Point(393, 13);
			this.rectangleExportDiskSpace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.rectangleExportDiskSpace.Name = "rectangleExportDiskSpace";
			this.rectangleExportDiskSpace.Size = new System.Drawing.Size(0, 30);
			this.rectangleExportDiskSpace.TabIndex = 54;
			// 
			// rectangleDataDiskSpace
			// 
			this.rectangleDataDiskSpace.BackColor = System.Drawing.Color.Blue;
			this.rectangleDataDiskSpace.Location = new System.Drawing.Point(393, 13);
			this.rectangleDataDiskSpace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.rectangleDataDiskSpace.Name = "rectangleDataDiskSpace";
			this.rectangleDataDiskSpace.Size = new System.Drawing.Size(0, 30);
			this.rectangleDataDiskSpace.TabIndex = 53;
			// 
			// rectangleOtherDiskSpace
			// 
			this.rectangleOtherDiskSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.rectangleOtherDiskSpace.Location = new System.Drawing.Point(393, 13);
			this.rectangleOtherDiskSpace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.rectangleOtherDiskSpace.Name = "rectangleOtherDiskSpace";
			this.rectangleOtherDiskSpace.Size = new System.Drawing.Size(0, 30);
			this.rectangleOtherDiskSpace.TabIndex = 52;
			// 
			// bttnGetDiskSpace
			// 
			this.bttnGetDiskSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnGetDiskSpace.Location = new System.Drawing.Point(623, 13);
			this.bttnGetDiskSpace.Margin = new System.Windows.Forms.Padding(2);
			this.bttnGetDiskSpace.Name = "bttnGetDiskSpace";
			this.bttnGetDiskSpace.Size = new System.Drawing.Size(114, 30);
			this.bttnGetDiskSpace.TabIndex = 51;
			this.bttnGetDiskSpace.Text = "Get Disk Space";
			this.bttnGetDiskSpace.UseVisualStyleBackColor = true;
			this.bttnGetDiskSpace.Click += new System.EventHandler(this.bttnGetDiskSpace_Click);
			// 
			// progressBarDiskSpace
			// 
			this.progressBarDiskSpace.BackColor = System.Drawing.Color.White;
			this.progressBarDiskSpace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.progressBarDiskSpace.Location = new System.Drawing.Point(393, 13);
			this.progressBarDiskSpace.Margin = new System.Windows.Forms.Padding(2);
			this.progressBarDiskSpace.Name = "progressBarDiskSpace";
			this.progressBarDiskSpace.Size = new System.Drawing.Size(225, 30);
			this.progressBarDiskSpace.TabIndex = 16;
			// 
			// tabCtlrMain
			// 
			this.tabCtlrMain.Controls.Add(this.tpgRunSamples);
			this.tabCtlrMain.Controls.Add(this.tpgCellTypes);
			this.tabCtlrMain.Controls.Add(this.tpgQualityControls);
			this.tabCtlrMain.Controls.Add(this.tpgResultsConfig);
			this.tabCtlrMain.Controls.Add(this.tpgMaintenance);
			this.tabCtlrMain.Controls.Add(this.tabErrorStatus);
			this.tabCtlrMain.Location = new System.Drawing.Point(0, 62);
			this.tabCtlrMain.Name = "tabCtlrMain";
			this.tabCtlrMain.SelectedIndex = 0;
			this.tabCtlrMain.Size = new System.Drawing.Size(931, 424);
			this.tabCtlrMain.TabIndex = 15;
			// 
			// tpgRunSamples
			// 
			this.tpgRunSamples.BackColor = System.Drawing.Color.Transparent;
			this.tpgRunSamples.Controls.Add(this.tabCtrlSamples);
			this.tpgRunSamples.Controls.Add(this.groupBox1);
			this.tpgRunSamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgRunSamples.Location = new System.Drawing.Point(4, 25);
			this.tpgRunSamples.Name = "tpgRunSamples";
			this.tpgRunSamples.Padding = new System.Windows.Forms.Padding(3);
			this.tpgRunSamples.Size = new System.Drawing.Size(923, 395);
			this.tpgRunSamples.TabIndex = 0;
			this.tpgRunSamples.Text = "Run Samples";
			// 
			// tabCtrlSamples
			// 
			this.tabCtrlSamples.Controls.Add(this.tpgSingleSampleSetup);
			this.tabCtrlSamples.Controls.Add(this.tpgMultiSampleSetup);
			this.tabCtrlSamples.Controls.Add(this.tabPage1);
			this.tabCtrlSamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabCtrlSamples.Location = new System.Drawing.Point(3, 11);
			this.tabCtrlSamples.Name = "tabCtrlSamples";
			this.tabCtrlSamples.SelectedIndex = 0;
			this.tabCtrlSamples.Size = new System.Drawing.Size(749, 361);
			this.tabCtrlSamples.TabIndex = 11;
			// 
			// tpgSingleSampleSetup
			// 
			this.tpgSingleSampleSetup.BackColor = System.Drawing.Color.Transparent;
			this.tpgSingleSampleSetup.Controls.Add(this.cb_CellTypeorQC);
			this.tpgSingleSampleSetup.Controls.Add(this.gb_PostWashOption);
			this.tpgSingleSampleSetup.Controls.Add(this.numUD_NthImageSave);
			this.tpgSingleSampleSetup.Controls.Add(this.bttnSaveSample);
			this.tpgSingleSampleSetup.Controls.Add(this.label14);
			this.tpgSingleSampleSetup.Controls.Add(this.numUD_Dilution);
			this.tpgSingleSampleSetup.Controls.Add(this.bttnLoadSample);
			this.tpgSingleSampleSetup.Controls.Add(this.label7);
			this.tpgSingleSampleSetup.Controls.Add(this.txtSampleName);
			this.tpgSingleSampleSetup.Controls.Add(this.txtSampleTag);
			this.tpgSingleSampleSetup.Controls.Add(this.bttnStartSample);
			this.tpgSingleSampleSetup.Controls.Add(this.label6);
			this.tpgSingleSampleSetup.Controls.Add(this.label5);
			this.tpgSingleSampleSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgSingleSampleSetup.Location = new System.Drawing.Point(4, 25);
			this.tpgSingleSampleSetup.Name = "tpgSingleSampleSetup";
			this.tpgSingleSampleSetup.Padding = new System.Windows.Forms.Padding(3);
			this.tpgSingleSampleSetup.Size = new System.Drawing.Size(741, 332);
			this.tpgSingleSampleSetup.TabIndex = 0;
			this.tpgSingleSampleSetup.Text = "Single Sample";
			// 
			// cb_CellTypeorQC
			// 
			this.cb_CellTypeorQC.Controls.Add(this.rbQualityControl);
			this.cb_CellTypeorQC.Controls.Add(this.rbCellType);
			this.cb_CellTypeorQC.Controls.Add(this.label3);
			this.cb_CellTypeorQC.Controls.Add(this.txtSampleCellTypeorQCName);
			this.cb_CellTypeorQC.Location = new System.Drawing.Point(134, 60);
			this.cb_CellTypeorQC.Name = "cb_CellTypeorQC";
			this.cb_CellTypeorQC.Size = new System.Drawing.Size(356, 93);
			this.cb_CellTypeorQC.TabIndex = 45;
			this.cb_CellTypeorQC.TabStop = false;
			this.cb_CellTypeorQC.Text = "Sample Definition";
			// 
			// rbQualityControl
			// 
			this.rbQualityControl.AutoSize = true;
			this.rbQualityControl.Location = new System.Drawing.Point(32, 51);
			this.rbQualityControl.Name = "rbQualityControl";
			this.rbQualityControl.Size = new System.Drawing.Size(112, 20);
			this.rbQualityControl.TabIndex = 19;
			this.rbQualityControl.TabStop = true;
			this.rbQualityControl.Text = "Quality Control";
			this.rbQualityControl.UseVisualStyleBackColor = true;
			// 
			// rbCellType
			// 
			this.rbCellType.AutoSize = true;
			this.rbCellType.Checked = true;
			this.rbCellType.Location = new System.Drawing.Point(32, 25);
			this.rbCellType.Name = "rbCellType";
			this.rbCellType.Size = new System.Drawing.Size(84, 20);
			this.rbCellType.TabIndex = 18;
			this.rbCellType.TabStop = true;
			this.rbCellType.Text = "Cell Type";
			this.rbCellType.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(165, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "Name:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtSampleCellTypeorQCName
			// 
			this.txtSampleCellTypeorQCName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSampleCellTypeorQCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleCellTypeorQCName.Location = new System.Drawing.Point(168, 51);
			this.txtSampleCellTypeorQCName.Name = "txtSampleCellTypeorQCName";
			this.txtSampleCellTypeorQCName.Size = new System.Drawing.Size(150, 22);
			this.txtSampleCellTypeorQCName.TabIndex = 15;
			this.txtSampleCellTypeorQCName.Text = "Yeast";
			// 
			// gb_PostWashOption
			// 
			this.gb_PostWashOption.Controls.Add(this.rb_PostWashFast);
			this.gb_PostWashOption.Controls.Add(this.rb_PostWashNormal);
			this.gb_PostWashOption.Location = new System.Drawing.Point(316, 216);
			this.gb_PostWashOption.Name = "gb_PostWashOption";
			this.gb_PostWashOption.Size = new System.Drawing.Size(231, 50);
			this.gb_PostWashOption.TabIndex = 44;
			this.gb_PostWashOption.TabStop = false;
			this.gb_PostWashOption.Text = "Workflow";
			// 
			// rb_PostWashFast
			// 
			this.rb_PostWashFast.AutoSize = true;
			this.rb_PostWashFast.Location = new System.Drawing.Point(83, 22);
			this.rb_PostWashFast.Name = "rb_PostWashFast";
			this.rb_PostWashFast.Size = new System.Drawing.Size(124, 20);
			this.rb_PostWashFast.TabIndex = 1;
			this.rb_PostWashFast.TabStop = true;
			this.rb_PostWashFast.Text = "Low Cell Density";
			this.rb_PostWashFast.UseVisualStyleBackColor = true;
			// 
			// rb_PostWashNormal
			// 
			this.rb_PostWashNormal.AutoSize = true;
			this.rb_PostWashNormal.Checked = true;
			this.rb_PostWashNormal.Location = new System.Drawing.Point(7, 22);
			this.rb_PostWashNormal.Name = "rb_PostWashNormal";
			this.rb_PostWashNormal.Size = new System.Drawing.Size(70, 20);
			this.rb_PostWashNormal.TabIndex = 0;
			this.rb_PostWashNormal.TabStop = true;
			this.rb_PostWashNormal.Text = "Normal";
			this.rb_PostWashNormal.UseVisualStyleBackColor = true;
			// 
			// numUD_NthImageSave
			// 
			this.numUD_NthImageSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUD_NthImageSave.Location = new System.Drawing.Point(210, 244);
			this.numUD_NthImageSave.Name = "numUD_NthImageSave";
			this.numUD_NthImageSave.Size = new System.Drawing.Size(49, 22);
			this.numUD_NthImageSave.TabIndex = 42;
			this.numUD_NthImageSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUD_NthImageSave.ThousandsSeparator = true;
			this.numUD_NthImageSave.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUD_NthImageSave.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numUD_NthImageSave.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUD_NthImageSave_KeyUp);
			// 
			// bttnSaveSample
			// 
			this.bttnSaveSample.BackColor = System.Drawing.SystemColors.Control;
			this.bttnSaveSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSaveSample.Location = new System.Drawing.Point(12, 46);
			this.bttnSaveSample.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnSaveSample.Name = "bttnSaveSample";
			this.bttnSaveSample.Size = new System.Drawing.Size(80, 25);
			this.bttnSaveSample.TabIndex = 43;
			this.bttnSaveSample.Text = "Save";
			this.bttnSaveSample.UseVisualStyleBackColor = false;
			this.bttnSaveSample.Click += new System.EventHandler(this.bttnSaveSample_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(53, 246);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(142, 16);
			this.label14.TabIndex = 41;
			this.label14.Text = "Save Every Nth Image";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUD_Dilution
			// 
			this.numUD_Dilution.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUD_Dilution.Location = new System.Drawing.Point(210, 205);
			this.numUD_Dilution.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numUD_Dilution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numUD_Dilution.Name = "numUD_Dilution";
			this.numUD_Dilution.Size = new System.Drawing.Size(77, 22);
			this.numUD_Dilution.TabIndex = 25;
			this.numUD_Dilution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUD_Dilution.ThousandsSeparator = true;
			this.numUD_Dilution.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUD_Dilution.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.numUD_Dilution.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUD_Dilution_KeyUp);
			this.numUD_Dilution.Leave += new System.EventHandler(this.numUD_Dilution_Leave);
			// 
			// bttnLoadSample
			// 
			this.bttnLoadSample.BackColor = System.Drawing.SystemColors.Control;
			this.bttnLoadSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnLoadSample.Location = new System.Drawing.Point(12, 9);
			this.bttnLoadSample.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnLoadSample.Name = "bttnLoadSample";
			this.bttnLoadSample.Size = new System.Drawing.Size(80, 25);
			this.bttnLoadSample.TabIndex = 40;
			this.bttnLoadSample.Text = "Load";
			this.bttnLoadSample.UseVisualStyleBackColor = false;
			this.bttnLoadSample.Click += new System.EventHandler(this.bttnLoadSample_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(140, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(55, 16);
			this.label7.TabIndex = 24;
			this.label7.Text = "Dilution:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtSampleName
			// 
			this.txtSampleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSampleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleName.Location = new System.Drawing.Point(211, 12);
			this.txtSampleName.Name = "txtSampleName";
			this.txtSampleName.Size = new System.Drawing.Size(209, 22);
			this.txtSampleName.TabIndex = 19;
			this.txtSampleName.Text = "My_Sample";
			// 
			// txtSampleTag
			// 
			this.txtSampleTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSampleTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleTag.Location = new System.Drawing.Point(211, 168);
			this.txtSampleTag.Name = "txtSampleTag";
			this.txtSampleTag.Size = new System.Drawing.Size(209, 22);
			this.txtSampleTag.TabIndex = 21;
			this.txtSampleTag.Text = "My Tag";
			// 
			// bttnStartSample
			// 
			this.bttnStartSample.BackColor = System.Drawing.SystemColors.Control;
			this.bttnStartSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnStartSample.Location = new System.Drawing.Point(177, 289);
			this.bttnStartSample.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnStartSample.Name = "bttnStartSample";
			this.bttnStartSample.Size = new System.Drawing.Size(110, 30);
			this.bttnStartSample.TabIndex = 9;
			this.bttnStartSample.Text = "Start Sample";
			this.bttnStartSample.UseVisualStyleBackColor = false;
			this.bttnStartSample.Click += new System.EventHandler(this.bttnStartSample_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(159, 170);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 16);
			this.label6.TabIndex = 20;
			this.label6.Text = "Tag:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(147, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 18;
			this.label5.Text = "Name:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpgMultiSampleSetup
			// 
			this.tpgMultiSampleSetup.BackColor = System.Drawing.SystemColors.Control;
			this.tpgMultiSampleSetup.Controls.Add(this.groupBox4);
			this.tpgMultiSampleSetup.Controls.Add(this.bttnSampleSet_Clear);
			this.tpgMultiSampleSetup.Controls.Add(this.bttnSaveSet);
			this.tpgMultiSampleSetup.Controls.Add(this.bttnLoadSet);
			this.tpgMultiSampleSetup.Controls.Add(this.dgvSet);
			this.tpgMultiSampleSetup.Controls.Add(this.txtSetName);
			this.tpgMultiSampleSetup.Controls.Add(this.label11);
			this.tpgMultiSampleSetup.Controls.Add(this.bttnStartSet);
			this.tpgMultiSampleSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgMultiSampleSetup.Location = new System.Drawing.Point(4, 25);
			this.tpgMultiSampleSetup.Name = "tpgMultiSampleSetup";
			this.tpgMultiSampleSetup.Padding = new System.Windows.Forms.Padding(3);
			this.tpgMultiSampleSetup.Size = new System.Drawing.Size(741, 332);
			this.tpgMultiSampleSetup.TabIndex = 1;
			this.tpgMultiSampleSetup.Text = "Sample Set";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.rdoSortColumn);
			this.groupBox4.Controls.Add(this.rdoSortRow);
			this.groupBox4.Location = new System.Drawing.Point(440, 9);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(101, 69);
			this.groupBox4.TabIndex = 47;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Sort Order";
			// 
			// rdoSortColumn
			// 
			this.rdoSortColumn.AutoSize = true;
			this.rdoSortColumn.Location = new System.Drawing.Point(10, 45);
			this.rdoSortColumn.Name = "rdoSortColumn";
			this.rdoSortColumn.Size = new System.Drawing.Size(71, 20);
			this.rdoSortColumn.TabIndex = 1;
			this.rdoSortColumn.Text = "Column";
			this.rdoSortColumn.UseVisualStyleBackColor = true;
			// 
			// rdoSortRow
			// 
			this.rdoSortRow.AutoSize = true;
			this.rdoSortRow.Checked = true;
			this.rdoSortRow.Location = new System.Drawing.Point(10, 19);
			this.rdoSortRow.Name = "rdoSortRow";
			this.rdoSortRow.Size = new System.Drawing.Size(53, 20);
			this.rdoSortRow.TabIndex = 0;
			this.rdoSortRow.TabStop = true;
			this.rdoSortRow.Text = "Row";
			this.rdoSortRow.UseVisualStyleBackColor = true;
			// 
			// bttnSampleSet_Clear
			// 
			this.bttnSampleSet_Clear.BackColor = System.Drawing.SystemColors.Control;
			this.bttnSampleSet_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSampleSet_Clear.Location = new System.Drawing.Point(108, 38);
			this.bttnSampleSet_Clear.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnSampleSet_Clear.Name = "bttnSampleSet_Clear";
			this.bttnSampleSet_Clear.Size = new System.Drawing.Size(80, 25);
			this.bttnSampleSet_Clear.TabIndex = 46;
			this.bttnSampleSet_Clear.Text = "Clear";
			this.bttnSampleSet_Clear.UseVisualStyleBackColor = false;
			this.bttnSampleSet_Clear.Click += new System.EventHandler(this.bttnSampleSet_Clear_Click);
			// 
			// bttnSaveSet
			// 
			this.bttnSaveSet.BackColor = System.Drawing.SystemColors.Control;
			this.bttnSaveSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSaveSet.Location = new System.Drawing.Point(6, 38);
			this.bttnSaveSet.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnSaveSet.Name = "bttnSaveSet";
			this.bttnSaveSet.Size = new System.Drawing.Size(80, 25);
			this.bttnSaveSet.TabIndex = 45;
			this.bttnSaveSet.Text = "Save";
			this.bttnSaveSet.UseVisualStyleBackColor = false;
			this.bttnSaveSet.Click += new System.EventHandler(this.bttnSaveSet_Click);
			// 
			// bttnLoadSet
			// 
			this.bttnLoadSet.BackColor = System.Drawing.SystemColors.Control;
			this.bttnLoadSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnLoadSet.Location = new System.Drawing.Point(6, 9);
			this.bttnLoadSet.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnLoadSet.Name = "bttnLoadSet";
			this.bttnLoadSet.Size = new System.Drawing.Size(80, 25);
			this.bttnLoadSet.TabIndex = 44;
			this.bttnLoadSet.Text = "Load";
			this.bttnLoadSet.UseVisualStyleBackColor = false;
			this.bttnLoadSet.Click += new System.EventHandler(this.bttnLoadSet_Click);
			// 
			// dgvSet
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dgvSet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSet.Location = new System.Drawing.Point(3, 84);
			this.dgvSet.Name = "dgvSet";
			this.dgvSet.RowHeadersWidth = 51;
			this.dgvSet.Size = new System.Drawing.Size(732, 309);
			this.dgvSet.TabIndex = 22;
			// 
			// txtSetName
			// 
			this.txtSetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSetName.Location = new System.Drawing.Point(208, 32);
			this.txtSetName.Name = "txtSetName";
			this.txtSetName.Size = new System.Drawing.Size(209, 22);
			this.txtSetName.TabIndex = 21;
			this.txtSetName.Text = "My_Set";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(205, 13);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 16);
			this.label11.TabIndex = 20;
			this.label11.Text = "Name:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bttnStartSet
			// 
			this.bttnStartSet.BackColor = System.Drawing.SystemColors.Control;
			this.bttnStartSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnStartSet.Location = new System.Drawing.Point(619, 20);
			this.bttnStartSet.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnStartSet.Name = "bttnStartSet";
			this.bttnStartSet.Size = new System.Drawing.Size(110, 30);
			this.bttnStartSet.TabIndex = 10;
			this.bttnStartSet.Text = "Start Set";
			this.bttnStartSet.UseVisualStyleBackColor = false;
			this.bttnStartSet.Click += new System.EventHandler(this.bttnStartSet_Click);
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Transparent;
			this.tabPage1.Controls.Add(this.label38);
			this.tabPage1.Controls.Add(this.WorklistBox);
			this.tabPage1.Controls.Add(this.groupBox6);
			this.tabPage1.Controls.Add(this.groupBox5);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage1.Size = new System.Drawing.Size(741, 332);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Sample Status";
			// 
			// label38
			// 
			this.label38.Location = new System.Drawing.Point(449, 116);
			this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(184, 22);
			this.label38.TabIndex = 3;
			this.label38.Text = "Completed Worklist ";
			// 
			// WorklistBox
			// 
			this.WorklistBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WorklistBox.FormattingEnabled = true;
			this.WorklistBox.ItemHeight = 16;
			this.WorklistBox.Location = new System.Drawing.Point(449, 141);
			this.WorklistBox.Margin = new System.Windows.Forms.Padding(2);
			this.WorklistBox.Name = "WorklistBox";
			this.WorklistBox.Size = new System.Drawing.Size(269, 244);
			this.WorklistBox.TabIndex = 2;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.lstSampleCompleteData);
			this.groupBox6.Controls.Add(this.LastSampleTxt);
			this.groupBox6.Location = new System.Drawing.Point(4, 103);
			this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox6.Size = new System.Drawing.Size(428, 292);
			this.groupBox6.TabIndex = 1;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Last Sample";
			// 
			// lstSampleCompleteData
			// 
			this.lstSampleCompleteData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstSampleCompleteData.FormattingEnabled = true;
			this.lstSampleCompleteData.ItemHeight = 16;
			this.lstSampleCompleteData.Location = new System.Drawing.Point(4, 22);
			this.lstSampleCompleteData.Margin = new System.Windows.Forms.Padding(2);
			this.lstSampleCompleteData.Name = "lstSampleCompleteData";
			this.lstSampleCompleteData.ScrollAlwaysVisible = true;
			this.lstSampleCompleteData.Size = new System.Drawing.Size(420, 228);
			this.lstSampleCompleteData.TabIndex = 61;
			this.lstSampleCompleteData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstSampleCompleteData_MouseUp);
			// 
			// LastSampleTxt
			// 
			this.LastSampleTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LastSampleTxt.Location = new System.Drawing.Point(4, 20);
			this.LastSampleTxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.LastSampleTxt.Name = "LastSampleTxt";
			this.LastSampleTxt.Size = new System.Drawing.Size(420, 69);
			this.LastSampleTxt.TabIndex = 0;
			this.LastSampleTxt.Text = " ";
			this.LastSampleTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.label55);
			this.groupBox5.Controls.Add(this.label54);
			this.groupBox5.Controls.Add(this.label53);
			this.groupBox5.Controls.Add(this.CurrentSampleStatus);
			this.groupBox5.Controls.Add(this.CurrentSamplePosition);
			this.groupBox5.Controls.Add(this.CurrentSampleOwner);
			this.groupBox5.Controls.Add(this.CurrentSampleId);
			this.groupBox5.Controls.Add(this.label1);
			this.groupBox5.Location = new System.Drawing.Point(15, 14);
			this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox5.Size = new System.Drawing.Size(710, 78);
			this.groupBox5.TabIndex = 0;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Current Sample";
			// 
			// label55
			// 
			this.label55.AutoSize = true;
			this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label55.Location = new System.Drawing.Point(595, 22);
			this.label55.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(46, 16);
			this.label55.TabIndex = 23;
			this.label55.Text = "Owner";
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label54.Location = new System.Drawing.Point(432, 22);
			this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(56, 16);
			this.label54.TabIndex = 22;
			this.label54.Text = "Position";
			// 
			// label53
			// 
			this.label53.AutoSize = true;
			this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label53.Location = new System.Drawing.Point(262, 22);
			this.label53.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(45, 16);
			this.label53.TabIndex = 21;
			this.label53.Text = "Status";
			// 
			// CurrentSampleStatus
			// 
			this.CurrentSampleStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CurrentSampleStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CurrentSampleStatus.Location = new System.Drawing.Point(184, 43);
			this.CurrentSampleStatus.Name = "CurrentSampleStatus";
			this.CurrentSampleStatus.Size = new System.Drawing.Size(193, 25);
			this.CurrentSampleStatus.TabIndex = 20;
			this.CurrentSampleStatus.Text = "---";
			this.CurrentSampleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CurrentSamplePosition
			// 
			this.CurrentSamplePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CurrentSamplePosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CurrentSamplePosition.Location = new System.Drawing.Point(398, 43);
			this.CurrentSamplePosition.Name = "CurrentSamplePosition";
			this.CurrentSamplePosition.Size = new System.Drawing.Size(117, 25);
			this.CurrentSamplePosition.TabIndex = 19;
			this.CurrentSamplePosition.Text = "---";
			this.CurrentSamplePosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CurrentSampleOwner
			// 
			this.CurrentSampleOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CurrentSampleOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CurrentSampleOwner.Location = new System.Drawing.Point(536, 43);
			this.CurrentSampleOwner.Name = "CurrentSampleOwner";
			this.CurrentSampleOwner.Size = new System.Drawing.Size(153, 25);
			this.CurrentSampleOwner.TabIndex = 18;
			this.CurrentSampleOwner.Text = "---";
			this.CurrentSampleOwner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CurrentSampleId
			// 
			this.CurrentSampleId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CurrentSampleId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CurrentSampleId.Location = new System.Drawing.Point(22, 43);
			this.CurrentSampleId.Name = "CurrentSampleId";
			this.CurrentSampleId.Size = new System.Drawing.Size(140, 25);
			this.CurrentSampleId.TabIndex = 17;
			this.CurrentSampleId.Text = "---";
			this.CurrentSampleId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(64, 22);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sample ID";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.bttnSampleResume);
			this.groupBox1.Controls.Add(this.bttnSamplePause);
			this.groupBox1.Controls.Add(this.bttnSampleStop);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(758, 19);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(130, 340);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Run Control";
			// 
			// bttnSampleResume
			// 
			this.bttnSampleResume.BackColor = System.Drawing.SystemColors.Control;
			this.bttnSampleResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSampleResume.Location = new System.Drawing.Point(12, 160);
			this.bttnSampleResume.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnSampleResume.Name = "bttnSampleResume";
			this.bttnSampleResume.Size = new System.Drawing.Size(100, 35);
			this.bttnSampleResume.TabIndex = 11;
			this.bttnSampleResume.Text = "Resume";
			this.bttnSampleResume.UseVisualStyleBackColor = false;
			this.bttnSampleResume.Click += new System.EventHandler(this.bttnSampleResume_Click);
			// 
			// bttnSamplePause
			// 
			this.bttnSamplePause.BackColor = System.Drawing.SystemColors.Control;
			this.bttnSamplePause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSamplePause.Location = new System.Drawing.Point(12, 88);
			this.bttnSamplePause.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnSamplePause.Name = "bttnSamplePause";
			this.bttnSamplePause.Size = new System.Drawing.Size(100, 35);
			this.bttnSamplePause.TabIndex = 10;
			this.bttnSamplePause.Text = "Pause";
			this.bttnSamplePause.UseVisualStyleBackColor = false;
			this.bttnSamplePause.Click += new System.EventHandler(this.bttnSamplePause_Click);
			// 
			// bttnSampleStop
			// 
			this.bttnSampleStop.BackColor = System.Drawing.SystemColors.Control;
			this.bttnSampleStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnSampleStop.Location = new System.Drawing.Point(12, 236);
			this.bttnSampleStop.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnSampleStop.Name = "bttnSampleStop";
			this.bttnSampleStop.Size = new System.Drawing.Size(100, 35);
			this.bttnSampleStop.TabIndex = 9;
			this.bttnSampleStop.Text = "Stop";
			this.bttnSampleStop.UseVisualStyleBackColor = false;
			this.bttnSampleStop.Click += new System.EventHandler(this.bttnSampleStop_Click);
			// 
			// tpgCellTypes
			// 
			this.tpgCellTypes.BackColor = System.Drawing.SystemColors.Control;
			this.tpgCellTypes.Controls.Add(this.bttnCT_Refresh);
			this.tpgCellTypes.Controls.Add(this.label30);
			this.tpgCellTypes.Controls.Add(this.lstCellTypes);
			this.tpgCellTypes.Controls.Add(this.bttnCT_Delete);
			this.tpgCellTypes.Controls.Add(this.grpCT_Create);
			this.tpgCellTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgCellTypes.Location = new System.Drawing.Point(4, 25);
			this.tpgCellTypes.Name = "tpgCellTypes";
			this.tpgCellTypes.Padding = new System.Windows.Forms.Padding(3);
			this.tpgCellTypes.Size = new System.Drawing.Size(923, 395);
			this.tpgCellTypes.TabIndex = 1;
			this.tpgCellTypes.Text = "Cell Types";
			// 
			// bttnCT_Refresh
			// 
			this.bttnCT_Refresh.BackColor = System.Drawing.SystemColors.Control;
			this.bttnCT_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnCT_Refresh.Location = new System.Drawing.Point(808, 9);
			this.bttnCT_Refresh.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnCT_Refresh.Name = "bttnCT_Refresh";
			this.bttnCT_Refresh.Size = new System.Drawing.Size(80, 25);
			this.bttnCT_Refresh.TabIndex = 48;
			this.bttnCT_Refresh.Text = "Refresh";
			this.bttnCT_Refresh.UseVisualStyleBackColor = false;
			this.bttnCT_Refresh.Click += new System.EventHandler(this.bttnCT_Refresh_Click);
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label30.Location = new System.Drawing.Point(684, 40);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(133, 16);
			this.label30.TabIndex = 47;
			this.label30.Text = "Available Cell Types";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lstCellTypes
			// 
			this.lstCellTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstCellTypes.FormattingEnabled = true;
			this.lstCellTypes.ItemHeight = 16;
			this.lstCellTypes.Location = new System.Drawing.Point(684, 63);
			this.lstCellTypes.Name = "lstCellTypes";
			this.lstCellTypes.Size = new System.Drawing.Size(204, 244);
			this.lstCellTypes.TabIndex = 45;
			this.lstCellTypes.SelectedIndexChanged += new System.EventHandler(this.lstCellTypes_SelectedIndexChanged);
			// 
			// bttnCT_Delete
			// 
			this.bttnCT_Delete.BackColor = System.Drawing.SystemColors.Control;
			this.bttnCT_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnCT_Delete.Location = new System.Drawing.Point(735, 341);
			this.bttnCT_Delete.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnCT_Delete.Name = "bttnCT_Delete";
			this.bttnCT_Delete.Size = new System.Drawing.Size(100, 35);
			this.bttnCT_Delete.TabIndex = 43;
			this.bttnCT_Delete.Text = "Delete";
			this.bttnCT_Delete.UseVisualStyleBackColor = false;
			this.bttnCT_Delete.Click += new System.EventHandler(this.bttnCT_Delete_Click);
			// 
			// grpCT_Create
			// 
			this.grpCT_Create.BackColor = System.Drawing.SystemColors.Control;
			this.grpCT_Create.Controls.Add(this.bttnCTCreate_Save);
			this.grpCT_Create.Controls.Add(this.bttnCTCreate_Load);
			this.grpCT_Create.Controls.Add(this.numUDCT_ConcAdjFactor);
			this.grpCT_Create.Controls.Add(this.label27);
			this.grpCT_Create.Controls.Add(this.label25);
			this.grpCT_Create.Controls.Add(this.numUDCT_ViaSpotArea);
			this.grpCT_Create.Controls.Add(this.label26);
			this.grpCT_Create.Controls.Add(this.label24);
			this.grpCT_Create.Controls.Add(this.numUDCT_ViaSpotBright);
			this.grpCT_Create.Controls.Add(this.label23);
			this.grpCT_Create.Controls.Add(this.numUDCT_MixCycles);
			this.grpCT_Create.Controls.Add(this.label22);
			this.grpCT_Create.Controls.Add(this.numUDCT_AspCycles);
			this.grpCT_Create.Controls.Add(this.label21);
			this.grpCT_Create.Controls.Add(this.cbxCT_Decluster);
			this.grpCT_Create.Controls.Add(this.label20);
			this.grpCT_Create.Controls.Add(this.numUDCT_MinCirc);
			this.grpCT_Create.Controls.Add(this.label17);
			this.grpCT_Create.Controls.Add(this.numUDCT_Sharpness);
			this.grpCT_Create.Controls.Add(this.label19);
			this.grpCT_Create.Controls.Add(this.numUDCT_Images);
			this.grpCT_Create.Controls.Add(this.label18);
			this.grpCT_Create.Controls.Add(this.numUDCT_MaxDiam);
			this.grpCT_Create.Controls.Add(this.label15);
			this.grpCT_Create.Controls.Add(this.numUDCT_MinDiam);
			this.grpCT_Create.Controls.Add(this.label2);
			this.grpCT_Create.Controls.Add(this.txtCTCreate_Name);
			this.grpCT_Create.Controls.Add(this.label16);
			this.grpCT_Create.Controls.Add(this.bttnCT_Create);
			this.grpCT_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpCT_Create.Location = new System.Drawing.Point(17, 18);
			this.grpCT_Create.Name = "grpCT_Create";
			this.grpCT_Create.Size = new System.Drawing.Size(586, 354);
			this.grpCT_Create.TabIndex = 42;
			this.grpCT_Create.TabStop = false;
			this.grpCT_Create.Text = "Create Cell Type";
			// 
			// bttnCTCreate_Save
			// 
			this.bttnCTCreate_Save.BackColor = System.Drawing.SystemColors.Control;
			this.bttnCTCreate_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnCTCreate_Save.Location = new System.Drawing.Point(12, 70);
			this.bttnCTCreate_Save.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnCTCreate_Save.Name = "bttnCTCreate_Save";
			this.bttnCTCreate_Save.Size = new System.Drawing.Size(80, 25);
			this.bttnCTCreate_Save.TabIndex = 54;
			this.bttnCTCreate_Save.Text = "Save";
			this.bttnCTCreate_Save.UseVisualStyleBackColor = false;
			this.bttnCTCreate_Save.Click += new System.EventHandler(this.bttnCTCreate_Save_Click);
			// 
			// bttnCTCreate_Load
			// 
			this.bttnCTCreate_Load.BackColor = System.Drawing.SystemColors.Control;
			this.bttnCTCreate_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnCTCreate_Load.Location = new System.Drawing.Point(12, 34);
			this.bttnCTCreate_Load.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnCTCreate_Load.Name = "bttnCTCreate_Load";
			this.bttnCTCreate_Load.Size = new System.Drawing.Size(80, 25);
			this.bttnCTCreate_Load.TabIndex = 55;
			this.bttnCTCreate_Load.Text = "Load";
			this.bttnCTCreate_Load.UseVisualStyleBackColor = false;
			this.bttnCTCreate_Load.Click += new System.EventHandler(this.bttnCTCreate_Load_Click);
			// 
			// numUDCT_ConcAdjFactor
			// 
			this.numUDCT_ConcAdjFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_ConcAdjFactor.DecimalPlaces = 1;
			this.numUDCT_ConcAdjFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_ConcAdjFactor.Location = new System.Drawing.Point(395, 183);
			this.numUDCT_ConcAdjFactor.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_ConcAdjFactor.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_ConcAdjFactor.Name = "numUDCT_ConcAdjFactor";
			this.numUDCT_ConcAdjFactor.Size = new System.Drawing.Size(67, 26);
			this.numUDCT_ConcAdjFactor.TabIndex = 53;
			this.numUDCT_ConcAdjFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_ConcAdjFactor.ThousandsSeparator = true;
			this.numUDCT_ConcAdjFactor.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_ConcAdjFactor.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numUDCT_ConcAdjFactor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_ConcAdjFactor_KeyUp);
			this.numUDCT_ConcAdjFactor.Leave += new System.EventHandler(this.numUDCT_ConcAdjFactor_Leave);
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label27.Location = new System.Drawing.Point(354, 161);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(200, 16);
			this.label27.TabIndex = 52;
			this.label27.Text = "Concentration Adjustment Factor";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label25.Location = new System.Drawing.Point(466, 118);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(20, 16);
			this.label25.TabIndex = 51;
			this.label25.Text = "%";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_ViaSpotArea
			// 
			this.numUDCT_ViaSpotArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_ViaSpotArea.DecimalPlaces = 1;
			this.numUDCT_ViaSpotArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_ViaSpotArea.Location = new System.Drawing.Point(393, 113);
			this.numUDCT_ViaSpotArea.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_ViaSpotArea.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_ViaSpotArea.Name = "numUDCT_ViaSpotArea";
			this.numUDCT_ViaSpotArea.Size = new System.Drawing.Size(67, 26);
			this.numUDCT_ViaSpotArea.TabIndex = 50;
			this.numUDCT_ViaSpotArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_ViaSpotArea.ThousandsSeparator = true;
			this.numUDCT_ViaSpotArea.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_ViaSpotArea.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numUDCT_ViaSpotArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_ViaSpotArea_KeyUp);
			this.numUDCT_ViaSpotArea.Leave += new System.EventHandler(this.numUDCT_ViaSpotArea_Leave);
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label26.Location = new System.Drawing.Point(367, 94);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(110, 16);
			this.label26.TabIndex = 49;
			this.label26.Text = "Viable Spot Area";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.Location = new System.Drawing.Point(466, 61);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(20, 16);
			this.label24.TabIndex = 48;
			this.label24.Text = "%";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_ViaSpotBright
			// 
			this.numUDCT_ViaSpotBright.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_ViaSpotBright.DecimalPlaces = 1;
			this.numUDCT_ViaSpotBright.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_ViaSpotBright.Location = new System.Drawing.Point(393, 56);
			this.numUDCT_ViaSpotBright.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_ViaSpotBright.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_ViaSpotBright.Name = "numUDCT_ViaSpotBright";
			this.numUDCT_ViaSpotBright.Size = new System.Drawing.Size(67, 26);
			this.numUDCT_ViaSpotBright.TabIndex = 47;
			this.numUDCT_ViaSpotBright.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_ViaSpotBright.ThousandsSeparator = true;
			this.numUDCT_ViaSpotBright.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_ViaSpotBright.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numUDCT_ViaSpotBright.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_ViaSpotBright_KeyUp);
			this.numUDCT_ViaSpotBright.Leave += new System.EventHandler(this.numUDCT_ViaSpotBright_Leave);
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label23.Location = new System.Drawing.Point(367, 37);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(144, 16);
			this.label23.TabIndex = 46;
			this.label23.Text = "Viable Spot Brightness";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_MixCycles
			// 
			this.numUDCT_MixCycles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_MixCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_MixCycles.Location = new System.Drawing.Point(197, 293);
			this.numUDCT_MixCycles.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_MixCycles.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_MixCycles.Name = "numUDCT_MixCycles";
			this.numUDCT_MixCycles.Size = new System.Drawing.Size(49, 26);
			this.numUDCT_MixCycles.TabIndex = 45;
			this.numUDCT_MixCycles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_MixCycles.ThousandsSeparator = true;
			this.numUDCT_MixCycles.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_MixCycles.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numUDCT_MixCycles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_MixCycles_KeyUp);
			this.numUDCT_MixCycles.Leave += new System.EventHandler(this.numUDCT_MixCycles_Leave);
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label22.Location = new System.Drawing.Point(101, 298);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(90, 16);
			this.label22.TabIndex = 44;
			this.label22.Text = "Mixing Cycles";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_AspCycles
			// 
			this.numUDCT_AspCycles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_AspCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_AspCycles.Location = new System.Drawing.Point(197, 259);
			this.numUDCT_AspCycles.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_AspCycles.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_AspCycles.Name = "numUDCT_AspCycles";
			this.numUDCT_AspCycles.Size = new System.Drawing.Size(49, 26);
			this.numUDCT_AspCycles.TabIndex = 43;
			this.numUDCT_AspCycles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_AspCycles.ThousandsSeparator = true;
			this.numUDCT_AspCycles.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_AspCycles.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numUDCT_AspCycles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_AspCycles_KeyUp);
			this.numUDCT_AspCycles.Leave += new System.EventHandler(this.numUDCT_AspCycles_Leave);
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label21.Location = new System.Drawing.Point(79, 264);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(112, 16);
			this.label21.TabIndex = 42;
			this.label21.Text = "Aspiration Cycles";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbxCT_Decluster
			// 
			this.cbxCT_Decluster.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxCT_Decluster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.cbxCT_Decluster.FormattingEnabled = true;
			this.cbxCT_Decluster.Items.AddRange(new object[] {
            "None",
            "Low",
            "Medium",
            "High"});
			this.cbxCT_Decluster.Location = new System.Drawing.Point(199, 225);
			this.cbxCT_Decluster.Name = "cbxCT_Decluster";
			this.cbxCT_Decluster.Size = new System.Drawing.Size(121, 24);
			this.cbxCT_Decluster.TabIndex = 41;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label20.Location = new System.Drawing.Point(116, 235);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(65, 16);
			this.label20.TabIndex = 40;
			this.label20.Text = "Decluster";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_MinCirc
			// 
			this.numUDCT_MinCirc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_MinCirc.DecimalPlaces = 2;
			this.numUDCT_MinCirc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_MinCirc.Location = new System.Drawing.Point(199, 193);
			this.numUDCT_MinCirc.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_MinCirc.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_MinCirc.Name = "numUDCT_MinCirc";
			this.numUDCT_MinCirc.Size = new System.Drawing.Size(77, 26);
			this.numUDCT_MinCirc.TabIndex = 39;
			this.numUDCT_MinCirc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_MinCirc.ThousandsSeparator = true;
			this.numUDCT_MinCirc.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_MinCirc.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			this.numUDCT_MinCirc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_MinCirc_KeyUp);
			this.numUDCT_MinCirc.Leave += new System.EventHandler(this.numUDCT_MinCirc_Leave);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.Location = new System.Drawing.Point(91, 198);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(90, 16);
			this.label17.TabIndex = 38;
			this.label17.Text = "Min Circularity";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_Sharpness
			// 
			this.numUDCT_Sharpness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_Sharpness.DecimalPlaces = 1;
			this.numUDCT_Sharpness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_Sharpness.Location = new System.Drawing.Point(199, 161);
			this.numUDCT_Sharpness.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_Sharpness.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_Sharpness.Name = "numUDCT_Sharpness";
			this.numUDCT_Sharpness.Size = new System.Drawing.Size(77, 26);
			this.numUDCT_Sharpness.TabIndex = 37;
			this.numUDCT_Sharpness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_Sharpness.ThousandsSeparator = true;
			this.numUDCT_Sharpness.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_Sharpness.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numUDCT_Sharpness.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_Sharpness_KeyUp);
			this.numUDCT_Sharpness.Leave += new System.EventHandler(this.numUDCT_Sharpness_Leave);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(111, 166);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(76, 16);
			this.label19.TabIndex = 36;
			this.label19.Text = "Sharpness:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_Images
			// 
			this.numUDCT_Images.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_Images.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_Images.Location = new System.Drawing.Point(199, 129);
			this.numUDCT_Images.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_Images.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numUDCT_Images.Name = "numUDCT_Images";
			this.numUDCT_Images.Size = new System.Drawing.Size(77, 26);
			this.numUDCT_Images.TabIndex = 35;
			this.numUDCT_Images.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_Images.ThousandsSeparator = true;
			this.numUDCT_Images.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_Images.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numUDCT_Images.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_Images_KeyUp);
			this.numUDCT_Images.Leave += new System.EventHandler(this.numUDCT_Images_Leave);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.Location = new System.Drawing.Point(135, 134);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(56, 16);
			this.label18.TabIndex = 34;
			this.label18.Text = "Images:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_MaxDiam
			// 
			this.numUDCT_MaxDiam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_MaxDiam.DecimalPlaces = 2;
			this.numUDCT_MaxDiam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_MaxDiam.Location = new System.Drawing.Point(199, 97);
			this.numUDCT_MaxDiam.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_MaxDiam.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_MaxDiam.Name = "numUDCT_MaxDiam";
			this.numUDCT_MaxDiam.Size = new System.Drawing.Size(77, 26);
			this.numUDCT_MaxDiam.TabIndex = 33;
			this.numUDCT_MaxDiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_MaxDiam.ThousandsSeparator = true;
			this.numUDCT_MaxDiam.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_MaxDiam.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numUDCT_MaxDiam.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_MaxDiam_KeyUp);
			this.numUDCT_MaxDiam.Leave += new System.EventHandler(this.numUDCT_MaxDiam_Leave);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(116, 102);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(71, 16);
			this.label15.TabIndex = 32;
			this.label15.Text = "Max Diam:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDCT_MinDiam
			// 
			this.numUDCT_MinDiam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDCT_MinDiam.DecimalPlaces = 2;
			this.numUDCT_MinDiam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDCT_MinDiam.Location = new System.Drawing.Point(199, 65);
			this.numUDCT_MinDiam.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDCT_MinDiam.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDCT_MinDiam.Name = "numUDCT_MinDiam";
			this.numUDCT_MinDiam.Size = new System.Drawing.Size(77, 26);
			this.numUDCT_MinDiam.TabIndex = 31;
			this.numUDCT_MinDiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDCT_MinDiam.ThousandsSeparator = true;
			this.numUDCT_MinDiam.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDCT_MinDiam.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numUDCT_MinDiam.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDCT_MinDiam_KeyUp);
			this.numUDCT_MinDiam.Leave += new System.EventHandler(this.numUDCT_MinDiam_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(126, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 16);
			this.label2.TabIndex = 30;
			this.label2.Text = "Min Diam:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCTCreate_Name
			// 
			this.txtCTCreate_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCTCreate_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCTCreate_Name.Location = new System.Drawing.Point(199, 36);
			this.txtCTCreate_Name.Name = "txtCTCreate_Name";
			this.txtCTCreate_Name.Size = new System.Drawing.Size(150, 22);
			this.txtCTCreate_Name.TabIndex = 29;
			this.txtCTCreate_Name.Text = "My Cell Type";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(145, 38);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(48, 16);
			this.label16.TabIndex = 28;
			this.label16.Text = "Name:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bttnCT_Create
			// 
			this.bttnCT_Create.BackColor = System.Drawing.SystemColors.Control;
			this.bttnCT_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnCT_Create.Location = new System.Drawing.Point(345, 275);
			this.bttnCT_Create.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnCT_Create.Name = "bttnCT_Create";
			this.bttnCT_Create.Size = new System.Drawing.Size(184, 39);
			this.bttnCT_Create.TabIndex = 11;
			this.bttnCT_Create.Text = "Create/Modify Cell Type";
			this.bttnCT_Create.UseVisualStyleBackColor = false;
			this.bttnCT_Create.Click += new System.EventHandler(this.bttnCT_Create_Click);
			// 
			// tpgQualityControls
			// 
			this.tpgQualityControls.BackColor = System.Drawing.SystemColors.Control;
			this.tpgQualityControls.Controls.Add(this.bttnQC_Refresh);
			this.tpgQualityControls.Controls.Add(this.label31);
			this.tpgQualityControls.Controls.Add(this.lstQualityControls);
			this.tpgQualityControls.Controls.Add(this.groupBox2);
			this.tpgQualityControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgQualityControls.Location = new System.Drawing.Point(4, 25);
			this.tpgQualityControls.Name = "tpgQualityControls";
			this.tpgQualityControls.Size = new System.Drawing.Size(923, 395);
			this.tpgQualityControls.TabIndex = 2;
			this.tpgQualityControls.Text = "QC s";
			// 
			// bttnQC_Refresh
			// 
			this.bttnQC_Refresh.BackColor = System.Drawing.SystemColors.Control;
			this.bttnQC_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnQC_Refresh.Location = new System.Drawing.Point(799, 13);
			this.bttnQC_Refresh.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnQC_Refresh.Name = "bttnQC_Refresh";
			this.bttnQC_Refresh.Size = new System.Drawing.Size(80, 25);
			this.bttnQC_Refresh.TabIndex = 50;
			this.bttnQC_Refresh.Text = "Refresh";
			this.bttnQC_Refresh.UseVisualStyleBackColor = false;
			this.bttnQC_Refresh.Click += new System.EventHandler(this.bttnQC_Refresh_Click);
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label31.Location = new System.Drawing.Point(672, 50);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(161, 16);
			this.label31.TabIndex = 49;
			this.label31.Text = "Available Quality Controls";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lstQualityControls
			// 
			this.lstQualityControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstQualityControls.FormattingEnabled = true;
			this.lstQualityControls.ItemHeight = 16;
			this.lstQualityControls.Location = new System.Drawing.Point(675, 73);
			this.lstQualityControls.Name = "lstQualityControls";
			this.lstQualityControls.Size = new System.Drawing.Size(204, 276);
			this.lstQualityControls.TabIndex = 48;
			this.lstQualityControls.SelectedIndexChanged += new System.EventHandler(this.lstQualityControls_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.dtpQCExpires);
			this.groupBox2.Controls.Add(this.label29);
			this.groupBox2.Controls.Add(this.txtQCCreate_Comment);
			this.groupBox2.Controls.Add(this.label28);
			this.groupBox2.Controls.Add(this.txtQCCreate_LotNumber);
			this.groupBox2.Controls.Add(this.label39);
			this.groupBox2.Controls.Add(this.txtQCCreate_CellTypeName);
			this.groupBox2.Controls.Add(this.bttnQC_Save);
			this.groupBox2.Controls.Add(this.bttnQC_Load);
			this.groupBox2.Controls.Add(this.label33);
			this.groupBox2.Controls.Add(this.cbxQC_AssayParam);
			this.groupBox2.Controls.Add(this.label34);
			this.groupBox2.Controls.Add(this.numUDQC_AssayValue);
			this.groupBox2.Controls.Add(this.label35);
			this.groupBox2.Controls.Add(this.numUDQC_AcceptLimits);
			this.groupBox2.Controls.Add(this.txtQCCreate_Name);
			this.groupBox2.Controls.Add(this.label40);
			this.groupBox2.Controls.Add(this.bttnQC_Create);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(19, 13);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(641, 336);
			this.groupBox2.TabIndex = 43;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Create Quality Control";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(149, 254);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(99, 16);
			this.label12.TabIndex = 63;
			this.label12.Text = "Expiration Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpQCExpires
			// 
			this.dtpQCExpires.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpQCExpires.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpQCExpires.Location = new System.Drawing.Point(254, 249);
			this.dtpQCExpires.Name = "dtpQCExpires";
			this.dtpQCExpires.Size = new System.Drawing.Size(251, 22);
			this.dtpQCExpires.TabIndex = 62;
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label29.Location = new System.Drawing.Point(173, 297);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(75, 16);
			this.label29.TabIndex = 61;
			this.label29.Text = "Comments:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtQCCreate_Comment
			// 
			this.txtQCCreate_Comment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtQCCreate_Comment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQCCreate_Comment.Location = new System.Drawing.Point(254, 295);
			this.txtQCCreate_Comment.Name = "txtQCCreate_Comment";
			this.txtQCCreate_Comment.Size = new System.Drawing.Size(150, 22);
			this.txtQCCreate_Comment.TabIndex = 60;
			this.txtQCCreate_Comment.Text = "Comment Here";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label28.Location = new System.Drawing.Point(349, 74);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(80, 16);
			this.label28.TabIndex = 59;
			this.label28.Text = "Lot Number:";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtQCCreate_LotNumber
			// 
			this.txtQCCreate_LotNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtQCCreate_LotNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQCCreate_LotNumber.Location = new System.Drawing.Point(319, 93);
			this.txtQCCreate_LotNumber.Name = "txtQCCreate_LotNumber";
			this.txtQCCreate_LotNumber.Size = new System.Drawing.Size(150, 22);
			this.txtQCCreate_LotNumber.TabIndex = 58;
			this.txtQCCreate_LotNumber.Text = "Lot Num";
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label39.Location = new System.Drawing.Point(153, 74);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(109, 16);
			this.label39.TabIndex = 57;
			this.label39.Text = "Cell Type Name:";
			this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtQCCreate_CellTypeName
			// 
			this.txtQCCreate_CellTypeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtQCCreate_CellTypeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQCCreate_CellTypeName.Location = new System.Drawing.Point(138, 93);
			this.txtQCCreate_CellTypeName.Name = "txtQCCreate_CellTypeName";
			this.txtQCCreate_CellTypeName.Size = new System.Drawing.Size(150, 22);
			this.txtQCCreate_CellTypeName.TabIndex = 56;
			this.txtQCCreate_CellTypeName.Text = "Cell Type Name";
			// 
			// bttnQC_Save
			// 
			this.bttnQC_Save.BackColor = System.Drawing.SystemColors.Control;
			this.bttnQC_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnQC_Save.Location = new System.Drawing.Point(12, 70);
			this.bttnQC_Save.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnQC_Save.Name = "bttnQC_Save";
			this.bttnQC_Save.Size = new System.Drawing.Size(80, 25);
			this.bttnQC_Save.TabIndex = 54;
			this.bttnQC_Save.Text = "Save";
			this.bttnQC_Save.UseVisualStyleBackColor = false;
			this.bttnQC_Save.Click += new System.EventHandler(this.bttnQC_Save_Click);
			// 
			// bttnQC_Load
			// 
			this.bttnQC_Load.BackColor = System.Drawing.SystemColors.Control;
			this.bttnQC_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnQC_Load.Location = new System.Drawing.Point(12, 33);
			this.bttnQC_Load.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnQC_Load.Name = "bttnQC_Load";
			this.bttnQC_Load.Size = new System.Drawing.Size(80, 25);
			this.bttnQC_Load.TabIndex = 55;
			this.bttnQC_Load.Text = "Load";
			this.bttnQC_Load.UseVisualStyleBackColor = false;
			this.bttnQC_Load.Click += new System.EventHandler(this.bttnQC_Load_Click);
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label33.Location = new System.Drawing.Point(131, 209);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(117, 16);
			this.label33.TabIndex = 42;
			this.label33.Text = "Acceptance Limits";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbxQC_AssayParam
			// 
			this.cbxQC_AssayParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxQC_AssayParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.cbxQC_AssayParam.FormattingEnabled = true;
			this.cbxQC_AssayParam.Items.AddRange(new object[] {
            "Concentration",
            "Percentage",
            "Size"});
			this.cbxQC_AssayParam.Location = new System.Drawing.Point(254, 131);
			this.cbxQC_AssayParam.Name = "cbxQC_AssayParam";
			this.cbxQC_AssayParam.Size = new System.Drawing.Size(116, 24);
			this.cbxQC_AssayParam.TabIndex = 41;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label34.Location = new System.Drawing.Point(136, 137);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(112, 16);
			this.label34.TabIndex = 40;
			this.label34.Text = "Assay Parameter";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDQC_AssayValue
			// 
			this.numUDQC_AssayValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDQC_AssayValue.DecimalPlaces = 2;
			this.numUDQC_AssayValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDQC_AssayValue.Location = new System.Drawing.Point(254, 170);
			this.numUDQC_AssayValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDQC_AssayValue.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDQC_AssayValue.Name = "numUDQC_AssayValue";
			this.numUDQC_AssayValue.Size = new System.Drawing.Size(77, 26);
			this.numUDQC_AssayValue.TabIndex = 39;
			this.numUDQC_AssayValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDQC_AssayValue.ThousandsSeparator = true;
			this.numUDQC_AssayValue.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDQC_AssayValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			this.numUDQC_AssayValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDQC_AssayValue_KeyUp);
			this.numUDQC_AssayValue.Leave += new System.EventHandler(this.numUDQC_AssayValue_Leave);
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label35.Location = new System.Drawing.Point(164, 175);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(84, 16);
			this.label35.TabIndex = 38;
			this.label35.Text = "Assay Value";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numUDQC_AcceptLimits
			// 
			this.numUDQC_AcceptLimits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numUDQC_AcceptLimits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUDQC_AcceptLimits.Location = new System.Drawing.Point(254, 204);
			this.numUDQC_AcceptLimits.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numUDQC_AcceptLimits.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numUDQC_AcceptLimits.Name = "numUDQC_AcceptLimits";
			this.numUDQC_AcceptLimits.Size = new System.Drawing.Size(77, 26);
			this.numUDQC_AcceptLimits.TabIndex = 35;
			this.numUDQC_AcceptLimits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUDQC_AcceptLimits.ThousandsSeparator = true;
			this.numUDQC_AcceptLimits.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.numUDQC_AcceptLimits.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numUDQC_AcceptLimits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUDQC_AcceptLimits_KeyUp);
			this.numUDQC_AcceptLimits.Leave += new System.EventHandler(this.numUDQC_AcceptLimits_Leave);
			// 
			// txtQCCreate_Name
			// 
			this.txtQCCreate_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtQCCreate_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQCCreate_Name.Location = new System.Drawing.Point(140, 37);
			this.txtQCCreate_Name.Name = "txtQCCreate_Name";
			this.txtQCCreate_Name.Size = new System.Drawing.Size(150, 22);
			this.txtQCCreate_Name.TabIndex = 29;
			this.txtQCCreate_Name.Text = "QC Name";
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label40.Location = new System.Drawing.Point(182, 17);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(48, 16);
			this.label40.TabIndex = 28;
			this.label40.Text = "Name:";
			this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bttnQC_Create
			// 
			this.bttnQC_Create.BackColor = System.Drawing.SystemColors.Control;
			this.bttnQC_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnQC_Create.Location = new System.Drawing.Point(529, 254);
			this.bttnQC_Create.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnQC_Create.Name = "bttnQC_Create";
			this.bttnQC_Create.Size = new System.Drawing.Size(100, 62);
			this.bttnQC_Create.TabIndex = 11;
			this.bttnQC_Create.Text = "Create QC";
			this.bttnQC_Create.UseVisualStyleBackColor = false;
			this.bttnQC_Create.Click += new System.EventHandler(this.bttnQC_Create_Click);
			// 
			// tpgResultsConfig
			// 
			this.tpgResultsConfig.BackColor = System.Drawing.Color.Transparent;
			this.tpgResultsConfig.Controls.Add(this.bttnExportConfig);
			this.tpgResultsConfig.Controls.Add(this.bttnImportConfig);
			this.tpgResultsConfig.Controls.Add(this.lstResultsStatus);
			this.tpgResultsConfig.Controls.Add(this.label58);
			this.tpgResultsConfig.Controls.Add(this.tabCtrlResults);
			this.tpgResultsConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgResultsConfig.Location = new System.Drawing.Point(4, 25);
			this.tpgResultsConfig.Name = "tpgResultsConfig";
			this.tpgResultsConfig.Size = new System.Drawing.Size(923, 395);
			this.tpgResultsConfig.TabIndex = 3;
			this.tpgResultsConfig.Text = "Results / Config";
			// 
			// bttnExportConfig
			// 
			this.bttnExportConfig.BackColor = System.Drawing.SystemColors.Control;
			this.bttnExportConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnExportConfig.Location = new System.Drawing.Point(766, 13);
			this.bttnExportConfig.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnExportConfig.Name = "bttnExportConfig";
			this.bttnExportConfig.Size = new System.Drawing.Size(144, 35);
			this.bttnExportConfig.TabIndex = 79;
			this.bttnExportConfig.Text = "Export Config";
			this.bttnExportConfig.UseVisualStyleBackColor = false;
			this.bttnExportConfig.Click += new System.EventHandler(this.bttnExportConfig_Click);
			// 
			// bttnImportConfig
			// 
			this.bttnImportConfig.BackColor = System.Drawing.SystemColors.Control;
			this.bttnImportConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnImportConfig.Location = new System.Drawing.Point(766, 60);
			this.bttnImportConfig.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnImportConfig.Name = "bttnImportConfig";
			this.bttnImportConfig.Size = new System.Drawing.Size(144, 35);
			this.bttnImportConfig.TabIndex = 80;
			this.bttnImportConfig.Text = "Import Config";
			this.bttnImportConfig.UseVisualStyleBackColor = false;
			this.bttnImportConfig.Click += new System.EventHandler(this.bttnImportConfig_Click);
			// 
			// lstResultsStatus
			// 
			this.lstResultsStatus.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstResultsStatus.FormattingEnabled = true;
			this.lstResultsStatus.HorizontalScrollbar = true;
			this.lstResultsStatus.ItemHeight = 15;
			this.lstResultsStatus.Location = new System.Drawing.Point(758, 143);
			this.lstResultsStatus.Margin = new System.Windows.Forms.Padding(2);
			this.lstResultsStatus.Name = "lstResultsStatus";
			this.lstResultsStatus.ScrollAlwaysVisible = true;
			this.lstResultsStatus.Size = new System.Drawing.Size(170, 199);
			this.lstResultsStatus.TabIndex = 48;
			// 
			// label58
			// 
			this.label58.AutoSize = true;
			this.label58.Location = new System.Drawing.Point(810, 114);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(59, 16);
			this.label58.TabIndex = 38;
			this.label58.Text = "- Status -";
			// 
			// tabCtrlResults
			// 
			this.tabCtrlResults.Controls.Add(this.tpgGetResults);
			this.tabCtrlResults.Controls.Add(this.tabPage2);
			this.tabCtrlResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabCtrlResults.Location = new System.Drawing.Point(3, 13);
			this.tabCtrlResults.Name = "tabCtrlResults";
			this.tabCtrlResults.SelectedIndex = 0;
			this.tabCtrlResults.Size = new System.Drawing.Size(743, 338);
			this.tabCtrlResults.TabIndex = 36;
			// 
			// tpgGetResults
			// 
			this.tpgGetResults.BackColor = System.Drawing.SystemColors.Control;
			this.tpgGetResults.Controls.Add(this.SampleResults_TagLabel);
			this.tpgGetResults.Controls.Add(this.SampleResults_TagBox);
			this.tpgGetResults.Controls.Add(this.SampleFilterComboBox);
			this.tpgGetResults.Controls.Add(this.label52);
			this.tpgGetResults.Controls.Add(this.SampleResults_CTQCLabel);
			this.tpgGetResults.Controls.Add(this.SampleResults_CTQCNameBox);
			this.tpgGetResults.Controls.Add(this.lblFilterType);
			this.tpgGetResults.Controls.Add(this.bttnGetResults);
			this.tpgGetResults.Controls.Add(this.txtSampleResults_SearchString);
			this.tpgGetResults.Controls.Add(this.dtpSampleResults_StartDate);
			this.tpgGetResults.Controls.Add(this.label48);
			this.tpgGetResults.Controls.Add(this.label13);
			this.tpgGetResults.Controls.Add(this.txtSampleResults_Username);
			this.tpgGetResults.Controls.Add(this.dtpSampleResults_EndDate);
			this.tpgGetResults.Controls.Add(this.label47);
			this.tpgGetResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgGetResults.Location = new System.Drawing.Point(4, 25);
			this.tpgGetResults.Name = "tpgGetResults";
			this.tpgGetResults.Padding = new System.Windows.Forms.Padding(3);
			this.tpgGetResults.Size = new System.Drawing.Size(735, 309);
			this.tpgGetResults.TabIndex = 0;
			this.tpgGetResults.Text = "Get Sample Results";
			// 
			// SampleResults_TagLabel
			// 
			this.SampleResults_TagLabel.AutoSize = true;
			this.SampleResults_TagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SampleResults_TagLabel.Location = new System.Drawing.Point(94, 263);
			this.SampleResults_TagLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SampleResults_TagLabel.Name = "SampleResults_TagLabel";
			this.SampleResults_TagLabel.Size = new System.Drawing.Size(33, 16);
			this.SampleResults_TagLabel.TabIndex = 77;
			this.SampleResults_TagLabel.Text = "Tag";
			this.SampleResults_TagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.SampleResults_TagLabel.Visible = false;
			// 
			// SampleResults_TagBox
			// 
			this.SampleResults_TagBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SampleResults_TagBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SampleResults_TagBox.Location = new System.Drawing.Point(135, 261);
			this.SampleResults_TagBox.Margin = new System.Windows.Forms.Padding(4);
			this.SampleResults_TagBox.Name = "SampleResults_TagBox";
			this.SampleResults_TagBox.Size = new System.Drawing.Size(169, 22);
			this.SampleResults_TagBox.TabIndex = 78;
			this.SampleResults_TagBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.SampleResults_TagBox.Visible = false;
			// 
			// SampleFilterComboBox
			// 
			this.SampleFilterComboBox.AccessibleDescription = "";
			this.SampleFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SampleFilterComboBox.FormattingEnabled = true;
			this.SampleFilterComboBox.Items.AddRange(new object[] {
            "Sample Set Filter",
            "Sample Filter"});
			this.SampleFilterComboBox.Location = new System.Drawing.Point(135, 22);
			this.SampleFilterComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.SampleFilterComboBox.Name = "SampleFilterComboBox";
			this.SampleFilterComboBox.Size = new System.Drawing.Size(140, 24);
			this.SampleFilterComboBox.TabIndex = 63;
			this.SampleFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.SampleFilterComboBox_SelectedIndexChanged);
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label52.Location = new System.Drawing.Point(86, 24);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(37, 16);
			this.label52.TabIndex = 62;
			this.label52.Text = "Filter";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SampleResults_CTQCLabel
			// 
			this.SampleResults_CTQCLabel.AutoSize = true;
			this.SampleResults_CTQCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SampleResults_CTQCLabel.Location = new System.Drawing.Point(19, 224);
			this.SampleResults_CTQCLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SampleResults_CTQCLabel.Name = "SampleResults_CTQCLabel";
			this.SampleResults_CTQCLabel.Size = new System.Drawing.Size(108, 16);
			this.SampleResults_CTQCLabel.TabIndex = 72;
			this.SampleResults_CTQCLabel.Text = "Cell or QC Name";
			this.SampleResults_CTQCLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.SampleResults_CTQCLabel.Visible = false;
			// 
			// SampleResults_CTQCNameBox
			// 
			this.SampleResults_CTQCNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SampleResults_CTQCNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SampleResults_CTQCNameBox.Location = new System.Drawing.Point(135, 222);
			this.SampleResults_CTQCNameBox.Margin = new System.Windows.Forms.Padding(4);
			this.SampleResults_CTQCNameBox.Name = "SampleResults_CTQCNameBox";
			this.SampleResults_CTQCNameBox.Size = new System.Drawing.Size(169, 22);
			this.SampleResults_CTQCNameBox.TabIndex = 73;
			this.SampleResults_CTQCNameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.SampleResults_CTQCNameBox.Visible = false;
			// 
			// lblFilterType
			// 
			this.lblFilterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFilterType.Location = new System.Drawing.Point(7, 184);
			this.lblFilterType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFilterType.Name = "lblFilterType";
			this.lblFilterType.Size = new System.Drawing.Size(125, 16);
			this.lblFilterType.TabIndex = 70;
			this.lblFilterType.Text = "Sample Set";
			this.lblFilterType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bttnGetResults
			// 
			this.bttnGetResults.BackColor = System.Drawing.SystemColors.Control;
			this.bttnGetResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnGetResults.Location = new System.Drawing.Point(346, 209);
			this.bttnGetResults.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnGetResults.Name = "bttnGetResults";
			this.bttnGetResults.Size = new System.Drawing.Size(140, 35);
			this.bttnGetResults.TabIndex = 11;
			this.bttnGetResults.Text = "Get Results";
			this.bttnGetResults.UseVisualStyleBackColor = false;
			this.bttnGetResults.Click += new System.EventHandler(this.bttnGetResults_Click);
			// 
			// txtSampleResults_SearchString
			// 
			this.txtSampleResults_SearchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSampleResults_SearchString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleResults_SearchString.Location = new System.Drawing.Point(135, 182);
			this.txtSampleResults_SearchString.Margin = new System.Windows.Forms.Padding(4);
			this.txtSampleResults_SearchString.Name = "txtSampleResults_SearchString";
			this.txtSampleResults_SearchString.Size = new System.Drawing.Size(169, 22);
			this.txtSampleResults_SearchString.TabIndex = 71;
			this.txtSampleResults_SearchString.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// dtpSampleResults_StartDate
			// 
			this.dtpSampleResults_StartDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpSampleResults_StartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpSampleResults_StartDate.Location = new System.Drawing.Point(135, 64);
			this.dtpSampleResults_StartDate.Name = "dtpSampleResults_StartDate";
			this.dtpSampleResults_StartDate.Size = new System.Drawing.Size(251, 22);
			this.dtpSampleResults_StartDate.TabIndex = 64;
			// 
			// label48
			// 
			this.label48.AutoSize = true;
			this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label48.Location = new System.Drawing.Point(88, 146);
			this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(37, 16);
			this.label48.TabIndex = 68;
			this.label48.Text = "User";
			this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(62, 69);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(67, 16);
			this.label13.TabIndex = 65;
			this.label13.Text = "Start Date";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtSampleResults_Username
			// 
			this.txtSampleResults_Username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSampleResults_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleResults_Username.Location = new System.Drawing.Point(135, 144);
			this.txtSampleResults_Username.Margin = new System.Windows.Forms.Padding(4);
			this.txtSampleResults_Username.Name = "txtSampleResults_Username";
			this.txtSampleResults_Username.Size = new System.Drawing.Size(169, 22);
			this.txtSampleResults_Username.TabIndex = 69;
			this.txtSampleResults_Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// dtpSampleResults_EndDate
			// 
			this.dtpSampleResults_EndDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpSampleResults_EndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpSampleResults_EndDate.Location = new System.Drawing.Point(135, 104);
			this.dtpSampleResults_EndDate.Name = "dtpSampleResults_EndDate";
			this.dtpSampleResults_EndDate.Size = new System.Drawing.Size(251, 22);
			this.dtpSampleResults_EndDate.TabIndex = 66;
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label47.Location = new System.Drawing.Point(62, 109);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(64, 16);
			this.label47.TabIndex = 67;
			this.label47.Text = "End Date";
			this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.btnExportEncryptedResult);
			this.tabPage2.Controls.Add(this.btnDeleteAllResults);
			this.tabPage2.Controls.Add(this.listBoxCurrentSampleResult);
			this.tabPage2.Controls.Add(this.lblSelectedSampleId);
			this.tabPage2.Controls.Add(this.lblSampleResultCount);
			this.tabPage2.Controls.Add(this.label51);
			this.tabPage2.Controls.Add(this.bttnDeleteResult);
			this.tabPage2.Controls.Add(this.bttnExportResult);
			this.tabPage2.Controls.Add(this.label50);
			this.tabPage2.Controls.Add(this.label43);
			this.tabPage2.Controls.Add(this.lstResultIds);
			this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(735, 309);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Manage Results";
			// 
			// btnExportEncryptedResult
			// 
			this.btnExportEncryptedResult.BackColor = System.Drawing.SystemColors.Control;
			this.btnExportEncryptedResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExportEncryptedResult.Location = new System.Drawing.Point(319, 50);
			this.btnExportEncryptedResult.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.btnExportEncryptedResult.Name = "btnExportEncryptedResult";
			this.btnExportEncryptedResult.Size = new System.Drawing.Size(199, 35);
			this.btnExportEncryptedResult.TabIndex = 62;
			this.btnExportEncryptedResult.Text = "Export For Offline Analysis";
			this.btnExportEncryptedResult.UseVisualStyleBackColor = false;
			this.btnExportEncryptedResult.Click += new System.EventHandler(this.btnExportOfflineResults_Click);
			// 
			// btnDeleteAllResults
			// 
			this.btnDeleteAllResults.BackColor = System.Drawing.SystemColors.Control;
			this.btnDeleteAllResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDeleteAllResults.Location = new System.Drawing.Point(577, 50);
			this.btnDeleteAllResults.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.btnDeleteAllResults.Name = "btnDeleteAllResults";
			this.btnDeleteAllResults.Size = new System.Drawing.Size(146, 36);
			this.btnDeleteAllResults.TabIndex = 61;
			this.btnDeleteAllResults.Text = "Delete All Results";
			this.btnDeleteAllResults.UseVisualStyleBackColor = false;
			// 
			// listBoxCurrentSampleResult
			// 
			this.listBoxCurrentSampleResult.FormattingEnabled = true;
			this.listBoxCurrentSampleResult.ItemHeight = 16;
			this.listBoxCurrentSampleResult.Location = new System.Drawing.Point(319, 160);
			this.listBoxCurrentSampleResult.Margin = new System.Windows.Forms.Padding(2);
			this.listBoxCurrentSampleResult.Name = "listBoxCurrentSampleResult";
			this.listBoxCurrentSampleResult.ScrollAlwaysVisible = true;
			this.listBoxCurrentSampleResult.Size = new System.Drawing.Size(400, 148);
			this.listBoxCurrentSampleResult.TabIndex = 60;
			// 
			// lblSelectedSampleId
			// 
			this.lblSelectedSampleId.BackColor = System.Drawing.SystemColors.HighlightText;
			this.lblSelectedSampleId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSelectedSampleId.Location = new System.Drawing.Point(346, 117);
			this.lblSelectedSampleId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblSelectedSampleId.Name = "lblSelectedSampleId";
			this.lblSelectedSampleId.Size = new System.Drawing.Size(326, 22);
			this.lblSelectedSampleId.TabIndex = 59;
			this.lblSelectedSampleId.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblSampleResultCount
			// 
			this.lblSampleResultCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSampleResultCount.Location = new System.Drawing.Point(183, 9);
			this.lblSampleResultCount.Name = "lblSampleResultCount";
			this.lblSampleResultCount.Size = new System.Drawing.Size(114, 16);
			this.lblSampleResultCount.TabIndex = 58;
			this.lblSampleResultCount.Text = "0";
			this.lblSampleResultCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label51.Location = new System.Drawing.Point(319, 119);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(21, 16);
			this.label51.TabIndex = 57;
			this.label51.Text = "ID";
			this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bttnDeleteResult
			// 
			this.bttnDeleteResult.BackColor = System.Drawing.SystemColors.Control;
			this.bttnDeleteResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnDeleteResult.Location = new System.Drawing.Point(577, 9);
			this.bttnDeleteResult.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnDeleteResult.Name = "bttnDeleteResult";
			this.bttnDeleteResult.Size = new System.Drawing.Size(111, 35);
			this.bttnDeleteResult.TabIndex = 55;
			this.bttnDeleteResult.Text = "Delete Result";
			this.bttnDeleteResult.UseVisualStyleBackColor = false;
			this.bttnDeleteResult.Click += new System.EventHandler(this.bttnDeleteResult_Click);
			// 
			// bttnExportResult
			// 
			this.bttnExportResult.BackColor = System.Drawing.SystemColors.Control;
			this.bttnExportResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnExportResult.Location = new System.Drawing.Point(319, 9);
			this.bttnExportResult.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnExportResult.Name = "bttnExportResult";
			this.bttnExportResult.Size = new System.Drawing.Size(126, 35);
			this.bttnExportResult.TabIndex = 54;
			this.bttnExportResult.Text = "Export Results";
			this.bttnExportResult.UseVisualStyleBackColor = false;
			this.bttnExportResult.Click += new System.EventHandler(this.bttnExportResults_Click);
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label50.Location = new System.Drawing.Point(448, 92);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(127, 16);
			this.label50.TabIndex = 53;
			this.label50.Text = "Selected Result Info";
			this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label43.Location = new System.Drawing.Point(74, 9);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(67, 16);
			this.label43.TabIndex = 51;
			this.label43.Text = "Result Ids";
			this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lstResultIds
			// 
			this.lstResultIds.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstResultIds.FormattingEnabled = true;
			this.lstResultIds.ItemHeight = 16;
			this.lstResultIds.Location = new System.Drawing.Point(6, 32);
			this.lstResultIds.Name = "lstResultIds";
			this.lstResultIds.ScrollAlwaysVisible = true;
			this.lstResultIds.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstResultIds.Size = new System.Drawing.Size(291, 276);
			this.lstResultIds.TabIndex = 50;
			this.lstResultIds.SelectedIndexChanged += new System.EventHandler(this.lstResultIds_SelectedIndexChanged);
			// 
			// tpgMaintenance
			// 
			this.tpgMaintenance.BackColor = System.Drawing.SystemColors.Control;
			this.tpgMaintenance.Controls.Add(this.tabControlReagents);
			this.tpgMaintenance.Controls.Add(this.panel2);
			this.tpgMaintenance.Controls.Add(this.bttnDeleteCampaignData);
			this.tpgMaintenance.Controls.Add(this.btnReboot);
			this.tpgMaintenance.Controls.Add(this.btnShutdown);
			this.tpgMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgMaintenance.Location = new System.Drawing.Point(4, 25);
			this.tpgMaintenance.Name = "tpgMaintenance";
			this.tpgMaintenance.Size = new System.Drawing.Size(923, 395);
			this.tpgMaintenance.TabIndex = 3;
			this.tpgMaintenance.Text = "Maintenance";
			// 
			// tabControlReagents
			// 
			this.tabControlReagents.Controls.Add(this.tpgReagentVolume);
			this.tabControlReagents.Controls.Add(this.tpgCleanFluidics);
			this.tabControlReagents.Controls.Add(this.tpgPrimeReagents);
			this.tabControlReagents.Controls.Add(this.tpgPurgeReagents);
			this.tabControlReagents.Controls.Add(this.tpgDecontaminate);
			this.tabControlReagents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabControlReagents.Location = new System.Drawing.Point(13, 13);
			this.tabControlReagents.Name = "tabControlReagents";
			this.tabControlReagents.SelectedIndex = 0;
			this.tabControlReagents.Size = new System.Drawing.Size(495, 233);
			this.tabControlReagents.TabIndex = 68;
			// 
			// tpgReagentVolume
			// 
			this.tpgReagentVolume.BackColor = System.Drawing.SystemColors.Control;
			this.tpgReagentVolume.Controls.Add(this.txtAddReagentVolume);
			this.tpgReagentVolume.Controls.Add(this.cbxSelectReagent);
			this.tpgReagentVolume.Controls.Add(this.txtSetReagentVolume);
			this.tpgReagentVolume.Controls.Add(this.label61);
			this.tpgReagentVolume.Controls.Add(this.lblReagentVolume);
			this.tpgReagentVolume.Controls.Add(this.btnGetReagentVolume);
			this.tpgReagentVolume.Controls.Add(this.btnAddReagentVolume);
			this.tpgReagentVolume.Controls.Add(this.btnSetReagentVolume);
			this.tpgReagentVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgReagentVolume.Location = new System.Drawing.Point(4, 25);
			this.tpgReagentVolume.Name = "tpgReagentVolume";
			this.tpgReagentVolume.Padding = new System.Windows.Forms.Padding(3);
			this.tpgReagentVolume.Size = new System.Drawing.Size(487, 204);
			this.tpgReagentVolume.TabIndex = 1;
			this.tpgReagentVolume.Text = "Reagent Volume";
			// 
			// txtAddReagentVolume
			// 
			this.txtAddReagentVolume.Location = new System.Drawing.Point(207, 151);
			this.txtAddReagentVolume.Name = "txtAddReagentVolume";
			this.txtAddReagentVolume.Size = new System.Drawing.Size(95, 22);
			this.txtAddReagentVolume.TabIndex = 68;
			// 
			// cbxSelectReagent
			// 
			this.cbxSelectReagent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxSelectReagent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.cbxSelectReagent.FormattingEnabled = true;
			this.cbxSelectReagent.Items.AddRange(new object[] {
            "Trypan Blue",
            "Cleaner",
            "Conditioning Solution",
            "Buffer Solution",
            "Diluent"});
			this.cbxSelectReagent.Location = new System.Drawing.Point(183, 19);
			this.cbxSelectReagent.Name = "cbxSelectReagent";
			this.cbxSelectReagent.Size = new System.Drawing.Size(134, 24);
			this.cbxSelectReagent.TabIndex = 65;
			// 
			// txtSetReagentVolume
			// 
			this.txtSetReagentVolume.Location = new System.Drawing.Point(207, 108);
			this.txtSetReagentVolume.Name = "txtSetReagentVolume";
			this.txtSetReagentVolume.Size = new System.Drawing.Size(95, 22);
			this.txtSetReagentVolume.TabIndex = 67;
			// 
			// label61
			// 
			this.label61.AutoSize = true;
			this.label61.Location = new System.Drawing.Point(18, 22);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(159, 16);
			this.label61.TabIndex = 66;
			this.label61.Text = "Select Reagent Type:";
			// 
			// lblReagentVolume
			// 
			this.lblReagentVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblReagentVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReagentVolume.Location = new System.Drawing.Point(207, 60);
			this.lblReagentVolume.Name = "lblReagentVolume";
			this.lblReagentVolume.Size = new System.Drawing.Size(95, 25);
			this.lblReagentVolume.TabIndex = 62;
			this.lblReagentVolume.Text = "---";
			this.lblReagentVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnGetReagentVolume
			// 
			this.btnGetReagentVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetReagentVolume.Location = new System.Drawing.Point(43, 60);
			this.btnGetReagentVolume.Name = "btnGetReagentVolume";
			this.btnGetReagentVolume.Size = new System.Drawing.Size(153, 25);
			this.btnGetReagentVolume.TabIndex = 61;
			this.btnGetReagentVolume.Text = "Get Reagent Volume";
			this.btnGetReagentVolume.UseVisualStyleBackColor = true;
			this.btnGetReagentVolume.Click += new System.EventHandler(this.btnGetReagentVolume_Click);
			// 
			// btnAddReagentVolume
			// 
			this.btnAddReagentVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddReagentVolume.Location = new System.Drawing.Point(43, 148);
			this.btnAddReagentVolume.Name = "btnAddReagentVolume";
			this.btnAddReagentVolume.Size = new System.Drawing.Size(153, 25);
			this.btnAddReagentVolume.TabIndex = 61;
			this.btnAddReagentVolume.Text = "Add Reagent Volume";
			this.btnAddReagentVolume.UseVisualStyleBackColor = true;
			this.btnAddReagentVolume.Click += new System.EventHandler(this.btnAddReagentVolume_Click);
			// 
			// btnSetReagentVolume
			// 
			this.btnSetReagentVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSetReagentVolume.Location = new System.Drawing.Point(43, 105);
			this.btnSetReagentVolume.Name = "btnSetReagentVolume";
			this.btnSetReagentVolume.Size = new System.Drawing.Size(153, 25);
			this.btnSetReagentVolume.TabIndex = 61;
			this.btnSetReagentVolume.Text = "Set Reagent Volume";
			this.btnSetReagentVolume.UseVisualStyleBackColor = true;
			this.btnSetReagentVolume.Click += new System.EventHandler(this.btnSetReagentVolume_Click);
			// 
			// tpgCleanFluidics
			// 
			this.tpgCleanFluidics.BackColor = System.Drawing.SystemColors.Control;
			this.tpgCleanFluidics.Controls.Add(this.btnCleanFluidics);
			this.tpgCleanFluidics.Controls.Add(this.lblCleanFluidicsStatus);
			this.tpgCleanFluidics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgCleanFluidics.Location = new System.Drawing.Point(4, 25);
			this.tpgCleanFluidics.Name = "tpgCleanFluidics";
			this.tpgCleanFluidics.Padding = new System.Windows.Forms.Padding(3);
			this.tpgCleanFluidics.Size = new System.Drawing.Size(487, 204);
			this.tpgCleanFluidics.TabIndex = 0;
			this.tpgCleanFluidics.Text = "Clean Fluidics";
			// 
			// btnCleanFluidics
			// 
			this.btnCleanFluidics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCleanFluidics.Location = new System.Drawing.Point(20, 17);
			this.btnCleanFluidics.Name = "btnCleanFluidics";
			this.btnCleanFluidics.Size = new System.Drawing.Size(129, 25);
			this.btnCleanFluidics.TabIndex = 61;
			this.btnCleanFluidics.Text = "Start Clean";
			this.btnCleanFluidics.UseVisualStyleBackColor = true;
			this.btnCleanFluidics.Click += new System.EventHandler(this.btnCleanFluidics_Click);
			// 
			// lblCleanFluidicsStatus
			// 
			this.lblCleanFluidicsStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblCleanFluidicsStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCleanFluidicsStatus.Location = new System.Drawing.Point(182, 17);
			this.lblCleanFluidicsStatus.Name = "lblCleanFluidicsStatus";
			this.lblCleanFluidicsStatus.Size = new System.Drawing.Size(159, 25);
			this.lblCleanFluidicsStatus.TabIndex = 62;
			this.lblCleanFluidicsStatus.Text = "---";
			this.lblCleanFluidicsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tpgPrimeReagents
			// 
			this.tpgPrimeReagents.BackColor = System.Drawing.SystemColors.Control;
			this.tpgPrimeReagents.Controls.Add(this.btnCancelPrimeReagents);
			this.tpgPrimeReagents.Controls.Add(this.btnPrimeReagents);
			this.tpgPrimeReagents.Controls.Add(this.lblPrimeReagentsStatus);
			this.tpgPrimeReagents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgPrimeReagents.Location = new System.Drawing.Point(4, 25);
			this.tpgPrimeReagents.Name = "tpgPrimeReagents";
			this.tpgPrimeReagents.Size = new System.Drawing.Size(487, 204);
			this.tpgPrimeReagents.TabIndex = 2;
			this.tpgPrimeReagents.Text = "Prime Reagents";
			// 
			// btnCancelPrimeReagents
			// 
			this.btnCancelPrimeReagents.Location = new System.Drawing.Point(18, 62);
			this.btnCancelPrimeReagents.Name = "btnCancelPrimeReagents";
			this.btnCancelPrimeReagents.Size = new System.Drawing.Size(129, 25);
			this.btnCancelPrimeReagents.TabIndex = 65;
			this.btnCancelPrimeReagents.Text = "Cancel Prime";
			this.btnCancelPrimeReagents.UseVisualStyleBackColor = true;
			this.btnCancelPrimeReagents.Click += new System.EventHandler(this.btnCancelPrimeReagents_Click);
			// 
			// btnPrimeReagents
			// 
			this.btnPrimeReagents.Location = new System.Drawing.Point(18, 16);
			this.btnPrimeReagents.Name = "btnPrimeReagents";
			this.btnPrimeReagents.Size = new System.Drawing.Size(129, 25);
			this.btnPrimeReagents.TabIndex = 63;
			this.btnPrimeReagents.Text = "Start Prime";
			this.btnPrimeReagents.UseVisualStyleBackColor = true;
			this.btnPrimeReagents.Click += new System.EventHandler(this.btnPrimeReagents_Click);
			// 
			// lblPrimeReagentsStatus
			// 
			this.lblPrimeReagentsStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPrimeReagentsStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPrimeReagentsStatus.Location = new System.Drawing.Point(177, 16);
			this.lblPrimeReagentsStatus.Name = "lblPrimeReagentsStatus";
			this.lblPrimeReagentsStatus.Size = new System.Drawing.Size(159, 25);
			this.lblPrimeReagentsStatus.TabIndex = 64;
			this.lblPrimeReagentsStatus.Text = "---";
			this.lblPrimeReagentsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tpgPurgeReagents
			// 
			this.tpgPurgeReagents.BackColor = System.Drawing.SystemColors.Control;
			this.tpgPurgeReagents.Controls.Add(this.btnCancelPurgeReagents);
			this.tpgPurgeReagents.Controls.Add(this.btnPurgeReagents);
			this.tpgPurgeReagents.Controls.Add(this.lblPurgeReagentssStatus);
			this.tpgPurgeReagents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpgPurgeReagents.Location = new System.Drawing.Point(4, 25);
			this.tpgPurgeReagents.Name = "tpgPurgeReagents";
			this.tpgPurgeReagents.Size = new System.Drawing.Size(487, 204);
			this.tpgPurgeReagents.TabIndex = 3;
			this.tpgPurgeReagents.Text = "Purge Reagents";
			// 
			// btnCancelPurgeReagents
			// 
			this.btnCancelPurgeReagents.Enabled = false;
			this.btnCancelPurgeReagents.Location = new System.Drawing.Point(18, 62);
			this.btnCancelPurgeReagents.Name = "btnCancelPurgeReagents";
			this.btnCancelPurgeReagents.Size = new System.Drawing.Size(129, 25);
			this.btnCancelPurgeReagents.TabIndex = 67;
			this.btnCancelPurgeReagents.Text = "Cancel Purge";
			this.btnCancelPurgeReagents.UseVisualStyleBackColor = true;
			this.btnCancelPurgeReagents.Click += new System.EventHandler(this.btnCancelPurgeReagents_Click);
			// 
			// btnPurgeReagents
			// 
			this.btnPurgeReagents.Enabled = false;
			this.btnPurgeReagents.Location = new System.Drawing.Point(18, 17);
			this.btnPurgeReagents.Name = "btnPurgeReagents";
			this.btnPurgeReagents.Size = new System.Drawing.Size(129, 25);
			this.btnPurgeReagents.TabIndex = 65;
			this.btnPurgeReagents.Text = "Start Purge";
			this.btnPurgeReagents.UseVisualStyleBackColor = true;
			this.btnPurgeReagents.Click += new System.EventHandler(this.btnPurgeReagents_Click);
			// 
			// lblPurgeReagentssStatus
			// 
			this.lblPurgeReagentssStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPurgeReagentssStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPurgeReagentssStatus.Location = new System.Drawing.Point(177, 17);
			this.lblPurgeReagentssStatus.Name = "lblPurgeReagentssStatus";
			this.lblPurgeReagentssStatus.Size = new System.Drawing.Size(159, 25);
			this.lblPurgeReagentssStatus.TabIndex = 66;
			this.lblPurgeReagentssStatus.Text = "---";
			this.lblPurgeReagentssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tpgDecontaminate
			// 
			this.tpgDecontaminate.BackColor = System.Drawing.SystemColors.Control;
			this.tpgDecontaminate.Controls.Add(this.btnCancelDecontaminate);
			this.tpgDecontaminate.Controls.Add(this.btnDecontaminate);
			this.tpgDecontaminate.Controls.Add(this.lblDecontaminateStatus);
			this.tpgDecontaminate.Location = new System.Drawing.Point(4, 25);
			this.tpgDecontaminate.Name = "tpgDecontaminate";
			this.tpgDecontaminate.Size = new System.Drawing.Size(487, 204);
			this.tpgDecontaminate.TabIndex = 4;
			this.tpgDecontaminate.Text = "Decontaminate";
			// 
			// btnCancelDecontaminate
			// 
			this.btnCancelDecontaminate.Enabled = false;
			this.btnCancelDecontaminate.Location = new System.Drawing.Point(20, 84);
			this.btnCancelDecontaminate.Name = "btnCancelDecontaminate";
			this.btnCancelDecontaminate.Size = new System.Drawing.Size(129, 49);
			this.btnCancelDecontaminate.TabIndex = 70;
			this.btnCancelDecontaminate.Text = "Cancel Decontaminate";
			this.btnCancelDecontaminate.UseVisualStyleBackColor = true;
			this.btnCancelDecontaminate.Click += new System.EventHandler(this.btnCancelDecontaminate_Click);
			// 
			// btnDecontaminate
			// 
			this.btnDecontaminate.Enabled = false;
			this.btnDecontaminate.Location = new System.Drawing.Point(20, 18);
			this.btnDecontaminate.Name = "btnDecontaminate";
			this.btnDecontaminate.Size = new System.Drawing.Size(129, 51);
			this.btnDecontaminate.TabIndex = 68;
			this.btnDecontaminate.Text = "Start Decontaminate";
			this.btnDecontaminate.UseVisualStyleBackColor = true;
			this.btnDecontaminate.Click += new System.EventHandler(this.btnDecontaminate_Click);
			// 
			// lblDecontaminateStatus
			// 
			this.lblDecontaminateStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDecontaminateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDecontaminateStatus.Location = new System.Drawing.Point(179, 18);
			this.lblDecontaminateStatus.Name = "lblDecontaminateStatus";
			this.lblDecontaminateStatus.Size = new System.Drawing.Size(159, 25);
			this.lblDecontaminateStatus.TabIndex = 69;
			this.lblDecontaminateStatus.Text = "---";
			this.lblDecontaminateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.dtpConfigAudit_StartDate);
			this.panel2.Controls.Add(this.label59);
			this.panel2.Controls.Add(this.lstConfigAuditStatus);
			this.panel2.Controls.Add(this.label49);
			this.panel2.Controls.Add(this.dtpConfigAudit_EndDate);
			this.panel2.Controls.Add(this.bttnStartLogDataExport);
			this.panel2.Controls.Add(this.label60);
			this.panel2.Location = new System.Drawing.Point(552, 98);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(364, 264);
			this.panel2.TabIndex = 68;
			// 
			// dtpConfigAudit_StartDate
			// 
			this.dtpConfigAudit_StartDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpConfigAudit_StartDate.CustomFormat = "yyyy-MM-dd";
			this.dtpConfigAudit_StartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpConfigAudit_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpConfigAudit_StartDate.Location = new System.Drawing.Point(88, 18);
			this.dtpConfigAudit_StartDate.MinDate = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
			this.dtpConfigAudit_StartDate.Name = "dtpConfigAudit_StartDate";
			this.dtpConfigAudit_StartDate.Size = new System.Drawing.Size(93, 22);
			this.dtpConfigAudit_StartDate.TabIndex = 73;
			// 
			// label59
			// 
			this.label59.AutoSize = true;
			this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label59.Location = new System.Drawing.Point(15, 23);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(67, 16);
			this.label59.TabIndex = 74;
			this.label59.Text = "Start Date";
			this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lstConfigAuditStatus
			// 
			this.lstConfigAuditStatus.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstConfigAuditStatus.FormattingEnabled = true;
			this.lstConfigAuditStatus.HorizontalScrollbar = true;
			this.lstConfigAuditStatus.ItemHeight = 15;
			this.lstConfigAuditStatus.Location = new System.Drawing.Point(0, 120);
			this.lstConfigAuditStatus.Margin = new System.Windows.Forms.Padding(2);
			this.lstConfigAuditStatus.Name = "lstConfigAuditStatus";
			this.lstConfigAuditStatus.ScrollAlwaysVisible = true;
			this.lstConfigAuditStatus.Size = new System.Drawing.Size(360, 154);
			this.lstConfigAuditStatus.TabIndex = 72;
			// 
			// label49
			// 
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(129, 93);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(59, 16);
			this.label49.TabIndex = 71;
			this.label49.Text = "- Status -";
			// 
			// dtpConfigAudit_EndDate
			// 
			this.dtpConfigAudit_EndDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpConfigAudit_EndDate.CustomFormat = "yyyy-MM-dd";
			this.dtpConfigAudit_EndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpConfigAudit_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpConfigAudit_EndDate.Location = new System.Drawing.Point(88, 58);
			this.dtpConfigAudit_EndDate.Name = "dtpConfigAudit_EndDate";
			this.dtpConfigAudit_EndDate.Size = new System.Drawing.Size(92, 22);
			this.dtpConfigAudit_EndDate.TabIndex = 75;
			// 
			// bttnStartLogDataExport
			// 
			this.bttnStartLogDataExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnStartLogDataExport.Location = new System.Drawing.Point(206, 14);
			this.bttnStartLogDataExport.Name = "bttnStartLogDataExport";
			this.bttnStartLogDataExport.Size = new System.Drawing.Size(128, 35);
			this.bttnStartLogDataExport.TabIndex = 70;
			this.bttnStartLogDataExport.Text = "Log Data Export";
			this.bttnStartLogDataExport.UseVisualStyleBackColor = true;
			this.bttnStartLogDataExport.Click += new System.EventHandler(this.bttnStartLogDataExport_Click);
			// 
			// label60
			// 
			this.label60.AutoSize = true;
			this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label60.Location = new System.Drawing.Point(15, 63);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(64, 16);
			this.label60.TabIndex = 76;
			this.label60.Text = "End Date";
			this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bttnDeleteCampaignData
			// 
			this.bttnDeleteCampaignData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnDeleteCampaignData.Location = new System.Drawing.Point(552, 13);
			this.bttnDeleteCampaignData.Name = "bttnDeleteCampaignData";
			this.bttnDeleteCampaignData.Size = new System.Drawing.Size(129, 41);
			this.bttnDeleteCampaignData.TabIndex = 67;
			this.bttnDeleteCampaignData.Text = "Delete Campaign Data";
			this.bttnDeleteCampaignData.UseVisualStyleBackColor = true;
			this.bttnDeleteCampaignData.Click += new System.EventHandler(this.btnDeleteCampaignData_Click);
			// 
			// btnReboot
			// 
			this.btnReboot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReboot.Location = new System.Drawing.Point(740, 56);
			this.btnReboot.Name = "btnReboot";
			this.btnReboot.Size = new System.Drawing.Size(129, 25);
			this.btnReboot.TabIndex = 66;
			this.btnReboot.Text = "Reboot";
			this.btnReboot.UseVisualStyleBackColor = true;
			this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
			// 
			// btnShutdown
			// 
			this.btnShutdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShutdown.Location = new System.Drawing.Point(740, 13);
			this.btnShutdown.Name = "btnShutdown";
			this.btnShutdown.Size = new System.Drawing.Size(129, 25);
			this.btnShutdown.TabIndex = 63;
			this.btnShutdown.Text = "Shutdown";
			this.btnShutdown.UseVisualStyleBackColor = true;
			this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
			// 
			// tabErrorStatus
			// 
			this.tabErrorStatus.BackColor = System.Drawing.SystemColors.Control;
			this.tabErrorStatus.Controls.Add(this.btnClearErrorStatus);
			this.tabErrorStatus.Controls.Add(this.lstErrorStatus);
			this.tabErrorStatus.Location = new System.Drawing.Point(4, 25);
			this.tabErrorStatus.Name = "tabErrorStatus";
			this.tabErrorStatus.Padding = new System.Windows.Forms.Padding(3);
			this.tabErrorStatus.Size = new System.Drawing.Size(923, 395);
			this.tabErrorStatus.TabIndex = 4;
			this.tabErrorStatus.Text = "Error Status";
			// 
			// btnClearErrorStatus
			// 
			this.btnClearErrorStatus.Location = new System.Drawing.Point(6, 10);
			this.btnClearErrorStatus.Name = "btnClearErrorStatus";
			this.btnClearErrorStatus.Size = new System.Drawing.Size(144, 26);
			this.btnClearErrorStatus.TabIndex = 64;
			this.btnClearErrorStatus.Text = "Clear ErrorStatus";
			this.btnClearErrorStatus.UseVisualStyleBackColor = true;
			this.btnClearErrorStatus.Click += new System.EventHandler(this.btnClearErrorStatus_Click);
			// 
			// lstErrorStatus
			// 
			this.lstErrorStatus.BackColor = System.Drawing.SystemColors.Control;
			this.lstErrorStatus.FormattingEnabled = true;
			this.lstErrorStatus.ItemHeight = 16;
			this.lstErrorStatus.Location = new System.Drawing.Point(0, 42);
			this.lstErrorStatus.Name = "lstErrorStatus";
			this.lstErrorStatus.Size = new System.Drawing.Size(924, 324);
			this.lstErrorStatus.TabIndex = 63;
			// 
			// bttnEjectStage
			// 
			this.bttnEjectStage.BackColor = System.Drawing.SystemColors.Control;
			this.bttnEjectStage.Enabled = false;
			this.bttnEjectStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnEjectStage.Location = new System.Drawing.Point(762, 13);
			this.bttnEjectStage.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnEjectStage.Name = "bttnEjectStage";
			this.bttnEjectStage.Size = new System.Drawing.Size(125, 30);
			this.bttnEjectStage.TabIndex = 14;
			this.bttnEjectStage.Text = "Eject Stage";
			this.bttnEjectStage.UseVisualStyleBackColor = false;
			this.bttnEjectStage.Visible = false;
			this.bttnEjectStage.Click += new System.EventHandler(this.bttnEject_Click);
			// 
			// bttnUnlock
			// 
			this.bttnUnlock.BackColor = System.Drawing.SystemColors.Control;
			this.bttnUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnUnlock.Location = new System.Drawing.Point(126, 18);
			this.bttnUnlock.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnUnlock.Name = "bttnUnlock";
			this.bttnUnlock.Size = new System.Drawing.Size(105, 30);
			this.bttnUnlock.TabIndex = 9;
			this.bttnUnlock.Text = "Un-Lock";
			this.bttnUnlock.UseVisualStyleBackColor = false;
			this.bttnUnlock.Click += new System.EventHandler(this.bttnUnlock_Click);
			// 
			// bttnRequestLock
			// 
			this.bttnRequestLock.BackColor = System.Drawing.SystemColors.Control;
			this.bttnRequestLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bttnRequestLock.Location = new System.Drawing.Point(14, 33);
			this.bttnRequestLock.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
			this.bttnRequestLock.Name = "bttnRequestLock";
			this.bttnRequestLock.Size = new System.Drawing.Size(140, 35);
			this.bttnRequestLock.TabIndex = 9;
			this.bttnRequestLock.Text = "Request Lock";
			this.bttnRequestLock.UseVisualStyleBackColor = false;
			this.bttnRequestLock.Click += new System.EventHandler(this.bttnRequestLock_Click);
			// 
			// label44
			// 
			this.label44.Location = new System.Drawing.Point(0, 0);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(100, 23);
			this.label44.TabIndex = 0;
			// 
			// menuStripMain
			// 
			this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tsmiUtils});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
			this.menuStripMain.Size = new System.Drawing.Size(966, 24);
			this.menuStripMain.TabIndex = 18;
			this.menuStripMain.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// tsmiUtils
			// 
			this.tsmiUtils.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCnxConfig,
            this.tsmiUtils_CreateSingle,
            this.tsmiUtils_CreateSet});
			this.tsmiUtils.Name = "tsmiUtils";
			this.tsmiUtils.Size = new System.Drawing.Size(79, 20);
			this.tsmiUtils.Text = "Run Config";
			// 
			// tsmiCnxConfig
			// 
			this.tsmiCnxConfig.Name = "tsmiCnxConfig";
			this.tsmiCnxConfig.Size = new System.Drawing.Size(175, 22);
			this.tsmiCnxConfig.Text = "Connection Config";
			this.tsmiCnxConfig.Click += new System.EventHandler(this.tsmiCnxConfig_Click);
			// 
			// tsmiUtils_CreateSingle
			// 
			this.tsmiUtils_CreateSingle.Name = "tsmiUtils_CreateSingle";
			this.tsmiUtils_CreateSingle.Size = new System.Drawing.Size(175, 22);
			this.tsmiUtils_CreateSingle.Text = "Single Sample";
			this.tsmiUtils_CreateSingle.Click += new System.EventHandler(this.tsmiUtils_CreateSingle_Click);
			// 
			// tsmiUtils_CreateSet
			// 
			this.tsmiUtils_CreateSet.Name = "tsmiUtils_CreateSet";
			this.tsmiUtils_CreateSet.Size = new System.Drawing.Size(175, 22);
			this.tsmiUtils_CreateSet.Text = "Sample Set";
			this.tsmiUtils_CreateSet.Click += new System.EventHandler(this.tsmiUtils_CreateSet_Click);
			// 
			// lstCallResult
			// 
			this.lstCallResult.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstCallResult.FormattingEnabled = true;
			this.lstCallResult.HorizontalScrollbar = true;
			this.lstCallResult.ItemHeight = 16;
			this.lstCallResult.Location = new System.Drawing.Point(629, 4);
			this.lstCallResult.Margin = new System.Windows.Forms.Padding(2);
			this.lstCallResult.Name = "lstCallResult";
			this.lstCallResult.ScrollAlwaysVisible = true;
			this.lstCallResult.Size = new System.Drawing.Size(328, 84);
			this.lstCallResult.TabIndex = 47;
			// 
			// lblAppVersion
			// 
			this.lblAppVersion.AutoSize = true;
			this.lblAppVersion.Location = new System.Drawing.Point(26, 736);
			this.lblAppVersion.Name = "lblAppVersion";
			this.lblAppVersion.Size = new System.Drawing.Size(41, 13);
			this.lblAppVersion.TabIndex = 48;
			this.lblAppVersion.Text = "label64";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(966, 758);
			this.Controls.Add(this.lblAppVersion);
			this.Controls.Add(this.lstCallResult);
			this.Controls.Add(this.grpConnected);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.menuStripMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStripMain;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cell Health dotNET Test App";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.grpConnected.ResumeLayout(false);
			this.grpConnected.PerformLayout();
			this.grpInstrumentLocked.ResumeLayout(false);
			this.grpInstrumentLocked.PerformLayout();
			this.tabCtlrMain.ResumeLayout(false);
			this.tpgRunSamples.ResumeLayout(false);
			this.tabCtrlSamples.ResumeLayout(false);
			this.tpgSingleSampleSetup.ResumeLayout(false);
			this.tpgSingleSampleSetup.PerformLayout();
			this.cb_CellTypeorQC.ResumeLayout(false);
			this.cb_CellTypeorQC.PerformLayout();
			this.gb_PostWashOption.ResumeLayout(false);
			this.gb_PostWashOption.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUD_NthImageSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUD_Dilution)).EndInit();
			this.tpgMultiSampleSetup.ResumeLayout(false);
			this.tpgMultiSampleSetup.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSet)).EndInit();
			this.tabPage1.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tpgCellTypes.ResumeLayout(false);
			this.tpgCellTypes.PerformLayout();
			this.grpCT_Create.ResumeLayout(false);
			this.grpCT_Create.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_ConcAdjFactor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_ViaSpotArea)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_ViaSpotBright)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MixCycles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_AspCycles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MinCirc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_Sharpness)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_Images)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MaxDiam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDCT_MinDiam)).EndInit();
			this.tpgQualityControls.ResumeLayout(false);
			this.tpgQualityControls.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUDQC_AssayValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUDQC_AcceptLimits)).EndInit();
			this.tpgResultsConfig.ResumeLayout(false);
			this.tpgResultsConfig.PerformLayout();
			this.tabCtrlResults.ResumeLayout(false);
			this.tpgGetResults.ResumeLayout(false);
			this.tpgGetResults.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tpgMaintenance.ResumeLayout(false);
			this.tabControlReagents.ResumeLayout(false);
			this.tpgReagentVolume.ResumeLayout(false);
			this.tpgReagentVolume.PerformLayout();
			this.tpgCleanFluidics.ResumeLayout(false);
			this.tpgPrimeReagents.ResumeLayout(false);
			this.tpgPurgeReagents.ResumeLayout(false);
			this.tpgDecontaminate.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tabErrorStatus.ResumeLayout(false);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIPAddr;
        internal System.Windows.Forms.Button bttnConnect;
        private System.Windows.Forms.GroupBox grpConnected;
        private System.Windows.Forms.GroupBox grpInstrumentLocked;
        private System.Windows.Forms.TabControl tabCtlrMain;
        private System.Windows.Forms.TabPage tpgRunSamples;
        private System.Windows.Forms.TabControl tabCtrlSamples;
        private System.Windows.Forms.TabPage tpgSingleSampleSetup;
        private System.Windows.Forms.NumericUpDown numUD_NthImageSave;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Button bttnLoadSample;
        private System.Windows.Forms.NumericUpDown numUD_Dilution;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSampleTag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSampleName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSampleCellTypeorQCName;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button bttnStartSample;
        private System.Windows.Forms.TabPage tpgMultiSampleSetup;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Button bttnSampleResume;
        internal System.Windows.Forms.Button bttnSamplePause;
        internal System.Windows.Forms.Button bttnSampleStop;
        private System.Windows.Forms.TabPage tpgCellTypes;
        private System.Windows.Forms.TabPage tpgQualityControls;
        private System.Windows.Forms.TabPage tpgResultsConfig;
        private System.Windows.Forms.TabPage tpgMaintenance;
        internal System.Windows.Forms.Button bttnEjectStage;
        internal System.Windows.Forms.Button bttnUnlock;
        internal System.Windows.Forms.Button bttnRequestLock;
        private System.Windows.Forms.GroupBox grpCT_Create;
        private System.Windows.Forms.NumericUpDown numUDCT_Sharpness;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numUDCT_Images;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numUDCT_MaxDiam;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numUDCT_MinDiam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCTCreate_Name;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Button bttnCT_Create;
        private System.Windows.Forms.NumericUpDown numUDCT_MinCirc;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown numUDCT_ConcAdjFactor;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown numUDCT_ViaSpotArea;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numUDCT_ViaSpotBright;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown numUDCT_MixCycles;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown numUDCT_AspCycles;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbxCT_Decluster;
        internal System.Windows.Forms.Button bttnCTCreate_Save;
        internal System.Windows.Forms.Button bttnCTCreate_Load;
        internal System.Windows.Forms.Button bttnCT_Delete;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Button bttnQC_Save;
        internal System.Windows.Forms.Button bttnQC_Load;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cbxQC_AssayParam;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.NumericUpDown numUDQC_AssayValue;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.NumericUpDown numUDQC_AcceptLimits;
        private System.Windows.Forms.TextBox txtQCCreate_Name;
        private System.Windows.Forms.Label label40;
        internal System.Windows.Forms.Button bttnQC_Create;
        internal System.Windows.Forms.Button bttnSaveSample;
        internal System.Windows.Forms.Button bttnStartSet;
        private System.Windows.Forms.DataGridView dgvSet;
        private System.Windows.Forms.TextBox txtSetName;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Button bttnSaveSet;
        internal System.Windows.Forms.Button bttnLoadSet;
        internal System.Windows.Forms.Button bttnSampleSet_Clear;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtQCCreate_LotNumber;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtQCCreate_CellTypeName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpQCExpires;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtQCCreate_Comment;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ListBox lstCellTypes;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ListBox lstQualityControls;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblWastTubeCapacity;
        private System.Windows.Forms.Label lblReagentUses;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblInstrumentID;
        private System.Windows.Forms.Label lblInstrumentStatus;
        private System.Windows.Forms.Label lblFilterType;
        private System.Windows.Forms.TextBox txtSampleResults_SearchString;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtSampleResults_Username;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.DateTimePicker dtpSampleResults_EndDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpSampleResults_StartDate;
        internal System.Windows.Forms.Button bttnGetResults;
        private System.Windows.Forms.TabControl tabCtrlResults;
        private System.Windows.Forms.TabPage tpgGetResults;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiUtils;
        private System.Windows.Forms.ToolStripMenuItem tsmiUtils_CreateSingle;
        private System.Windows.Forms.ToolStripMenuItem tsmiUtils_CreateSet;
        internal System.Windows.Forms.Button bttnDeleteResult;
        internal System.Windows.Forms.Button bttnExportResult;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ListBox lstResultIds;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblSampleResultCount;
        private System.Windows.Forms.Label SampleResults_CTQCLabel;
        private System.Windows.Forms.TextBox SampleResults_CTQCNameBox;
        internal System.Windows.Forms.Button bttnCT_Refresh;
        internal System.Windows.Forms.Button bttnQC_Refresh;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox SampleFilterComboBox;
        private System.Windows.Forms.RadioButton rdoSortColumn;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.RadioButton rdoSortRow;
        private System.Windows.Forms.Label SampleResults_TagLabel;
        private System.Windows.Forms.TextBox SampleResults_TagBox;
        private System.Windows.Forms.Label lblSelectedSampleId;
        private System.Windows.Forms.ListBox lstCallResult;
        private System.Windows.Forms.ToolStripMenuItem tsmiCnxConfig;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label CurrentSampleStatus;
        private System.Windows.Forms.Label CurrentSamplePosition;
        private System.Windows.Forms.Label CurrentSampleOwner;
        private System.Windows.Forms.Label CurrentSampleId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label LastSampleTxt;
        private System.Windows.Forms.ListBox WorklistBox;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button bttnGetDiskSpace;
        private System.Windows.Forms.ProgressBar progressBarDiskSpace;
        private System.Windows.Forms.Label legendExport;
        private System.Windows.Forms.Label rectangleExportDiskSpace;
        private System.Windows.Forms.Label rectangleDataDiskSpace;
        private System.Windows.Forms.Label rectangleOtherDiskSpace;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label diskSpaceLabel;
        private System.Windows.Forms.ListBox listBoxCurrentSampleResult;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ListBox lstResultsStatus;
        private System.Windows.Forms.ListBox lstSampleCompleteData;
		private System.Windows.Forms.Button btnCleanFluidics;
		private System.Windows.Forms.Label lblCleanFluidicsStatus;
		private System.Windows.Forms.ComboBox cbxSelectReagent;
		private System.Windows.Forms.Button btnAddReagentVolume;
		private System.Windows.Forms.Button btnSetReagentVolume;
		private System.Windows.Forms.Button btnGetReagentVolume;
		private System.Windows.Forms.Label lblReagentVolume;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.TextBox txtAddReagentVolume;
		private System.Windows.Forms.TextBox txtSetReagentVolume;
		private System.Windows.Forms.Button btnDeleteAllResults;
		private System.Windows.Forms.Button btnReboot;
		private System.Windows.Forms.Button btnShutdown;
		private System.Windows.Forms.Button bttnDeleteCampaignData;
		private System.Windows.Forms.TabControl tabControlReagents;
		private System.Windows.Forms.TabPage tpgReagentVolume;
		private System.Windows.Forms.TabPage tpgCleanFluidics;
		private System.Windows.Forms.TabPage tpgPrimeReagents;
		private System.Windows.Forms.Button btnPrimeReagents;
		private System.Windows.Forms.Label lblPrimeReagentsStatus;
		private System.Windows.Forms.TabPage tpgPurgeReagents;
		private System.Windows.Forms.Button btnPurgeReagents;
		private System.Windows.Forms.Label lblPurgeReagentssStatus;
		private System.Windows.Forms.Button btnCancelPrimeReagents;
		private System.Windows.Forms.Button btnCancelPurgeReagents;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button bttnStartLogDataExport;
		private System.Windows.Forms.DateTimePicker dtpConfigAudit_StartDate;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.ListBox lstConfigAuditStatus;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.DateTimePicker dtpConfigAudit_EndDate;
		private System.Windows.Forms.Label label60;
		internal System.Windows.Forms.Button bttnExportConfig;
		internal System.Windows.Forms.Button bttnImportConfig;
		private System.Windows.Forms.TabPage tpgDecontaminate;
		private System.Windows.Forms.Button btnCancelDecontaminate;
		private System.Windows.Forms.Button btnDecontaminate;
		private System.Windows.Forms.Label lblDecontaminateStatus;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.Label lblSoftwareVersion;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.Label lblFirmwareVersion;
		private System.Windows.Forms.ListBox lstErrorStatus;
		private System.Windows.Forms.Button btnClearErrorStatus;
		internal System.Windows.Forms.Button btnExportEncryptedResult;
		private System.Windows.Forms.TabPage tabErrorStatus;
		private System.Windows.Forms.Label lblAppVersion;
		private System.Windows.Forms.GroupBox gb_PostWashOption;
		private System.Windows.Forms.RadioButton rb_PostWashFast;
		private System.Windows.Forms.RadioButton rb_PostWashNormal;
		private System.Windows.Forms.GroupBox cb_CellTypeorQC;
		private System.Windows.Forms.RadioButton rbQualityControl;
		private System.Windows.Forms.RadioButton rbCellType;
	}
}
