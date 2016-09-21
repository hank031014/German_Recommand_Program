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

        public Sentence(int norder, String ntext, Form1 cur)
        {
            order = norder;
            text = ntext;
            curForm = cur;
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
                Words word = new Words(tmp[i], curForm);
                word.wordProperty();
                word_stack.Add(word);
                
            }

        }

        public void wordAnalyze()
        {

        }

    }
}
