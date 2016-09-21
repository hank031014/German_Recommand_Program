using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace german_recommend_program
{
    class Words : Object
    {
        private Form1 curForm;
        private FlowLayoutPanel formPanel;

        private int id, pos, noun_gender;
        private String word, chinese, english;
        private String noun_pural, adj_orgi_change, adj_compa, adj_super;
        private String verb_prep_obj, verb_radical;
        private int conj_type, prep_type, verb_type;
        private int verb_pp_auv, verb_vt, verb_sich, verb_give, verb_part, verb_inf_dass;

        private string cn;
        private SqlConnection db;

        public Words(String text, Form1 cur)
        {
            word = text;
            curForm = cur;
            formPanel = curForm.getFormPanel();
            cn = @"Data Source=140.116.154.85;" + "Database=german_project;" + "Uid=user;"  + "Pwd=!ncku99!;";
            db = new SqlConnection(cn);
            db.Open();
            //MessageBox.Show(word + "@@@", "單字");
            
        }


        public void wordProperty()
        {
            Label wl = new Label();
            wl.Text = word;
            wl.AutoSize = true;
            wl.BorderStyle = BorderStyle.FixedSingle;
            wl.Font = new Font("Microsoft JhengHei", 12);
            wl.ForeColor = Color.Black;
            wl.Click += new EventHandler(label_onclick);
            wl.Parent = formPanel;

            
        }

        protected void label_onclick(object sender, EventArgs e)
        {
            Label tmp = (Label)sender;
            String sql = "SELECT * FROM general_words WHERE word='" + word + "'";
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
            db.Close();
        }

    }
}
