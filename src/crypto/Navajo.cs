// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.Collections.Generic;

namespace Cryptage
{
    /// <summary>
    /// Translation from or to Navajo code.
    /// </summary>
    public class Navajo : ICrypt
    {
        private readonly Util.MetaData _met;

        private bool _finished;
        private int prog;

        private readonly List<string> _dico = Util.DicoInit.Min();
        private readonly List<string> _dicoNavajo = Util.DicoInit.NavajoMin();
        private readonly List<string> _dicoNavajoMaj = Util.DicoInit.NavajoMaj();

        private List<string> _mots = new List<string>();
        private List<string> _codes = new List<string>();

        private readonly bool _keepInvalidChar;


        public Navajo(Util.MetaData met)
        {
            this._met = met;
            this.prog = 0;
            this._finished = false;
            this._keepInvalidChar = _met.keepInvalidChar;
        }


        public void Translate()
        {
            if (_met.State == Util.State.CODE)
            {
                _mots = Util.ReadFile(_met.Input);
                Code();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 5, _codes);
                    else
                        Util.WriteFile(_met.Output, null, null, _codes);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 5, 20,_codes, _mots);
                }
            }
            else
            {
                _codes = Util.ReadFile(_met.Input);
                Decode();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 20, _mots);
                    else
                        Util.WriteFile(_met.Output, null, null, _mots);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 20, 5,_mots, _codes);
                }
            }

            _finished = true;
        }

        public int GetProg() => prog;

        public bool GetState() => _finished;

        public List<string> GetResult()
        {
            if (_finished)
                return (_met.State.Equals(Util.State.CODE)) ? _codes : _mots;
            else
                return new List<string>() {"null", "null", "null"};
        }


        private void Code()
        {
            // lecture du texte
            for (int i = 0; i < _mots.Count; i++)
            {
                string mot = _mots[i];
                string code = "";
                
                // analyse lettre par lettre
                for (int j = 0; j < mot.Length; j++)
                {
                    string lettre = mot[j].ToString();
                    string mode = "";
                    int pos = -1;
                    
                    // scan dico
                    for (int k = 0; k < _dico.Count; k++)
                    {
                        if (_dico[k].Equals(lettre))
                        {
                            mode = "min";
                            pos = k;
                            break;
                        }

                        if (_dico[k].ToUpper().Equals(lettre))
                        {
                            mode = "maj";
                            pos = k;
                            break;
                        }
                    }
                    
                    // traduction si possible
                    if (mode != "" && pos != -1)
                    {
                        if (mode == "min")
                        {
                            code += _dicoNavajo[pos];
                        }
                        else
                        {
                            code += _dicoNavajoMaj[pos];
                        }
                    }
                    else
                    {
                        if (_keepInvalidChar)
                            code += lettre;
                    }

                    code += ".";
                }
                
                _codes.Add(code);
            }
        }


        private void Decode()
        {
            CodeCleaner();

            // lecture du texte
            for (int i = 0; i < _codes.Count; i++)
            {
                string mot = _codes[i];
                string decode = "";

                string[] data = mot.Split(".");
                
                // analyse lettre par lettre
                foreach (string s in data)
                {
                    string mode = "";
                    int pos = -1;
                    
                    // scan dico
                    for (int j = 0; j < _dicoNavajo.Count; j++)
                    {
                        if (_dicoNavajo[j].Equals(s))
                        {
                            mode = "min";
                            pos = j;
                            break;
                        }

                        if (_dicoNavajoMaj[j].Equals(s))
                        {
                            mode = "maj";
                            pos = j;
                            break;
                        }
                    }
                    
                    // traduction si possible
                    if (mode != "" && pos != -1)
                    {
                        if (mode == "min")
                        {
                            decode += _dico[pos];
                        }
                        else
                        {
                            decode += _dico[pos].ToUpper();
                        }
                    }
                    else
                    {
                        if (_keepInvalidChar)
                            decode += s;
                    }
                }
                
                _mots.Add(decode);
            }
        }


        private void CodeCleaner()
        {
            // Deletes useless strings in list
            // This allows us to have a better layout for the output file.
            
            List<string> tmp = new List<string>();

            foreach (string s in _codes)
            {
                if (s.StartsWith("\r\n"))
                {
                    string s1 = s.Substring("\r\n".Length);
                    tmp.Add(s1);
                } else if (s.EndsWith("\r\n"))
                {
                    string s2 = s.Substring(0, s.Length - "\r\n".Length);
                    tmp.Add(s2);
                }
                else
                {
                    tmp.Add(s);
                }
            }

            _codes = tmp;
        }
    }
}