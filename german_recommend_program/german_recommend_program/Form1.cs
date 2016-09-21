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
    public partial class Form1 : Form
    {

        private SentenceAnalyzer sentenceAnalyzer;

        public Form1()
        {
            InitializeComponent();
            sentenceAnalyzer = new SentenceAnalyzer(this);
            writtentxb.TextChanged -= writtentxb_TextChanged;
        }

        private void writtentxb_TextChanged(object sender, EventArgs e)
        {
            String wtxt = writtentxb.Text;
            displaybox.Text = wtxt;
            sentenceAnalyzer.textChange(wtxt);
        }

        private void btn_analyze_Click(object sender, EventArgs e)
        {
            String wtxt = writtentxb.Text;
            displaybox.Text = wtxt;
            sentenceAnalyzer.textChange(wtxt);
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Checked)
            {
                btnSwitch.Text = "預測模式";
                sentenceAnalyzer.modeChange(1);
                btn_analyze.Visible = false;
                btn_analyze.Enabled = false;
                writtentxb.TextChanged += writtentxb_TextChanged;
            }
            else
            {
                btnSwitch.Text = "一般模式";
                sentenceAnalyzer.modeChange(0);
                btn_analyze.Visible = true;
                btn_analyze.Enabled = true;
                writtentxb.TextChanged -= writtentxb_TextChanged;
            }
        }

        public FlowLayoutPanel getFormPanel()
        {
            return displayPanel;
        }


    }
}
