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
        enum MainRole { M, S, UZ, D };
        enum Role { M_C, M_F, M_S, M_V1, M_V2, M_O1, M_O2, M_P, // main sentence
                    S_C, S_F, S_S, S_V1, S_V2, S_O1, S_02, S_P, // sub sentence
                    UZ_U, UZ_O1, UZ_O2, UZ_P, UZ_Z, UZ_V, // Um...zu or Damit sentence
                    D_D, D_S, D_V1, D_V2, D_O1, D_O2, D_P }; // dass sentence

        private String[] no_init_query = { "der", "die", "das", "des", "dem", "den", "ein", "eine", "eines", "einem", "einen", "einer", "kein", "keine", "keines", "keinem", "keinen", "keiner" };
        private String[] pos1_verb = { "haben", "sein", "werden", "können", "sollen", "wollen", "dürfen", "möchten", "müssen", "mögen" };
        private String[] part = { "wiederan", "wiederauf", "wiederaus", "wiederein", "wiederzu", "ineinander",
                                "gegenüber", "spazieren", "stehen", "bekannt", "zusammen", "herunter", "herbei", "herab", "heraus", 
                                "heran", "herein", "hervor", "herum", "hinauf", "hinaus", "hinein", "hinterher", "hinüber", "hinuter",
                                "hinzu", "fernüber", "zurück", "nieder", "offen", "sicher", "zuvor", "gleich", "davon", "dazu", 
                                "durch", "gegen", "empor", "weiter", "hinzu", "wieder", "rüber", "still", "voran", "voraus", "vorbei",
                                "vorein", "vorher", "vorüber", "kennen", "warm", "hoch", "fort", "voll", "kopf", "teil", "über", 
                                "fern", "fest", "frei", "mich", "nach", "rück", "weg", "dar", "hin", "mit", "los", "ein", "aus",
                                "auf", "vor", "bei", "zu", "an", "ab", "um" };

        private String[,] d_art = { { "der", "des", "dem", "den" }, 
                                    { "die", "der", "der", "die" },
                                    { "das", "des", "dem", "das" },
                                    { "die", "der", "den", "die" } };
        private String[,] ek_art = { { "", "es", "em", "en" }, 
                                     { "e", "er", "er", "e" },
                                     { "", "es", "em", "" },
                                     { "e", "er", "en", "e" } };
        private String[] pron_gen = { "mein", "dein", "ihr", "sein", "unser", "unsr", "euer", "eur" };

        private String[,] d_adj_end = { { "e", "en", "en", "en" }, 
                                        { "e", "en", "en", "e" },
                                        { "e", "en", "en", "e" },
                                        { "en", "en", "en", "en" } };
        private String[,] ek_adj_end = { { "er", "en", "en", "en" }, 
                                         { "e", "en", "en", "e" },
                                         { "es", "en", "en", "es" },
                                         { "e", "er", "en", "e" } };
        private String[,] n_adj_end = { { "er", "en", "em", "en" }, 
                                        { "e", "er", "er", "e" },
                                        { "es", "en", "em", "es" },
                                        { "e", "er", "en", "e" } };
        private String[] sich_akk = { "mich", "uns", "dich", "euch", "sich", "sich", "sich" };

        private List<Words> stack, setz;
        private Hashtable st_element;
        
        private int state, mainState;

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
            setz[0].IsFinished = true;
            switch (setz[0].POS)
            {
                case 1:
                    state = (int)Role.M_S;
                    mainState = (int)MainRole.M;
                    artNounCheck(0);
                    break;
                case 2:
                    state = (int)Role.M_S;
                    mainState = (int)MainRole.M;
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
                    mainState = (int)MainRole.M;
                    vPosOneCheck(0);
                    st_element.Add("M_V1", 0);
                    break;
                case 4:
                    state = (int)Role.M_S;
                    mainState = (int)MainRole.M;

                    int next = 1;
                    while (next < setz.Count)
                    {
                        if (setz[next].POS == 4)
                        {
                            next++;
                        }
                        else if (setz[next].POS == 3)
                        {
                            break;
                        }
                        else if (setz[next].POS == 2)
                        {
                            setz[next].IsFinished = true;
                            adjBeforeNounCheck(3, setz[next].Gender, 4, -1, next);
                            break;
                        }
                        else
                        {
                            setz[next].IsCheck = false;
                            setz[next].Error_msg = "形容詞之後出現錯誤用法";
                            break;
                        }
                    }

                    break;
                case 5:
                    if (setz[0].Conj_type == 1)
                    {
                        state = (int)Role.M_C;
                        mainState = (int)MainRole.M;
                        st_element.Add("M_conj", 0);
                    }
                    if (setz[0].Conj_type == 2)
                    {
                        state = (int)Role.S_C;
                        mainState = (int)MainRole.S;
                        st_element.Add("S_conj", 0);
                    }    
                    break;
                case 6:
                    state = (int)Role.M_F;
                    prepHandle(setz[0].Prep_type, 0);
                    break;
                case 7:
                    state = (int)Role.M_F;
                    break;
                case 8:
                    if (setz[0].N_case == 1)
                    {
                        state = (int)Role.M_S;
                        mainState = (int)MainRole.M;
                        if(setz[0].Pron_type == 0){
                            st_element.Add("M_subject", 1);
                        }
                        else{
                            st_element.Add("M_subject", setz[0].Pron_type);
                        }
                        
                    }
                    if (setz[0].N_case == 2)
                    {
                        state = (int)Role.M_S;
                        mainState = (int)MainRole.M;
                    }
                    else
                    {
                        state = (int)Role.M_F;
                        mainState = (int)MainRole.M;
                    }
                    break;
                default:
                    setz[0].IsCheck = false;
                    break;
            }
        }

        private void stBody(int cur)
        {
            if (setz[cur].IsFinished)
            {
                return;
            }
            else
            {
                setz[cur].IsFinished = true;
            }
            switch (setz[cur].POS)
            {
                case 1:
                    if (setz[cur].N_case == 2)
                    {
                        if (setz[cur - 1].POS != 2)
                        {
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "冠詞使用方式有誤";
                        }
                    }
                    if (setz[cur - 1].POS == 1)
                    {
                        if (setz[cur].N_case == setz[cur - 1].N_case)
                        {
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "冠詞使用方式有誤";
                        }
                    }
                    else
                    {
                        artNounCheck(cur);
                    }
                    break;
                case 2:
                    //state = (int)Role.M_S;
                    
                    break;
                case 3:
                    if (mainState.Equals((int)MainRole.M))
                    {
                        state = (int)Role.M_V1;
                        if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_V1"))
                        {
                            vPosOneCheck(cur);
                            st_element.Add("M_V1", cur);
                        }
                    }
                    if (mainState.Equals((int)MainRole.M))
                    {
                        if (!st_element.ContainsKey("M_subject") && setz[cur -1].POS == 1 && setz[cur - 1].N_case == 1)
                        {
                            if (setz[cur - 1].Pron_type == 5)
                            {
                                if (setz[cur].POS_dt.EndsWith("_s3") || setz[cur].POS_dt.EndsWith("_s"))
                                {
                                    state = (int)Role.M_V1;
                                    if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_V1"))
                                    {
                                        vPosOneCheck(cur);
                                        st_element.Add("M_V1", cur);
                                    }
                                }
                                else
                                {
                                    setz[cur].IsCheck = false;
                                    Debug.WriteLine("W1");
                                    setz[cur].Error_msg = "動詞格位變化有誤";
                                }
                            }
                            if (setz[cur - 1].Pron_type == 6)
                            {
                                if (setz[cur].POS_dt.EndsWith("_pl"))
                                {
                                    state = (int)Role.M_V1;
                                    if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_V1"))
                                    {
                                        vPosOneCheck(cur);
                                        st_element.Add("M_V1", cur);
                                    }
                                }
                                else
                                {
                                    setz[cur].IsCheck = false;
                                    Debug.WriteLine("W2");
                                    setz[cur].Error_msg = "動詞格位變化有誤";
                                }
                            }
                        }
                    }
                    if (pos1_verb.Any(verb => verb == setz[cur].Ori_word))
                    {
                        int next = cur + 1;
                        while(next < setz.Count){
                            if (setz[next].Marks == String.Empty)
                            {
                                if (setz[next].POS == 3)
                                {
                                    setz[next].IsCheck = false;
                                    setz[next].IsFinished = true;
                                    setz[cur].Error_msg = "動詞擺放位置有誤";
                                }
                                next++;
                            }
                            else
                            {
                                if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_V2") && setz[next].Marks != String.Empty && setz[next].POS == 3)
                                {
                                    st_element.Add("M_V2", next);
                                    Debug.WriteLine("M_V2");
                                    
                                }
                                break;
                            }
                        }
                    }
                    if (mainState == (int)MainRole.M)
                    {
                        //Debug.WriteLine("Contains MV1?: {0}", st_element.ContainsKey("M_V1"));
                        if (st_element.ContainsKey("M_V2") && st_element.ContainsKey("M_V1"))
                        {
                            Debug.WriteLine("MV1");
                            int v2pos = (int)st_element["M_V2"];
                            int v1pos = (int)st_element["M_V1"];
                            if (setz[v2pos].Verb_sich == 1)
                            {
                                int sich_pos = v1pos + 1;
                                if (setz[sich_pos].POS == 8)
                                {
                                    if (st_element.ContainsKey("M_subject"))
                                    {
                                        int stype = (int)st_element["M_subject"];
                                        if (setz[sich_pos].Text != sich_akk[stype - 1])
                                        {
                                            setz[sich_pos].IsCheck = false;
                                            setz[sich_pos].IsFinished = true;
                                            setz[sich_pos].Error_msg = "此處要使用反身代名詞，請參考主詞";
                                        }
                                    }
                                }
                                else{
                                    setz[sich_pos].IsCheck = false;
                                    setz[sich_pos].IsFinished = true;
                                    setz[sich_pos].Error_msg = "此處要使用反身代名詞";
                                }
                            }
                        }
                        else if (!st_element.ContainsKey("M_V2") && st_element.ContainsKey("M_V1"))
                        { 
                            int v1pos = (int)st_element["M_V1"];

                            //Debug.WriteLine("give?: {0}", setz[v1pos].Verb_give);
                            if (setz[v1pos].Text == "sein")
                            {
                                int next = v1pos + 1;
                                if (next >= setz.Count)
                                {
                                    setz[v1pos].IsCheck = false;
                                    setz[v1pos].Error_msg = "sein動詞之後應該出現代名詞、形容詞、名詞、介詞";
                                }
                                else
                                {
                                    if (setz[next].POS == 8 && setz[next].N_case == 3)
                                    {
                                        setz[next].IsFinished = true;
                                        if (next + 1 >= setz.Count)
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "受詞之後缺少形容詞";
                                        }
                                        else if (setz[next + 1].POS == 4)
                                        {
                                            setz[next + 1].IsFinished = true;
                                        }
                                        else if (setz[next + 1].POS == 5 && setz[next + 2].POS == 4)
                                        {
                                            setz[next + 1].IsFinished = true;
                                            setz[next + 2].IsFinished = true;
                                        }
                                        else
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "受詞之後缺少形容詞";
                                        }
                                    }
                                    else if (setz[next].POS == 1)
                                    {
                                        int resp = artNounCheck(next);
                                        if (resp == 1 || resp == 5)
                                        {
                                            setz[next].IsFinished = true;
                                        }
                                        else if (resp == 3)
                                        {
                                            int pos = next;
                                            while (pos < setz.Count)
                                            {
                                                if (setz[pos].POS != 2)
                                                {
                                                    pos++;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }

                                            if (next + 1 >= setz.Count)
                                            {
                                                setz[v1pos].IsCheck = false;
                                                setz[v1pos].Error_msg = "受詞之後缺少形容詞";
                                            } 
                                            else if (setz[pos + 1].POS == 4)
                                            {
                                                setz[pos + 1].IsFinished = true;
                                            }
                                            else if (setz[pos + 1].POS == 5 && setz[pos + 2].POS == 4)
                                            {
                                                setz[pos + 1].IsFinished = true;
                                                setz[pos + 2].IsFinished = true;
                                            }
                                            else
                                            {
                                                setz[v1pos].IsCheck = false;
                                                setz[v1pos].Error_msg = "受詞之後缺少形容詞";
                                            }
                                        }
                                        else
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "受詞格位有誤";
                                        }
                                    }
                                    else if (setz[next].POS == 6)
                                    {

                                    }
                                    else if (setz[next].POS == 4)
                                    {
                                        setz[next].IsFinished = true;
                                    }
                                    else if (setz[next].POS == 5 && setz[next + 1].POS == 4)
                                    {
                                        setz[next].IsFinished = true;
                                        setz[next + 1].IsFinished = true;
                                    }
                                    else
                                    {
                                        setz[next].IsFinished = true;
                                        setz[next].IsCheck = false;
                                        setz[next].Error_msg = "sein動詞之後應該出現代名詞、形容詞、名詞、介詞等";
                                    }
                                }
                                
                            }
                            else if (setz[v1pos].Verb_sich == 1)
                            {
                                int sich_pos = v1pos + 1;
                                if (sich_pos >= setz.Count)
                                {
                                    setz[v1pos].IsCheck = false;
                                    setz[v1pos].Error_msg = "缺少反身代名詞";
                                }
                                else if (setz[sich_pos].POS == 8)
                                {
                                    if (st_element.ContainsKey("M_subject"))
                                    {
                                        int stype = (int)st_element["M_subject"];
                                        Debug.WriteLine("stype: {0}", stype);
                                        if (setz[sich_pos].Text != sich_akk[stype - 1])
                                        {
                                            setz[sich_pos].IsCheck = false;
                                            setz[sich_pos].IsFinished = true;
                                            setz[sich_pos].Error_msg = "此處要使用反身代名詞，請參考主詞";
                                        }
                                    }
                                }
                                else
                                {
                                    setz[sich_pos].IsCheck = false;
                                    setz[sich_pos].IsFinished = true;
                                    setz[sich_pos].Error_msg = "此處要使用反身代名詞";
                                }

                                if (setz[v1pos].Verb_vt == 2)
                                {
                                    int next = v1pos + 2;
                                    if (next >= setz.Count)
                                    {
                                        setz[v1pos].IsCheck = false;
                                        setz[v1pos].Error_msg = "缺少受詞";
                                    }
                                    else if (setz[next].POS == 8 && setz[next].N_case == 4)
                                    {
                                        setz[next].IsFinished = true;
                                        
                                    }
                                    else if (setz[next].POS == 1)
                                    {
                                        int resp = artNounCheck(next, 4);
                                        if (resp == 5)
                                        {
                                            setz[next].IsFinished = true;
                                        }
                                        else if (resp == 4)
                                        {
                                            
                                        }
                                        else
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "動詞之後沒有受詞";
                                        }
                                    }
                                }
                                else if (setz[v1pos].Verb_vt == 3)
                                {
                                    int next = v1pos + 2;
                                    if (next >= setz.Count)
                                    {
                                        setz[v1pos].IsCheck = false;
                                        setz[v1pos].Error_msg = "缺少與格受詞";
                                    }
                                    else if (setz[next].POS == 8 && setz[next].N_case == 3)
                                    {
                                        setz[next].IsFinished = true;

                                    }
                                    else if (setz[next].POS == 1)
                                    {
                                        int resp = artNounCheck(next, 3);
                                        if (resp != 3)
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "動詞之後沒有與格受詞";
                                        }
                                    }
                                }
                                else if (setz[v1pos].Verb_vt == 1)
                                {
                                    int next = v1pos + 2;
                                    if (next < setz.Count)
                                    {
                                        if (setz[next].POS == 1 || setz[next].POS == 2)
                                        {
                                            setz[next].IsCheck = false;
                                            setz[next].Error_msg = "不及物動詞之後不能有受詞";
                                        }
                                    }
                                    
                                }
                            }
                            else if (setz[v1pos].Verb_give == 1)
                            {
                                Debug.WriteLine("give");
                                int next = v1pos + 1;
                                if (next >= setz.Count)
                                {
                                    setz[v1pos].IsCheck = false;
                                    setz[v1pos].Error_msg = "動詞之後沒有受詞";
                                }
                                else
                                {
                                    Debug.WriteLine("give");
                                    if (setz[next].POS == 8 && setz[next].N_case == 3)
                                    {
                                        Debug.WriteLine("give");
                                        setz[next].IsFinished = true;
                                        int next2 = next + 1;
                                        if (next2 < setz.Count)
                                        {
                                            if (setz[next2].POS == 8 && setz[next2].N_case == 4)
                                            {
                                                setz[next2].IsCheck = false;
                                                setz[next2].Error_msg = "若授予動詞後要使用兩個代名詞，賓格代名詞要至於前";
                                            }
                                            else if (setz[next2].POS == 8 && setz[next2].N_case == 3)
                                            {
                                                setz[next2].IsCheck = false;
                                                setz[next2].Error_msg = "此受詞應為與格";
                                            }
                                            else if(setz[next2].POS == 1)
                                            {
                                                Debug.WriteLine("give");
                                                int resp = artNounCheck(next2, 4);
                                                if (resp == 5)
                                                {
                                                    setz[next2].IsFinished = true;
                                                }
                                                else if (resp == 4 || resp == 1)
                                                {

                                                }
                                                else
                                                {
                                                    setz[next2].IsCheck = false;
                                                    setz[next2].Error_msg = "此處應有第二個受詞";
                                                }
                                            }
                                        }
                                    }
                                    else if (setz[next].POS == 8 && setz[next].N_case == 4)
                                    {
                                        setz[next].IsFinished = true;
                                        int next2 = next + 1;
                                        if (next2 < setz.Count)
                                        {
                                            if (setz[next2].POS == 8 && setz[next2].N_case == 4)
                                            {
                                                setz[next2].IsCheck = false;
                                                setz[next2].Error_msg = "此受詞應為與格";
                                            }
                                            else if (setz[next2].POS == 1)
                                            {
                                                int resp = artNounCheck(next2, 3);
                                                if (resp != 3){
                                                    setz[next2].IsCheck = false;
                                                    setz[next2].Error_msg = "此處應有第一個受詞";
                                                }
                                            }
                                        }
                                    }
                                    else if (setz[next].POS == 1)
                                    {
                                        int resp = artNounCheck(next, 3);
                                        if (resp != 3)
                                        {
                                            setz[next].IsCheck = false;
                                            setz[next].Error_msg = "此處應有第一個受詞";

                                        }
                                        else
                                        {
                                            int next2 = next;
                                            while (next2 < setz.Count)
                                            {
                                                if (setz[next].POS != 2)
                                                {
                                                    next2++;
                                                }
                                                else
                                                {
                                                    next2++;
                                                    break;
                                                }
                                            }
                                            if (next2 < setz.Count)
                                            {
                                                int resp2 = artNounCheck(next2, 4);
                                                if (resp2 != 4)
                                                {
                                                    setz[next2].IsCheck = false;
                                                    setz[next2].Error_msg = "此處應有第二個受詞";

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (setz[v1pos].Verb_vt == 2)
                            {
                                int next = v1pos + 1;
                                if (next >= setz.Count)
                                {
                                    setz[v1pos].IsCheck = false;
                                    setz[v1pos].Error_msg = "動詞之後沒有受詞";
                                }
                                else
                                {
                                    if (setz[next].POS == 8 && setz[next].N_case == 4)
                                    {
                                        setz[next].IsFinished = true;

                                    }
                                    else if (setz[next].POS == 1)
                                    {
                                        int resp = artNounCheck(next, 4);
                                        if (resp == 5)
                                        {
                                            setz[next].IsFinished = true;
                                        }
                                        else if (resp == 4 || resp == 1)
                                        {

                                        }
                                        else
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "動詞之後沒有受詞";
                                        }
                                    }
                                }
                                
                            }
                            else if (setz[v1pos].Verb_vt == 3)
                            {
                                int next = v1pos + 1;
                                if (next >= setz.Count)
                                {
                                    setz[v1pos].IsCheck = false;
                                    setz[v1pos].Error_msg = "動詞之後沒有受詞";
                                }
                                else
                                {
                                    if (setz[next].POS == 8 && setz[next].N_case == 3)
                                    {
                                        setz[next].IsFinished = true;

                                    }
                                    else if (setz[next].POS == 1)
                                    {
                                        int resp = artNounCheck(next, 3);
                                        if (resp != 3)
                                        {
                                            setz[v1pos].IsCheck = false;
                                            setz[v1pos].Error_msg = "動詞之後沒有與格受詞";
                                        }
                                    }
                                }
                                
                            }
                            else if (setz[v1pos].Verb_vt == 1)
                            {
                                int next = v1pos + 1;
                                if (next < setz.Count)
                                {
                                    if (setz[next].POS == 1 || setz[next].POS == 2)
                                    {
                                        setz[next].IsCheck = false;
                                        setz[next].Error_msg = "不及物動詞之後不能有受詞";
                                    }
                                }
                                
                            }
                        }
                    }
                    break;
                case 4:
                    //state = (int)Role.M_S;
                    int next3 = cur + 1;
                    while (next3 < setz.Count)
                    {
                        if (setz[next3].POS == 4)
                        {
                            next3++;
                        }
                        else if (setz[next3].POS == 3)
                        {
                            break;
                        }
                        else if (setz[next3].POS == 2)
                        {
                            setz[next3].IsFinished = true;
                            adjBeforeNounCheck(3, setz[next3].Gender, 4, cur - 1, next3);
                            break;
                        }
                        else
                        {
                            setz[next3].IsCheck = false;
                            setz[next3].Error_msg = "形容詞之後出現錯誤用法";
                            break;
                        }
                    }
                    break;
                case 5:
                    state = (int)Role.M_C;
                    break;
                case 6:
                    state = (int)Role.M_F;
                    prepHandle(setz[cur].Prep_type, cur);
                    break;
                case 7:
                    state = (int)Role.M_F;
                    break;
                case 8:
                    
                    if (state.Equals((int)Role.M_S) || state.Equals((int)Role.S_S) || state.Equals((int)Role.D_S))
                    {
                        if(setz[(cur-1 <= 0 ? 0 : cur-1)].Text != "und" || setz[cur-1 <= 0 ? 0 : cur-1].Text != "oder"){
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "代名詞擺放位置有誤";
                        }
                        else if(setz[cur].N_case == 2){
                            setz[cur].IsCheck = true;
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
                        if (!st_element.ContainsKey("M_subject"))
                        {
                            switch ((int)setz[cur].Pron_type)
                            {

                                case 1:
                                    st_element.Add("M_subject", 1);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_sm"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                case 2:
                                    st_element.Add("M_subject", 2);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                case 3:
                                    st_element.Add("M_subject", 3);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_y2"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                case 4:
                                    st_element.Add("M_subject", 4);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_y2"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                case 5:
                                    st_element.Add("M_subject", 5);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_s3"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                case 6:
                                    st_element.Add("M_subject", 6);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                case 7:
                                    st_element.Add("M_subject", 7);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                        setz[cur].Error_msg = "動詞格位變化有誤";
                                    }
                                    break;
                                default:
                                    Debug.WriteLine("!!!");
                                    break;
                            }
                        }
                        //Debug.WriteLine("pt: " + setz[cur].Pron_type);
                        else
                        {
                            if (setz[cur].N_case == 2 || pron_gen.Any(p => setz[cur].Text.ToLower().StartsWith(p)))
                            {
                                Debug.WriteLine("go");
                                artNounCheck(cur);
                            }
                            if (mainState.Equals((int)MainRole.M))
                            {
                                if (st_element.ContainsKey("M_V2"))
                                {

                                }
                                else if (!st_element.ContainsKey("M_V2") && st_element.ContainsKey("M_V1"))
                                {

                                }
                            }
                        }
                    }
                    if (setz[cur].N_case == 2 || pron_gen.Any(p => setz[cur].Text.ToLower().StartsWith(p)))
                    {
                        Debug.WriteLine("go");
                        artNounCheck(cur);
                    }
                    break;
                default:
                    setz[cur].IsCheck = false;
                    setz[cur].Error_msg = "拼字應該有誤";
                    break;
            }
        }

        private void vPosOneCheck(int cur)
        {
            int next = cur;
            String pt = String.Empty;
            while (next < setz.Count)
            {
                //Debug.WriteLine("YEE");
                if (setz[next].Marks.Equals(String.Empty))
                {
                    next++;
                }
                else
                {
                    if (part.Any(p => p.Equals(setz[next].Text)))
                    {
                        pt = setz[next].Text;
                        Boolean isPart = setz[cur].partVerbSearch(pt);
                        if (isPart)
                        {
                            setz[next].setVerbPart(setz[cur].ID, setz[cur].English, setz[cur].Ori_word, setz[cur].POS, setz[cur].POS_dt, setz[cur].N_case);
                            setz[next].IsFinished = true;
                        }
                        else
                        {
                            setz[next].IsCheck = false;
                            setz[next].IsFinished = true;
                            setz[cur].Error_msg = "非可分離動詞的開頭字根";
                        }
                    }
                    break;
                }
            }
        }

        private int artNounCheck(int Cur, int Ncase = 1)
        {
            int cur = Cur;
            int ncase = Ncase;
            int next = cur + 1;
            int ret = 0;

            if (setz[cur].POS == 1 && Ncase != 2 && setz[cur].Text.EndsWith("es"))
            {
                setz[cur].N_case = 2;
            }
            while (next < setz.Count)
            {
                if (setz[next].POS != 2 || setz[next].POS != 4)
                {
                    if (setz[next].POS == 3)
                    {
                        String txt = setz[cur].Text.ToLower();
                        int gender = 1;
                        switch(txt){
                            case "der":
                                gender = 1;
                                ret = 1;
                                break;
                            case "die":
                                gender = 2;
                                ret = 5;
                                break;
                            case "das":
                                gender = 3;
                                ret = 5;
                                break;
                            case "dem":
                                gender = 1;
                                ret = 3;
                                break;
                            case "den":
                                gender = 1;
                                ret = 4;
                                break;
                        }
                        addNounRole(1, gender, cur);
                        break;
                    }
                    
                }
                if (setz[next].POS == 2)
                {
                    if (ncase != 1)
                    {
                        setz[next].N_case = ncase;
                    }

                    int nc = setz[next].N_case;
                    int gender = setz[next].Gender;
                    String art = setz[cur].Text.ToLower();
                    
                    if (art.StartsWith("d"))
                    {
                        //Debug.WriteLine(nc + ", " + gender);
                        if (nc == 5)
                        {
                            if (art.Equals(d_art[gender - 1, 2]))
                            {
                                setz[next].N_case = 3;
                                setz[cur].N_case = 3;
                                setz[cur].Gender = gender;
                                addNounRole(3, gender, next);
                                adjBeforeNounCheck(1, gender, 3, cur, next);
                                ret = 3;
                            }
                            else if (art.Equals(d_art[gender - 1, 3]))
                            {
                                setz[next].N_case = 4;
                                setz[cur].N_case = 4;
                                setz[cur].Gender = gender;
                                addNounRole(4, gender, next);
                                adjBeforeNounCheck(1, gender, 4, cur, next);
                                ret = 4;
                            }
                            else
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "名詞使用方式有誤";
                            }
                        }
                        else if(art.Equals(d_art[gender - 1, nc - 1])){
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                            adjBeforeNounCheck(1, gender, nc, cur, next);
                            ret = nc;
                        }
                        else if (art.Equals(d_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(1, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                            
                        }
                        else if (art.Equals(d_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(1, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else if (art.Equals(d_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(1, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else if (art.Equals(d_art[3, 1]) && setz[next].Gender == 4 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(1, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "冠詞使用方式有誤";
                        }
                    }

                    if (art.StartsWith("ein"))
                    {
                        if (nc == 5)
                        {
                            if (art.Equals("ein" + ek_art[gender - 1, 2]))
                            {
                                setz[next].N_case = 3;
                                setz[cur].N_case = 3;
                                setz[cur].Gender = gender;
                                addNounRole(3, gender, next);
                                adjBeforeNounCheck(1, gender, 3, cur, next);
                                ret = 3;
                            }
                            else if (art.Equals("ein" + ek_art[gender - 1, 3]))
                            {
                                setz[next].N_case = 4;
                                setz[cur].N_case = 4;
                                setz[cur].Gender = gender;
                                addNounRole(4, gender, next);
                                adjBeforeNounCheck(1, gender, 4, cur, next);
                                ret = 4;
                            }
                            else
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "名詞使用方式有誤";
                            }
                        }
                        else if (art.Equals("ein" + ek_art[gender - 1, nc - 1]))
                        {
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                            adjBeforeNounCheck(2, gender, nc, cur, next);
                            ret = nc;
                        }
                        else if (art.Equals("ein" + ek_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }

                        }
                        else if (art.Equals("ein" + ek_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else if (art.Equals("ein" + ek_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "冠詞使用方式有誤";
                        }
                    }
                    if (art.StartsWith("kein"))
                    {
                        if (nc == 5)
                        {
                            if (art.Equals("kein" + ek_art[gender - 1, 2]))
                            {
                                setz[next].N_case = 3;
                                setz[cur].N_case = 3;
                                setz[cur].Gender = gender;
                                addNounRole(3, gender, next);
                                adjBeforeNounCheck(1, gender, 3, cur, next);
                                ret = 3;
                            }
                            else if (art.Equals("kein" + ek_art[gender - 1, 3]))
                            {
                                setz[next].N_case = 4;
                                setz[cur].N_case = 4;
                                setz[cur].Gender = gender;
                                addNounRole(4, gender, next);
                                adjBeforeNounCheck(1, gender, 4, cur, next);
                                ret = 4;
                            }
                            else
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "名詞使用方式有誤";
                            }
                        }
                        else if (art.Equals("kein" + ek_art[gender - 1, nc - 1]))
                        {
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                            adjBeforeNounCheck(2, gender, nc, cur, next);
                            ret = nc;
                        }
                        else if (art.Equals("kein" + ek_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else if (art.Equals("kein" + ek_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else if (art.Equals("kein" + ek_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else if (art.Equals("kein" + ek_art[3, 1]) && setz[next].Gender == 4 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格冠詞使用方式有誤";
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "冠詞使用方式有誤";
                        }
                    }

                    if (pron_gen.Any(g => art.StartsWith(g)))
                    {
                        String match = Array.Find(pron_gen, g => art.StartsWith(g));
                        Debug.WriteLine(match + ek_art[gender - 1, nc - 1]);
                        if (nc == 5)
                        {
                            if (art.Equals(match + ek_art[gender - 1, 2]))
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 3;
                                setz[cur].N_case = 3;
                                setz[cur].Gender = gender;
                                addNounRole(3, gender, next);
                                adjBeforeNounCheck(1, gender, 3, cur, next);
                                ret = 3;
                            }
                            else if (art.Equals(match + ek_art[gender - 1, 3]))
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 4;
                                setz[cur].N_case = 4;
                                setz[cur].Gender = gender;
                                addNounRole(4, gender, next);
                                adjBeforeNounCheck(1, gender, 4, cur, next);
                                ret = 4;
                            }
                            else
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "名詞使用方式有誤";
                            }
                        }
                        else if (art.Equals(match + ek_art[gender - 1, nc - 1]))
                        {
                            setz[cur].IsCheck = true;
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                            adjBeforeNounCheck(2, gender, nc, cur, next);
                            ret = nc;
                        }
                        else if (art.Equals(match + ek_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格代名詞使用方式有誤";
                            }
                        }
                        else if (art.Equals(match + ek_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格代名詞使用方式有誤";
                            }
                        }
                        else if (art.Equals(match + ek_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格代名詞使用方式有誤";
                            }
                        }
                        else if (art.Equals(match + ek_art[3, 1]) && setz[next].Gender == 4 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                adjBeforeNounCheck(2, gender, 2, cur, next);
                                ret = 2;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                setz[cur].Error_msg = "所有格代名詞使用方式有誤";
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                            setz[cur].Error_msg = "所有格代名詞使用方式有誤";
                        }
                    }

                    break;
                }
                
                next++;
            }
            return ret;
        }

        private void adjBeforeNounCheck(int art, int gender, int nc, int cur, int next)
        {
            int tcur = cur + 1;
            switch (art)
            {
                case 1: // 定冠詞
                    while (tcur < next)
                    {
                        if (setz[tcur].POS != 4)
                        {
                            setz[tcur].IsCheck = false;
                            setz[tcur].Error_msg = "應該放置形容詞";
                        }
                        else
                        {
                            if (!setz[tcur].Text.EndsWith(d_adj_end[gender - 1, nc - 1]))
                            {
                                setz[tcur].IsCheck = false;
                                Debug.WriteLine("END: " + d_adj_end[gender - 1, nc - 1]);
                                setz[tcur].Error_msg = "形容詞變格有誤";
                            }
                        }
                        setz[tcur].IsFinished = true;
                        tcur++;
                    }
                    break;
                case 2: // 不定冠詞、否定冠詞、所有格代名詞
                    while (tcur < next)
                    {
                        if (setz[tcur].POS != 4)
                        {
                            setz[tcur].IsCheck = false;
                            setz[tcur].Error_msg = "應該放置形容詞";
                        }
                        else
                        {
                            if (!setz[tcur].Text.EndsWith(ek_adj_end[gender - 1, nc - 1]))
                            {
                                setz[tcur].IsCheck = false;
                                setz[tcur].Error_msg = "形容詞變格有誤";
                            }
                        }
                        setz[tcur].IsFinished = true;
                        tcur++;
                    }
                    break;
                case 3: // 無冠詞
                    while (tcur < next)
                    {
                        if (setz[tcur].POS != 4)
                        {
                            setz[tcur].IsCheck = false;
                            setz[tcur].Error_msg = "應該放置形容詞";
                        }
                        else
                        {
                            if (!setz[tcur].Text.EndsWith(n_adj_end[gender - 1, nc - 1]))
                            {
                                setz[tcur].IsCheck = false;
                                setz[tcur].Error_msg = "形容詞變格有誤";
                            }
                        }
                        setz[tcur].IsFinished = true;
                        tcur++;
                    }
                    break;
            }
            
        }

        private void addNounRole(int nc, int gender, int next)
        {
            switch (nc)
            {
                case 1:
                    if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_subject"))
                    {
                        st_element.Add("M_subject", (gender == 4 ? 6 : 5));
                    }
                    else if (mainState == (int)MainRole.S && !st_element.ContainsKey("S_subject"))
                    {
                        st_element.Add("S_subject", (gender == 4 ? 6 : 5));
                    }
                    break;
                case 3:
                    if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_O2"))
                    {
                        st_element.Add("M_O2", next);
                    }
                    else if (mainState == (int)MainRole.S && !st_element.ContainsKey("S_O2"))
                    {
                        st_element.Add("S_O2", next);
                    }
                    else if (mainState == (int)MainRole.UZ && !st_element.ContainsKey("UZ_O2"))
                    {
                        st_element.Add("UZ_O2", next);
                    }
                    break;
                case 4:
                    if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_O1"))
                    {
                        st_element.Add("M_O1", next);
                    }
                    else if (mainState == (int)MainRole.S && !st_element.ContainsKey("S_O1"))
                    {
                        st_element.Add("S_O1", next);
                    }
                    else if (mainState == (int)MainRole.UZ && !st_element.ContainsKey("UZ_O1"))
                    {
                        st_element.Add("UZ_O1", next);
                    }
                    break;
            }
        }

        private void prepHandle(int ptype, int cur){
            int next = cur + 1;
            if (setz[next].POS == 1) //art
            {
                setz[next].IsFinished = true;
                switch (ptype)
                {
                    case 1:
                        artNounCheck(next, 4);
                        break;
                    case 2:
                        artNounCheck(next, 3);
                        break;
                    case 3:
                        artNounCheck(next, 5);
                        break;
                    case 4:
                    case 5:
                        artNounCheck(next, 2);
                        break;
                }            
            }
            else if (setz[next].POS == 2) //noun
            {
                setz[next].IsFinished = true;
            }
            else if (setz[next].POS == 4) //adj
            {
                int nnxt = next + 1;
                while (nnxt < setz.Count)
                {
                    if (setz[nnxt].POS == 4)
                    {
                        nnxt++;
                    }
                    else if (setz[nnxt].POS == 2)
                    {
                        setz[next].IsFinished = true;
                        switch (ptype)
                        {
                            case 1:
                                adjBeforeNounCheck(3, setz[nnxt].Gender, 4, next - 1, nnxt);
                                break;
                            case 2:
                                adjBeforeNounCheck(3, setz[nnxt].Gender, 3, next - 1, nnxt);
                                break;
                            case 3:
                                adjBeforeNounCheck(3, setz[nnxt].Gender, 5, next - 1, nnxt);
                                break;
                            case 4:
                            case 5:
                                adjBeforeNounCheck(3, setz[nnxt].Gender, 2, next - 1, nnxt);
                                break;
                        }
                        break;
                    }
                    else
                    {
                        setz[next].IsCheck = false;
                        setz[nnxt].Error_msg = "介詞之後沒有名詞或代名詞";
                        break;
                    }
                }
            }
            else if (setz[next].POS == 8) //pron
            {
                setz[next].IsFinished = true;
                switch (ptype)
                {
                    case 1:
                        if (setz[next].N_case != 4)
                        {
                            int checking = setz[next].chooseOption(8, 4);
                            if (checking != 2)
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "代詞格位有誤";
                            }
                        }
                        
                        break;
                    case 2:
                        if (setz[next].N_case != 3)
                        {
                            int checking = setz[next].chooseOption(8, 3);
                            if (checking != 2)
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "代詞格位有誤";
                            }
                        }
                        break;
                    case 3:
                        if (!(setz[next].N_case == 3 || setz[next].N_case == 4))
                        {
                            int checking = setz[next].chooseOption(8, 4);
                            if (checking != 2)
                            {
                                checking = setz[next].chooseOption(8, 3);
                                if (checking != 2)
                                {
                                    setz[next].IsCheck = false;
                                    setz[next].Error_msg = "代詞格位有誤";
                                }
                            }
                        }
                        break;
                    case 4:
                    case 5:
                        if (setz[next].N_case != 2)
                        {
                            int checking = setz[next].chooseOption(8, 2);
                            if (checking != 2)
                            {
                                setz[next].IsCheck = false;
                                setz[next].Error_msg = "代詞格位有誤";
                            }
                        }
                        break;
                } 
            }
        }

    }
}
