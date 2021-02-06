// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System;
using System.Collections.Generic;

namespace Cryptage
{
    /// <summary>
    /// Translation from or to Cesar code.
    /// </summary>
    public class Cesar : ICrypt
    {
        private readonly Util.MetaData _met;
        private readonly int _key;
        private readonly bool _leftToRight;

        private bool _finished;
        private int _prog;

        private readonly List<string> _dico = Util.DicoInit.Min();

        private List<string> _mots = new List<string>();
        private List<string> _codes = new List<string>();

        private readonly bool _keepInvalidChar;
        

        public Cesar(Util.MetaData met, bool leftToRight)
        {
            this._met = met;

            if (_met.cesarKey is null)
                throw new NullReferenceException($"Error in Cesar constructor: {nameof(_met.cesarKey)} cannot be null");
            this._key = Math.Abs((int)_met.cesarKey);
            this._leftToRight = leftToRight;

            this._finished = false;
            this._prog = 0;

            this._keepInvalidChar = _met.keepInvalidChar;
        }

        public int GetProg()
        {
            return _prog;
        }

        public bool GetState()
        {
            return _finished;
        }

        
        public void Translate()
        {
            //int separator = (_met.type == Util.WrittingType.NORMAL) ? 25 : 0;
            
            if (_met.State.Equals(Util.State.CODE))
            {
                _mots = Util.ReadFile(_met.Input);
                this.Code();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 25, _codes);
                    else
                        Util.WriteFile(_met.Output, null, null, _codes);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 25, 25,_codes, _mots);
                }
            }
            else
            {
                _codes = Util.ReadFile(_met.Input);
                this.Decode();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 25, _mots);
                    else
                        Util.WriteFile(_met.Output, null, null, _mots);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 25, 25, _mots, _codes);
                }
            }

            _finished = true;
        }

        
        public List<string> GetResult()
        {
            if (_finished)
                return (_met.State == Util.State.CODE) ? _codes : _mots;
            else
                return new List<string>(){"null", "null", "null"};
        }



        private void Code()
        {

            // analyse du texte
            for (int i = 0; i < _mots.Count; i++)
            {
                string mot = _mots[i];              // On récupère le i-eme mot du texte
                string code = "";                   // Version traduite du mot
                
                // analyse lettre par lettre
                for (int j = 0; j < mot.Length; j++)
                {
                    string lettre = mot[j].ToString();
                    int pos = 0;
                    string mode = "";
                    
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
                    
                    // Traduction de la lettre si possible
                    if (!mode.Equals("") && pos != -1)
                    {
                        int newPos = pos;

                        if (_leftToRight)
                        {
                            newPos += _key;
                            while (newPos >= _dico.Count)
                                newPos -= _dico.Count;
                        }
                        else
                        {
                            newPos -= _key;
                            while (newPos < 0)
                                newPos += _dico.Count;
                        }


                        if (mode == "min")
                        {
                            code += _dico[newPos];
                        }
                        else
                        {
                            code += _dico[newPos].ToUpper();
                        }
                    }
                    else
                    {
                        if (_keepInvalidChar)
                            code += lettre;
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
                string mot = _codes[i];                     // On récupère le i-eme mot de la liste
                string decode = "";                         // Version décodée du mot
                
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
                    
                    // On traduit la lettre si possible
                    if (!mode.Equals("") && pos != -1)
                    {
                        int newPos = pos;

                        if (_leftToRight)
                        {
                            newPos -= _key;

                            while (newPos < 0)
                                newPos += _dico.Count;
                        }
                        else
                        {
                            newPos += _key;

                            while (newPos >= _dico.Count)
                                newPos -= _dico.Count;
                        }

                        if (mode == "min")
                            decode += _dico[newPos];
                        else
                            decode += _dico[newPos].ToUpper();
                    }
                    else
                    {
                        if (_keepInvalidChar)
                            decode += lettre;
                    }
                }
                
                _mots.Add(decode);
            }
        }
    }
}