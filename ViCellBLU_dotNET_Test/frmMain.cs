using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Reflection;

using Opc;
using Opc.Ua;

using ViCellBlu;
using ViCellBLU_dotNET;
using CellType = ViCellBlu.CellType;
using SampleConfig = ViCellBLU_dotNET.SampleConfig;
using SamplePosition = ViCellBLU_dotNET.SamplePosition;
using Opc.Ua.Client;

namespace ViCellBLU_dotNET_Test
{
    public partial class frmMain : Form
    {

        ViCellBLU _myBlu = null;
        private int _progressBarWidth; //reference point for disk space progress bars
        static private int ReagentTypeEnumOffset = 1;

        // ******************************************************************
        public frmMain()
        {
            InitializeComponent();
        }

        // ******************************************************************
        private static void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (e.Error.StatusCode != StatusCodes.BadCertificateUntrusted)
            {
                return;
            }
            // Always accepts the certificate 
            e.Accept = true;
            return;
        }


        // ******************************************************************
        private static void SetOpcCertificate(ViCellBLU blu)
        {
            blu.OpcAppConfig.SecurityConfiguration.ApplicationCertificate = new CertificateIdentifier
            {
                StoreType = @"Directory",
                StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\own",
                SubjectName = "CN=Vi-Cell BLU Client, C=US, S=Colorado, O=Beckman Coulter, DC=" + Dns.GetHostName()
            };
            blu.OpcAppConfig.SecurityConfiguration.TrustedIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"% %\ViCellBlu_dotNET\pki\issuers" };
            blu.OpcAppConfig.SecurityConfiguration.TrustedPeerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\trusted" };
            blu.OpcAppConfig.SecurityConfiguration.RejectedCertificateStore = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\rejected" };
            blu.OpcAppConfig.SecurityConfiguration.UserIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\issuerUser" };
            blu.OpcAppConfig.SecurityConfiguration.TrustedUserCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\trustedUser" };
            blu.OpcAppConfig.SecurityConfiguration.AddAppCertToTrustedStore = true;
            blu.OpcAppConfig.SecurityConfiguration.RejectSHA1SignedCertificates = false;
            blu.OpcAppConfig.SecurityConfiguration.RejectUnknownRevocationStatus = true;
            blu.OpcAppConfig.SecurityConfiguration.MinimumCertificateKeySize = 2048;
            blu.OpcAppConfig.SecurityConfiguration.SendCertificateChain = true;
            blu.OpcAppConfig.SecurityConfiguration.AutoAcceptUntrustedCertificates = true;
            blu.OpcAppConfig.CertificateValidator.CertificateValidation += CertificateValidator_CertificateValidation;
        }

        // ******************************************************************
        private void OnUpdateLockStateCB(LockStateEnum state)
        {
            Invoke(new Action(() =>
            {
                grpInstrumentLocked.Text = state.ToString();
            })); 
        }

        // ******************************************************************
        private void OnUpdateReagentCB(UInt32 remaining)
        {
            Invoke(new Action(() =>
            {
                lblReagentUses.Text = remaining.ToString();
            }));
        }

        // ******************************************************************
        private void OnUpdateWasteTubeCB(UInt32 remaining)
        {
            Invoke(new Action(() =>
            {
                lblWastTubeCapacity.Text = remaining.ToString();
            }));
        }

        // ******************************************************************
        private void OnUpdateSystemStatusCB(ViCellStatusEnum status)
        {
            Invoke(new Action(() =>
            {
                lblInstrumentStatus.Text = status.ToString();
                switch (status)
                {
                    case ViCellStatusEnum.Stopped:
                    case ViCellStatusEnum.Stopping:
                    case ViCellStatusEnum.Paused:
                    {
                        lblInstrumentStatus.BackColor = Color.Orange;
                        break;
                    }
                    case ViCellStatusEnum.Pausing:
                    {
                        lblInstrumentStatus.BackColor = Color.Goldenrod;
                        break;
                    }
                    case ViCellStatusEnum.Idle:
                    case ViCellStatusEnum.NightlyClean:
                    {
                        lblInstrumentStatus.BackColor = Color.LightBlue;
                        break;
                    }
                    case ViCellStatusEnum.Faulted:
                    {
                        lblInstrumentStatus.BackColor = Color.Salmon;
                        break;
                    }
                    case ViCellStatusEnum.SearchingForTubes:
                    case ViCellStatusEnum.Running:
                    {
                        lblInstrumentStatus.BackColor = Color.LightGreen;
                        break;
                    }
                }
            }
            ));
        }

        private void OnExportStatusCB(ExportStatusData status)
        {
            if (!_exportInProgress) return;

            Invoke(new Action(() =>
            {
	            lstResultsStatus.Items.Clear();
	            lstResultsStatus.BackColor = Color.Yellow;
	            lstResultsStatus.Items.Add("Id:");
	            lstResultsStatus.Items.Add(status.BulkDataId);
	            lstResultsStatus.Items.Add("Status : ");
	            lstResultsStatus.Items.Add(status.ExportStatus);
	            lstResultsStatus.Items.Add("Percent: ");
	            lstResultsStatus.Items.Add(status.ExportPercent.ToString());

	            lstConfigAuditStatus.Items.Clear();
	            lstConfigAuditStatus.BackColor = Color.Yellow;
	            lstConfigAuditStatus.Items.Add("Id:");
	            lstConfigAuditStatus.Items.Add(status.BulkDataId);
	            lstConfigAuditStatus.Items.Add("Status : ");
	            lstConfigAuditStatus.Items.Add(status.ExportStatus);
	            lstConfigAuditStatus.Items.Add("Percent: ");
	            lstConfigAuditStatus.Items.Add(status.ExportPercent.ToString());
            }));
        }

		private bool _exportInProgress = false;
        private void OnExportCompleteCB(ExportStatusEnum status, string outFile)
        {
            Invoke(new Action(() =>
            {
                lstResultsStatus.Items.Clear();
                if (status == ExportStatusEnum.Ready)
                {
                    lstResultsStatus.BackColor = Color.LightGreen;
                }
                else
                {
                    lstResultsStatus.BackColor = Color.Tomato;
                }
                lstResultsStatus.Items.Add("Export Complete");
                lstResultsStatus.Items.Add("  From: " + dtpConfigAudit_StartDate.Value.ToString());
                lstResultsStatus.Items.Add("    To: " + dtpConfigAudit_EndDate.Value.ToString());
                lstResultsStatus.Items.Add("Outfile: ");
                lstResultsStatus.Items.Add(outFile);

                lstConfigAuditStatus.Items.Clear();
                if (status == ExportStatusEnum.Ready)
                {
	                lstConfigAuditStatus.BackColor = Color.LightGreen;
                }
                else
                {
	                lstConfigAuditStatus.BackColor = Color.Tomato;
                }
                lstConfigAuditStatus.Items.Add("Export Complete");
                lstConfigAuditStatus.Items.Add("  From: " + dtpConfigAudit_StartDate.Value.ToString());
                lstConfigAuditStatus.Items.Add("    To: " + dtpConfigAudit_EndDate.Value.ToString());
                lstConfigAuditStatus.Items.Add("Outfile: ");
                lstConfigAuditStatus.Items.Add(outFile);

				_exportInProgress = false;
            }));
        }

        private void OnDeleteStatusUpdateCB(DeleteSampleStatus status)
        {
            Invoke(new Action(() =>
            {
                lstResultsStatus.Items.Clear();
                lstResultsStatus.Items.Add("Delete Progress:");
                lstResultsStatus.Items.Add("Status : ");
                lstResultsStatus.Items.Add(status.DeleteStatus);
                lstResultsStatus.Items.Add("Percent: ");
                lstResultsStatus.Items.Add(status.DeletePercent.ToString());

                if (status.DeleteStatus == DeleteStatusEnum.Failed)
                {
                    lstResultsStatus.BackColor = Color.Tomato;
                }
                else if (status.DeleteStatus == DeleteStatusEnum.Deleting)
                {
                    lstResultsStatus.BackColor = Color.Yellow;
                }
                else if (status.DeleteStatus == DeleteStatusEnum.Done)
                {
                    lstResultsStatus.BackColor = Color.LightGreen;
                }
                else
                {
                    lstResultsStatus.BackColor = Color.Red;
                }
            }));
        }

        private void OnUpdateViCellIdentifierCB(string id)
        {
            Invoke(new Action(() => lblInstrumentID.Text = id));
        }

        private void ReconnectHandlerCB()
        {
            if (_myBlu.IsConnected)
            {
                Invoke(new Action(() =>
                {
                    grpConnected.Enabled = true;
                    grpConnected.Text = "Connected";
                    ClearCallResultDisplay(Color.LightGreen);
                    lstCallResult.Items.Add("Reconnected");
                    bttnConnect.Text = "Disconnect";
                    bttnRequestLock.Select();
                }));
            }
        }

        private void DisconnectHandlerCB(StatusCode code)
        {
            if (_myBlu.IsConnected)
            {
                Invoke(new Action (() =>
                {
                    ClearInstrumentInfo();
                    ClearCallResultDisplay(Color.Salmon);
                    lstCallResult.Items.Add("Disconnected");
                    _myBlu.Disconnect();
                    grpConnected.Enabled = false;
                    grpConnected.Text = "Disconnected";
                    bttnConnect.Text = "Connect";
                    grpInstrumentLocked.Text = "*** ??? ***";
                    bttnConnect.Select();
                    rectangleOtherDiskSpace.Width = 0;
                    rectangleDataDiskSpace.Width = 0;
                    rectangleExportDiskSpace.Width = 0;
                    diskSpaceLabel.Text = string.Empty;
                    ShowButtons(true);
                }));
            }
        }

        private void OnSampleStatusChangedCB(SampleStatusData status)
        {
            Invoke(new Action(() => 
            {
                if (status.SampleStatus == SampleStatusEnum.Completed)
                {
                    LastSampleTxt.Text = "";
                    LastSampleTxt.Text += status.SampleDataUuid.ToString() + "\r\n";
                    LastSampleTxt.Text += status.SampleId + "\r\n";
                    LastSampleTxt.Text += "(" + status.SamplePosition.Row + ", " + status.SamplePosition.Column + ")" + "\r\n";
                    LastSampleTxt.Text += status.AnalysisBy + "\r\n";
                    if (!status.AnalysisBy.Equals(_myBlu.LastSampleResult.AnalysisBy))
                        lstSampleCompleteData.Items.Clear();
                }
                else
                {
                    CurrentSampleId.Text = status.SampleId;
                    CurrentSampleStatus.Text = status.SampleStatus.ToString();
                    CurrentSamplePosition.Text = "(" + status.SamplePosition.Row + ", " + status.SamplePosition.Column.ToString() + ")";
                    CurrentSampleOwner.Text = status.AnalysisBy;
                }
            }));
        }

        private void OnSampleCompleteCB(SampleResult result)
        {
            Invoke(new Action(() =>
            {
                lstSampleCompleteData.Items.Clear();
                Misc.PopulateSampleResultsBox(result, ref lstSampleCompleteData);
            }));
        }

        private void OnWorklistCompleteCB(Uuid[] uuids)
        {
            Invoke(new Action(() =>
            {
                WorklistBox.Items.Clear();
                CurrentSampleId.Text = "---";
                CurrentSampleStatus.Text = "---";
                CurrentSamplePosition.Text = "---";
                CurrentSampleOwner.Text = "---";
                foreach (Uuid uuid in uuids)
                {
                    WorklistBox.Items.Add(uuid.GuidString);
                }
            }));
        }

        private void OnCleanFluidicsStatusUpdateCB(CleanFluidicsStatus obj)
        {
	        if (obj.Status == CleanFluidicsStatusEnum.Completed)
		        obj.Status = CleanFluidicsStatusEnum.Idle;

			Invoke(new Action(() =>
	        {
				lblCleanFluidicsStatus.Text = obj.Status.ToString();
			}));
        }

		private void OnPrimeReagentsStatusUpdateCB(PrimeReagentsStatus obj)
		{
			if (obj.Status == PrimeReagentsStatusEnum.Completed)
				obj.Status = PrimeReagentsStatusEnum.Idle;

			Invoke(new Action(() =>
			{
				lblPrimeReagentsStatus.Text = obj.Status.ToString();
			}));
		}

		private void OnPurgeReagentsStatusUpdateCB(PurgeReagentsStatus obj)
		{
			if (obj.Status == PurgeReagentsStatusEnum.Completed)
				obj.Status = PurgeReagentsStatusEnum.Idle;

			Invoke(new Action(() =>
			{
				lblPurgeReagentssStatus.Text = obj.Status.ToString();
			}));
		}

		private void OnDecontaminateStatusUpdateCB(DecontaminateStatus obj)
		{
			if (obj.Status == DecontaminateStatusEnum.Completed)
				obj.Status = DecontaminateStatusEnum.Idle;

			Invoke(new Action(() =>
			{
				lblDecontaminateStatus.Text = obj.Status.ToString();
			}));
		}

		private void OnSoftwareVersionUpdateCB(string id)
		{
			Invoke(new Action(() => lblSoftwareVersion.Text = id));
		}

		private void OnFirmwareVersionUpdateCB(string id)
		{
			Invoke(new Action(() => lblFirmwareVersion.Text = id));
		}

		private void OnErrorStatusUpdateCB(ErrorStatusType obj)
		{
			Invoke(new Action(() =>
			{
				var str = obj.ErrorCode + " | " + obj.Severity + " | " + obj.System + " | " +
				          obj.SubSystem + " | " + obj.Instance + " | " + obj.FailureMode;
				lstErrorStatus.Items.Add(str);
			}));
		}

		DataTable _dtSet;
        // ******************************************************************
        private void frmMain_Load(object sender, EventArgs e)
        {
	        _myBlu = new ViCellBLU();
	        _progressBarWidth = progressBarDiskSpace.Width;
	        SetOpcCertificate(_myBlu);

            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            lblAppVersion.Text = "Version " + ver.ToString();

	        grpConnected.Enabled = true;
	        cbxCT_Decluster.SelectedIndex = 0;
	        cbxQC_AssayParam.SelectedIndex = 0;
	        cbxSelectReagent.SelectedIndex = 0;
	        txtIPAddr.Text = Properties.Settings.Default.IPAddr;
	        txtPort.Text = Properties.Settings.Default.CnxPort.ToString();
	        txtUserName.Text = Properties.Settings.Default.Username;
	        txtPassword.Text = Properties.Settings.Default.Password;
	        bttnConnect.Select();
	        SampleFilterComboBox.SelectedIndex = 0;

	        _dtSet = CreateSampleTable();
	        dgvSet.DataSource = _dtSet;
	        UpdateSetGridWidths();

	        grpConnected.Enabled = false;
	        bttnConnect.Text = "Connect";

	        ClearInstrumentInfo();

	        _myBlu.OnUpdateLockState += OnUpdateLockStateCB;
	        _myBlu.OnUpdateSystemStatus += OnUpdateSystemStatusCB;
	        _myBlu.OnUpdateReagentRemaining += OnUpdateReagentCB;
	        _myBlu.OnUpdateWasteTubeCapacity += OnUpdateWasteTubeCB;
	        _myBlu.OnUpdateViCellIdentifier += OnUpdateViCellIdentifierCB;
	        _myBlu.OnDisconnect += DisconnectHandlerCB;
	        _myBlu.OnReconnect += ReconnectHandlerCB;
	        _myBlu.OnSampleStatusChanged += OnSampleStatusChangedCB;
	        _myBlu.OnSampleComplete += OnSampleCompleteCB;
	        _myBlu.OnWorklistComplete += OnWorklistCompleteCB;
	        _myBlu.OnExportStatusUpdate += OnExportStatusCB;
	        _myBlu.OnExportComplete += OnExportCompleteCB;
	        _myBlu.OnDeleteSampleStatusUpdate += OnDeleteStatusUpdateCB;
	        _myBlu.OnCleanFluidicsStatusUpdate += OnCleanFluidicsStatusUpdateCB;
	        _myBlu.OnPrimeReagentsStatusUpdate += OnPrimeReagentsStatusUpdateCB;
	        _myBlu.OnPurgeReagentsStatusUpdate += OnPurgeReagentsStatusUpdateCB;
	        _myBlu.OnDecontaminateStatusUpdate += OnDecontaminateStatusUpdateCB;
	        _myBlu.OnSoftwareVersionUpdate += OnSoftwareVersionUpdateCB;
	        _myBlu.OnFirmwareVersionUpdate += OnFirmwareVersionUpdateCB;
	        _myBlu.OnErrorStatusUpdate += OnErrorStatusUpdateCB;

			var obj = new CleanFluidicsStatus()
	        {
		        Status = CleanFluidicsStatusEnum.Idle
	        };
	        OnCleanFluidicsStatusUpdateCB(obj);
        }

        // ******************************************************************
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (_myBlu != null)
                {
                    if (_myBlu.IsConnected)
                    {
                        _myBlu.ReleaseLock();
                        _myBlu.Disconnect();
                    }
                    _myBlu = null;
                }
            }
            catch { }
        }

        // ******************************************************************
        private void ClearCallResultDisplay(Color back)
        {
            lstCallResult.BackColor = Color.White;
            Refresh();
            System.Threading.Thread.Sleep(75);

            lstCallResult.Items.Clear();
            lstCallResult.BackColor = back;
            Refresh();
            System.Threading.Thread.Sleep(150);
        }

        // ******************************************************************
        private void UpdateCallResultDisplay(MethodResultEnum methodResult, ErrorLevelEnum errorLevel, string description)
        {
            lstCallResult.Items.Add(methodResult.ToString() + " : " + errorLevel.ToString());
            string respStr = description;
            if ((respStr != null) && (respStr.Length > 0))
            {
                List<string> output = new List<string>();
                int idx = respStr.IndexOf("Cannot perform");
                if (idx > 0)
                {
                    string tmp = respStr.Substring(idx, respStr.Length - idx - 1);
                    respStr = respStr.Substring(0, idx - 1);
                    output.Add(respStr);
                    idx = tmp.IndexOf("The Instrument's");
                    if (idx > 0)
                    {
                        string tmp1 = tmp.Substring(idx, tmp.Length - idx - 1);
                        tmp = tmp.Substring(0, idx - 1);
                        output.Add(tmp);
                        output.Add(tmp1);
                    }
                    else
                    {
                        output.Add(tmp);
                    }
                }
                else if (methodResult != MethodResultEnum.Success)
                    output.Add(description);
                foreach (var st in output)
                {
                    lstCallResult.Items.Add(st);
                }
            }
            if (methodResult == MethodResultEnum.Failure)
            {
                lstCallResult.BackColor = Color.Salmon;
            }
            else
            {
                lstCallResult.BackColor = Color.LightGreen;
            }
        }


        // ******************************************************************
        private void ClearInstrumentInfo()
        {
            lblInstrumentStatus.Text = "---";
            lblInstrumentID.Text = "---";
            listBoxCurrentSampleResult.Items.Clear();
            lblReagentUses.Text = "---";
            lblSampleResultCount.Text = "---";
            lblSelectedSampleId.Text = "---";
            lblWastTubeCapacity.Text = "---";
            lstResultIds.Items.Clear();
            lblInstrumentStatus.BackColor = Color.Gainsboro;
            lstResultsStatus.Items.Clear();
            lstResultsStatus.BackColor = Color.White;
            lstConfigAuditStatus.Items.Clear();
            lstConfigAuditStatus.BackColor = Color.White;
            lstCellTypes.Items.Clear();
            lstQualityControls.Items.Clear();

            lstSampleCompleteData.Items.Clear();
            lstSampleCompleteData.Items.Add("Left click to Refresh");
            lstSampleCompleteData.Items.Add("");
            lstSampleCompleteData.Items.Add("Right click to Clear");

        }

        // ******************************************************************
        private void bttnConnect_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.White);
            ClearInstrumentInfo();

            grpInstrumentLocked.Text = "*** ??? ***";

            try
            {
                if (_myBlu.IsConnected)
                {
                    ClearCallResultDisplay(Color.Yellow);
                    var res = _myBlu.Disconnect();
                    UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
                }
                else
                {
                    IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
                    UInt32 port = 62641;
                    UInt32.TryParse(txtPort.Text, out port);
                    IPAddress.TryParse(txtIPAddr.Text, out ipAddr);

                    Properties.Settings.Default.IPAddr = txtIPAddr.Text;
                    Properties.Settings.Default.CnxPort = port;
                    Properties.Settings.Default.Username = txtUserName.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;
                    Properties.Settings.Default.Save();

                    ClearCallResultDisplay(Color.Yellow);

                    var res = _myBlu.Connect(txtUserName.Text, txtPassword.Text, ipAddr, port);
                    UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
                }

                if (_myBlu.IsConnected)
                {
                    grpConnected.Enabled = true;
                    grpConnected.Text = "Connected";
                    bttnConnect.Text = "Disconnect";
                    var tmpDateTime = DateTime.Now.AddDays(-7);
                    dtpSampleResults_StartDate.Value = new DateTime(tmpDateTime.Year, tmpDateTime.Month, tmpDateTime.Day, 0, 0, 0);
                    dtpConfigAudit_StartDate.Value = new DateTime(tmpDateTime.Year, tmpDateTime.Month, tmpDateTime.Day, 0, 0, 0);
                    tmpDateTime = DateTime.Now;
                    dtpSampleResults_EndDate.Value = new DateTime(tmpDateTime.Year, tmpDateTime.Month, tmpDateTime.Day, 23, 59, 59);
                    dtpConfigAudit_EndDate.Value = new DateTime(tmpDateTime.Year, tmpDateTime.Month, tmpDateTime.Day, 23, 59, 59);
                    dtpQCExpires.Value = DateTime.Now.AddDays(14);
                    bttnRequestLock.Select();
                }
                else
                {
                    grpConnected.Enabled = false;
                    grpConnected.Text = "Disconnected";
                    bttnConnect.Text = "Connect";
                    bttnConnect.Select();
                    rectangleOtherDiskSpace.Width = 0;
                    rectangleDataDiskSpace.Width = 0;
                    rectangleExportDiskSpace.Width = 0;
                    diskSpaceLabel.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                lstCallResult.Items.Add("Connect exception:");
                lstCallResult.Items.Add(ex.ToString());
            }

            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnRequestLock_Click(object sender, EventArgs e)
        {

            ShowButtons(false);
            ClearCallResultDisplay(Color.White);
            string prevText = grpInstrumentLocked.Text;
            grpInstrumentLocked.Text = "*** ??? ***";
            try
            {
                if (_myBlu != null)
                {
                    if (_myBlu.IsConnected)
                    {
                        ClearCallResultDisplay(Color.Yellow);
                        var res = _myBlu.RequestLock();
                        UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
                        if (res.MethodResult == MethodResultEnum.Failure)
                        {
                            grpInstrumentLocked.Text = prevText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstCallResult.Items.Add("RequestLock exception:");
                lstCallResult.Items.Add(ex.ToString());
            }

            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnUnlock_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            string prevText = grpInstrumentLocked.Text;
            grpInstrumentLocked.Text = "*** ??? ***";
            try
            {
                if (_myBlu != null)
                {
                    if (_myBlu.IsConnected)
                    {
                        ClearCallResultDisplay(Color.Yellow);
                        var res = _myBlu.ReleaseLock();
                        UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
                        if (res.MethodResult == MethodResultEnum.Failure)
                        {
                            grpInstrumentLocked.Text = prevText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstCallResult.Items.Clear();
                lstCallResult.BackColor = Color.Red;
                lstCallResult.Items.Add("Unlock exception:");
                lstCallResult.Items.Add(ex.ToString());
            }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnStartSample_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                if (txtSampleCellType.Text.Length > 0)
                {
                    SampleConfig sample = new SampleConfig();
                    sample.Name = txtSampleName.Text;
                    sample.Tag = txtSampleTag.Text;
                    sample.Dilution = (uint)numUD_Dilution.Value;
                    sample.SaveEveryNthImage = (uint)numUD_NthImageSave.Value;
                    sample.CellTypeName = txtSampleCellType.Text;
                    sample.QCName = "";
                    var res = _myBlu.StartSample(sample);
                    UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);

                }
                else if (txtSampleQCType.Text.Length > 0)
                {
                    SampleConfig sample = new SampleConfig();
                    sample.Name = txtSampleName.Text;
                    sample.Tag = txtSampleTag.Text;
                    sample.Dilution = (uint)numUD_Dilution.Value;
                    sample.SaveEveryNthImage = (uint)numUD_NthImageSave.Value;
                    sample.CellTypeName = "";
                    sample.QCName = txtSampleQCType.Text;

                    var res = _myBlu.StartSample(sample);
                    UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
                }
            }
            catch (Exception ex)
            {
                lstCallResult.Items.Add("StartSample exception:");
                lstCallResult.Items.Add(ex.ToString());
            }
            ShowButtons(true);
        }



        // ******************************************************************
        public static bool RunSample(String strConnectionConfig, String strSampleConfig, bool isSampleSetSet, bool waitComplete)
        {
            if (!File.Exists(strConnectionConfig) || !File.Exists(strSampleConfig))
            {
                Console.WriteLine("Run sample failed - no file: " + strConnectionConfig + " or " + strSampleConfig);
                return false;
            }

            ConnectionCfg cnxCfg = XmlUtils<ConnectionCfg>.Load(strConnectionConfig);
            if (cnxCfg == null)
            {
                Console.WriteLine("Failed to load : " + strConnectionConfig);
                return false;
            }

            SampleSetConfig setConfig = new SampleSetConfig();
            SampleConfig sampleConfig = new SampleConfig();

            if (isSampleSetSet)
            {
                setConfig = XmlUtils<SampleSetConfig>.Load(strSampleConfig);
                if (setConfig == null)
                {
                    Console.WriteLine("Failed to load RunSampleSetCfg: " + strSampleConfig);
                    return false;
                }
                Console.WriteLine("Loaded RunSampleSetCfg: " + strSampleConfig);
            }
            else
            {
                sampleConfig = XmlUtils<SampleConfig>.Load(strSampleConfig);
                if (sampleConfig == null)
                {
                    Console.WriteLine("Failed to load RunSingleSampleCfg: " + strSampleConfig);
                    return false;
                }
                Console.WriteLine("Loaded RunSingleSampleCfg: " + strSampleConfig);
            }

            ViCellBLU theInstrument = new ViCellBLU();
            SetOpcCertificate(theInstrument);

            IPAddress ipAddr;
            if (!IPAddress.TryParse(cnxCfg.IPAddr, out ipAddr))
            {
                Console.WriteLine("Invalid IP Addr: " + cnxCfg.IPAddr);
                return false;
            }

            theInstrument.Connect(cnxCfg.Username, cnxCfg.Password, ipAddr, cnxCfg.Port);
            if (!theInstrument.IsConnected)
            {
                Console.WriteLine("Failed to connect to: " + cnxCfg.IPAddr + ":" + cnxCfg.Port.ToString());
                return false;
            }

            Console.WriteLine("Connected to " + cnxCfg.IPAddr + ":" + cnxCfg.Port.ToString());

            if (theInstrument.RequestLock().MethodResult != MethodResultEnum.Success)
            {
                Console.WriteLine("Failed to get Lock");
                theInstrument.Disconnect();
                return false;
            }

            const double kSECONDS_PER_SAMPLE = 210;

            double expectedRunSeconds = kSECONDS_PER_SAMPLE;
            Console.WriteLine("Locked OK");
            VcbResult callResult = new VcbResult { MethodResult = MethodResultEnum.Failure };

            if (isSampleSetSet)
            {
                Console.WriteLine("Start Sample Set");
                callResult = theInstrument.StartSampleSet(setConfig, PlatePrecessionEnum.ColumnMajor);
                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    expectedRunSeconds = (UInt32)(kSECONDS_PER_SAMPLE * setConfig.Samples.Count);
                }
            }
            else
            {
                Console.WriteLine("Start Single Sample");
                callResult = theInstrument.StartSample(sampleConfig);
                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    expectedRunSeconds = kSECONDS_PER_SAMPLE;
                }
            }

            if (callResult.MethodResult == MethodResultEnum.Success)
            {

                DateTime startTime = DateTime.Now;
                if (waitComplete)
                {
                    double remaining = 0;
                    // wait for the sate to change from Idle
                    Console.WriteLine("Waiting for the sample to start ...");
                    do
                    {
                        Thread.Sleep(5 * 1000);
                        TimeSpan tsElapsed = DateTime.Now.Subtract(startTime);
                        remaining = 30 - tsElapsed.TotalSeconds;
                        if (theInstrument.CurrentStatus != ViCellStatusEnum.Idle)
                        {
                            break;
                        }
                        Console.WriteLine("Waiting for started signal: " + remaining.ToString("N0"));
                    } while (remaining > 0);

                    Console.WriteLine("Waiting for the sample to complete ...");
                    do
                    {
                        Thread.Sleep(15*1000);
                        TimeSpan tsElapsed = DateTime.Now.Subtract(startTime);
                        remaining = expectedRunSeconds - tsElapsed.TotalSeconds;
                        if (theInstrument.CurrentStatus == ViCellStatusEnum.Idle)
                        {
                            break;
                        }
                        Console.WriteLine("Max seconds remaining: " + remaining.ToString("N0"));
                    } while (remaining > 0);
                }
            }
            else
            {
                Console.WriteLine("Failed to start sample / sample set");
            }
            Console.WriteLine("Release lock");
            theInstrument.ReleaseLock();

            Console.WriteLine("Disconnect");
            theInstrument.Disconnect();
            return true;

        }

        // ******************************************************************
        public static bool RunAPITests(String testCfg)
        {
            System.Threading.Thread.Sleep(3000);
            return false;
        }


        // ******************************************************************
        private void bttnLoadSample_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                ofd.Filter = "Sample Config (.bsc)|*.bsc";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var path = Path.GetDirectoryName(ofd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }

                    var cfg = XmlUtils<SampleConfig>.Load(ofd.FileName);
                    if (cfg == null)
                    {
                        ShowButtons(true);
                        return;
                    }

                    txtSampleName.Text = cfg.Name;
                    txtSampleTag.Text = cfg.Tag;
                    txtSampleCellType.Text = cfg.CellTypeName;
                    txtSampleQCType.Text = cfg.QCName;
                    numUD_Dilution.Value = cfg.Dilution;
                    numUD_NthImageSave.Value = cfg.SaveEveryNthImage;
                }
            }
            catch { }
            ShowButtons(true);

        }

        // ******************************************************************
        private void ShowButtons(bool show)
        {
            bttnSamplePause.Visible = show;
            bttnSamplePause.Enabled = show;

            bttnSampleResume.Visible = show;
            bttnSampleResume.Enabled = show;

            bttnSampleStop.Visible = show;
            bttnSampleStop.Enabled = show;

            bttnEjectStage.Visible = show;
//NOTE: disabled this button.  CellHealth module does not have a stage.
//            bttnEject.Enabled = show;

            bttnCT_Create.Visible = show;
            bttnCT_Create.Enabled = show;

            bttnCT_Delete.Visible = show;
            bttnCT_Delete.Enabled = show;

            bttnLoadSample.Visible = show;
            bttnLoadSample.Enabled = show;

            bttnLoadSet.Visible = show;
            bttnLoadSet.Enabled = show;

            bttnQC_Create.Visible = show;
            bttnQC_Create.Enabled = show;

            bttnQC_Load.Visible = show;
            bttnQC_Load.Enabled = show;

            bttnQC_Save.Visible = show;
            bttnQC_Save.Enabled = show;

            bttnSampleSet_Clear.Visible = show;
            bttnSampleSet_Clear.Enabled = show;

            bttnSaveSample.Visible = show;
            bttnSaveSample.Enabled = show;

            bttnSaveSet.Visible = show;
            bttnSaveSet.Enabled = show;

            bttnStartSample.Visible = show;
            bttnStartSample.Enabled = show;

            bttnStartSet.Visible = show;
            bttnStartSet.Enabled = show;

            bttnDeleteResult.Visible = show;
            bttnDeleteResult.Enabled = show;

            bttnExportResult.Visible = show;
            bttnExportResult.Enabled = show;

            btnExportEncryptedResult.Visible = show;
            btnExportEncryptedResult.Enabled = show;

	        bttnImportConfig.Visible = show;
            bttnImportConfig.Enabled = show;

            bttnExportConfig.Visible = show;
            bttnExportConfig.Enabled = show;

            bttnGetResults.Visible = show;
            bttnGetResults.Enabled = show;

            bttnUnlock.Visible = show;
            bttnUnlock.Enabled = show;

            bttnRequestLock.Visible = show;
            bttnRequestLock.Enabled = show;

            bttnGetDiskSpace.Visible = show;
            bttnGetDiskSpace.Enabled = show;

            btnCleanFluidics.Visible = show;

			btnPrimeReagents.Visible = show;
			btnPrimeReagents.Enabled = show;

			btnCancelPrimeReagents.Visible = show;
			btnCancelPrimeReagents.Enabled = show;

			btnPurgeReagents.Visible = show;
			btnPurgeReagents.Enabled = show;

			btnCancelPurgeReagents.Visible = show;
			btnCancelPurgeReagents.Enabled = show;

			btnDecontaminate.Visible = show;
			btnDecontaminate.Enabled = show;

			btnCancelDecontaminate.Visible = show;
			btnCancelDecontaminate.Enabled = show;

            Refresh();
            if (show)
            {
                Thread.Sleep(125);
            }

        }

        // ******************************************************************
        private void bttnSamplePause_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                var res = _myBlu.Pause();
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch { }
            ShowButtons(true);

        }

        // ******************************************************************
        private void bttnSampleResume_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                var res = _myBlu.Resume();
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnSampleStop_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                var res = _myBlu.Stop();
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnEject_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                var res = _myBlu.EjectStage();
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnCT_Create_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                CellType cell = new CellType();
                cell.CellTypeName = txtCTCreate_Name.Text;
                cell.ConcentrationAdjustmentFactor = (float)numUDCT_ConcAdjFactor.Value;
                cell.NumAspirationCycles = (int)numUDCT_AspCycles.Value;
                cell.DeclusterDegree = (DeclusterDegreeEnum)cbxCT_Decluster.SelectedIndex;
                cell.MaxDiameter = (double)numUDCT_MaxDiam.Value;
                cell.MinDiameter = (double)numUDCT_MinDiam.Value;
                cell.NumMixingCycles = (int)numUDCT_MixCycles.Value;
                cell.NumImages = (int)numUDCT_Images.Value;
                cell.CellSharpness = (float)numUDCT_Sharpness.Value;
                cell.MinCircularity = (double)numUDCT_MinCirc.Value;
                cell.ViableSpotArea = (float)numUDCT_ViaSpotArea.Value;
                cell.ViableSpotBrightness = (float)numUDCT_ViaSpotBright.Value;

                var res = _myBlu.CreateCellType(cell);
                if (res.ErrorLevel == ErrorLevelEnum.NoError)
                    bttnCT_Refresh_Click(null, null);
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);

            }
            catch (Exception ex)
            {
                lstCallResult.Items.Add("CreateCellType exception:");
                lstCallResult.Items.Add(ex.ToString());
            }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnCTCreate_Load_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                ofd.Filter = "Cell Type (.bct)|*.bct";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var path = Path.GetDirectoryName(ofd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }

                    var cell = XmlUtils<CellType>.Load(ofd.FileName);
                    if (cell == null)
                    {
                        ShowButtons(true);
                        return;
                    }

                    txtCTCreate_Name.Text = cell.CellTypeName;
                    numUDCT_MinDiam.Value = (decimal)cell.MinDiameter;
                    numUDCT_MaxDiam.Value = (decimal)cell.MaxDiameter;
                    numUDCT_Images.Value = cell.NumImages;
                    numUDCT_Sharpness.Value = (decimal)cell.CellSharpness;
                    numUDCT_MinCirc.Value = (decimal)cell.MinCircularity;
                    cbxCT_Decluster.SelectedIndex = (int)cell.DeclusterDegree;
                    numUDCT_AspCycles.Value = cell.NumAspirationCycles;
                    numUDCT_ViaSpotBright.Value = (decimal)cell.ViableSpotBrightness;
                    numUDCT_ViaSpotArea.Value = (decimal)cell.ViableSpotArea;
                    numUDCT_MixCycles.Value = cell.NumMixingCycles;
                    numUDCT_ConcAdjFactor.Value = (decimal)cell.ConcentrationAdjustmentFactor;

                }
            }
            catch { }
            ShowButtons(true);

        }

        // ******************************************************************
        private void bttnCTCreate_Save_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                var suggestFile = txtCTCreate_Name.Text;
                suggestFile = suggestFile.Replace(" ", "_");
                suggestFile += ".bct";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                sfd.FileName = suggestFile;
                sfd.Filter = "Cell Type (.bct)|*.bct";
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Save file
                    CellType cell = new CellType();
                    cell.CellTypeName = txtCTCreate_Name.Text;
                    cell.MinDiameter = (double)numUDCT_MinDiam.Value;
                    cell.MaxDiameter = (double)numUDCT_MaxDiam.Value;
                    cell.NumImages = (int)numUDCT_Images.Value;
                    cell.CellSharpness = (float)numUDCT_Sharpness.Value;
                    cell.MinCircularity = (double)numUDCT_MinCirc.Value;
                    cell.DeclusterDegree = (DeclusterDegreeEnum)cbxCT_Decluster.SelectedIndex;
                    cell.NumAspirationCycles = (int)numUDCT_AspCycles.Value;
                    cell.ViableSpotBrightness = (float)numUDCT_ViaSpotBright.Value;
                    cell.ViableSpotArea = (float)numUDCT_ViaSpotArea.Value;
                    cell.NumMixingCycles = (int)numUDCT_MixCycles.Value;
                    cell.ConcentrationAdjustmentFactor = (float)numUDCT_ConcAdjFactor.Value;

                    XmlUtils<CellType>.Save(sfd.FileName, cell);

                    var path = Path.GetDirectoryName(sfd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }

                }
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnCT_Delete_Click(object sender, EventArgs e)
        {
            if (lstCellTypes.SelectedIndex <= -1)
                return;
            ShowButtons(false);
            try
            {
                ClearCallResultDisplay(Color.Yellow);
                var res = _myBlu.DeleteCellType(_myBlu.GetCellTypes()[lstCellTypes.SelectedIndex].CellTypeName);
                if (res.ErrorLevel == ErrorLevelEnum.NoError)
                {
                    Invoke(new Action(() =>
                    {
						List<CellType> celltypes = _myBlu.GetCellTypes();
						lstCellTypes.Items.Clear();
						foreach (var cell in celltypes)
						{
							lstCellTypes.Items.Add(cell.CellTypeName);
						}
                    }));
				}
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch { }

            ShowButtons(true);
        }

        // ******************************************************************
        private SampleSetConfig GetSetConfig()
        {
            SampleSetConfig setCfg = new SampleSetConfig();
            setCfg.Name = txtSetName.Text;
            setCfg.PlateSortOrder = rdoSortColumn.Checked ? PlatePrecessionEnum.ColumnMajor : PlatePrecessionEnum.RowMajor;
            
            foreach (DataRow row in _dtSet.Rows)
            {
                SampleConfig sample = new SampleConfig();
                sample.Name = Convert.ToString(row[CN_Name]);
                sample.Tag = Convert.ToString(row[CN_Tag]);
                sample.Dilution = Convert.ToUInt32(row[CN_Dilution]);
                sample.SaveEveryNthImage = Convert.ToUInt32(row[CN_SaveEveryNthImage]);
                sample.CellTypeName = Convert.ToString(row[CN_CellTypeName]);
                sample.QCName = Convert.ToString(row[CN_QCName]);
                var r = char.ToUpper(Convert.ToChar(row[CN_Row]));
                sample.Position.Set((SamplePosition.RowDef)r, (SamplePosition.ColumnDef) Convert.ToByte(row[CN_Col]));
                var wt = Convert.ToUInt32(row[CN_WashType]);
                sample.WashType = (ViCellBLU.WashType)wt;
                setCfg.Samples.Add(sample);
            }
            return setCfg;

        }

        // ******************************************************************
        private void bttnStartSet_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                SampleSetConfig setCfg = GetSetConfig();
                ViCellBlu.PlatePrecessionEnum sortOrder = PlatePrecessionEnum.RowMajor;
                if (rdoSortColumn.Checked)
                    sortOrder = PlatePrecessionEnum.ColumnMajor;

                var res = _myBlu.StartSampleSet(setCfg, sortOrder);
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch (Exception ex)
            {
                lstCallResult.Items.Add("StartSampleSet exception:");
                lstCallResult.Items.Add(ex.ToString());
            }
            ShowButtons(true);
        }

		// ******************************************************************
		private void btnPrimeReagents_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.PrimeReagents();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		// ******************************************************************
		private void btnCancelPrimeReagents_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.CancelPrimeReagents();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		// ******************************************************************
		private void btnPurgeReagents_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.PurgeReagents();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		// ******************************************************************
		private void btnCancelPurgeReagents_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.CancelPurgeReagents();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		// ******************************************************************
		private void btnDecontaminate_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.Decontaminate();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		// ******************************************************************
		private void btnCancelDecontaminate_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.CancelDecontaminate();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

	#region String_Constants
        public const string CN_Name = "Name";
        public const string CN_Row = "Row";
        public const string CN_Col = "Col";
        public const string CN_Tag = "Tag";
        public const string CN_Dilution = "Dilution";
        public const string CN_CellTypeName = "CellTypeName";
        public const string CN_QCName = "QCName";
        public const string CN_SaveEveryNthImage = "SaveEveryNthImage";
        public const string CN_WashType = "WashType";
	#endregion

        // **************************************************************************
        public static DataTable CreateSampleTable()
        {
            DataTable dt = new DataTable("Samples");

            dt.Columns.Add(CN_Name, typeof(System.String));
            dt.Columns[CN_Name].DefaultValue = "";

            dt.Columns.Add(CN_Row, typeof(System.Char));
            dt.Columns[CN_Row].DefaultValue = "-";

            dt.Columns.Add(CN_Col, typeof(System.Byte));
            dt.Columns[CN_Col].DefaultValue = 0;

            dt.Columns.Add(CN_CellTypeName, typeof(System.String));
            dt.Columns[CN_CellTypeName].DefaultValue = "";

            dt.Columns.Add(CN_QCName, typeof(System.String));
            dt.Columns[CN_QCName].DefaultValue = "";

            dt.Columns.Add(CN_Dilution, typeof(System.UInt32));
            dt.Columns[CN_Dilution].DefaultValue = 1;

            dt.Columns.Add(CN_SaveEveryNthImage, typeof(System.UInt32));
            dt.Columns[CN_SaveEveryNthImage].DefaultValue = 1;

            dt.Columns.Add(CN_WashType, typeof(System.UInt32));
            dt.Columns[CN_WashType].DefaultValue = (uint)ViCellBLU.WashType.Normal;

            dt.Columns.Add(CN_Tag, typeof(System.String));
            dt.Columns[CN_Tag].DefaultValue = "";

            return dt;
        }

        // ******************************************************************
        private void bttnQC_Create_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            try
            {
                QualityControl qc = new QualityControl();
                qc.QualityControlName = txtQCCreate_Name.Text;
                qc.CellTypeName = txtQCCreate_CellTypeName.Text;
                qc.AcceptanceLimits = (int)numUDQC_AcceptLimits.Value;
                qc.AssayParameter = (AssayParameterEnum)cbxQC_AssayParam.SelectedIndex;
                qc.AssayValue = (double)numUDQC_AssayValue.Value;
                qc.Comments = txtQCCreate_Comment.Text;
                qc.ExpirationDate = dtpQCExpires.Value;
                qc.LotNumber = txtQCCreate_LotNumber.Text;

                var res = _myBlu.CreateQualityControl(qc);
                if (res.ErrorLevel == ErrorLevelEnum.NoError)
                    bttnQC_Refresh_Click(null, null);
                UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
            }
            catch (Exception ex)
            {
                lstCallResult.Items.Add("CreateQualityControl exception:");
                lstCallResult.Items.Add(ex.ToString());
            }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnLoadSet_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                ofd.Filter = "Sample Set Config (.stc)|*.stc";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var path = Path.GetDirectoryName(ofd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }

                    var setCfg = XmlUtils<SampleSetConfig>.Load(ofd.FileName);
                    if (setCfg == null)
                    {
                        ShowButtons(true);
                        return;
                    }
                    txtSetName.Text = setCfg.Name;

                    // Force the radio button to update
                    rdoSortRow.Checked = true;
                    if (setCfg.PlateSortOrder == PlatePrecessionEnum.ColumnMajor)
                        rdoSortColumn.Checked = true;

                    dgvSet.DataSource = null;
                    _dtSet.Rows.Clear();
                    foreach (var cfg in setCfg.Samples)
                    {
                        var row = _dtSet.NewRow();
                        row[CN_Name] = cfg.Name;
                        row[CN_CellTypeName] = cfg.CellTypeName;
                        row[CN_QCName] = cfg.QCName;
                        row[CN_Tag] = cfg.Tag;
                        row[CN_SaveEveryNthImage] = cfg.SaveEveryNthImage;
                        row[CN_Row] = (Char)cfg.Position.Row;
                        row[CN_Col] = (Char)cfg.Position.Column;
                        row[CN_WashType] = cfg.WashType;
                        row[CN_Dilution] = cfg.Dilution;
                        _dtSet.Rows.Add(row);
                    }
                    dgvSet.DataSource = _dtSet;
                    UpdateSetGridWidths();
                }
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void UpdateSetGridWidths()
        {
            dgvSet.Columns[CN_Name].Width = 125;
            dgvSet.Columns[CN_Row].Width = 40;
            dgvSet.Columns[CN_Col].Width = 40;
            dgvSet.Columns[CN_Dilution].Width = 60;
            dgvSet.Columns[CN_CellTypeName].Width = 110;
            dgvSet.Columns[CN_QCName].Width = 85;
            dgvSet.Columns[CN_SaveEveryNthImage].Width = 90;
            dgvSet.Columns[CN_WashType].Width = 65;
            dgvSet.Columns[CN_Tag].Width = 80;
        }

        // ******************************************************************
        private void bttnSaveSet_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                var suggestFile = txtSetName.Text;
                suggestFile = suggestFile.Replace(" ", "_");
                suggestFile += ".stc";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                sfd.FileName = suggestFile;
                sfd.Filter = "Sample Set Config (.stc)|*.stc";
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Save file
                    SampleSetConfig setCfg = GetSetConfig();

                    XmlUtils<SampleSetConfig>.Save(sfd.FileName, setCfg);

                    var path = Path.GetDirectoryName(sfd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch { }

            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnSampleSet_Clear_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            _dtSet.Rows.Clear();
            txtSetName.Text = "";
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnSaveSample_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                var suggestFile = txtSampleName.Text;
                suggestFile = suggestFile.Replace(" ", "_");
                suggestFile += ".bsc";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                sfd.FileName = suggestFile;
                sfd.Filter = "Sample Config (.bsc)|*.bsc";
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Save file
                    SampleConfig cfg = new SampleConfig();

                    cfg.Name = txtSampleName.Text;
                    cfg.Tag = txtSampleTag.Text;
                    cfg.CellTypeName = txtSampleCellType.Text;
                    cfg.QCName = txtSampleQCType.Text;
                    cfg.Dilution = (uint)numUD_Dilution.Value;
                    cfg.SaveEveryNthImage = (uint)numUD_NthImageSave.Value;
                    XmlUtils<SampleConfig>.Save(sfd.FileName, cfg);

                    var path = Path.GetDirectoryName(sfd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }

                }
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnQC_Load_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                ofd.Filter = "Quality Control (.bqc)|*.bqc";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var path = Path.GetDirectoryName(ofd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }

                    var qc = XmlUtils<QualityControl>.Load(ofd.FileName);
                    if (qc == null)
                    {
                        ShowButtons(true);
                        return;
                    }

                    txtQCCreate_Name.Text = qc.QualityControlName;
                    txtQCCreate_CellTypeName.Text = qc.CellTypeName;
                    txtQCCreate_Comment.Text = qc.Comments;
                    txtQCCreate_LotNumber.Text = qc.LotNumber;

                    cbxQC_AssayParam.SelectedIndex = (int)qc.AssayParameter;
                    numUDQC_AssayValue.Value = (decimal)qc.AssayValue;
                    numUDQC_AcceptLimits.Value = (decimal)qc.AcceptanceLimits;
                    dtpQCExpires.Value = qc.ExpirationDate;

                }
            }
            catch { }
            ShowButtons(true);

        }

        // ******************************************************************
        private void bttnQC_Save_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                var suggestFile = txtQCCreate_Name.Text;
                suggestFile = suggestFile.Replace(" ", "_");
                suggestFile += ".bqc";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                sfd.FileName = suggestFile;
                sfd.Filter = "Quality Control (.bqc)|*.bqc";
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Save file
                    QualityControl qc = new QualityControl();

                    qc.QualityControlName = txtQCCreate_Name.Text;
                    qc.CellTypeName = txtQCCreate_CellTypeName.Text;
                    qc.Comments = txtQCCreate_Comment.Text;
                    qc.LotNumber = txtQCCreate_LotNumber.Text;

                    qc.AssayParameter = (AssayParameterEnum)cbxQC_AssayParam.SelectedIndex;
                    qc.AssayValue = (double)numUDQC_AssayValue.Value;
                    qc.AcceptanceLimits = (int)numUDQC_AcceptLimits.Value;
                    qc.ExpirationDate = dtpQCExpires.Value;

                    XmlUtils<QualityControl>.Save(sfd.FileName, qc);

                    var path = Path.GetDirectoryName(sfd.FileName);
                    if (path != Properties.Settings.Default.SamplesDir)
                    {
                        Properties.Settings.Default.SamplesDir = path;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void tsmiUtils_CreateSingle_Click(object sender, EventArgs e)
        {
            frmConfigRun gui = new frmConfigRun();
            gui.ShowDialog();
        }

        // ******************************************************************
        private void tsmiUtils_CreateSet_Click(object sender, EventArgs e)
        {
            frmConfigRunSet gui = new frmConfigRunSet();
            gui.ShowDialog();
        }

        // ******************************************************************
        private void bttnExportConfig_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.White);
            lstResultsStatus.Items.Clear();
            lstResultsStatus.BackColor = Color.White;
            try
            {
                var sfd = new SaveFileDialog {InitialDirectory = Properties.Settings.Default.SamplesDir};
                sfd.Filter = "Configuration (.cfg)|*.cfg";
                var dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Properties.Settings.Default.SamplesDir = Path.GetDirectoryName(sfd.FileName);
                    Properties.Settings.Default.Save();
                    ClearCallResultDisplay(Color.Yellow);
                    var response = _myBlu.ExportConfig(sfd.FileName);
                    UpdateCallResultDisplay(response.MethodResult, response.ErrorLevel, response.ResponseDescription);
                    if(response.MethodResult == MethodResultEnum.Success)
                    {
                        lstResultsStatus.Items.Add("Export Success");
                        lstResultsStatus.Items.Add(sfd.FileName);
                    }
                    else
                    {
                        lstResultsStatus.Items.Add("Export Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                lstResultsStatus.Items.Add("Export Failure exception: " + ex.Message);
            }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnImportConfig_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.White);
            lstResultsStatus.Items.Clear();
            lstResultsStatus.BackColor = Color.White;
            try
            {
                var ofd = new OpenFileDialog {InitialDirectory = Properties.Settings.Default.SamplesDir};
                ofd.Filter = "Configuration (.cfg)|*.cfg";
                var dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    ClearCallResultDisplay(Color.Yellow);
                    var response = _myBlu.ImportConfig(ofd.FileName);
                    UpdateCallResultDisplay(response.MethodResult, response.ErrorLevel, response.ResponseDescription);
                    if (response.ErrorLevel == ErrorLevelEnum.NoError)
                    {
                        lstResultsStatus.Items.Add("Import Success");
                        lstResultsStatus.Items.Add(ofd.FileName);
                    }
                    else
                    {
                        lstResultsStatus.Items.Add("Import Failure");
                    }
                }
            }
            catch(Exception ex)
            {
                lstResultsStatus.Items.Add("Import Failure exception: " + ex.Message);
            }
            ShowButtons(true);
        }


        private List<ViCellBlu.SampleResult> _currentResults;
        // ******************************************************************
        private void bttnGetResults_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            lstResultIds.Items.Clear();
            lblSampleResultCount.Text = "";
            lblSelectedSampleId.Text = "";
            listBoxCurrentSampleResult.Items.Clear();
            ClearCallResultDisplay(Color.Yellow);

            lstResultsStatus.Items.Clear();
            lstResultsStatus.BackColor = Color.White;

            try
            {                
                var callResult = _myBlu.GetSampleResults(txtSampleResults_Username.Text, dtpSampleResults_StartDate.Value, dtpSampleResults_EndDate.Value,
                    (FilterOnEnum)Enum.ToObject(typeof(FilterOnEnum), SampleFilterComboBox.SelectedIndex), SampleResults_CTQCNameBox.Text,
                    txtSampleResults_SearchString.Text, SampleResults_TagBox.Text, out _currentResults);

                UpdateCallResultDisplay(callResult.MethodResult, callResult.ErrorLevel, callResult.ResponseDescription);

                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    if (_currentResults.Count > 0)
                    {
                        lblSampleResultCount.Text = "Count: " + _currentResults.Count;
                        lstCallResult.Items.Add("Count: " + _currentResults.Count);
                        foreach (var res in _currentResults)
                        {
                            lstResultIds.Items.Add(res.SampleDataUuid.ToString());
                        }
                    }
                }
            }
            catch { }
            ShowButtons(true);
        }

        // ******************************************************************
        private void bttnExportResults_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.White);
            lstResultsStatus.Items.Clear();
            lstResultsStatus.BackColor = Color.White;

            var sampleIds = new List<Guid>();
            foreach (var selected in lstResultIds.SelectedItems)
            {
                if (Guid.TryParse(selected.ToString(), out var res))
                {
                    sampleIds.Add(res);
                }
            }

            try
            {
                var sfd = new SaveFileDialog { InitialDirectory = Properties.Settings.Default.SamplesDir };
                sfd.Filter = "Zip File (.zip)|*.zip";
                var dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Properties.Settings.Default.SamplesDir = Path.GetDirectoryName(sfd.FileName);
                    Properties.Settings.Default.Save();

                    ClearCallResultDisplay(Color.Yellow);
                    var response = _myBlu.RetrieveSampleExport(sampleIds, sfd.FileName);
                    _exportInProgress = false;
                    if (response.MethodResult == MethodResultEnum.Success)
                    {
                        _exportInProgress = true;
                    }    

                    UpdateCallResultDisplay(response.MethodResult, response.ErrorLevel, response.ResponseDescription);
                }
            }
            catch(Exception ex)
            {
                lstResultsStatus.Items.Add($"ExportResult Exception : {ex.Message}");
            }

            ShowButtons(true);
        }

        // ******************************************************************
        private void btnExportOfflineResults_Click(object sender, EventArgs e)
        {
	        ShowButtons(false);
	        ClearCallResultDisplay(Color.White);
	        lstResultsStatus.Items.Clear();
	        lstResultsStatus.BackColor = Color.White;

	        var sampleIds = new List<Guid>();
	        foreach (var selected in lstResultIds.SelectedItems)
	        {
		        if (Guid.TryParse(selected.ToString(), out var res))
		        {
			        sampleIds.Add(res);
		        }
	        }

	        try
	        {
		        var sfd = new SaveFileDialog { InitialDirectory = Properties.Settings.Default.SamplesDir };
		        sfd.Filter = "Zip File (.zip)|*.zip";
		        var dr = sfd.ShowDialog();
		        if (dr == DialogResult.OK)
		        {
			        Properties.Settings.Default.SamplesDir = Path.GetDirectoryName(sfd.FileName);
			        Properties.Settings.Default.Save();

			        ClearCallResultDisplay(Color.Yellow);
			        var response = _myBlu.RetrieveOfflineSampleExport(sampleIds, sfd.FileName);
			        _exportInProgress = false;
			        if (response.MethodResult == MethodResultEnum.Success)
			        {
				        _exportInProgress = true;
			        }

			        UpdateCallResultDisplay(response.MethodResult, response.ErrorLevel, response.ResponseDescription);
		        }
	        }
	        catch (Exception ex)
	        {
		        lstResultsStatus.Items.Add($"ExportResult Exception : {ex.Message}");
	        }

	        ShowButtons(true);
        }

	    // *****************************************************************
	    private void bttnDeleteResult_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            ClearCallResultDisplay(Color.Yellow);
            lblSampleResultCount.Text = "";
            listBoxCurrentSampleResult.Items.Clear();
            lstResultsStatus.Items.Clear();
            lstResultsStatus.BackColor = Color.White;

            try
            {
                var sampleIds = new List<Guid>();
                foreach (var selected in lstResultIds.SelectedItems)
                {
                    if (Guid.TryParse(selected.ToString(), out var res))
                    {
                        sampleIds.Add(res);
                    }
                }

                var callResult = _myBlu.DeleteSampleResults(sampleIds);
                UpdateCallResultDisplay(callResult.MethodResult, callResult.ErrorLevel, callResult.ResponseDescription);

                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    var count = lstResultIds.Items.Count - 1;
                    for (var a = count; a >= 0; a--)
                    {
                        if(lstResultIds.SelectedItems.Contains(lstResultIds.Items[a]))
                            lstResultIds.Items.RemoveAt(a);
                    }

                    lblSampleResultCount.Text = "Count: " + lstResultIds.Items.Count;
                    lblSelectedSampleId.Text = "";
                }
            }
            catch { }
            ShowButtons(true);
        }

        private void lstResultIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = lstResultIds.SelectedIndex;
            if (idx < _currentResults.Count && idx >= 0)
            {
                listBoxCurrentSampleResult.Items.Clear();
                lblSelectedSampleId.Text = _currentResults[idx].SampleDataUuid.ToString();
                Misc.PopulateSampleResultsBox(_currentResults[idx], ref listBoxCurrentSampleResult);
            }

        }

        private void bttnQC_Refresh_Click(object sender, EventArgs e)
        {
			List<QualityControl> qcs = _myBlu.GetQualityControls();
            Invoke(new Action(() =>
            {
                lstQualityControls.Items.Clear();
                foreach (var qc in qcs)
                {
                    lstQualityControls.Items.Add(qc.QualityControlName);
                }
            }));
        }

        private void bttnCT_Refresh_Click(object sender, EventArgs e)
        {
            List<CellType> cells = new List<CellType>();
            _myBlu.RefreshCellTypes(out cells);
            Invoke(new Action(() =>
            {
                lstCellTypes.Items.Clear();
                foreach (var cell in cells)
                {
                    lstCellTypes.Items.Add(cell.CellTypeName);
                }
            }));    
        }

        private void SampleFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    SampleResults_CTQCLabel.Visible = false;
                    SampleResults_CTQCNameBox.Visible = false;
                    SampleResults_CTQCNameBox.Text = "";
                    SampleResults_TagBox.Visible = false;
                    SampleResults_TagBox.Text = "";
                    SampleResults_TagLabel.Visible = false;
                    lblFilterType.Text = "Sample Set Name";
                    break;
                case 1:
                    SampleResults_CTQCLabel.Visible = true;
                    SampleResults_CTQCNameBox.Visible = true;
                    SampleResults_TagBox.Visible = true;
                    SampleResults_TagLabel.Visible = true;
                    lblFilterType.Text = "Sample Id";
                    break;
            }
        }

        private void tsmiCnxConfig_Click(object sender, EventArgs e)
        {
            frmConfigConnection gui = new frmConfigConnection();
            gui.ShowDialog();
        }

        private void lstCellTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cell = _myBlu.GetCellTypes()[lstCellTypes.SelectedIndex];
                txtCTCreate_Name.Text = cell.CellTypeName;
                numUDCT_MinDiam.Value = (decimal)cell.MinDiameter;
                numUDCT_MaxDiam.Value = (decimal)cell.MaxDiameter;
                numUDCT_Images.Value = cell.NumImages;
                numUDCT_Sharpness.Value = (decimal)cell.CellSharpness;
                numUDCT_MinCirc.Value = (decimal)cell.MinCircularity;
                cbxCT_Decluster.SelectedIndex = (int)cell.DeclusterDegree;
                numUDCT_AspCycles.Value = cell.NumAspirationCycles;
                numUDCT_ViaSpotBright.Value = (decimal)cell.ViableSpotBrightness;
                numUDCT_ViaSpotArea.Value = (decimal)cell.ViableSpotArea;
                numUDCT_MixCycles.Value = cell.NumMixingCycles;
                numUDCT_ConcAdjFactor.Value = (decimal)cell.ConcentrationAdjustmentFactor;
            }
            catch
            {
                //do nothing
            }
        }

        private void lstQualityControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var qc = _myBlu.GetQualityControls()[lstQualityControls.SelectedIndex];

                txtQCCreate_Name.Text = qc.QualityControlName;
                txtQCCreate_CellTypeName.Text = qc.CellTypeName;
                txtQCCreate_Comment.Text = qc.Comments;
                txtQCCreate_LotNumber.Text = qc.LotNumber;

                cbxQC_AssayParam.SelectedIndex = (int)qc.AssayParameter;
                numUDQC_AssayValue.Value = (decimal)qc.AssayValue;
                numUDQC_AcceptLimits.Value = (decimal)qc.AcceptanceLimits;
                dtpQCExpires.Value = qc.ExpirationDate;
            }
            catch 
            {
                //do nothing
            }
        }

        private void bttnGetDiskSpace_Click(object sender, EventArgs e)
        {
            try
            {
                ShowButtons(false);
                rectangleDataDiskSpace.Width = 0;
                rectangleOtherDiskSpace.Width = 0;
                rectangleExportDiskSpace.Width = 0;
                ClearCallResultDisplay(Color.Yellow);
                var result = _myBlu.GetDiskSpacePercentages();
                if (result.ErrorLevel == ErrorLevelEnum.NoError)
                {
                    double otherPercentage = 0;
                    double dataPercentage = 0;
                    double exportPercentage = 0;

                    var sumAll = result.DiskSpaceOtherBytes + result.DiskSpaceDataBytes + result.DiskSpaceExportBytes;
                    double divisor = result.TotalSizeBytes;
                    if ((result.TotalSizeBytes == 0) || (sumAll == 0) || (result.TotalSizeBytes < sumAll))
                    {
                        divisor = sumAll;
                    }

                    if (result.TotalSizeBytes > 0)
                    {
                        otherPercentage = result.DiskSpaceOtherBytes / divisor;
                        dataPercentage = result.DiskSpaceDataBytes / divisor;
                        exportPercentage = result.DiskSpaceExportBytes / divisor;
                    }

                    otherPercentage = Math.Min(otherPercentage, 1);
                    otherPercentage = Math.Max(otherPercentage, 0);
                    dataPercentage = Math.Min(dataPercentage, 1);
                    dataPercentage = Math.Max(dataPercentage, 0);
                    exportPercentage = Math.Min(exportPercentage, 1);
                    exportPercentage = Math.Max(exportPercentage, 0);

                    Invoke(new Action(() =>
                    {
                        rectangleOtherDiskSpace.Width = Convert.ToInt32(_progressBarWidth * otherPercentage);

                        rectangleDataDiskSpace.Width = Convert.ToInt32(_progressBarWidth * dataPercentage);
                        rectangleDataDiskSpace.Location = new Point(rectangleOtherDiskSpace.Location.X + rectangleOtherDiskSpace.Width, rectangleDataDiskSpace.Location.Y);

                        rectangleExportDiskSpace.Width = Convert.ToInt32(_progressBarWidth * exportPercentage);
                        rectangleExportDiskSpace.Location = new Point(rectangleDataDiskSpace.Location.X + rectangleDataDiskSpace.Width, rectangleExportDiskSpace.Location.Y);

                        UpdateCallResultDisplay(result.MethodResult, result.ErrorLevel, result.ResponseDescription);

                        diskSpaceLabel.Text = string.Format("{0} free of {1}", Misc.ConvertBytesToSize(result.TotalFreeBytes), Misc.ConvertBytesToSize(result.TotalSizeBytes));
                    }));

                }
                else
                {
                    ClearCallResultDisplay(Color.Salmon);
                    rectangleOtherDiskSpace.Width = 0;
                    rectangleDataDiskSpace.Width = 0;
                    rectangleExportDiskSpace.Width = 0;
                    diskSpaceLabel.Text = string.Empty;
                    lstCallResult.Items.Add("GetDiskSpace failed: ");
                    lstCallResult.Items.Add(result.ResponseDescription);
                }
            }
            catch (Exception ex)
            {
                lstCallResult.Items.Clear();
                lstCallResult.BackColor = Color.Red;
                lstCallResult.Items.Add("GetDiskSpace exception:");
                lstCallResult.Items.Add(ex.ToString());
            }

            ShowButtons(true);
        }

        private void btnCleanFluidics_Click(object sender, EventArgs e)
        {
	        ShowButtons(false);
	        ClearCallResultDisplay(Color.Yellow);
	        try
	        {
		        var res = _myBlu.CleanFluidics();
		        UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
	        }
	        catch { }
	        ShowButtons(true);
        }

		private void lstSampleCompleteData_MouseUp(object sender, MouseEventArgs e)
        {
            lstSampleCompleteData.Items.Clear();
            lstSampleCompleteData.Refresh();
            if (e.Button == MouseButtons.Left)
            {
                Misc.PopulateSampleResultsBox(_myBlu.LastSampleResult, ref lstSampleCompleteData);
            }
            else if(e.Button == MouseButtons.Right)
            {
                lstSampleCompleteData.Items.Add("Left click to Refresh");
                lstSampleCompleteData.Items.Add("");
                lstSampleCompleteData.Items.Add("Right click to Clear");
            }
            System.Threading.Thread.Sleep(100);
            lstSampleCompleteData.Refresh();
        }

        private void numUDQC_AcceptLimits_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDQC_AcceptLimits.Text))
            {
                numUDQC_AcceptLimits.Value = 0;
            }
        }

        private void numUDQC_AssayValue_KeyUp(object sender, KeyEventArgs e)
        {
           if (string.IsNullOrEmpty(numUDQC_AssayValue.Text))
           {
                numUDQC_AssayValue.Value = 0;
            }
        }

        private void numUD_Dilution_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUD_Dilution.Text))
            {
                numUD_Dilution.Value = 0;
            }
        }

        private void numUD_NthImageSave_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUD_NthImageSave.Text))
            {
                numUD_NthImageSave.Value = 0;
            }
        }

        private void numUDCT_MinDiam_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MinDiam.Text))
            {
                numUDCT_MinDiam.Value = 0;
            }
        }

        private void numUDCT_MaxDiam_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MaxDiam.Text))
            {
                numUDCT_MaxDiam.Value = 0;
            }
        }

        private void numUDCT_Images_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_Images.Text))
            {
                numUDCT_Images.Value = 0;
            }
        }

        private void numUDCT_Sharpness_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_Sharpness.Text))
            {
                numUDCT_Sharpness.Value = 0;
            }
        }

        private void numUDCT_MinCirc_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MinCirc.Text))
            {
                numUDCT_MinCirc.Value = 0;
            }
        }

        private void numUDCT_ViaSpotBright_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_ViaSpotBright.Text))
            {
                numUDCT_ViaSpotBright.Value = 0;
            }
        }

        private void numUDCT_ViaSpotArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_ViaSpotArea.Text))
            {
                numUDCT_ViaSpotArea.Value = 0;
            }
        }

        private void numUDCT_ConcAdjFactor_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_ConcAdjFactor.Text))
            {
                numUDCT_ConcAdjFactor.Value = 0;
            }
        }

        private void numUDCT_AspCycles_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_AspCycles.Text))
            {
                numUDCT_AspCycles.Value = 0;
            }
        }

        private void numUDCT_MixCycles_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MixCycles.Text))
            {
                numUDCT_MixCycles.Value = 0;
            }
        }

        private void numUDQC_AssayValue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDQC_AssayValue.Text))
            {
                numUDQC_AssayValue.Value = 0;
                numUDQC_AssayValue.Text = "0";
            }
        }

        private void numUDQC_AcceptLimits_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDQC_AcceptLimits.Text))
            {
                numUDQC_AcceptLimits.Value = 0;
                numUDQC_AcceptLimits.Text = "0";
            }
        }

        private void numUD_Dilution_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUD_Dilution.Text))
            {
                numUD_Dilution.Value = 0;
                numUD_Dilution.Text = "0";
            }
        }

        private void numUDCT_MinDiam_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MinDiam.Text))
            {
                numUDCT_MinDiam.Value = 0;
                numUDCT_MinDiam.Text = "0";
            }
        }

        private void numUDCT_MaxDiam_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MaxDiam.Text))
            {
                numUDCT_MaxDiam.Value = 0;
                numUDCT_MaxDiam.Text = "0";
            }
        }

        private void numUDCT_Images_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_Images.Text))
            {
                numUDCT_Images.Value = 0;
                numUDCT_Images.Text = "0";
            }
        }

        private void numUDCT_Sharpness_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_Sharpness.Text))
            {
                numUDCT_Sharpness.Value = 0;
                numUDCT_Sharpness.Text = "0";
            }
        }

        private void numUDCT_MinCirc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MinCirc.Text))
            {
                numUDCT_MinCirc.Value = 0;
                numUDCT_MinCirc.Text = "0";
            }
        }

        private void numUDCT_AspCycles_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_AspCycles.Text))
            {
                numUDCT_AspCycles.Value = 0;
                numUDCT_AspCycles.Text = "0";
            }
        }

        private void numUDCT_ViaSpotBright_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_ViaSpotBright.Text))
            {
                numUDCT_ViaSpotBright.Value = 0;
                numUDCT_ViaSpotBright.Text = "0";
            }
        }

        private void numUDCT_ViaSpotArea_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_ViaSpotArea.Text))
            {
                numUDCT_ViaSpotArea.Value = 0;
                numUDCT_ViaSpotArea.Text = "0";
            }
        }

        private void numUDCT_ConcAdjFactor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_ConcAdjFactor.Text))
            {
                numUDCT_ConcAdjFactor.Value = 0;
                numUDCT_ConcAdjFactor.Text = "0";
            }
        }

        private void numUDCT_MixCycles_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numUDCT_MixCycles.Text))
            {
                numUDCT_MixCycles.Value = 0;
                numUDCT_MixCycles.Text = "0";
            }
        }

		private void btnGetReagentVolume_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var type = (CellHealthFluidTypeEnum) Enum.ToObject(
					typeof(CellHealthFluidTypeEnum), cbxSelectReagent.SelectedIndex + ReagentTypeEnumOffset);

				var res = _myBlu.GetReagentVolume(type);
				lblReagentVolume.Text = res.Volume.ToString();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		private void btnSetReagentVolume_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var type = (CellHealthFluidTypeEnum)Enum.ToObject(
					typeof(CellHealthFluidTypeEnum), cbxSelectReagent.SelectedIndex + ReagentTypeEnumOffset);

				Int32 volume = 0;
				Int32.TryParse(txtSetReagentVolume.Text, out volume);

				var res = _myBlu.SetReagentVolume (type, volume);
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		private void btnAddReagentVolume_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var type = (CellHealthFluidTypeEnum)Enum.ToObject(
					typeof(CellHealthFluidTypeEnum), cbxSelectReagent.SelectedIndex + ReagentTypeEnumOffset);

				Int32 volume = 0;
				Int32.TryParse(txtAddReagentVolume.Text, out volume);

				var res = _myBlu.AddReagentVolume (type, volume);
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		private void btnShutdown_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			try
			{
				DialogResult dialogResult = MessageBox.Show("Continue with shutdown?", "Confirm", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					ClearCallResultDisplay(Color.Yellow);
					var res = _myBlu.ShutdownOrReboot(ShutdownOrRebootEnum.Shutdown);
					UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
				}
			}
			catch { }
			ShowButtons(true);
		}

		private void btnReboot_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				DialogResult dialogResult = MessageBox.Show("Continue with reboot?", "Confirm", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					ClearCallResultDisplay(Color.Yellow);
					var res = _myBlu.ShutdownOrReboot(ShutdownOrRebootEnum.Reboot);
					UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
				}
			}
			catch { }
			ShowButtons(true);
		}

		private void btnDeleteCampaignData_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.Yellow);
			try
			{
				var res = _myBlu.DeleteCampaignData();
				UpdateCallResultDisplay(res.MethodResult, res.ErrorLevel, res.ResponseDescription);
			}
			catch { }
			ShowButtons(true);
		}

		// ******************************************************************
		private void bttnStartLogDataExport_Click(object sender, EventArgs e)
		{
			ShowButtons(false);
			ClearCallResultDisplay(Color.White);
			lstResultsStatus.Items.Clear();
			lstResultsStatus.BackColor = Color.White;

			var sampleIds = new List<Guid>();
			foreach (var selected in lstResultIds.SelectedItems)
			{
				if (Guid.TryParse(selected.ToString(), out var res))
				{
					sampleIds.Add(res);
				}
			}

			try
			{
				var sfd = new SaveFileDialog { InitialDirectory = Properties.Settings.Default.SamplesDir };
				sfd.Filter = "Excel File (.csv)|*.csv";
				var dr = sfd.ShowDialog();
				if (dr == DialogResult.OK)
				{
					Properties.Settings.Default.SamplesDir = Path.GetDirectoryName(sfd.FileName);
					Properties.Settings.Default.Save();

					ClearCallResultDisplay(Color.Yellow);
					var response = _myBlu.StartLogDataExport(sfd.FileName, dtpConfigAudit_StartDate.Value, dtpConfigAudit_EndDate.Value);
					_exportInProgress = false;
					if (response.MethodResult == MethodResultEnum.Success)
					{
						_exportInProgress = true;
					}

					UpdateCallResultDisplay(response.MethodResult, response.ErrorLevel, response.ResponseDescription);
				}
			}
			catch (Exception ex)
			{
				lstResultsStatus.Items.Add($"ExportResult Exception : {ex.Message}");
			}

			ShowButtons(true);
		}

		private void btnClearErrorStatus_Click(object sender, EventArgs e)
		{
			lstErrorStatus.Items.Clear();
		}
	}
}
