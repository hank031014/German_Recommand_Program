using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace german_recommend_program
{
    class SentenceAnalyzer : Object
    {
        private int mode;
        private String original_text;
        private List<Sentence> sentences_stack;
        private Form1 curForm;
        private SqlConnection db;

        public SentenceAnalyzer(Form1 cur, SqlConnection db)
        {
            mode = 0;
            curForm = cur;
            this.db = db;
            sentences_stack = new List<Sentence>();
        }

        public void modeChange(int nmode){
            mode = nmode;
        }

        public void textChange(String ntxt)
        {
            original_text = ntxt;
            String separator = @"(?<=[.!?])";        
            String[] tmp = Regex.Split(original_text, separator).Where(s => s != String.Empty).ToArray<String>();
            tmp = (from t in tmp  select t.Trim()).ToArray();
            sentences_stack.Clear();
            if (mode == 0)
            {
                for (int i = 0; i < tmp.Length; i++)
                {
                    //Debug.WriteLine("i_" + i + ": " + tmp[i]);
                    Sentence st = new Sentence(i, tmp[i], curForm, db);

                    sentences_stack.Insert(i, st);
                }

                for (int j = 0; j < sentences_stack.Count; j++)
                {
                    //Debug.WriteLine("j_" + j + ": " + sentences_stack[j].Text);
                    sentences_stack[j].sentenceProperty();
                }
            }
            if (mode == 1)
            {
                Sentence st = new Sentence(tmp.Length - 1, tmp[tmp.Length - 1], curForm, db);
                st.sentenceProperty(1);
            }
        }

        public void sentenceCheck()
        {

        }




    }
}
