// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace Cryptage
{
    /// <summary>
    /// A class containing static methods used by all other objects of the program.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// This class allows us to initialize our dictionnaries.
        /// </summary>
        public static class DicoInit
        {
            // Classic dictionaries
            public static List<string> Min()
            {
                List<string> res = new List<string>();
                
                res.Add("a");
                res.Add("b");
                res.Add("c");
                res.Add("d");
                res.Add("e");
                res.Add("f");
                res.Add("g");
                res.Add("h");
                res.Add("i");
                res.Add("j");
                res.Add("k");
                res.Add("l");
                res.Add("m");
                res.Add("n");
                res.Add("o");
                res.Add("p");
                res.Add("q");
                res.Add("r");
                res.Add("s");
                res.Add("t");
                res.Add("u");
                res.Add("v");
                res.Add("w");
                res.Add("x");
                res.Add("y");
                res.Add("z");

                return res;
            }

            public static List<string> Maj()
            {
                List<string> tmp = Min();
                List<string> res = new List<string>();

                foreach (string s in tmp)
                {
                    res.Add(s.ToUpper());
                }

                return res;
            }
            
            
            
            // Navajo dictionaries
            public static List<string> NavajoMin()
            {
                List<string> res = new List<string>();
                
                res.Add("Wol-la-chee");
                res.Add("Shush");
                res.Add("Moasi");
                res.Add("Be");
                res.Add("Dzeh");
                res.Add("Ma-e");
                res.Add("Klizzie");
                res.Add("Lin");
                res.Add("Tkin");
                res.Add("Tkele-cho-gi");
                res.Add("Klizzie-yazzie");
                res.Add("Dibeh-yazzie");
                res.Add("Na-as-tso-si");
                res.Add("Nesh-chee");
                res.Add("Ne-ahs-jah");
                res.Add("Bi-sodih");
                res.Add("Ca-yeilth");
                res.Add("Gah");
                res.Add("Dibeh");
                res.Add("Than-zie");
                res.Add("No-da-ih");
                res.Add("A-keh-di-glini");
                res.Add("Gloe-ih");
                res.Add("Al-an-as-dzoh");
                res.Add("Tsah-as-zih");
                res.Add("Besh-do-gliz");

                return res;
            }

            public static List<string> NavajoMaj()
            {
                List<string> tmp = NavajoMin();
                List<string> res = new List<string>();

                foreach (string s in tmp)
                {
                    res.Add(s.ToUpper());
                }

                return res;
            }
            
            
            // Aero dictionaries
            public static List<string> AeroMin()
            {
                List<string> res = new List<string>();
                
                res.Add("Alfa");
                res.Add("Bravo");
                res.Add("Charlie");
                res.Add("Delta");
                res.Add("Echo");
                res.Add("Fox-Trot");
                res.Add("Golf");
                res.Add("Hotel");
                res.Add("India");
                res.Add("Juliett");
                res.Add("Kilo");
                res.Add("Lima");
                res.Add("Mike");
                res.Add("November");
                res.Add("Oscar");
                res.Add("Papa");
                res.Add("Quebec");
                res.Add("Romeo");
                res.Add("Sierra");
                res.Add("Tango");
                res.Add("Uniform");
                res.Add("Victor");
                res.Add("Whiskey");
                res.Add("X-ray");
                res.Add("Yankee");
                res.Add("Zoulou");

                return res;
            }

            public static List<string> AeroMaj()
            {
                List<string> tmp = AeroMin();
                List<string> res = new List<string>();

                foreach (string s in tmp)
                {
                    res.Add(s.ToUpper());
                }

                return res;
            }

            public static List<string> AeroFrMin()
            {
                List<string> res = new List<string>();
                
                res.Add("Anatole");
                res.Add("Berthe");
                res.Add("Célestine");
                res.Add("Désirée");
                res.Add("Eugène");
                res.Add("François");
                res.Add("Gaston");
                res.Add("Henri");
                res.Add("Irma");
                res.Add("Joseph");
                res.Add("Kléber");
                res.Add("Louis");
                res.Add("Marcel");
                res.Add("Nicolas");
                res.Add("Oscar");
                res.Add("Pierre");
                res.Add("Quintal");
                res.Add("Raoul");
                res.Add("Suzanne");
                res.Add("Thérèse");
                res.Add("Ursule");
                res.Add("Victor");
                res.Add("William");
                res.Add("Xavier");
                res.Add("Yvonne");
                res.Add("Zoé");

                return res;
            }

            public static List<string> AeroFrMaj()
            {
                List<string> tmp = AeroFrMin();
                List<string> res = new List<string>();

                foreach (string s in tmp)
                {
                    res.Add(s.ToUpper());
                }

                return res;
            }
            
            // Morse dictionaries
            public static List<string> Morse()
            {
                List<string> res = new List<string>();
                
                res.Add("._");
                res.Add("_...");
                res.Add("_._.");
                res.Add("_..");
                res.Add(".");
                res.Add(".._.");
                res.Add("__.");
                res.Add("....");
                res.Add("..");
                res.Add(".___");
                res.Add("_._");
                res.Add("._..");
                res.Add("__");
                res.Add("_.");
                res.Add("___");
                res.Add(".__.");
                res.Add("__._");
                res.Add("._.");
                res.Add("...");
                res.Add("_");
                res.Add(".._");
                res.Add("..._");
                res.Add(".__");
                res.Add("_.._");
                res.Add("_.__");
                res.Add("__..");
                res.Add(".____");
                res.Add("..___");
                res.Add("...__");
                res.Add("...._");
                res.Add(".....");
                res.Add("_....");
                res.Add("__...");
                res.Add("___..");
                res.Add("____.");
                res.Add("_____");

                return res;
            }

            public static List<string> ClassicDicoForMorse()
            {
                List<string> res = Maj();
                
                res.Add("0");
                res.Add("1");
                res.Add("2");
                res.Add("3");
                res.Add("4");
                res.Add("5");
                res.Add("6");
                res.Add("7");
                res.Add("8");
                res.Add("9");

                return res;
            }
        }

        /// <summary>
        /// Reads the data contained in a given text file.
        /// </summary>
        /// <param name="path">A <see cref="string"/> containing the absolute path of the file to be read.</param>
        /// <param name="separator">A <see cref="Nullable"/> <see cref="string"/> containing the character(s) used to separate each word of the file.
        /// Note : If the separator is null, we will use a single space to separate each word.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="string"/> containing all the data from the given file.</returns>
        /// <exception cref="ArgumentException">Incorrect file path</exception>
        public static List<string> ReadFile(string path, string? separator = null)
        {
            if (path.Equals("") || !path.EndsWith(".txt"))
                throw new ArgumentException("Incorrect file path", nameof(path));

            List<string> res = new List<string>();

            try
            {
                StreamReader file = new StreamReader(path);

                string[] data = (separator is not null)
                    ? file.ReadToEnd().Split(separator)
                    : file.ReadToEnd().Split(" ");

                res.AddRange(data);

                file.Close();
            }
            catch (Exception e)
            {
                Error err = new Error("Erreur le fichier source entré est introuvable.",
                    path,
                    "Exception in function Util.ReadFile: \n" + e.ToString());
                Program.SetError(err);
            } 

            return res;
        }


        /// <summary>
        /// Writes the content of a given <see cref="List{T}"/> of <see cref="string"/> into a given file.
        /// </summary>
        /// <param name="path">A <see cref="string"/> containing the absolute path of the file to be written.</param>
        /// <param name="wordSeparator">A <see cref="Nullable"/> <see cref="string"/> containing the character(s) used to separate each word in the output file.
        /// Note : if wordSeparator is null, we will separate each word by one single space.</param>
        /// <param name="lineSeparator">A <see cref="Nullable"/> <see cref="int"/> indicating the number of word to be written per line.
        /// Note : if lineSeparator is null, we will write one word per line.</param>
        /// <param name="arg">A <see cref="List{T}"/> of <see cref="string"/> containing the data to be written.</param>
        /// <exception cref="ArgumentException">Incorrect file path.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Line separator cannot be negative.</exception>
        public static void WriteFile(string path, string? wordSeparator, int? lineSeparator, List<string> arg)
        {
            if (path.Equals("") || !path.EndsWith(".txt"))
                throw new ArgumentException("Incorrect file path", nameof(path));

            if (lineSeparator < 0)
                throw new ArgumentOutOfRangeException(nameof(lineSeparator), "Line separator cannot be negative");

            try
            {
                StreamWriter file = new StreamWriter(path);

                for (int i = 0, j = 1; i < arg.Count; i++, j++)
                {
                    file.Write(arg[i]);
                    if (wordSeparator is not null)
                        file.Write(wordSeparator);
                    else 
                        file.Write(" ");

                    if (j == lineSeparator)
                    {
                        file.WriteLine(" ");
                        j = 0;
                    } else if (lineSeparator is null) 
                        file.WriteLine("");
                }
                
                file.Close();
            }
            catch (Exception e)
            {
                Error err = new Error($"A problem occurs while writing in file {path}",
                    nameof(WriteFile),
                    e.ToString());
                
                Program.SetError(err);
            }
        }

        /// <summary>
        /// Writes a code and its original source in a given file.
        /// </summary>
        /// <param name="path">A <see cref="string"/> containing the path of the file to be written.</param>
        /// <param name="wordSeparatorCode">A <see cref="Nullable"/> <see cref="string"/> containing the character(s) used to separate each word or the crypted text in the output file.
        /// Note : if wordSeparatorCode is <see langword="null"/>, each word will be separated by one single space.</param>
        /// <param name="wordSeparatorSource"> A <see cref="Nullable"/> <see cref="string"/> containing the character(s) used to separate each word of the source text in the output file.
        /// Note : if wordSeparatorSource is <see langword="null"/>, each word will be separated by one single space.</param>
        /// <param name="lineSeparatorCode">A <see cref="Nullable"/> <see cref="int"/> indicating the number of word to be written per line for the crypted text.
        /// Note : if lineSeparatorCode is <see langword="null"/></param>, we will write one word per line.
        /// <param name="lineSeparatorSource">A <see cref="Nullable"/> <see cref="int"/> indicating the number of word to be written per line for the source text.
        /// Note : if lineSeparatorSource is <see langword="null"/>, we will write one word per line.</param>
        /// <param name="argCode">A <see cref="List{T}"/> of <see cref="string"/> containing the data of the crypted text.</param>
        /// <param name="argSource">A <see cref="List{T}"/> of <see cref="string"/> containing the data of the source text.</param>
        /// <exception cref="ArgumentException">Incorrect file path.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Line separator cannot be negative.</exception>
        public static void WriteFileWithOriginalText(string path, string? wordSeparatorCode,
            string? wordSeparatorSource, int? lineSeparatorCode, int? lineSeparatorSource, List<string> argCode,
            List<string> argSource)
        {
            if (path.Equals("") || !path.EndsWith(".txt"))
                throw new ArgumentException("Incorrect file path", nameof(path));

            if (lineSeparatorCode < 0 || lineSeparatorSource < 0)
                throw new ArgumentOutOfRangeException(
                    (lineSeparatorCode < 0) ? nameof(lineSeparatorCode) : nameof(lineSeparatorSource),
                    "Line separator cannot be negative");

            try
            {
                StreamWriter file = new StreamWriter(path);

                file.WriteLine("Texte source :");
                file.WriteLine(" ");

                for (int i = 0, j = 1; i < argSource.Count; i++, j++)
                {
                    file.Write(argSource[i]);
                    if (wordSeparatorSource is not null)
                        file.Write(wordSeparatorSource);
                    else 
                        file.Write(" ");

                    if (j == lineSeparatorSource)
                    {
                        file.WriteLine(" ");
                        j = 0;
                    }
                    else if (lineSeparatorSource is null)
                    {
                        file.WriteLine(" ");
                    }
                }

                file.WriteLine(" ");
                file.WriteLine("################################################################################");
                file.WriteLine(" ");
                file.WriteLine("Texte crypté:");
                file.WriteLine(" ");
                
                for (int i = 0, j = 1; i < argCode.Count; i++, j++)
                {
                    file.Write(argCode[i]);
                    if (wordSeparatorCode is not null)
                        file.Write(wordSeparatorCode);
                    else 
                        file.Write(" ");

                    if (j == lineSeparatorCode)
                    {
                        file.WriteLine(" ");
                        j = 0;
                    }
                    else if (lineSeparatorCode is null)
                    {
                        file.WriteLine(" ");
                    }
                }

                file.Close();
            }
            catch (Exception e)
            {
                Error err = new Error($"A problem occurs while writing in file {path}",
                    nameof(WriteFileWithOriginalText),
                    e.ToString());
                
                Program.SetError(err);
            }


        }
        
        public enum State
        {
            CODE = 0,
            DECODE = 1
        }

        /// <summary>
        /// A structure containing all the metadata used by the crypt algorithms.
        /// </summary>
        public struct MetaData
        {
            // absolute file directories
            public readonly string Input;
            public readonly string Output;

            // file names (without extension)
            public readonly string InputName;
            public readonly string OutputName;

            // informations about crypting
            public readonly State State;
            public readonly bool keepInvalidChar;
            public readonly bool codeAndTranslate;
            public readonly WrittingType type;
            
            // Only for Cesar
            public readonly int? cesarKey;
            
            // Constructor
            public MetaData(string input, string output, State state, bool keepInvalidChar, bool codeAndTranslate, WrittingType type, int? cesarKey = null)
            {
                if (input.Equals("") || !(input.EndsWith(".txt") || input.EndsWith(".txt/")))
                    throw new ArgumentException("Invalid input path", nameof(input));

                if (output.Equals("") || !(output.EndsWith(".txt") || output.EndsWith(".txt/")))
                    throw new ArgumentException("Invalid output path", nameof(output));
                
                
                this.Input = input;
                this.Output = output;

                string[] inp = input.Split("/");
                string[] inpF = inp[^1].Split(".");
                this.InputName = inpF[0];

                string[] outp = output.Split("/");
                string[] outpF = outp[^1].Split(".");
                this.OutputName = outpF[0];

                this.State = state;
                this.keepInvalidChar = keepInvalidChar;
                this.codeAndTranslate = codeAndTranslate;
                this.type = type;
                this.cesarKey = cesarKey;
            }
        }

        /// <summary>
        /// A structure containing an error, used by the program to display the details of the error.
        /// </summary>
        public struct Error
        {
            public readonly string Message;
            public readonly string Source;
            public readonly string? Details;

            public readonly bool IsDetails;

            public Error(string message, string source, string? details)
            {
                this.Message = message;
                this.Source = source;
                this.Details = details;

                this.IsDetails = (details is not null);
            }

            public override string ToString()
            {
                string res = Message;
                res += "\n";
                res += "Source de l'erreur: ";
                res += Source;
                res += "\n";
                if (IsDetails)
                {
                    res += "Détails: \n";
                    res += Details;
                }

                return res;
            }
        }

        /// <summary>
        /// An enumeration used to indicate which algorithm should be used by the program.
        /// </summary>
        public enum TypeC
        {
            CESAR = 0,
            MORSE = 1,
            AERO = 2,
            NAVAJO = 3,
            VIGENERE = 4,
            BINAIRE = 5
        }

        /// <summary>
        /// Convert a TypeC value into a string.
        /// </summary>
        /// <param name="type">The <see cref="TypeC"/> value to be converted.</param>
        /// <returns>A <see cref="string"/> containing the equivalent of the <see cref="TypeC"/> value.</returns>
        public static string TypeToString(TypeC? type)
        {
            return (type == TypeC.CESAR) ? "Cesar"
                : (type == TypeC.AERO) ? "Aéronautique"
                : (type == TypeC.MORSE) ? "Morse"
                : (type == TypeC.NAVAJO) ?"Navajo"
                    : "A propos";
        }

        /// <summary>
        /// An enumeration used to indicate the writing type to be used by the program.
        /// </summary>
        public enum WrittingType
        {
            NORMAL = 0,
            WORD_BY_WORD = 1
        }
    }
}