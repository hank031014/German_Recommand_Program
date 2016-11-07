using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private GrammarRules gr;
        private Boolean aux, que, give, sich, time, freq, place;
        private int start; // 0:一般直述句, 1:問句(動詞前置), 2:受詞或補述前置
        private int subject; //(主詞) 1:第一人稱單數, 2:第一人稱複數, 3:第二人稱單數, 4:第二人稱複數, 5:第三人稱單數, 6:第三人稱複數, 7:第二人稱單數(Sie)
        private int tense, obj1, obj2;

        public Sentence(int norder, String ntext, Form1 cur)
        {
            order = norder;
            text = ntext;
            curForm = cur;
            property = 0;
            word_stack = new List<Words>();
            gr = new GrammarRules();
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

        public void sentenceProperty()
        {
            Regex rqm = new Regex(@"\?");
            Regex rco = new Regex(@",");
            Regex rem = new Regex(@"!");
            Boolean qm = rqm.IsMatch(text);
            Boolean co = rco.IsMatch(text);
            Boolean em = rem.IsMatch(text);

            if (co == false && qm == false)
            {
                Debug.WriteLine("一般直述句");
                property = 0;
            }
            if (co == true && qm == false)
            {
                Debug.WriteLine("子句直述句");
                property = 1;
            }
            else if (co == false && qm == true)
            {
                Debug.WriteLine("一般問句");
                property = 2;
            }
            else if (co == true && qm == true)
            {
                Debug.WriteLine("子句問句");
                property = 3;
            }
            else if (em == true)
            {
                Debug.WriteLine("感嘆句");
                property = 4;
            }

            String[] tmp = text.Split(' ');
            for (int i = 0; i < tmp.Length; i++)
            {
                Boolean isFirst;
                Words word = new Words(tmp[i], curForm);
                word_stack.Add(word);
                //gr.KnowWordRoleInSent(word, word_stack);
                if (i == 0)
                    isFirst = true;
                else
                    isFirst = false;

                word.wordProperty(isFirst);
                //word.word_add_label();
                /*if (i == 0)
                {
                    switch (word.POS)
                    {
                        case 0:

                            break;
                        case 3:
                            start = 1;
                            Debug.WriteLine("start:" + start);
                            break;
                        case 8:
                            start = 0;
                            word.Element = "S";
                            Debug.WriteLine("start:" + start);
                            break;
                    }
                }
                else
                {
                    switch (word.POS)
                    {
                        case 0:

                            break;
                        case 3:
                            if(start == 1){
                                Debug.WriteLine("Eror: Two verb!");
                            }
                            break;
                        case 8:
                            if (start == 0)
                            {
                                Debug.WriteLine("Eror: Two subject!");
                            }
                            break;
                    }
                }*/
                //if (i == 1)
                    //word.IsCheck = false;
                
                
            }
            for (int i = 0; i < word_stack.Count; i++)
            {
                word_stack[i].word_add_label();
            }

        }

        public void wordAnalyze()
        {

        }

    }
}
