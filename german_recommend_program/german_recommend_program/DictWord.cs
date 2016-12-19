using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class DictWord : Object
    {
        private int id, pos, noun_gender;
        private String word, chinese, english;
        private String noun_pural, adj_compa, adj_super;
        private int verb_ppaux, verb_vt, verb_sich, verb_give;
        private String verb_part, verb_present_trans, verb_prog, verb_pp, verb_past;
        private int conj_type, prep_type;
        private String ori_word;
        private int n_case, pron_type;

        public DictWord(SqlDataReader dr)
        {
            id = Int32.Parse(dr[0].ToString());
            word = dr[1].ToString();
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
            ori_word = (dr[19].ToString() == "" ? dr[1].ToString() : dr[19].ToString());
            n_case = Int32.Parse(dr[20].ToString());
            pron_type = Int32.Parse(dr[21].ToString());
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public String getPos()
        {
            String ret = String.Empty;
            switch (pos)
            {
                case 1:
                    ret = "冠詞";
                    break;
                case 2:
                    ret = "名詞";
                    break;
                case 3:
                    ret = "動詞";
                    break;
                case 4:
                    ret = "形容詞";
                    break;
                case 5:
                    ret = "連詞";
                    break;
                case 6:
                    ret = "介詞";
                    break;
                case 7:
                    ret = "副詞";
                    break;
                case 8:
                    ret = "代詞";
                    break;
                case 10:
                    ret = "嘆詞";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }

        public String getNGender()
        {
            String ret = String.Empty;
            switch (noun_gender)
            {
                case 0:
                    ret = String.Empty;
                    break;
                case 1:
                    ret = "陽性";
                    break;
                case 2:
                    ret = "陰性";
                    break;
                case 3:
                    ret = "中性";
                    break;
                case 4:
                    ret = "複數形";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }

        public String Word
        {
            get
            {
                return word;
            }
        }

        public String English
        {
            get
            {
                return english;
            }
        }

        public String N_pural
        {
            get
            {
                return noun_pural;
            }
        }

        public String Adj_compa
        {
            get
            {
                return adj_compa;
            }
        }

        public String Adj_super
        {
            get
            {
                return adj_super;
            }
        }

        public String getVppAux()
        {
            String ret = String.Empty;
            switch (verb_ppaux)
            {
                case 0:
                    ret = "非動詞";
                    break;
                case 1:
                    ret = "haben";
                    break;
                case 2:
                    ret = "sein";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }

        public String getVvt()
        {
            String ret = String.Empty;
            switch (verb_ppaux)
            {
                case 0:
                    ret = "非動詞";
                    break;
                case 1:
                    ret = "不及物動詞";
                    break;
                case 2:
                    ret = "一般及物動詞";
                    break;
                case 3:
                    ret = "Daktiv及物動詞";
                    break;                    
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }

        public String getVsich()
        {
            String ret = String.Empty;
            switch (verb_sich)
            {
                case 0:
                    ret = "非動詞或一般動詞";
                    break;
                case 1:
                    ret = "反身動詞";
                    break;                   
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }

        public String getVgive()
        {
            String ret = String.Empty;
            switch (verb_ppaux)
            {
                case 0:
                    ret = "非動詞或不是";
                    break;
                case 1:
                    ret = "是";
                    break;                    
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }
        
        public String V_part
        {
            get
            {
                return verb_part;
            }
        }
        
        public String V_present_trans
        {
            get
            {
                return verb_present_trans;
            }
        }
        
        public String V_prog
        {
            get
            {
                return verb_prog;
            }
        }
            
       public String V_pp
        {
            get
            {
                return verb_pp;
            }
        }
 
        public String V_past
        {
            get
            {
                return verb_past;
            }
        } 
                
        public String getConjType()
        {
            String ret = String.Empty;
            switch (conj_type)
            {
                case 0:
                    ret = "非連詞";
                    break;
                case 1:
                    ret = "對等連接詞";
                    break;
                case 2:
                    ret = "從屬連接詞";
                    break;
                case 3:
                    ret = "副詞連接詞";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }
        
        public String getPrepType()
        {
            String ret = String.Empty;
            switch (prep_type)
            {
                case 0:
                    ret = "非介詞";
                    break;
                case 1:
                    ret = "受詞必須為Akkusativ";
                    break;
                case 2:
                    ret = "受詞必須為Dativ";
                    break;
                case 3:
                    ret = "受詞可接Akk或Dat";
                    break;
                case 4:
                    ret = "受詞必須為Gentiv";
                    break;
                case 5:
                    ret = "受詞可接Gen或Dat";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }
             
        public String getPronType()
        {
            String ret = String.Empty;
            switch (pron_type)
            {
                case 0:
                    ret = "非代詞";
                    break;
                case 1:
                    ret = "第一人稱單數";
                    break;
                case 2:
                    ret = "第一人稱複數";
                    break;
                case 3:
                    ret = "第二人稱單數";
                    break;
                case 4:
                    ret = "第二人稱複數";
                    break;
                case 5:
                    ret = "第三人稱單數";
                    break;
                case 6:
                    ret = "第三人稱複數";
                    break;
                case 7:
                    ret = "第二人稱單數(Sie)";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }
          
    }
}
