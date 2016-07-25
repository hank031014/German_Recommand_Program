using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class Sentence : Object
    {
        private int order;
        private String text;

        public Sentence(int norder, String ntext)
        {
            order = norder;
            text = ntext;
        }
    }
}
