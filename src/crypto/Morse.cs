// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.Collections.Generic;

namespace Cryptage
{
    /// <summary>
    /// Translation from or to Morse code.
    /// </summary>
    public class Morse : ICrypt
    {
        private readonly Util.MetaData _met;

        private bool _finished;
        private int _prog;

        private readonly List<string> _dico = Util.DicoInit.ClassicDicoForMorse();
        private readonly List<string> _dicoMorse = Util.DicoInit.Morse();

        private List<string> _mots = new List<string>();
        private List<string> _codes = new List<string>();

        private readonly bool _keepInvalidChar;


        public Morse(Util.MetaData met)
        {
            this._met = met;
            this._finished = false;
            this._prog = 0;
            this._keepInvalidChar = _met.keepInvalidChar;
        }


        public void Translate()
        {
            if (_met.State.Equals(Util.State.CODE))
            {
                _mots = Util.ReadFile(_met.Input);
                this.Code();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 5, _codes);
                    else
                        Util.WriteFile(_met.Output, null, null, _codes);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 5, 20, _codes, _mots);
                }
            }
            else
            {
                _codes = Util.ReadFile(_met.Input);
                this.Decode();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 20, _mots);
                    else
                        Util.WriteFile(_met.Output, null, null, _mots);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 20, 5, _mots, _codes);
                }
            }

            _finished = true;
        }

        public int GetProg() => _prog;

        public bool GetState() => _finished;

        public List<string> GetResult()
        {
            if (_finished)
            {
                return _met.State.Equals(Util.State.CODE) ? _codes : _mots;
            }
            else
            {
                return new List<string>() {"null", "null", "null"};
            }
        }



        private void Code()
        {
            // analyse du texte
            for (int i = 0; i < _mots.Count; i++)
            {
                string mot = _mots[i];
                string code = "";

                // analyse lettre par lettre
                for (int j = 0; j < mot.Length; j++)
                {
                    string lettre = mot[j].ToString();
                    int pos = -1;

                    // scan dico
                    for (int k = 0; k < _dico.Count; k++)
                    {
                        if (_dico[k].Equals(lettre) || _dico[k].ToLower().Equals(lettre))
                        {
                            pos = k;
                            break;
                        }
                    }


                    // traduction si possible
                    if (pos != -1)
                    {
                        code += _dicoMorse[pos];
                        code += "/";
                    }
                    else
                    {
                        if (_keepInvalidChar)
                        {
                            code += lettre;
                            code += "/";
                        }
                    }
                }

                _codes.Add(code);
            }
        }

        private void Decode()
        {
            CodeCleaner();
            
            // analyse du texte
            for (int i = 0; i < _codes.Count; i++)
            {
                string mot = _codes[i];
                string decode = "";

                string[] data = mot.Split("/");

                // analyse lettre par lettre
                foreach (string s in data)
                {
                    int pos = -1;
                    for (int j = 0; j < _dicoMorse.Count; j++)
                    {
                        if (_dicoMorse[j].Equals(s))
                        {
                            pos = j;
                            break;
                        }
                    }

                    // traduction si possible
                    if (pos != -1)
                    {
                        decode += _dico[pos].ToLower();
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
            // Remove useless data in strings
            // This allows us to have a better layout for the output file.
            
            List<string> tmp = new List<string>();

            foreach (string s in _codes)
            {
                if (s.StartsWith("\r\n"))
                {
                    string s2 = s.Substring("\r\n".Length);
                    tmp.Add(s2);
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