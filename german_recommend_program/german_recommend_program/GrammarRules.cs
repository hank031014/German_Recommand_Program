﻿using System;
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
                    break;
                case 7:
                    state = (int)Role.M_F;
                    break;
                case 8:
                    if (setz[0].N_case == 1)
                    {
                        state = (int)Role.M_S;
                        mainState = (int)MainRole.M;
                        st_element.Add("M_subject", setz[0].Pron_type);
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
                        }
                    }
                    if (setz[cur - 1].POS == 1)
                    {
                        if (setz[cur].N_case == setz[cur - 1].N_case)
                        {
                            setz[cur].IsCheck = false;
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
                    if (state.Equals((int)Role.M_F) || state.Equals((int)Role.M_C))
                    {
                        state = (int)Role.M_V1;
                        if (mainState == (int)MainRole.M && !st_element.ContainsKey("M_V1"))
                        {
                            vPosOneCheck(cur);
                            st_element.Add("M_V1", cur);
                        }
                    }
                    if (state.Equals((int)Role.M_S))
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
                    break;
                case 4:
                    state = (int)Role.M_S;
                    break;
                case 5:
                    state = (int)Role.M_C;
                    break;
                case 6:
                    state = (int)Role.M_F;
                    break;
                case 7:
                    state = (int)Role.M_F;
                    break;
                case 8:
                    
                    if (state.Equals((int)Role.M_S) || state.Equals((int)Role.S_S) || state.Equals((int)Role.D_S))
                    {
                        if(setz[(cur-1 <= 0 ? 0 : cur-1)].Text != "und" || setz[cur-1 <= 0 ? 0 : cur-1].Text != "oder"){
                            setz[cur].IsCheck = false;
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
                                    }
                                    break;
                                case 2:
                                    st_element.Add("M_subject", 2);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                    }
                                    break;
                                case 3:
                                    st_element.Add("M_subject", 3);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_y2"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                    }
                                    break;
                                case 4:
                                    st_element.Add("M_subject", 4);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_y2"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                    }
                                    break;
                                case 5:
                                    st_element.Add("M_subject", 5);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_s3"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                    }
                                    break;
                                case 6:
                                    st_element.Add("M_subject", 6);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
                                    }
                                    break;
                                case 7:
                                    st_element.Add("M_subject", 7);
                                    if (!setz[(int)st_element["M_V1"]].POS_dt.EndsWith("_pl"))
                                    {
                                        setz[(int)st_element["M_V1"]].IsCheck = false;
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
                        }
                    }
                    break;
                }
            }
        }

        private void artNounCheck(int Cur, int Ncase = 1)
        {
            int cur = Cur;
            int ncase = Ncase;
            int next = cur + 1;

            if (setz[cur].POS == 1 && Ncase != 2 && setz[cur].Text.EndsWith("es"))
            {
                setz[cur].N_case = 2;
            }
            while (next < setz.Count)
            {
                if (setz[next].POS != 2 || setz[next].POS == 4)
                {
                    if (setz[next].POS == 3)
                    {
                        String txt = setz[cur].Text.ToLower();
                        int gender = 1;
                        switch(txt){
                            case "der":
                                gender = 1;
                                break;
                            case "die":
                                gender = 2;
                                break;
                            case "das":
                                gender = 3;
                                break;
                        }
                        addNounRole(1, gender, cur);
                    }
                    break;
                }
                if (setz[next].POS == 2)
                {
                    int nc = setz[next].N_case;
                    int gender = setz[next].Gender;
                    String art = setz[cur].Text.ToLower();
                    
                    if (art.StartsWith("d"))
                    {
                        //Debug.WriteLine(nc + ", " + gender);
                        if(art.Equals(d_art[gender - 1, nc - 1])){
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                            //Debug.WriteLine("ddd");
                        }
                        else if (art.Equals(d_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                            
                        }
                        else if (art.Equals(d_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals(d_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals(d_art[3, 1]) && setz[next].Gender == 4 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                        }
                    }

                    if (art.StartsWith("ein"))
                    {
                        if (art.Equals("ein" + ek_art[gender - 1, nc - 1]))
                        {
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                        }
                        else if (art.Equals("ein" + ek_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }

                        }
                        else if (art.Equals("ein" + ek_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals("ein" + ek_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                        }
                    }
                    if (art.StartsWith("kein"))
                    {
                        if (art.Equals("kein" + ek_art[gender - 1, nc - 1]))
                        {
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                        }
                        else if (art.Equals("kein" + ek_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals("kein" + ek_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals("kein" + ek_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals("kein" + ek_art[3, 1]) && setz[next].Gender == 4 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                        }
                    }

                    if (pron_gen.Any(g => art.StartsWith(g)))
                    {
                        String match = Array.Find(pron_gen, g => art.StartsWith(g));
                        Debug.WriteLine(match + ek_art[gender - 1, nc - 1]);
                        if (art.Equals(match + ek_art[gender - 1, nc - 1]))
                        {
                            setz[cur].IsCheck = true;
                            setz[cur].N_case = nc;
                            setz[cur].Gender = gender;
                            addNounRole(nc, gender, next);
                        }
                        else if (art.Equals(match + ek_art[0, 1]) && setz[next].Gender == 1 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                                //Debug.WriteLine("ppp2");
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                                //Debug.WriteLine("ppp1");
                            }
                        }
                        else if (art.Equals(match + ek_art[1, 1]) && setz[next].Gender == 2 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals(match + ek_art[2, 1]) && setz[next].Gender == 3 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else if (art.Equals(match + ek_art[3, 1]) && setz[next].Gender == 4 && cur - 1 >= 0)
                        {
                            if (setz[cur - 1].POS == 2 || setz[cur - 1].Prep_type == 4)
                            {
                                setz[cur].IsCheck = true;
                                setz[next].N_case = 2;
                                setz[next].IsFinished = true;
                            }
                            else
                            {
                                setz[cur].IsCheck = false;
                            }
                        }
                        else
                        {
                            setz[cur].IsCheck = false;
                            Debug.WriteLine("ppp");
                        }
                    }

                    break;
                }
                
                next++;
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

        /*
        public void KnowWordRoleInSent(Words word)
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
            
            int poss = 1;
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
            int poss = 1;
            Debug.WriteLine("V1 POS: " + word.POS + ", case: " + word.N_case + ", poss: " + poss);
            if (word.POS != 3)
            {
                word.IsCheck = false;
            }
        }

        public void Object1(Words word)
        {

        }
        */
    }
}
