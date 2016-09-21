using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

namespace german_recommend_program
{
    class SentenceAnalyzer : Object
    {
        private int mode;
        private String original_text;
        private List<Sentence> sentences_stack;
        private Form1 curForm;

        public SentenceAnalyzer(Form1 cur)
        {
            mode = 0;
            curForm = cur;
            sentences_stack = new List<Sentence>();
        }

        public void modeChange(int nmode){
            mode = nmode;
        }

        public void textChange(String ntxt)
        {
            original_text = ntxt;
            String separator = @"(?<=[.!?])";        
            String[] tmp = Regex.Split(original_text, separator);

            sentences_stack.Clear();

            for (int i = 0; i < tmp.Length; i++ )
            {
                Debug.WriteLine("i_" + i + ": " + tmp[i]);
                Sentence st = new Sentence(i, tmp[i], curForm);
                
                sentences_stack.Insert(i, st); 
            }

            for (int j = 0; j < sentences_stack.Count; j++)
            {
                Debug.WriteLine("j_" + j + ": " + sentences_stack[j].Text);
                sentences_stack[j].sentenceProperty();
            }
        }

        public void sentenceCheck()
        {

        }




    }
}
