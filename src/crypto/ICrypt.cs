// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.Collections.Generic;

namespace Cryptage
{
    public interface ICrypt
    {
        /// <summary>
        /// Launch the translation of the file.
        /// </summary>
        public void Translate();

        /// <summary>
        /// Returns the progression of the translation process.
        /// </summary>
        /// <returns>An integer between 0 and 100</returns>
        public int GetProg();

        /// <summary>
        /// Allows us to know if the translation is done.
        /// </summary>
        /// <returns><value>true</value> if the translation is done, <value>false</value> otherwise.</returns>
        public bool GetState();

        /// <summary>
        /// Returns the result of the translation
        /// </summary>
        /// <returns>A List of string which contains the translated text</returns>
        public List<string> GetResult();

        
    }
}