using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class GrammarRules : Object
    {
        private String[] no_init_query = { "der", "die", "das", "des", "dem", "den", "ein", "eine", "eines", "einem", "einen", "einer", "kein", "keine", "keines", "keinem", "keinen", "keiner" };
        private String[] sein_verb = { "bin", "sind", "bist", "seid", "ist", "sind", "sind" };
        private String[] verb_end = { "e", "en", "t", "st" };
        private String[] adj_end = {"e", "en", "er", "es", "em", "en" };
        private List<Words> stack;
        enum Role { A, F, S, V1, V2, O1, O2, Adv, SS };
        /*          0  1  2  3   4   5   6   7    8  */
        //Rule 1: 0_1_2_(7)_(5)_(6)_(3)_(4)_(,)_(9)
        //Rule 2: 0_(7,5,6)_2_1_(8)_(5)_(6)_(3)_(4)_(,)_(9)

        private int[] S_rule = { 1, 4, 2 };
        private int state;

        private int subj, aux, v, obj1, obj2, sich, give, subs;

        public GrammarRules()
        {
            stack = new List<Words>();
            state = 0;

        }

        public void KnowWordRoleInSent(Words word, List<Words> sen)
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
            
            int poss = word.wordProperty(false);
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
                for (int i = 0; i < adj_end.Length; i++)
                {
                    if (word.Text.EndsWith(adj_end[i]))
                    {
                        word.Ori_word = word.Text.Substring(0, word.Text.Length - adj_end[i].Length);
                        break;
                    }
                }
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
            int poss = word.wordProperty(false);
            Debug.WriteLine("V1 POS: " + word.POS + ", case: " + word.N_case + ", poss: " + poss);
            if (word.POS != 3)
            {
                word.IsCheck = false;
            }
        }

        public void Object1(Words word)
        {

        }

    }
}
