// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.Collections.Generic;


namespace Cryptage
{
    /// <summary>
    /// Translation from or to Aeronautic code.
    /// </summary>
    public class Aero : ICrypt
    {
        private readonly Util.MetaData _met;

        private bool _finished;
        private int _prog;

        private readonly List<string> _dico = Util.DicoInit.Min();
        private readonly List<string> _dicoAero;
        private readonly List<string> _dicoAeroMaj;

        private List<string> _mots = new List<string>();
        private List<string> _codes = new List<string>();

        private readonly bool _keepInvalidChar;

        public Aero(Util.MetaData met, bool useInternational)
        {
            this._met = met;
            this._finished = false;
            this._prog = 0;
            this._keepInvalidChar = _met.keepInvalidChar;

            this._dicoAero = (useInternational) ? Util.DicoInit.AeroMin() : Util.DicoInit.AeroFrMin();
            this._dicoAeroMaj = (useInternational) ? Util.DicoInit.AeroMaj() : Util.DicoInit.AeroFrMaj();
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
                        Util.WriteFile(_met.Output, " ", 1, _codes);
                    else
                        Util.WriteFile(_met.Output, null, null, _codes);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 1, 30, _codes, _mots);
                }
            }
            else
            {
                _codes = Util.ReadFile(_met.Input);
                this.Decode();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 30, _mots);
                    else
                        Util.WriteFile(_met.Output, null, null, _mots);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 30, 1, _mots, _codes);
                }
            }

            _finished = true;
        }

        public int GetProg() => _prog;

        public bool GetState() => _finished;

        public List<string> GetResult()
        {
            if (_finished)
                return _met.State.Equals(Util.State.CODE) ? _codes : _mots;
            else
                return new List<string>() {"null", "null", "null"};
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
                    if (!mode.Equals("") && pos != -1)
                    {
                        if (mode == "min")
                        {
                            code += _dicoAero[pos];
                            code += ".";
                        }
                        else
                        {
                            code += _dicoAeroMaj[pos];
                            code += ".";
                        }
                    }
                    else
                    {
                        if (_keepInvalidChar)
                        {
                            code += lettre;
                            code += ".";
                        }
                    }
                }
                
                _codes.Add(code);
            }
        }

        private void Decode()
        {
            // analyse du texte
            for (int i = 0; i < _codes.Count; i++)
            {
                string mot = _codes[i];
                string decode = "";

                string[] data = mot.Split(".");
                
                // analyse mot par mot
                foreach (string lettre in data)
                {
                    string mode = "";
                    int pos = -1;
                    
                    // scan dico
                    for (int k = 0; k < _dicoAero.Count; k++)
                    {
                        if (_dicoAero[k].Equals(lettre) || lettre.Contains(_dicoAero[k]))
                        {
                            mode = "min";
                            pos = k;
                            break;
                        }

                        if (_dicoAeroMaj[k].Equals(lettre) || lettre.Contains(_dicoAeroMaj[k]))
                        {
                            mode = "maj";
                            pos = k;
                            break;
                        }
                    }
                    
                    // traduction si possible
                    if (!mode.Equals("") && pos != -1)
                    {
                        if (mode == "min")
                            decode += _dico[pos];
                        else
                            decode += _dico[pos].ToUpper();
                    }
                    else
                    {
                        if (_keepInvalidChar)
                            decode += lettre;
                    }
                }
                
                _mots.Add(decode);
            }
            
            DecodeOptimization();
        }
        
        private void DecodeOptimization()
        {
            // Deletes empty strings from _mots
            // This allows us to have a better layout for the output file
            
            List<string> tmp = new List<string>();

            foreach (string s in _mots)
            {
                if (!s.Equals(""))
                    tmp.Add(s);
            }

            _mots = tmp;
        }
    }
}