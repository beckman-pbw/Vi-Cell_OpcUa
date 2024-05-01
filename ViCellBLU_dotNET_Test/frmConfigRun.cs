using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using ViCellBLU_dotNET;

namespace ViCellBLU_dotNET_Test
{
    public partial class frmConfigRun : Form
    {
        /* ******************************************************************
         * \brief
         */
        public frmConfigRun()
        {
            InitializeComponent();
        }

        /* ******************************************************************
         * \brief
         */
        private void frmConfigRun_Load(object sender, EventArgs e)
        {

        }

        /* ******************************************************************
         * \brief
        */
        private void ShowButtons(bool show)
        {
            if (show)
            {
                Thread.Sleep(100);
            }
            bttnDone.Visible = show;
            bttnDone.Enabled = show;
            bttnSC_Load.Visible = show;
            bttnSC_Load.Enabled = show;
            bttnSC_Save.Visible = show;
            bttnSC_Save.Enabled = show;
        }


        /* ******************************************************************
         * \brief
         */
        private void bttnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttnSC_Load_Click(object sender, EventArgs e)
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
                    var cfg = XmlUtils<SampleConfig>.Load(ofd.FileName);
                    if (cfg == null)
                    {
                        ShowButtons(true);
                        return;
                    }

                    txtSampleName.Text = cfg.Name;
                    txtSampleTag.Text = cfg.Tag;
                    txtSampleCellType.Text = cfg.CellTypeName;
                    numUD_Dilution.Value = cfg.Dilution;
                    numUD_NthImageSave.Value = cfg.SaveEveryNthImage;

                    var path = Path.GetDirectoryName(ofd.FileName);
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

        private void bttnSC_Save_Click(object sender, EventArgs e)
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

    }
}
