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
    public partial class frmConfigConnection : Form
    {
        public frmConfigConnection()
        {
            InitializeComponent();
        }

        private void frmConfigConnection_Load(object sender, EventArgs e)
        {

        }

        private void bttnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bttnLoad_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                ofd.Filter = "Connection Config (.ccf)|*.ccf";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var cfg = XmlUtils<ConnectionCfg>.Load(ofd.FileName);
                    if (cfg == null)
                    {
                        ShowButtons(true);
                        return;
                    }

                    txtIPAddr.Text = cfg.IPAddr.ToString();
                    txtPort.Text = cfg.Port.ToString();
                    txtUserName.Text = cfg.Username;
                    txtPassword.Text = cfg.Password;

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

        private void bttnSaveSample_Click(object sender, EventArgs e)
        {
            ShowButtons(false);
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Properties.Settings.Default.SamplesDir;
                sfd.Filter = "Connection Config (.ccf)|*.ccf";
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Save file
                    ConnectionCfg cfg = new ConnectionCfg();

                    IPAddress addr;
                    if (IPAddress.TryParse(txtIPAddr.Text, out addr))
                    {
                        cfg.IPAddr = addr.ToString();
                    }
                    UInt32 tmp;
                    if (UInt32.TryParse(txtPort.Text, out tmp))
                    {
                        cfg.Port = tmp;
                    }

                    cfg.Username = txtUserName.Text;
                    cfg.Password = txtPassword.Text;

                    XmlUtils<ConnectionCfg>.Save(sfd.FileName, cfg);

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

        private void ShowButtons(bool show)
        {
            if (show)
            {
                Thread.Sleep(100);
            }
            bttnDone.Visible = show;
            bttnDone.Enabled = show;
            bttnLoad.Visible = show;
            bttnLoad.Enabled = show;
            bttnSaveSample.Visible = show;
            bttnSaveSample.Enabled = show;
        }

    }
}
