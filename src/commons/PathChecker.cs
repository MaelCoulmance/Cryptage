// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System;
using System.IO;

namespace Cryptage
{
    /// <summary>
    /// A class containing static methods to check if a path or a file name is correctly written.
    /// </summary>
    public class PathChecker
    {
        /// <summary>
        /// Checks if a path is correctly written.
        /// </summary>
        /// <param name="arg"> A <see cref="string" /> containing the path to be checked.</param>
        /// <returns> <see langword="true"/> if the path is correctly written, <see langword="false"/> otherwise.</returns>
        public static bool CorrectPath(string arg)
        {
            // A correct relative path looks like this :
            // "./file.txt" or "./file.txt/"
            // "./folder/file.txt or ./folder/file.txt"
            // "./folder1/folder2/file.txt" or "./folder1/folder2/file.txt/"
            // etc...
            
            // A correct absolute path looks like this :
            // "c:\Users\Philibert\Documents\file.txt"

            if (arg == "")
                return false;
            
            if (!(arg.EndsWith(".txt") || arg.EndsWith(".txt/") || arg.EndsWith(".txt\\")))
                return false;

            if (!(arg.StartsWith("./") || arg.StartsWith("C:\\")))
                return false;

            string[] tmp = arg.Split("\\");
            string[] tmp2 = arg.Split("/");

            if (tmp.Length < 2 && tmp2.Length < 2)
                return false;

            return true;
        }


        /// <summary>
        /// Checks if a filename is correctly written.
        /// </summary>
        /// <param name="arg">A <see cref="string"/> containing the file name to be checked.</param>
        /// <returns><see langword="true"/> if the file name is correctly written, <see langword="false"/> otherwise.</returns>
        public static bool CorrectFileName(string arg)
        {
            // A correct file name looks like this:
            // file.txt
            // Example of incorrect file names:
            // fi.le.txt
            // fi/le.txt
            // file.jpg
            // file.txt.
            // etc...

            if (!arg.EndsWith(".txt"))
                return false;

            string[] tmp = arg.Split(".");
            return tmp.Length == 2;  // With the Split methods, which should have string[] tmp = {"file";"txt"}
        }


        /// <summary>
        /// Checks if a given file does actually exist.
        /// </summary>
        /// <param name="arg">A <see cref="string"/> containing the absolute path of the file to be checked.</param>
        /// <returns><see langword="true"/> if the file does exist, <see langword="false"/> otherwise.</returns>
        public static bool ExistingFile(string arg)
        {
            try
            {
                StreamReader file = new StreamReader(arg);
                file.Read();
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Do the full check of the given path
        /// </summary>
        /// <param name="path">A <see cref="string"/> containing the absolute path of file to be checked.</param>
        /// <param name="mode">A <see cref="string"/> indicating if the path corresponds to an input file or an output file.</param>
        /// <returns><see langword="true"/> if the path and file name is correct (and if the file does exist in the case of an input file), <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool CheckPath(string path, string mode)
        {
            if (mode == "in")
            {
                if (!ExistingFile(path))
                    return false;
                else
                    return true;
            }
            else if (mode == "out")
            {
                if (!CorrectPath(path))
                    return false;

                string[] tmp = path.Split("/");
                string[] tmp2 = path.Split("\\");
                string name = tmp[^1];
                string name2 = tmp2[^1];

                bool test1 = CorrectFileName(name);
                bool test2 = CorrectFileName(name2);

                return test1 || test2;
            }
            else
            {
                throw new ArgumentException("'mode' should be 'in' or 'out'", nameof(mode));
            }
        }
    }
}