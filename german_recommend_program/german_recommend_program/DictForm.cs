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
    public partial class DictForm : Form
    {
        private SqlConnection db;
        private List<DictWord> dwList;
        private DictWord dw;

        public DictForm(SqlConnection db)
        {
            InitializeComponent();
            this.db = db;
            dwList = new List<DictWord>();
        }

        private void DictForm_Load(object sender, EventArgs e)
        {
            lb_result.Text = String.Empty;
            lb_word.Text = String.Empty;
            lb_pos.Text = String.Empty;
            lb_id.Text = String.Empty;
            lb_english.Text = String.Empty;
            lb_n_gender.Text = String.Empty;
            lb_n_pural.Text = String.Empty;
            lb_adj_comp.Text = String.Empty;
            lb_adj_super.Text = String.Empty;
            lb_v_give.Text = String.Empty;
            lb_v_part.Text = String.Empty; 
            lb_v_sich.Text = String.Empty;
            lb_v_vt.Text = String.Empty;
            lb_v_ppv.Text = String.Empty;
            lb_v_ping.Text = String.Empty;
            lb_v_pt.Text = String.Empty;
            lb_v_p.Text = String.Empty;
            lb_v_pp.Text = String.Empty;
            lb_conj.Text = String.Empty;
            lb_prep.Text = String.Empty;
            lb_pron.Text = String.Empty;

            rtb_search.Focus();
        }

        private void rtb_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//enter
            {
                wordSearch();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            wordSearch();            
        }

        private void DictForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dwList.Clear();
            this.Dispose();
            GC.SuppressFinalize(this);
            
        }

        private void lb_result_SelectedIndexChanged(object sender, EventArgs e)
        {
            setLabels(lb_result.SelectedIndex);
        }

        private void wordSearch()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            String sql = String.Empty;
            String aim = rtb_search.Text;

            if (aim == String.Empty)
            {
                MessageBox.Show("搜尋字串不得為空！", "警告");

            }
            else
            {
                sql += "SELECT * FROM general_words WHERE word =N'" + aim + "' OR noun_pural=N'" + aim + "' ";
                sql += "OR adj_compa=N'" + aim + "' OR adj_super=N'" + aim + "' OR verb_present_trans=N'" + aim + "' ";
                sql += "OR verb_past=N'" + aim + "' OR verb_pp=N'" + aim + "' ORDER BY id ASC";

                cmd = new SqlCommand(sql, db);
                dr = cmd.ExecuteReader();
                dwList.Clear();
                while (dr.Read())
                {
                    dw = new DictWord(dr);
                    dwList.Add(dw);
                    dw = null;
                }
                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                if (dwList.Count == 0)
                {
                    MessageBox.Show("搜尋後找不到符合的單字！", "無結果");
                }
                else
                {
                    lb_result.Items.Clear();
                    lb_result.Text = "";
                    for (int i = 0; i < dwList.Count; i++)
                    {
                        lb_result.Items.Add(dwList[i].Word);
                    }
                    lb_result.SelectedIndex = 0;
                }
            }
        }

        private void setLabels(int index)
        {
            lb_word.Text = dwList[index].Word;
            lb_pos.Text = dwList[index].getPos();
            lb_id.Text = dwList[index].ID.ToString();
            lb_english.Text = dwList[index].English;
            lb_n_gender.Text = dwList[index].getNGender();
            lb_n_pural.Text = dwList[index].N_pural;
            lb_adj_comp.Text = dwList[index].Adj_compa;
            lb_adj_super.Text = dwList[index].Adj_super;
            lb_v_give.Text = dwList[index].getVgive();
            lb_v_part.Text = dwList[index].V_part;
            lb_v_sich.Text = dwList[index].getVsich();
            lb_v_vt.Text = dwList[index].getVvt();
            lb_v_ppv.Text = dwList[index].getVppAux();
            lb_v_ping.Text = dwList[index].V_prog;
            lb_v_pt.Text = dwList[index].V_present_trans;
            lb_v_p.Text = dwList[index].V_past;
            lb_v_pp.Text = dwList[index].V_pp;
            lb_conj.Text = dwList[index].getConjType();
            lb_prep.Text = dwList[index].getPrepType();
            lb_pron.Text = dwList[index].getPronType();
        }
    }
}
