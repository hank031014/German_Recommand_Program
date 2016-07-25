using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class SentenceAnalyzer : Object
    {
        private int mode;
        private String original_text;
        private List<Sentence> sentences_stack;
        

        public SentenceAnalyzer()
        {
            mode = 0;
            sentences_stack = new List<Sentence>();
        }

        public void modeChange(int nmode){
            mode = nmode;
        }

        public void textChange(String ntxt)
        {
            original_text = ntxt;
            Char[] seperator = {'.','!','?'};
            String[] tmp = original_text.Split(seperator);
            Boolean isExist;

            for (int i = 0; i < tmp.Length; i++ )
            {
                tmp[i] += " .";
                isExist = sentences_stack.Contains(new Sentence(i, original_text));
                if (!isExist)
                {
                    sentences_stack.Add(new Sentence(i, original_text));
                }
            }
        }
    }
}
