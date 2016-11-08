using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace german_recommend_program
{
    public partial class Form1 : Form
    {
        private SqlConnection db;
        private SentenceAnalyzer sentenceAnalyzer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            String cn = @"Data Source=140.116.154.85;" + "Database=german_project;" + "Uid=user;" + "Pwd=!ncku99!;";

            try
            {
                db = new SqlConnection(cn);
                db.Open();
            }
            catch
            {
                MessageBox.Show("沒有網路連線，無法連接至資料庫。\n請重新確認網路，謝謝！", "警告");
                this.FormClosing -= this.Form1_FormClosing;
                this.Close();
            }

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.lb_de, "以下是德文特有的字母，點一下按鈕就會將該字母新增到左方輸入框");

            sentenceAnalyzer = new SentenceAnalyzer(this, db);
            writtentxb.TextChanged -= writtentxb_TextChanged;
        }

        private void writtentxb_TextChanged(object sender, EventArgs e)
        {
            String wtxt = writtentxb.Text;
            //displaybox.Text = wtxt;
            sentenceAnalyzer.textChange(wtxt);
        }

        private void btn_analyze_Click(object sender, EventArgs e)
        {
            displayPanel.Controls.Clear();
            String wtxt = writtentxb.Text;
            //displaybox.Text = wtxt;
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

        private void btn_big_ae_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "Ä";
            writtentxb.Text = tmp;
        }

        private void btn_small_ae_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ä";
            writtentxb.Text = tmp;
        }

        private void btn_big_oe_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "Ö";
            writtentxb.Text = tmp;
        }

        private void btn_small_oe_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ö";
            writtentxb.Text = tmp;
        }

        private void btn_big_ue_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "Ü";
            writtentxb.Text = tmp;
        }

        private void btn_small_ue_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ü";
            writtentxb.Text = tmp;
        }

        private void btn_ss_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ß";
            writtentxb.Text = tmp;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您確定要離開嗎？", "Closing...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Cancel the Closing event from closing the form.
                db.Close();
                db.Dispose();
                e.Cancel = false;
                // Call method to save file...
            }
            else
            {
                e.Cancel = true;
            }
            
        }




    }
}
