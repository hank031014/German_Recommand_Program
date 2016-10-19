using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class GrammarRules : Object
    {
        private String[] no_init_query = { "der", "die", "das", "des", "dem", "den", "ein", "eine", "eines", "einem", "einen", "einer", "kein", "keine", "keines", "keinem", "keinen", "keiner" };
        private String[] adj_end = {"e", "en", "er", "es", "em", "en" };
        private List<Words> stack;
        enum Role { A, S, V1, V2, V3, O1, O2, Adv1, Adv2, SS };
        /*          0  1  2   3   4   5   6   7     8     9*/
        //Rule 1: 0_1_2_(7)_(5)_(6)_(3)_(4)_(,)_(9)
        //Rule 2: 0_(7,5,6)_2_1_(8)_(5)_(6)_(3)_(4)_(,)_(9)

        private int[] S_rule = { 1, 4, 2 };
        private int state; 

        public GrammarRules()
        {
            stack = new List<Words>();
            state = -1;

        }

        public void KnowWordRoleInSent(Words word)
        {
            switch (state)
            {
                case (int)Role.A:
                    Anfang(word);
                    break;
                case (int)Role.S:
                    Subject(word);
                    break;
                case (int)Role.V1:
                    Verb1(word);
                    break;
                case (int)Role.V2:

                    break;
                case (int)Role.V3:

                    break;
                case (int)Role.O1:
                    Object1(word);
                    break;
                case (int)Role.O2:

                    break;
                case (int)Role.Adv1:

                    break;
                case (int)Role.Adv2:

                    break;
            }
        }

        public void Anfang(Words word)
        {
            if ((Array.IndexOf(no_init_query, word.Text) == -1) ? true : false)
            {
                
                stack.Add(word);
            }
            else
            {
                word.wordProperty();
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
                if (word.POS == 8)
                {
                    state = (int)Role.S;
                }
            }
        }
        public void Subject(Words word)
        {

        }

        public void Verb1(Words word)
        {

        }

        public void Object1(Words word)
        {

        }

    }
}
