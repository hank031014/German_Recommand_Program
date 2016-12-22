using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace german_recommend_program
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        private void lkb_ding_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lkb_ding.LinkVisited = true;
            System.Diagnostics.Process.Start(lkb_ding.Text);
        }
    }
}
