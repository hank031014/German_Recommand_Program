using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace german_recommend_program
{
    class Words : Object
    {
        private Form1 curForm;
        private FlowLayoutPanel formPanel;
        private String marks;
        private Boolean IsCheck;

        private int id, pos, noun_gender;
        private String word, chinese, english;
        private String noun_pural, adj_compa, adj_super;
        private int verb_ppaux, verb_vt, verb_sich, verb_give;
        private String verb_part, verb_present_trans, verb_prog, verb_pp, verb_past;
        private int conj_type, prep_type;
        private String ori_word;
        private int n_case, pron_type;

        private String element;
        //private String ori_word;

        private string cn;
        private SqlConnection db;

        public Words(String text, Form1 cur)
        {
            IsCheck = false;
            marks = null;
            Regex rqm = new Regex(@"\?");
            Regex rco = new Regex(@",");
            Regex rem = new Regex(@"!");
            Regex rpr = new Regex(@"\.");
            if (rqm.IsMatch(text))
            {
                marks = "?";
            }
            else if (rco.IsMatch(text))
            {
                marks = ",";
            }
            else if (rem.IsMatch(text))
            {
                marks = "!";
            }
            else if (rpr.IsMatch(text))
            {
                marks = ".";
            }

            word = text.Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "");
            curForm = cur;
            formPanel = curForm.getFormPanel();
            cn = @"Data Source=140.116.154.85;" + "Database=german_project;" + "Uid=user;"  + "Pwd=!ncku99!;";
            db = new SqlConnection(cn);
            db.Open();
            //MessageBox.Show(word + "@@@", "單字");
            
        }


        public void wordProperty()
        {
            String sql = "SELECT * FROM general_words WHERE word LIKE'" + word + "%'";
            SqlCommand cmd = new SqlCommand(sql, db);
            SqlDataReader dr = cmd.ExecuteReader(); 
            while (dr.Read())
            {
                if (dr.FieldCount > 0)
                {
                    id = Int32.Parse(dr[0].ToString());
                    english = dr[2].ToString();
                    pos = Int32.Parse(dr[3].ToString());
                    noun_gender = Int32.Parse(dr[4].ToString());
                    noun_pural = dr[5].ToString();
                    adj_compa = dr[6].ToString();
                    adj_super = dr[7].ToString();
                    verb_vt = Int32.Parse(dr[8].ToString());
                    verb_give = Int32.Parse(dr[9].ToString());
                    verb_sich = Int32.Parse(dr[10].ToString());
                    verb_ppaux = Int32.Parse(dr[11].ToString());
                    verb_part = dr[12].ToString();
                    verb_present_trans = dr[13].ToString();
                    verb_prog = dr[14].ToString();
                    verb_pp = dr[15].ToString();
                    verb_past = dr[16].ToString();
                    conj_type = Int32.Parse(dr[17].ToString());
                    prep_type = Int32.Parse(dr[18].ToString());
                    ori_word = dr[19].ToString();
                    n_case = Int32.Parse(dr[20].ToString());
                    prep_type = Int32.Parse(dr[21].ToString());
                }              
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();

            
        }

        public void word_add_label(){
            Label wl = new Label();
            wl.Text = word;
            wl.AutoSize = true;
            wl.BorderStyle = BorderStyle.FixedSingle;
            wl.Font = new Font("Microsoft JhengHei", 12);
            wl.ForeColor = Color.Black;
            wl.Click += new EventHandler(label_onclick);
            wl.Parent = formPanel;

            if (!String.IsNullOrEmpty(marks))
            {
                Label ml = new Label();
                ml.Text = marks;
                ml.AutoSize = true;
                ml.BorderStyle = BorderStyle.FixedSingle;
                ml.Font = new Font("Microsoft JhengHei", 12);
                ml.ForeColor = Color.Black;
                ml.Click += new EventHandler(label_onclick);
                ml.Parent = formPanel;
            }
        }

        protected void label_onclick(object sender, EventArgs e)
        {
            Label tmp = (Label)sender;
            String sql = "SELECT * FROM general_words WHERE word='" + tmp.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, db);
            SqlDataReader dr = cmd.ExecuteReader();
            //Debug.WriteLine("DR:" + dr.Read());
            String str = "";
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    str += dr.GetName(i) + ": " + dr[i].ToString() + "\n";
                }
            }

            MessageBox.Show(str, "資料庫單字讀取結果");
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            //db.Close();
        }

        public int POS
        {
            get{
                return pos;
            }            
        }

        public String Text
        {
            get
            {
                return word;
            }
        }
        public String Ori_word
        {
            set
            {
                ori_word = value;
            }
        }

        public String Element {
            get
            {
                return element;
            }
            set
            {
                element = value;
            }
        }
    }
}
