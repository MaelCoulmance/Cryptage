// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptage
{
    /// <summary>
    /// Translation from or to binary code.
    /// </summary>
    public class Binaire : ICrypt
    {
        private readonly Util.MetaData _met;
        private bool _finished;
        private int _prog;

        private List<string> _mots = new List<string>();
        private List<string> _codes = new List<string>();

        private bool _keepInvalidChar;


        public Binaire(Util.MetaData met)
        {
            this._met = met;
            this._finished = false;
            this._prog = 0;
            this._keepInvalidChar = _met.keepInvalidChar;
        }

        public int GetProg() => _prog;

        public bool GetState() => _finished;

        public void Translate()
        {
            if (_met.State == Util.State.CODE)
            {
                _mots = Util.ReadFile(_met.Input);
                this.Code();
                if (!_met.codeAndTranslate)
                {
                    if (_met.type == Util.WrittingType.NORMAL)
                        Util.WriteFile(_met.Output, " ", 15, _codes);
                    else 
                        Util.WriteFile(_met.Output, null, null, _codes);
                }
                else
                {
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 15, 25, _codes, _mots);
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
                    Util.WriteFileWithOriginalText(_met.Output, " ", " ", 15, 25, _mots, _codes);
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


        private string StringToBin(string arg)
        {
            
            StringBuilder sb = new StringBuilder();

            foreach (char c in arg.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            return sb.ToString();
            
        }

        private string BinToString(string arg)
        {
            List<Byte> data = new List<byte>();

            for (int i = 0; i < arg.Length; i += 8)
            {
                data.Add(Convert.ToByte(arg.Substring(i, 8), 2));
            }

            return Encoding.UTF8.GetString(data.ToArray());
        }

        
        
        private void Code()
        {
            foreach (string s in _mots)
            {
                string code = StringToBin(s);
                _codes.Add(code);
            }
        }

        private void Decode()
        {
            List<string> dico = Util.DicoInit.Min();
            foreach (string s in _codes)
            {
                string decode = BinToString(s);

                string res = "";
                for (int i = 0; i < decode.Length; i++)
                {
                    char c = decode[i];
                    bool isCorrect = false;
                    foreach (string d in dico)
                    {
                        if (c.ToString() == d || c.ToString() == d.ToUpper())
                        {
                            isCorrect = true;
                            break;
                        }
                    }

                    if (isCorrect || _keepInvalidChar)
                        res += c;
                }
                
                _mots.Add(res);
            }
        }
    }
}