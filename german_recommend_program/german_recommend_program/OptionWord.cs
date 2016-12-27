using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace german_recommend_program
{
    class OptionWord : Object
    {
        public String originWord { get; set; }
        public String showWord { get; set; }
        public int type { get; set; }
        public int id { get; set; }

        public override string ToString()
        {
            return showWord;
        }
    }
}
