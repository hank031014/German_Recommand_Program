using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class GrammarRules : Object
    {
        enum Role { M_C, M_F, M_S, M_V1, M_V2, M_O1, M_O2, M_P, // main sentence
                    S_C, S_F, S_S, S_V1, S_V2, S_O1, S_02, S_P, // sub sentence
                    UZ_U, UZ_O1, UZ_O2, UZ_P, UZ_Z, UZ_V, // Um...zu or Damit sentence
                    D_D, D_S, D_V1, D_V2, D_O1, D_O2, D_P }; // dass sentence

        private String[] no_init_query = { "der", "die", "das", "des", "dem", "den", "ein", "eine", "eines", "einem", "einen", "einer", "kein", "keine", "keines", "keinem", "keinen", "keiner" };
        
        private List<Words> stack, setz;
        private Hashtable st_element;

        private int[] S_rule = { 1, 4, 2 };
        private int state;

        private int subj, aux, v, obj1, obj2, sich, give, subs;

        public GrammarRules(List<Words> setz)
        {
            this.setz = setz;
            stack = new List<Words>();
            st_element = new Hashtable();
            state = 0;
            sentenceGrammarCheck();
        }

        private void sentenceGrammarCheck()
        {
            for (int i = 0; i < setz.Count; i++)
            {
                if (i == 0)
                {
                    firstWord();
                }
                else
                {
                    stBody(i);
                }
            }
        }

        private void firstWord()
        {
            switch (setz[0].POS)
            {
                case 1:
                    state = (int)Role.M_S;
                    break;
                case 2:
                    state = (int)Role.M_S;
                    if (setz[0].POS_dt == "n_s")
                    {
                        st_element.Add("M_subject", 5);
                    }
                    if (setz[0].POS_dt == "n_pl")
                    {
                        st_element.Add("M_subject", 6);
                    }
                    break;
                case 3:
                    state = (int)Role.M_V1;
                    st_element.Add("M_V1", 0);
                    break;
                case 4:
                    state = (int)Role.M_S;
                    
                    break;
                case 5:
                    if (setz[0].Conj_type == 1)
                    {
                        state = (int)Role.M_C;
                        st_element.Add("M_conj", 0);
                    }
                    if (setz[0].Conj_type == 2)
                    {
                        state = (int)Role.S_C;
                        st_element.Add("S_conj", 0);
                    }    
                    break;
                case 6:
                    state = (int)Role.M_F;
                    break;
                case 7:
                    state = (int)Role.M_F;
                    break;
                case 8:
                    if (setz[0].N_case == 1)
                    {
                        state = (int)Role.M_S;
                        st_element.Add("M_subject", setz[0].Pron_type);
                    }
                    if (setz[0].N_case == 2)
                    {
                        state = (int)Role.M_S;
                    }
                    else
                    {
                        state = (int)Role.M_F;
                    }
                    break;
                default:
                    setz[0].IsCheck = false;
                    break;
            }
        }

        private void stBody(int cur)
        {
            
            switch (setz[cur].POS)
            {
                case 1:
                    if (setz[cur].N_case == 2)
                    {
                        if (setz[cur - 1].POS != 2)
                        {
                            setz[cur].IsCheck = false;
                        }
                    }
                    if (setz[cur - 1].POS == 1)
                    {
                        if (setz[cur].N_case == setz[cur - 1].N_case)
                        {
                            setz[cur].IsCheck = false;
                        }
                    }
                    break;
                case 2:
                    state = (int)Role.M_S;
                    break;
                case 3:
                    if (state.Equals((int)Role.M_F) || state.Equals((int)Role.M_C))
                    {
                        state = (int)Role.M_V1;
                    }
                    if (state.Equals((int)Role.M_S))
                    {
                        if (!st_element.ContainsKey("M_subject") && setz[cur -1].POS == 1 && setz[cur - 1].N_case == 1)
                        {
                            if (setz[cur - 1].Pron_type == 5)
                            {
                                if (setz[cur].POS_dt.EndsWith("_s3") || setz[cur].POS_dt.EndsWith("_s"))
                                {
                                    state = (int)Role.M_V1;
                                }
                                else
                                {
                                    setz[cur].IsCheck = false;
                                }
                            }
                            if (setz[cur - 1].Pron_type == 6)
                            {
                                if (setz[cur].POS_dt.EndsWith("_pl"))
                                {
                                    state = (int)Role.M_V1;
                                }
                                else
                                {
                                    setz[cur].IsCheck = false;
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    state = (int)Role.M_S;
                    break;
                case 5:
                    state = (int)Role.M_C;
                    break;
                case 6:
                    state = (int)Role.M_F;
                    break;
                case 7:
                    state = (int)Role.M_F;
                    break;
                case 8:
                    if (state.Equals((int)Role.M_S) || state.Equals((int)Role.S_S) || state.Equals((int)Role.D_S))
                    {
                        if(setz[(cur-1 <= 0 ? 0 : cur-1)].Text != "und" || setz[cur-1 <= 0 ? 0 : cur-1].Text != "oder"){
                            setz[cur].IsCheck = false;
                        }
                        else
                        {
                            if (setz[(cur - 1 <= 0 ? 0 : cur - 1)].Text == "und")
                            {
                                if(setz[cur].Text == "ich" || setz[cur].Text == "wir"){
                                    st_element.Add("M_subject", 2);
                                }
                                else if (setz[cur].Text == "du" || setz[cur].Text == "ihr" || setz[cur].Text == "Sie")
                                {
                                    st_element.Add("M_subject", 4);
                                }
                                else
                                {
                                    st_element.Add("M_subject", 6);
                                }
                            }
                        }

                    }
                    if (state.Equals((int)Role.M_V1))
                    {
                        //Debug.WriteLine("pt: " + setz[cur].Pron_type);
                        switch ((int)setz[cur].Pron_type)
                        {
                            
                            case 1:
                                st_element.Add("M_subject", 1);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_sm"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            case 2:
                                st_element.Add("M_subject", 2);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            case 3:
                                st_element.Add("M_subject", 3);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_y2"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            case 4:
                                st_element.Add("M_subject", 4);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_y2"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            case 5:
                                st_element.Add("M_subject", 5);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_s3"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            case 6:
                                st_element.Add("M_subject", 6);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            case 7:
                                st_element.Add("M_subject", 7);
                                if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                {
                                    setz[(int)st_element["M_V1"]].IsCheck = false;
                                }
                                break;
                            default:
                                Debug.WriteLine("!!!");
                                break;
                        }
                    }
                    break;
                default:
                    setz[cur].IsCheck = false;
                    break;
            }
        }

        /*
        public void KnowWordRoleInSent(Words word)
        {
            switch (state)
            {
                case (int)Role.A:
                    //Debug.WriteLine("OKKKK");
                    //Anfang(word);
                    break;
                case (int)Role.F:

                    break;
                case (int)Role.S:
                    //Subject(word);
                    break;
                case (int)Role.V1:
                    //Verb1(word, sen);
                    break;
                case (int)Role.V2:

                    break;
                
                case (int)Role.O1:
                    //Object1(word);
                    break;
                case (int)Role.O2:

                    break;
                case (int)Role.Adv:

                    break;
                case (int)Role.SS:

                    break;
            }

            //return word;
        }

        public void Anfang(Words word)
        {
            //Debug.WriteLine("HERE");
            //int poss = word.wordProperty();
            //Debug.WriteLine("POS: " + word.POS + ",case: " + word.N_case);
            
            int poss = 1;
            Debug.WriteLine("POS: " + word.POS + ", case: " + word.N_case + ", poss: " + poss);
            if (poss > 1)
            {
                //stack.Add(word);
                //Debug.WriteLine("HERE");
                //word.choseOption();
                word.chooseOption();
                if (word.POS == 1 && word.N_case == 1)
                {
                    state = (int)Role.S;
                }
            }
            if (word.POS == 2)
            {
                state = (int)Role.S;
            }
            if (word.POS == 3)
            {
                state = (int)Role.S;
            }
            if (word.POS == 4)
            {
                
                stack.Add(word);
            }
            if (word.POS == 5)
            {
                state = (int)Role.S;
            }
            if (word.POS == 6)
            {
                state = (int)Role.F;
            }
            if (word.POS == 7)
            {
                state = (int)Role.V1;
            }
            if (word.POS == 8 && word.N_case == 1)
            {
                //Debug.WriteLine("8, 1 !!!");
                state = (int)Role.V1;
                subj = word.Pron_type;
            }
            
            //return word;
        }

        public void Subject(Words word)
        {

        }

        public void Verb1(Words word, List<Words> sen)
        {
            int poss = 1;
            Debug.WriteLine("V1 POS: " + word.POS + ", case: " + word.N_case + ", poss: " + poss);
            if (word.POS != 3)
            {
                word.IsCheck = false;
            }
        }

        public void Object1(Words word)
        {

        }
        */
    }
}
