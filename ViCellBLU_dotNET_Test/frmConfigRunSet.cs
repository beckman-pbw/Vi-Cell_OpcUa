using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ViCellBlu;
using ViCellBLU_dotNET;

namespace ViCellBLU_dotNET_Test
{
    public partial class frmConfigRunSet : Form
    {
        // ******************************************************************
        public frmConfigRunSet()
        {
            InitializeComponent();
        }

        DataTable _dtSet;
        // ******************************************************************
        private void frmConfigRunSet_Load(object sender, EventArgs e)
        {
            _dtSet = frmMain.CreateSampleTable();
            dgvSet.DataSource = _dtSet;
            UpdateSetGridWidths();
        }

        // ******************************************************************
        private void ShowButtons(bool show)
        {
            if (show)
            {
                Thread.Sleep(100);
            }
            bttnDone.Visible = show;
            bttnDone.Enabled = show;
            bttnSaveSet.Visible = show;
            bttnSaveSet.Enabled = show;
            bttnLoadSet.Visible = show;
            bttnLoadSet.Enabled = show;
            bttnClearSet.Visible = show;
            bttnClearSet.Enabled = show;
        }

        // ******************************************************************
        private void bttnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ******************************************************************
        private void bttnClearSet_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            _dtSet.Rows.Clear();
            txtSetName.Text = "";
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
                        row[frmMain.CN_Name] = cfg.Name;
                        row[frmMain.CN_CellTypeName] = cfg.CellTypeName;
                        row[frmMain.CN_QCName] = cfg.QCName;
                        row[frmMain.CN_Tag] = cfg.Tag;
                        row[frmMain.CN_SaveEveryNthImage] = cfg.SaveEveryNthImage;
                        row[frmMain.CN_Row] = (Char)cfg.Position.Row;
                        row[frmMain.CN_Col] = (Char)cfg.Position.Column;
                        row[frmMain.CN_WashType] = cfg.WashType;
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
            dgvSet.Columns[frmMain.CN_Name].Width = 125;
            dgvSet.Columns[frmMain.CN_Row].Width = 40;
            dgvSet.Columns[frmMain.CN_Col].Width = 40;
            dgvSet.Columns[frmMain.CN_Dilution].Width = 60;
            dgvSet.Columns[frmMain.CN_CellTypeName].Width = 110;
            dgvSet.Columns[frmMain.CN_QCName].Width = 90;
            dgvSet.Columns[frmMain.CN_SaveEveryNthImage].Width = 90;
            dgvSet.Columns[frmMain.CN_WashType].Width = 65;
            dgvSet.Columns[frmMain.CN_Tag].Width = 80;

        }

        // ******************************************************************
        private SampleSetConfig GetSetConfig()
        {
            SampleSetConfig setCfg = new SampleSetConfig();
            setCfg.Name = txtSetName.Text;
            setCfg.PlateSortOrder = rdoSortColumn.Checked ? PlatePrecessionEnum.ColumnMajor : PlatePrecessionEnum.RowMajor;

            foreach (DataRow row in _dtSet.Rows)
            {
                ViCellBLU_dotNET.SampleConfig sample = new ViCellBLU_dotNET.SampleConfig();
                sample.Name = Convert.ToString(row[frmMain.CN_Name]);
                sample.Tag = Convert.ToString(row[frmMain.CN_Tag]);
                sample.Dilution = Convert.ToUInt32(row[frmMain.CN_Dilution]);
                sample.SaveEveryNthImage = Convert.ToUInt32(row[frmMain.CN_SaveEveryNthImage]);
                sample.CellTypeName = Convert.ToString(row[frmMain.CN_CellTypeName]);
                sample.QCName = Convert.ToString(row[frmMain.CN_QCName]);
                var r = char.ToUpper(Convert.ToChar(row[frmMain.CN_Row]));
                sample.Position.Set((ViCellBLU_dotNET.SamplePosition.RowDef)r, (ViCellBLU_dotNET.SamplePosition.ColumnDef)Convert.ToByte(row[frmMain.CN_Col]));
                var wt = Convert.ToUInt32(row[frmMain.CN_WashType]);
                sample.WashType = (ViCellBLU.WashType)wt;
                setCfg.Samples.Add(sample);
            }
            return setCfg;

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
    }
}
