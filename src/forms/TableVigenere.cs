// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.Drawing;
using System.Windows.Forms;
using ContentAlignment = System.Drawing.ContentAlignment;
using System.Collections.Generic;

namespace Cryptage
{
    public class TableVigenere : Form
    {
        /// <summary>
        /// Shows a window containing an image of the Vigenere cipher
        /// </summary>
        public TableVigenere()
        {
            InitUI();
        }

        private void InitUI()
        {
            ClientSize = new Size(529, 700);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Table de Vigenère";

            PictureBox pic = new PictureBox();
            Bitmap img = new Bitmap("./assets/vigenere.bmp");
            

            pic.Image = img;
            pic.Size = new Size(529, 700);
            Controls.Add(pic);
            
            CenterToParent();
        }
    }
}