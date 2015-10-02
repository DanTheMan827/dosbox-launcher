using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DOSBox_Launcher
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            Program.install();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            Program.uninstall();
        }

        private void btnSetLocation_Click(object sender, EventArgs e)
        {
            Program.changeDosboxPath();
        }
    }
}
