// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualBasic;

namespace Cryptage
{
    /// <summary>
    /// Translation from or to Vigenere code.
    /// </summary>
    public class Vigenere : ICrypt
    {
        private readonly Util.MetaData _met;
        private readonly string _key;
        private List<string> _fullKey;

        private bool _finished;
        private int _prog;

        private readonly bool _keepInvalidChar;

        private readonly List<string> _dico = Util.DicoInit.Min();
        private readonly Dictionary<string, List<string>> _table = InitTable();

        private List<string> _mots = new List<string>();
        private List<string> _codes = new List<string>();

        public Vigenere(Util.MetaData met, string key)
        {
            this._met = met;
            this._key = key;

            this._finished = false;
            this._prog = 0;

            this._keepInvalidChar = met.keepInvalidChar;
        }

        public int GetProg() => _prog;

        public bool GetState() => _finished;

        public void Translate()
        {
            if (_met.State == Util.State.CODE)
            {
                _mots = Util.ReadFile(_met.Input);
                _fullKey = SetFullKey();
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
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 25, 25, _codes, _mots);
                }
            }
            else
            {
                _codes = Util.ReadFile(_met.Input);
                _fullKey = SetFullKey();
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
                return new List<string>() {"null", "null", "null"};
        }


        private static Dictionary<string, List<string>> InitTable()
        {
            Dictionary<string, List<string>> tmp = new Dictionary<string, List<string>>();
            List<string> firstLine = Util.DicoInit.Min();
            tmp.Add("a", firstLine);

            for (int i = 1; i < 26; i++)
            {
                List<string> nextLine = new List<string>();
                for (int j = 1; j < firstLine.Count; j++)
                {
                    nextLine.Add(firstLine[j]);
                }
                nextLine.Add(firstLine[0]);

                string lettre = nextLine[0];
                tmp.Add(lettre, nextLine);

                firstLine = nextLine;
            }

            return tmp;
        }

        private List<string> SetFullKey()
        {
            if ((_met.State == Util.State.CODE && _mots.Count == 0) || (_met.State == Util.State.DECODE && _codes.Count == 0))
                throw new MethodAccessException("The method SetFullKey should be used only after the initialization of _mots or _codes");

            int length = 0;
            foreach (string s in (_met.State == Util.State.CODE) ? _mots : _codes)
            {
                foreach (char c in s)
                {
                    length++;
                }
            }

            List<string> res = new List<string>();
            while (length >= _key.Length)
            {
                foreach (char c in _key)
                {
                    res.Add(c.ToString());
                }

                length -= _key.Length;
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length),$"problem with length initialization : value={length}");
            }

            int compt = 0;
            while (length != 0)
            {
                res.Add(_key[compt].ToString());
                length--;
                compt++;
            }

            return res;
        }


        private void Code()
        {
            // analyse texte
            for (int i = 0, keyPos = 0; i < _mots.Count; i++)
            {
                string mot = _mots[i];
                string code = "";
                
                // analyse lettre par lettre
                for (int j = 0; j < mot.Length; j++)
                {
                    string lettre = mot[j].ToString();
                    string key = _fullKey[keyPos];
                    string mode = "";
                    int pos = -1;
                    
                    // scan dico
                    for (int k = 0; k < _dico.Count; k++)
                    {
                        if (_dico[k] == lettre)
                        {
                            mode = "min";
                            pos = k;
                            break;
                        }

                        if (_dico[k].ToUpper() == lettre)
                        {
                            mode = "maj";
                            pos = k;
                            break;
                        }
                    }
                    
                    // traduction si possible
                    if (mode != "" && pos != -1)
                    {
                        List<string> data = _table[key];

                        if (mode == "min")
                            code += data[pos];
                        else
                            code += data[pos].ToUpper();

                        keyPos++;
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
            // analyse texte
            for (int i = 0, keyPos = 0; i < _codes.Count; i++)
            {
                string mot = _codes[i];
                string decode = "";
                
                // analyse lettre par lettre
                for (int j = 0; j < mot.Length; j++)
                {
                    string key = _fullKey[keyPos];
                    string lettre = mot[j].ToString();
                    string mode = "";
                    int pos = -1;

                    List<string> data = _table[key];
                    
                    // scan data
                    for (int k = 0; k < data.Count; k++)
                    {
                        if (data[k] == lettre)
                        {
                            mode = "min";
                            pos = k;
                            break;
                        }

                        if (data[k].ToUpper() == lettre)
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
                            decode += _dico[pos];
                        else
                            decode += _dico[pos].ToUpper();

                        keyPos++;
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
        
        
        public List<string> GetFullKey()
        {
            if (_fullKey != null)
                return _fullKey;
            else
                return new List<string>() {"non init"};
        }
        
        
        // static field
        public static void PrintTable()
        {
            Console.Out.WriteLine(GetTable());
        }
        
        public static string GetTable()
        {
            var table = InitTable();

            List<string> d = Util.DicoInit.Min();
            string res = "";

            foreach (string s in d)
            {
                res += $"{s} : ";
                List<string> data = table[s];
                foreach (string sd in data)
                    res += $"{sd} ";
                res += "\n";
            }

            return res;
        }
    }
}