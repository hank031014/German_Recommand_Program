using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        private ListBox lb;
        private DictForm dform;
        private AboutForm aform;

        private SqlCommand cmd;
        private String sql = String.Empty;
        private OptionWord ow;

        private int minWidth, minHeight, maxWidth, maxHeight;
        private float widthScale, heightScale;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            minWidth = 1004;
            minHeight = 458;
            maxWidth = Screen.PrimaryScreen.Bounds.Width;
            maxHeight = Screen.PrimaryScreen.Bounds.Height;
            widthScale = (float)maxWidth / minWidth;
            heightScale = (float)maxHeight / minHeight;

            this.MinimumSize = new System.Drawing.Size(minWidth, minHeight);
            this.MaximumSize = new System.Drawing.Size(maxWidth, maxHeight);

            ToolTip toolTip1 = new ToolTip();
            lb = new ListBox();
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
            writtentxb.Focus();
            writtentxb.TextChanged -= writtentxb_TextChanged;

            //listBox1.Items.Add("123");
            //listBox1.Items.Add("AAA");
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
            writtentxb.Focus();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Checked)
            {
                btnSwitch.Text = "預測模式";
                sentenceAnalyzer.modeChange(1);
                btn_analyze.Visible = false;
                btn_analyze.Enabled = false;
                //writtentxb.TextChanged += writtentxb_TextChanged;
            }
            else
            {
                btnSwitch.Text = "一般模式";
                sentenceAnalyzer.modeChange(0);
                btn_analyze.Visible = true;
                btn_analyze.Enabled = true;
                writtentxb.TextChanged -= writtentxb_TextChanged;
            }
            writtentxb.Focus();
        }

        public FlowLayoutPanel getFormPanel()
        {
            return displayPanel;
        }

        public ListBox getListBox()
        {
            return listBox1;
        }

        public RichTextBox getRichTextBox()
        {
            return writtentxb;
        }

        private void btn_big_ae_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "Ä";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void btn_small_ae_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ä";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void btn_big_oe_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "Ö";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void btn_small_oe_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ö";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void btn_big_ue_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "Ü";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void btn_small_ue_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ü";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void btn_ss_Click(object sender, EventArgs e)
        {
            String tmp = writtentxb.Text + "ß";
            writtentxb.Text = tmp;
            writtentxb.Focus();
            writtentxb.Select(writtentxb.Text.Length, 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您確定要離開嗎？", "Closing...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dform != null)
                {
                    if (dform.Visible)
                    {
                        dform.Close();
                        dform.Dispose();
                        GC.SuppressFinalize(dform);
                    }
                }
                if (aform != null)
                {
                    if (aform.Visible)
                    {
                        aform.Close();
                        aform.Dispose();
                        GC.SuppressFinalize(aform);
                    }
                }
                db.Close();
                db.Dispose();
                e.Cancel = false;               
            }
            else
            {
                e.Cancel = true;
            }
            
        }

        private void writtentxb_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnSwitch.Checked && e.KeyCode == Keys.Space)
            {
                String wtxt = writtentxb.Text;
                //displaybox.Text = wtxt;
                sentenceAnalyzer.textChange(wtxt);               
            }
            else
                listBox1.Visible = false;
        }

        private void btn_dict_Click(object sender, EventArgs e)
        {
            if (dform == null)
            {
                dform = new DictForm(db);
                dform.Show();
            }
            else if (dform.IsDisposed)
            {
                dform = null;
                dform = new DictForm(db);
                dform.Show();
            }
            
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            if (aform == null)
            {
                aform = new AboutForm();
                aform.Show();
            }
            else if (aform.IsDisposed)
            {
                aform = null;
                aform = new AboutForm();
                aform.Show();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //Debug.WriteLine(widthScale + ", " + heightScale);
                displayPanel.Location = new Point((int)(179 * widthScale), (int)(12 * heightScale));
                displayPanel.Size = new Size((int)(675 * widthScale), (int)(183 * heightScale) + 15);

                writtentxb.Location = new Point((int)(179 * widthScale), (int)(221 * heightScale) - 15);
                writtentxb.Size = new Size((int)(675 * widthScale), (int)(182 * heightScale));

                btnSwitch.Location = new Point((int)(38 * widthScale), (int)(164 * heightScale) + 30);
                btn_analyze.Location = new Point((int)(38 * widthScale), (int)(203 * heightScale) + 30);
                btn_about.Location = new Point((int)(878 * widthScale + 25), (int)(28 * heightScale) + 90);
                btn_dict.Location = new Point((int)(878 * widthScale + 25), (int)(86 * heightScale) + 120);
                lb_de.Location = new Point((int)(866 * widthScale + 30), (int)(181 * heightScale) + 84);
                flowLayoutPanel1.Location = new Point((int)(866 * widthScale + 30), (int)(202 * heightScale) + 70);
            }

            if (this.Height == minHeight && this.Width == minWidth)
            {
                //Debug.WriteLine("min");
                displayPanel.Location = new Point(179, 12);
                displayPanel.Size = new Size(675, 183);

                writtentxb.Location = new Point(179, 221);
                writtentxb.Size = new Size(675, 182);

                btnSwitch.Location = new Point(38, 164);
                btn_analyze.Location = new Point(38, 203);
                btn_about.Location = new Point(878, 28);
                btn_dict.Location = new Point(878, 86);
                lb_de.Location = new Point(866, 181);
                flowLayoutPanel1.Location = new Point(866, 202);
            }

        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listBox1.SelectedItem != null)
                {
                    writtentxb.Text += listBox1.SelectedItem.ToString();

                    ow = (OptionWord) listBox1.SelectedItem;
                    if (ow.type == 1)
                    {
                        sql = String.Empty;
                        sql += "UPDATE verb_predict SET times = times + 1 WHERE id = '" + ow.id + "'";
                        cmd = new SqlCommand(sql, db);
                    }
                    if (ow.type == 2)
                    {
                        sql = String.Empty;
                        sql += "UPDATE predict SET times = times + 1 WHERE id = '" + ow.id + "'";
                        cmd = new SqlCommand(sql, db);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    
                    listBox1.Items.Clear();
                    listBox1.Text = String.Empty;
                    listBox1.Visible = false;
                    writtentxb.Focus();
                    writtentxb.Select(writtentxb.Text.Length, 0);
                }               
            }
            else if (listBox1.Visible && (e.KeyValue >= 46 && e.KeyValue <= 90 || e.KeyValue == 8))
            {
                if(e.KeyValue >= 48 && e.KeyValue <= 90){
                    KeysConverter kc = new KeysConverter();
                    String s = kc.ConvertToString(e.KeyValue);
                    writtentxb.Text += s.ToLower();
                }
                
                writtentxb.Focus();
                writtentxb.Select(writtentxb.Text.Length, 0);
                listBox1.Items.Clear();
                listBox1.Text = String.Empty;
                listBox1.Visible = false;
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                writtentxb.Text += listBox1.SelectedItem.ToString();
                listBox1.Items.Clear();
                listBox1.Text = String.Empty;
                listBox1.Visible = false;
                writtentxb.Focus();
                writtentxb.Select(writtentxb.Text.Length, 0);
            }
        }

        
    }
}
