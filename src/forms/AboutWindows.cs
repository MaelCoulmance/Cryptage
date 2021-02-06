// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.Drawing;
using System.Windows.Forms;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Cryptage
{
   /// <summary>
   /// Shows a new window containing information about crypto algorithms. 
   /// </summary>
   public class AboutWindow : Form
   {
      private static readonly Size AeroSize = new Size(605, 300);
      private static readonly Size CesarSize = new Size(565, 250);
      private static readonly Size MorseSize = new Size(580, 200);
      private static readonly Size NavajoSize = new Size(585, 230);
      private static readonly Size BinaireSize = new Size(710, 200);
      private static readonly Size AboutSize = new Size(710, 325);
      
      public AboutWindow(Util.TypeC? type)
      {
         InitUi(type);
      }

      private void InitUi(Util.TypeC? type)
      {
         string title = Util.TypeToString(type);
         title += " Infos";
         Text = title;


         ClientSize = SetSize(type);
         FormBorderStyle = FormBorderStyle.FixedSingle;

         string text = (type.Equals(Util.TypeC.AERO)) ? Ressources.AERO_ABOUT
            : (type.Equals(Util.TypeC.CESAR)) ? Ressources.CESAR_ABOUT
            : (type.Equals(Util.TypeC.MORSE)) ? Ressources.MORSE_ABOUT
            : (type.Equals(Util.TypeC.NAVAJO)) ? Ressources.NAVAJO_ABOUT
            : (type.Equals(Util.TypeC.VIGENERE)) ? Ressources.VIGENERE_ABOUT
            : (type.Equals(Util.TypeC.BINAIRE)) ? Ressources.BINAIRE_ABOUT
            : Ressources.PROGRAM_ABOUT;

         var font = new Font("Verdana", 9, FontStyle.Italic | FontStyle.Bold);

         var lyrics = new Label();
         lyrics.Parent = this;
         lyrics.Text = text;
         lyrics.Font = font;
         lyrics.AutoSize = true;
         lyrics.TextAlign = ContentAlignment.MiddleCenter;
         
         CenterToParent();
      }

      private Size SetSize(Util.TypeC? type)
      {
         return (type == Util.TypeC.AERO) ? AeroSize
            : (type == Util.TypeC.CESAR) ? CesarSize
            : (type == Util.TypeC.MORSE) ? MorseSize
            : (type == Util.TypeC.NAVAJO) ? NavajoSize
            : (type == Util.TypeC.BINAIRE) ? BinaireSize
            : AboutSize;
      }
   }
}