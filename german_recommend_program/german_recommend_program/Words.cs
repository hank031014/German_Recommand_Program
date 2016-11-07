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
        private Boolean isCheck;

        private int id, pos, noun_gender;
        private String word, chinese, english;
        private String noun_pural, adj_compa, adj_super;
        private int verb_ppaux, verb_vt, verb_sich, verb_give;
        private String verb_part, verb_present_trans, verb_prog, verb_pp, verb_past;
        private int conj_type, prep_type;
        private String ori_word;
        private int n_case, pron_type;

        private String pos_dt;

        private List<Words> options;
        private int option_cur;
        private int possibility;

        private String element;
        //private String ori_word;

        private string cn;
        private SqlConnection db;

        public Words(String text, Form1 cur)
        {
            options = new List<Words>();
            isCheck = true;
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
            ori_word = word;
            curForm = cur;
            formPanel = curForm.getFormPanel();
            cn = @"Data Source=140.116.154.85;" + "Database=german_project;" + "Uid=user;"  + "Pwd=!ncku99!;";
            db = new SqlConnection(cn);
            db.Open();
            
            String[] sein_verb = { "bin", "sind", "bist", "seid", "ist" };
            foreach (String sv in sein_verb)
            {
                if (word == sv)
                {
                    ori_word = "ist";
                    pos_dt = "v_sn";
                    break;
                }
            }
            String[] haben_verb = { "hat", "hast", "habt", "habe", "haben" };
            foreach (String hv in haben_verb)
            {
                if (word == hv)
                {
                    ori_word = "haben";
                    pos_dt = "v_hb";
                    break;
                }
            }
        }


        public int wordProperty(Boolean isFirst)
        {
            int poss = 0;
            Words w;
            SqlCommand cmd;
            SqlDataReader dr;
            String sql = "";
            if (isFirst) // is the first word in a sentence
            {
                sql = String.Empty;
                sql += "SELECT * FROM general_words WHERE word =N'" + ori_word + "' OR noun_pural=N'" + ori_word + "' ";
                sql += "OR adj_compa=N'" + ori_word + "' OR adj_super=N'" + ori_word + "' OR verb_present_trans=N'" + ori_word + "' ";
                sql += "OR verb_past=N'" + ori_word + "' OR verb_pp=N'" + ori_word + "' ORDER BY id ASC";
            }
            else //is NOT the first word in a sentence
            {
                sql = String.Empty;
                sql += "SELECT * FROM general_words WHERE word =N'" + ori_word + "' COLLATE Latin1_General_CS_AI OR noun_pural=N'" + ori_word + "' COLLATE Latin1_General_CS_AI ";
                sql += "OR adj_compa=N'" + ori_word + "' COLLATE Latin1_General_CS_AI OR adj_super=N'" + ori_word + "' COLLATE Latin1_General_CS_AI OR verb_present_trans=N'" + ori_word + "' COLLATE Latin1_General_CS_AI ";
                sql += "OR verb_past=N'" + ori_word + "' COLLATE Latin1_General_CS_AI OR verb_pp=N'" + ori_word + "' COLLATE Latin1_General_CS_AI ORDER BY id ASC";
            }
            cmd = new SqlCommand(sql, db);
            dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                w = new Words(word, curForm);
                w = w.addOption(w, dr);     
                options.Add(w);
                poss++;
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();

            if (poss == 0)
            {
                String[] verb_end = { "e", "est", "et", "st", "t" };
                String[] other_end = { "e", "er", "es", "en", "em" }; // adj., art. ...
                String tmp = String.Empty;
                Boolean isOther = Array.Exists(other_end,
                           oe =>
                           {
                               if (ori_word.EndsWith(oe))
                               {
                                   tmp = ori_word.Substring(0, ori_word.LastIndexOf(oe));
                                   return true;
                               }
                               else
                               {
                                   return false;
                               }
                           });;
                Boolean isVerb;
                
                while (isOther) // guess is not a verb
                {
                    if (isFirst) // is the first word in a sentence
                    {
                        sql = String.Empty;
                        sql += "SELECT * FROM general_words WHERE word =N'" + tmp + "' OR noun_pural=N'" + tmp + "' ";
                        sql += "OR adj_compa=N'" + tmp + "' OR adj_super=N'" + tmp + "' OR verb_present_trans=N'" + tmp + "' ";
                        sql += "OR verb_past=N'" + tmp + "' OR verb_pp=N'" + tmp + "' ORDER BY id ASC";
                    }
                    else //is NOT the first word in a sentence
                    {
                        sql = String.Empty;
                        sql += "SELECT * FROM general_words WHERE word =N'" + tmp + "' COLLATE Latin1_General_CS_AI OR noun_pural=N'" + tmp + "' COLLATE Latin1_General_CS_AI ";
                        sql += "OR adj_compa=N'" + tmp + "' COLLATE Latin1_General_CS_AI OR adj_super=N'" + tmp + "' COLLATE Latin1_General_CS_AI OR verb_present_trans=N'" + tmp + "' COLLATE Latin1_General_CS_AI ";
                        sql += "OR verb_past=N'" + tmp + "' COLLATE Latin1_General_CS_AI OR verb_pp=N'" + tmp + "' COLLATE Latin1_General_CS_AI ORDER BY id ASC";
                    }
                    cmd = new SqlCommand(sql, db);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        w = new Words(word, curForm);
                        w = addOption(w, dr);
                        options.Add(w);
                        poss++;
                    }
                    if (options.Count > 0)
                    {
                        if (options[0].pos == 2)
                        {
                            isOther = false;
                        }
                    }
                    
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    break;
                }
                isVerb = Array.Exists(verb_end,
                            ve =>
                            {
                                if (ori_word.EndsWith(ve))
                                {
                                    tmp = ori_word.Substring(0, ori_word.LastIndexOf(ve)) + "en";
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            });
                while((!isOther || poss == 0) && isVerb) // guess is a verb
                {
                    if (isFirst) // is the first word in a sentence
                    {
                        sql = String.Empty;
                        sql += "SELECT * FROM general_words WHERE word =N'" + tmp + "' OR noun_pural=N'" + tmp + "' ";
                        sql += "OR adj_compa=N'" + tmp + "' OR adj_super=N'" + tmp + "' OR verb_present_trans=N'" + tmp + "' ";
                        sql += "OR verb_past=N'" + tmp + "' OR verb_pp=N'" + tmp + "' ORDER BY id ASC";
                    }
                    else //is NOT the first word in a sentence
                    {
                        sql = String.Empty;
                        sql += "SELECT * FROM general_words WHERE word =N'" + tmp + "' COLLATE Latin1_General_CS_AI OR noun_pural=N'" + tmp + "' COLLATE Latin1_General_CS_AI ";
                        sql += "OR adj_compa=N'" + tmp + "' COLLATE Latin1_General_CS_AI OR adj_super=N'" + tmp + "' COLLATE Latin1_General_CS_AI OR verb_present_trans=N'" + tmp + "' COLLATE Latin1_General_CS_AI ";
                        sql += "OR verb_past=N'" + tmp + "' COLLATE Latin1_General_CS_AI OR verb_pp=N'" + tmp + "' COLLATE Latin1_General_CS_AI ORDER BY id ASC";
                    }
                    cmd = new SqlCommand(sql, db);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        w = new Words(word, curForm);
                        w = addOption(w, dr);
                        options.Add(w);
                        poss++;
                    }
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    break;
                }
                if (poss == 0)
                {
                    isCheck = false;
                }
            }
            
            if (poss == 1)
            {
                id = options[0].id;
                english = options[0].english;
                pos = options[0].pos;
                noun_gender = options[0].noun_gender;
                noun_pural = options[0].noun_pural;
                adj_compa = options[0].adj_compa;
                adj_super = options[0].adj_super;
                verb_vt = options[0].verb_vt;
                verb_give = options[0].verb_give;
                verb_sich = options[0].verb_sich;
                verb_ppaux = options[0].verb_ppaux;
                verb_part = options[0].verb_part;
                verb_present_trans = options[0].verb_present_trans;
                verb_prog = options[0].verb_prog;
                verb_pp = options[0].verb_pp;
                verb_past = options[0].verb_past;
                conj_type = options[0].conj_type;
                prep_type = options[0].prep_type;
                ori_word = options[0].ori_word;
                n_case = options[0].n_case;
                prep_type = options[0].prep_type;

                options.Clear();
            }
            else if (poss > 1)
            {
                firstChoose(isFirst);
            }

            return poss;
        }

        private Words addOption(Words w, SqlDataReader dr)
        {
            w.id = Int32.Parse(dr[0].ToString());
            w.english = dr[2].ToString();
            w.pos = Int32.Parse(dr[3].ToString());
            w.noun_gender = Int32.Parse(dr[4].ToString());
            w.noun_pural = dr[5].ToString();
            w.adj_compa = dr[6].ToString();
            w.adj_super = dr[7].ToString();
            w.verb_vt = Int32.Parse(dr[8].ToString());
            w.verb_give = Int32.Parse(dr[9].ToString());
            w.verb_sich = Int32.Parse(dr[10].ToString());
            w.verb_ppaux = Int32.Parse(dr[11].ToString());
            w.verb_part = dr[12].ToString();
            w.verb_present_trans = dr[13].ToString();
            w.verb_prog = dr[14].ToString();
            w.verb_pp = dr[15].ToString();
            w.verb_past = dr[16].ToString();
            w.conj_type = Int32.Parse(dr[17].ToString());
            w.prep_type = Int32.Parse(dr[18].ToString());
            w.ori_word = (dr[19].ToString() == "" ? dr[1].ToString() : dr[19].ToString());
            w.n_case = Int32.Parse(dr[20].ToString());
            w.prep_type = Int32.Parse(dr[21].ToString());
            return w;
        }

        private void firstChoose(Boolean isFirst)
        {
            int[] order = { 1, 8, 3, 2, 6, 5, 4, 7 }; //art. -> pron. -> v. -> n. -> prep. -> conj. -> adj. -> adv.
            for (int j = 0; j < order.Length; j++)
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i].pos == order[j] && order[j] == 1 && options[i].n_case == 1)
                    {
                        id = options[i].id;
                        english = options[i].english;
                        pos = options[i].pos;
                        noun_gender = options[i].noun_gender;
                        noun_pural = options[i].noun_pural;
                        adj_compa = options[i].adj_compa;
                        adj_super = options[i].adj_super;
                        verb_vt = options[i].verb_vt;
                        verb_give = options[i].verb_give;
                        verb_sich = options[i].verb_sich;
                        verb_ppaux = options[i].verb_ppaux;
                        verb_part = options[i].verb_part;
                        verb_present_trans = options[i].verb_present_trans;
                        verb_prog = options[i].verb_prog;
                        verb_pp = options[i].verb_pp;
                        verb_past = options[i].verb_past;
                        conj_type = options[i].conj_type;
                        prep_type = options[i].prep_type;
                        ori_word = options[i].ori_word;
                        n_case = options[i].n_case;
                        prep_type = options[i].prep_type;

                        options.RemoveAt(i);
                        return;
                    }
                    if (options[i].pos == order[j])
                    {
                        id = options[i].id;
                        english = options[i].english;
                        pos = options[i].pos;
                        noun_gender = options[i].noun_gender;
                        noun_pural = options[i].noun_pural;
                        adj_compa = options[i].adj_compa;
                        adj_super = options[i].adj_super;
                        verb_vt = options[i].verb_vt;
                        verb_give = options[i].verb_give;
                        verb_sich = options[i].verb_sich;
                        verb_ppaux = options[i].verb_ppaux;
                        verb_part = options[i].verb_part;
                        verb_present_trans = options[i].verb_present_trans;
                        verb_prog = options[i].verb_prog;
                        verb_pp = options[i].verb_pp;
                        verb_past = options[i].verb_past;
                        conj_type = options[i].conj_type;
                        prep_type = options[i].prep_type;
                        ori_word = options[i].ori_word;
                        n_case = options[i].n_case;
                        prep_type = options[i].prep_type;

                        options.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public void word_add_label(){
            Label wl = new Label();
            if (!isCheck)
            {
                wl.BackColor = Color.Red;
            }
            wl.Text = word + " (" + posText(pos) + ")";
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
            String str = "";

            str += "單字：" + word;
            str += "\n單字資料庫ID：" + id;
            str += "\n單字的原形：" + ori_word;
            str += "\n英文：" + english;
            str += "\n詞性：" + posText(pos);
            str += "\n當前格位：" + nCaseText(n_case);

            MessageBox.Show(str, "資料庫單字讀取結果");          
        }

        private String posText(int pos)
        {
            String txt = "";
            switch (pos)
            {
                case 1:
                    txt = "冠詞";
                    break;
                case 2:
                    txt = "名詞";
                    break;
                case 3:
                    txt = "動詞";
                    break;
                case 4:
                    txt = "形容詞";
                    break;
                case 5:
                    txt = "連詞";
                    break;
                case 6:
                    txt = "介詞";
                    break;
                case 7:
                    txt = "副詞";
                    break;
                case 8:
                    txt = "代詞";
                    break;
                case 9:
                    txt = "疑問詞";
                    break;
                case 10:
                    txt = "嘆詞";
                    break;
                case 11:
                    txt = "數詞";
                    break;
                default:
                    txt = "Error!";
                    break;
            }
            return txt;
        }

        private String nCaseText(int n_case)
        {
            String txt = "";
            switch (n_case)
            {
                case 1:
                    txt = "主格(Nom)";
                    break;
                case 2:
                    txt = "屬格(Gen)";
                    break;
                case 3:
                    txt = "與格(Dat)";
                    break;
                case 4:
                    txt = "賓格(Akk)";
                    break;
                default:
                    txt = "無格位";
                    break;
            }
            return txt;
        }

        public void chooseOption(int Gpos = 1, int Gn_case = 1)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].pos == Gpos && options[i].n_case == Gn_case)
                {
                    id = options[i].id;
                    english = options[i].english;
                    pos = options[i].pos;
                    noun_gender = options[i].noun_gender;
                    noun_pural = options[i].noun_pural;
                    adj_compa = options[i].adj_compa;
                    adj_super = options[i].adj_super;
                    verb_vt = options[i].verb_vt;
                    verb_give = options[i].verb_give;
                    verb_sich = options[i].verb_sich;
                    verb_ppaux = options[i].verb_ppaux;
                    verb_part = options[i].verb_part;
                    verb_present_trans = options[i].verb_present_trans;
                    verb_prog = options[i].verb_prog;
                    verb_pp = options[i].verb_pp;
                    verb_past = options[i].verb_past;
                    conj_type = options[i].conj_type;
                    prep_type = options[i].prep_type;
                    ori_word = options[i].ori_word;
                    n_case = options[i].n_case;
                    prep_type = options[i].prep_type;
                    return;
                }
            }
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].pos == 8 && options[i].n_case == Gn_case)
                {
                    id = options[i].id;
                    english = options[i].english;
                    pos = options[i].pos;
                    noun_gender = options[i].noun_gender;
                    noun_pural = options[i].noun_pural;
                    adj_compa = options[i].adj_compa;
                    adj_super = options[i].adj_super;
                    verb_vt = options[i].verb_vt;
                    verb_give = options[i].verb_give;
                    verb_sich = options[i].verb_sich;
                    verb_ppaux = options[i].verb_ppaux;
                    verb_part = options[i].verb_part;
                    verb_present_trans = options[i].verb_present_trans;
                    verb_prog = options[i].verb_prog;
                    verb_pp = options[i].verb_pp;
                    verb_past = options[i].verb_past;
                    conj_type = options[i].conj_type;
                    prep_type = options[i].prep_type;
                    ori_word = options[i].ori_word;
                    n_case = options[i].n_case;
                    prep_type = options[i].prep_type;
                    return;
                }
            }
             /*   if (option_cur < options.Count)
                {
                    id = options[option_cur].id;
                    english = options[option_cur].english;
                    pos = options[option_cur].pos;
                    noun_gender = options[option_cur].noun_gender;
                    noun_pural = options[option_cur].noun_pural;
                    adj_compa = options[option_cur].adj_compa;
                    adj_super = options[option_cur].adj_super;
                    verb_vt = options[option_cur].verb_vt;
                    verb_give = options[option_cur].verb_give;
                    verb_sich = options[option_cur].verb_sich;
                    verb_ppaux = options[option_cur].verb_ppaux;
                    verb_part = options[option_cur].verb_part;
                    verb_present_trans = options[option_cur].verb_present_trans;
                    verb_prog = options[option_cur].verb_prog;
                    verb_pp = options[option_cur].verb_pp;
                    verb_past = options[option_cur].verb_past;
                    conj_type = options[option_cur].conj_type;
                    prep_type = options[option_cur].prep_type;
                    ori_word = options[option_cur].ori_word;
                    n_case = options[option_cur].n_case;
                    prep_type = options[option_cur].prep_type;

                    option_cur++;
                }*/
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

        public int N_case
        {
            get
            {
                return n_case;
            }
        }

        public Boolean IsCheck
        {
            set
            {
                isCheck = value;
            }
        }

        public int Pron_type
        {
            get
            {
                return pron_type;
            }
        }
    }
}
