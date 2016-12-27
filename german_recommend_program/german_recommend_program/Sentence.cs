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
    class Sentence : Object
    {
        private int order;
        private String text;
        private int property; //0:一般(無子句)直述句, 1:有子句直述句, 2:問句&無子句(?), 3:問句&有子句(?), 4:感嘆句(!)
        private List<Words> word_stack;
        private Form1 curForm;
        private SqlConnection db;

        private GrammarRules gr;
        private Boolean aux, que, give, sich, time, freq, place;
        private int start; // 0:一般直述句, 1:問句(動詞前置), 2:受詞或補述前置
        private int subject; //(主詞) 1:第一人稱單數, 2:第一人稱複數, 3:第二人稱單數, 4:第二人稱複數, 5:第三人稱單數, 6:第三人稱複數, 7:第二人稱單數(Sie)
        private int tense, obj1, obj2;

        private String[] special_verb = { "haben", "sein", "werden", "können", "sollen", "wollen", "dürfen", "müssen", "mögen" };

        public Sentence(int norder, String ntext, Form1 cur, SqlConnection db)
        {
            order = norder;
            text = ntext;
            curForm = cur;
            this.db = db;
            property = 0;
            word_stack = new List<Words>();
            
        }

        public int Order
        {
            get
            {
                return order;
            } 
        }
        
        public String Text 
        { 
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public void sentenceProperty(int arg = 0)
        {
            Regex rqm = new Regex(@"\?");
            Regex rco = new Regex(@",");
            Regex rem = new Regex(@"!");
            Boolean qm = rqm.IsMatch(text);
            Boolean co = rco.IsMatch(text);
            Boolean em = rem.IsMatch(text);

            if (co == false && qm == false)
            {
                //Debug.WriteLine("一般直述句");
                property = 0;
            }
            if (co == true && qm == false)
            {
                //Debug.WriteLine("子句直述句");
                property = 1;
            }
            else if (co == false && qm == true)
            {
                //Debug.WriteLine("一般問句");
                property = 2;
            }
            else if (co == true && qm == true)
            {
                //Debug.WriteLine("子句問句");
                property = 3;
            }
            else if (em == true)
            {
                //Debug.WriteLine("感嘆句");
                property = 4;
            }

            String[] tmp = text.Split(' ');

            if (arg == 0)
            {
                for (int i = 0; i < tmp.Length; i++)
                {
                    Boolean isFirst;
                    Words word = new Words(tmp[i], curForm, db);
                    word_stack.Add(word);
                    //gr.KnowWordRoleInSent(word, word_stack);
                    if (i == 0)
                        isFirst = true;
                    else
                        isFirst = false;

                    word.wordProperty(isFirst);
                }

                gr = new GrammarRules(word_stack);

                for (int i = 0; i < word_stack.Count; i++)
                {
                    word_stack[i].word_add_label();

                }
            }
            if (arg == 1)
                //Debug.WriteLine("PPPPP");
            {
                SqlCommand cmd;
                SqlDataReader dr;
                String sql = String.Empty;
                List<OptionWord> options = new List<OptionWord>();
                int count = 0;
                ListBox lbox = curForm.getListBox();
                RichTextBox rtb = curForm.getRichTextBox();

                int len = tmp.Length;
                Words word = new Words(tmp[len - 1], curForm, db);
                Boolean isFirst;
                if ((len - 1) == 0){
                    isFirst = true;
                }
                else
                {
                    isFirst = false;
                }
                word.wordProperty(isFirst);
                
                if (((word.POS == 8 && word.N_case == 1) || word.POS == 2))
                {
                    //Debug.WriteLine("PPPPP");
                    sql += "SELECT * FROM verb_predict ORDER BY times DESC";
                    cmd = new SqlCommand(sql, db);
                    dr = cmd.ExecuteReader();
                    OptionWord ow;
                    while (dr.Read())
                    {
                        ow = new OptionWord();
                        ow.id = Int32.Parse(dr[0].ToString());
                        ow.originWord = dr[1].ToString();
                        ow.type = 1;
                        options.Add(ow);
                        count++;
                        if (count >= 5)
                        {
                            break;
                        }
                    }
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        if (word.POS == 8)
                        {
                            lbox.Items.Clear();
                            for (int i = 0; i < options.Count; i++)
                            {
                                Boolean result = special_verb.Any(w => { bool match = w.Equals(options[i].originWord);
                                                                         if (match)
                                                                         {
                                                                             options[i].showWord = matchWord(options[i].originWord, word.Pron_type);
                                                                         }
                                                                         return match; });
                                if(!result)
                                {
                                    options[i].showWord = wordProcess(options[i].originWord, word.Pron_type);
                                }
                               
                                lbox.Items.Add(options[i]);
                            }        
                        }
                        if (word.POS == 2)
                        {
                            for (int i = 0; i < options.Count; i++)
                            {
                                if (word.POS_dt.EndsWith("s"))
                                {
                                    options[i].showWord = wordProcess(options[i].originWord, word.Pron_type);
                                }
                                else if (word.POS_dt.EndsWith("pl"))
                                {
                                    options[i].showWord = wordProcess(options[i].originWord, word.Pron_type);
                                }
                                lbox.Items.Add(options[i]);
                            }
                        }
                        Point curPoint = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
                        curPoint.Offset(rtb.Left, rtb.Top + rtb.Font.Height);

                        lbox.Location = curPoint;
                        lbox.SelectedIndex = 0;
                        lbox.Visible = true;
                        lbox.BringToFront();
                        lbox.Focus();

                        options.Clear();
                    }  
                }
                if (word.POS == 3)
                {
                    try
                    {
                        sql += "UPDATE verb_predict SET times = times + 1 WHERE verb = '" + word.Ori_word + "'";
                        cmd = new SqlCommand(sql, db);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch
                    {

                    }
                    
                }
                if (word.POS == 1 || word.POS == 3 || word.POS == 6)
                {
                    sql += "SELECT * FROM predict WHERE front_word='" + word.Ori_word + "'ORDER BY times DESC";
                    cmd = new SqlCommand(sql, db);
                    dr = cmd.ExecuteReader();
                    OptionWord ow;
                    while (dr.Read())
                    {
                        ow = new OptionWord();
                        ow.type = 2;
                        ow.id = Int32.Parse(dr[0].ToString());
                        ow.showWord = dr[2].ToString();
                        options.Add(ow);
                        count++;
                        if (count >= 5)
                        {
                            break;
                        }
                    }
                    dr.Close();
                    dr.Dispose();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        for (int i = 0; i < options.Count; i++)
                        {
                            lbox.Items.Add(options[i]);
                        }
                        Point curPoint = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
                        curPoint.Offset(rtb.Left, rtb.Top + rtb.Font.Height);

                        lbox.Location = curPoint;
                        lbox.SelectedIndex = 0;
                        lbox.Visible = true;
                        lbox.BringToFront();
                        lbox.Focus();

                        options.Clear();
                    }
                }


            }

        }

        private String matchWord(String word, int type)
        {
            String ret = String.Empty;
            String[] sein = { "bin", "sind", "bist", "seid", "ist", "sind", "sind" };
            String[] haben = { "habe", "haben", "hast", "habt", "hat", "haben", "haben" };
            String[] werden = { "werde", "werden", "wirst", "werdet", "wird", "werden", "werden" };
            String[] koennen = {"kann", "können", "kannst", "könnt", "kann", "können", "können"};
            String[] sollen = {"soll", "sollen", "sollst", "sollt", "soll", "sollen", "sollen"};
            String[] wollen = {"will", "wollen", "willst", "wollt", "will", "wollen", "wollen"};
            String[] duerfen = {"darf", "dürfen", "darfst", "dürft", "darf","dürfen","dürfen"};
            String[] muessen = {"muß", "müssen", "mußt", "müßt", "muß","müssen","müssen"};
            String[] moegen = {"mag", "mögen", "magst", "mögt", "mag", "mögen", "mögen"};
            
            switch (word)
            {
                case "sein":
                    ret = sein[type - 1];
                    break;
                case "haben":
                    ret = haben[type - 1];
                    break;
                case "werden":
                    ret = werden[type - 1];
                    break;
                case "können":
                    ret = koennen[type - 1];
                    break;
                case "sollen":
                    ret = sollen[type - 1];
                    break;
                case "wollen":
                    ret = wollen[type - 1];
                    break;
                case "dürfen":
                    ret = duerfen[type - 1];
                    break;
                case "müssen":
                    ret = muessen[type - 1];
                    break;
                case "mögen":
                    ret = moegen[type - 1];
                    break;
            }
            return ret;
        }

        private String wordProcess(String word, int type)
        {
            String ret = String.Empty;
            switch (type)
            {
                case 1:
                    if (word.EndsWith("en"))
                    {
                        ret = word.Substring(0, word.Length - 1);
                    }
                    else if (word.EndsWith("n"))
                    {
                        ret = word.Substring(0, word.Length - 1) + "e";
                    }
                    break;
                case 2:
                    ret = word;
                    break;
                case 3:
                    if (word.EndsWith("ten") || word.EndsWith("den"))
                    {
                        ret = word.Substring(0, word.Length - 1) + "st";
                    }
                    else if (word.EndsWith("en"))
                    {
                        ret = word.Substring(0, word.Length - 2) + "st";
                    }
                    else if (word.EndsWith("n"))
                    {
                        ret = word.Substring(0, word.Length - 1) + "st";
                    }
                    break;
                case 4:
                case 5:
                    if (word.EndsWith("ten") || word.EndsWith("den"))
                    {
                        ret = word.Substring(0, word.Length - 1) + "t";
                    }
                    else if (word.EndsWith("en"))
                    {
                        ret = word.Substring(0, word.Length - 2) + "t";
                    }
                    else if (word.EndsWith("n"))
                    {
                        ret = word.Substring(0, word.Length - 1) + "t";
                    }
                    break;
                case 6:
                    ret = word;
                    break;
                case 7:
                    ret = word;
                    break;
            }
            return ret;
        }
        
    }
}
