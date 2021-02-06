// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptage
{
    /// <summary>
    /// The main class of the program
    /// </summary>
    public class Program : Form
    {

        // Metadata info
        private bool _codeAndTranslate;   //
        private Util.WrittingType _type;
        private string? _inputDir;       //
        private string? _outputDir;      //
        private bool _keepInvalidChar;   //
        private int? _cesarKey;
        private Util.TypeC _algorithm;      //
        
        // Specific Metadata infos
        private bool _aeroUseInt;
        private bool _cesarLeftToRight;
        private string? _vigenereKey;
        
        // Other
        private  static Util.Error? _error;
        

        public Program()
        {
            InitialiseUi();
            InitialiseMeta();
        }
        
        
        // Init methods

        /// <summary>
        /// Initialize all the component of the class.
        /// </summary>
        private void InitialiseMeta()
        {
            _codeAndTranslate = false;
            _type = Util.WrittingType.NORMAL;
            _keepInvalidChar = true;
            _cesarKey = 0;
            _algorithm = Util.TypeC.CESAR;
            _aeroUseInt = true;
            _cesarLeftToRight = true;
            _inputDir = null;
            _outputDir = null;
            _error = null;
            _vigenereKey = null;
        }


        /// <summary>
        /// Initialize the layout of the window.
        /// </summary>
        private void InitialiseUi()
        {
            Text = "Cryptage";
            
            
            // menus
            var ms = new MenuStrip();
            ms.Parent = this;
            
            
            // menus -> fichier
            var fileMenuItem = new ToolStripMenuItem("&Fichier");
            var aideFileMenuItem = new ToolStripMenuItem("&Aide", null, (_,_) => AfficheAide());
            var exitFileMenuItem = new ToolStripMenuItem("&Quitter", null, (_, _) => Close());

            aideFileMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            exitFileMenuItem.ShortcutKeys = Keys.Control | Keys.X;

            fileMenuItem.DropDownItems.Add(aideFileMenuItem);
            fileMenuItem.DropDownItems.Add(exitFileMenuItem);

            
            // menus -> options
            var optionMenuItem = new ToolStripMenuItem("&Options");
            // spec chars
            var specCharOptionMenuItem = new ToolStripMenuItem("&Caracteres non-traduisibles");
            var keepSpecCharOptionsMenuItem = new ToolStripMenuItem("&Conserver");
            var nokeepSpecCharOptionsMenuItem = new ToolStripMenuItem("&Ignorer");
            keepSpecCharOptionsMenuItem.Checked = true;
            nokeepSpecCharOptionsMenuItem.Checked = false;
            keepSpecCharOptionsMenuItem.Click += (_,_) => SetSpecChar(true, keepSpecCharOptionsMenuItem, nokeepSpecCharOptionsMenuItem);
            nokeepSpecCharOptionsMenuItem.Click += (_,_) => SetSpecChar(false, keepSpecCharOptionsMenuItem, nokeepSpecCharOptionsMenuItem);
            // mise en page
            var layoutOptionMenuItem = new ToolStripMenuItem("&Mise en page");
            var normalLayoutOptionMenuItem = new ToolStripMenuItem("&Normale");
            var mpmLayoutOptionMenuItem = new ToolStripMenuItem("&Mot par mot");
            normalLayoutOptionMenuItem.Checked = true;
            mpmLayoutOptionMenuItem.Checked = false;
            normalLayoutOptionMenuItem.Click += (_,_) => SetLineSeparator(Util.WrittingType.NORMAL, normalLayoutOptionMenuItem, mpmLayoutOptionMenuItem);
            mpmLayoutOptionMenuItem.Click += (_,_) => SetLineSeparator(Util.WrittingType.WORD_BY_WORD, normalLayoutOptionMenuItem, mpmLayoutOptionMenuItem);
            // format
            var outputLayoutOptionsMenuItem = new ToolStripMenuItem("&Format");
            var codeOutputLayoutOptionsMenuItem = new ToolStripMenuItem("&Traduction uniquement");
            var origOutputLayoutOptionsMenuItem =  new ToolStripMenuItem("&Original et traduction");
            codeOutputLayoutOptionsMenuItem.Checked = true;
            origOutputLayoutOptionsMenuItem.Checked = false;
            codeOutputLayoutOptionsMenuItem.Click += (_, _) => SetCodeAndTranslate(false, codeOutputLayoutOptionsMenuItem, origOutputLayoutOptionsMenuItem);
            origOutputLayoutOptionsMenuItem.Click += (_,_) => SetCodeAndTranslate(true, codeOutputLayoutOptionsMenuItem, origOutputLayoutOptionsMenuItem);

            outputLayoutOptionsMenuItem.DropDownItems.Add(codeOutputLayoutOptionsMenuItem);
            outputLayoutOptionsMenuItem.DropDownItems.Add(origOutputLayoutOptionsMenuItem);

            layoutOptionMenuItem.DropDownItems.Add(normalLayoutOptionMenuItem);
            layoutOptionMenuItem.DropDownItems.Add(mpmLayoutOptionMenuItem);

            specCharOptionMenuItem.DropDownItems.Add(keepSpecCharOptionsMenuItem);
            specCharOptionMenuItem.DropDownItems.Add(nokeepSpecCharOptionsMenuItem);

            optionMenuItem.DropDownItems.Add(specCharOptionMenuItem);
            optionMenuItem.DropDownItems.Add(layoutOptionMenuItem);
            optionMenuItem.DropDownItems.Add(outputLayoutOptionsMenuItem);
            
            
            // menus -> infos
            var infoMenuItem = new ToolStripMenuItem("&Infos");
            var cesarInfoMenuItem = new ToolStripMenuItem("&Code de César", null, (_,_) => ShowCesar());
            var morseInfoMenuItem = new ToolStripMenuItem("&Code Morse", null, (_, _) => ShowMorse());
            var aeroInfoMenuItem = new ToolStripMenuItem("&Code Aéronautique", null, (_, _) => ShowAero());
            var navajoInfoMenuItem = new ToolStripMenuItem("&Code Navajo", null, (_, _) => ShowNavajo());
            var vigenereInfoMenuItem = new ToolStripMenuItem("&Clé de Vigenère", null, (_, _) => ShowVigenere());
            var binaireInfoMenuItem = new ToolStripMenuItem("&Code binaire", null, (_, _) => ShowBinaire());

            infoMenuItem.DropDownItems.Add(cesarInfoMenuItem);
            infoMenuItem.DropDownItems.Add(morseInfoMenuItem);
            infoMenuItem.DropDownItems.Add(aeroInfoMenuItem);
            infoMenuItem.DropDownItems.Add(navajoInfoMenuItem);
            infoMenuItem.DropDownItems.Add(vigenereInfoMenuItem);
            infoMenuItem.DropDownItems.Add(binaireInfoMenuItem);

            // menus -> a propos
            var whatMenuItem = new ToolStripMenuItem("&?");
            var aboutWhatMenuItem = new ToolStripMenuItem("A propos", null, (_, _) => ShowAboutInfo());
            var vigenereTable = new ToolStripMenuItem("&Table de Vigenère", null, (_, _) => ShowVigTable());

            whatMenuItem.DropDownItems.Add(vigenereTable);
            whatMenuItem.DropDownItems.Add(aboutWhatMenuItem);

            ms.Items.Add(fileMenuItem);
            ms.Items.Add(optionMenuItem);
            ms.Items.Add(infoMenuItem);
            ms.Items.Add(whatMenuItem);
            
            
            // CheckBoxes
            var cesarCheckBox = new CheckBox();
            cesarCheckBox.Appearance = Appearance.Normal;
            cesarCheckBox.AutoCheck = true;
            cesarCheckBox.Visible = true;
            cesarCheckBox.FlatStyle = FlatStyle.System;
            cesarCheckBox.Checked = true;
            cesarCheckBox.Text = "César";


            var morseCheckBox = new CheckBox();
            morseCheckBox.Appearance = Appearance.Normal;
            morseCheckBox.AutoCheck = true;
            morseCheckBox.Visible = true;
            morseCheckBox.FlatStyle = FlatStyle.System;
            morseCheckBox.Checked = false;
            morseCheckBox.Text = "Morse";

            var aeroCheckBox = new CheckBox();
            aeroCheckBox.Appearance = Appearance.Normal;
            aeroCheckBox.AutoCheck = true;
            aeroCheckBox.Visible = true;
            aeroCheckBox.FlatStyle = FlatStyle.System;
            aeroCheckBox.Checked = false;
            aeroCheckBox.Text = "Aéro";

            var navCheckBox = new CheckBox();
            navCheckBox.Appearance = Appearance.Normal;
            navCheckBox.AutoCheck = true;
            navCheckBox.Visible = true;
            navCheckBox.FlatStyle = FlatStyle.System;
            navCheckBox.Checked = false;
            navCheckBox.Text = "Navajo";

            var vigCheckBox = new CheckBox();
            vigCheckBox.Appearance = Appearance.Normal;
            vigCheckBox.AutoCheck = true;
            vigCheckBox.Visible = true;
            vigCheckBox.FlatStyle = FlatStyle.System;
            vigCheckBox.Checked = false;
            vigCheckBox.Text = "Vigenère";

            var binCheckBox = new CheckBox();
            binCheckBox.Appearance = Appearance.Normal;
            binCheckBox.AutoCheck = true;
            binCheckBox.Visible = true;
            binCheckBox.FlatStyle = FlatStyle.System;
            binCheckBox.Checked = false;
            binCheckBox.Text = "Binaire";

            cesarCheckBox.Click += (_, _) => CesarChecked(cesarCheckBox, aeroCheckBox, morseCheckBox, navCheckBox, vigCheckBox, binCheckBox);
            morseCheckBox.Click += (_, _) => MorseChecked(morseCheckBox, cesarCheckBox, aeroCheckBox, navCheckBox, vigCheckBox, binCheckBox);
            aeroCheckBox.Click += (_, _) => AeroChecked(aeroCheckBox, cesarCheckBox, morseCheckBox, navCheckBox, vigCheckBox, binCheckBox);
            navCheckBox.Click += (_, _) => NavajoChecked(navCheckBox, cesarCheckBox, morseCheckBox, aeroCheckBox, vigCheckBox, binCheckBox);
            vigCheckBox.Click += (_, _) => VigenereChecked(vigCheckBox, cesarCheckBox, aeroCheckBox, morseCheckBox, navCheckBox, binCheckBox);
            binCheckBox.Click += (_,_) => BinaireChecked(binCheckBox, cesarCheckBox, aeroCheckBox, morseCheckBox, navCheckBox, vigCheckBox);

            var flowPanel = new FlowLayoutPanel();
            flowPanel.Location = new Point(10, 30);  // 300 ; 30
            flowPanel.Size = new Size(230, 120); // 230 ; 70
            flowPanel.BorderStyle = BorderStyle.FixedSingle;
            
            var checkBoxTextIndicator = new Label();
            checkBoxTextIndicator.Text = "  Algorithme de cryptage:";
            checkBoxTextIndicator.Font = new Font("Verdana", 9, FontStyle.Bold);
            checkBoxTextIndicator.Size = new Size(280, 30);
            
            

            // Cesar key text box
            var cesarKeyBox = new TextBox();
            cesarKeyBox.Multiline = false;
            cesarKeyBox.AutoCompleteMode = AutoCompleteMode.None;
            cesarKeyBox.TextChanged += CesarKeyBoxOnTextChanged;
            cesarKeyBox.Enabled = true;
            cesarKeyBox.MaxLength = 10;
            cesarKeyBox.Text = "0";

            var cesarKeyBoxText = new Label();
            cesarKeyBoxText.Text = "Clé de chiffrement (code césar)";
            cesarKeyBoxText.Visible = true;
            cesarKeyBoxText.Font = new Font("Verdana", 9, FontStyle.Bold);
            cesarKeyBoxText.Size = new Size(180, 40);

            var flowPanelKey = new FlowLayoutPanel();
            flowPanelKey.Location = new Point(250, 40);
            flowPanelKey.Size = new Size(260, 65); // 260
            flowPanelKey.BorderStyle = BorderStyle.FixedSingle;
            flowPanelKey.AutoSize = true;

            cesarCheckBox.Click += (_, _) => CesarKeyBoxSetEnabled(cesarKeyBox);
            aeroCheckBox.Click += (_, _) => CesarKeyBoxSetDisabled(cesarKeyBox);
            morseCheckBox.Click += (_, _) => CesarKeyBoxSetDisabled(cesarKeyBox);
            navCheckBox.Click += (_, _) => CesarKeyBoxSetDisabled(cesarKeyBox);
            vigCheckBox.Click += (_, _) => CesarKeyBoxSetDisabled(cesarKeyBox);
            binCheckBox.Click += (_, _) => CesarKeyBoxSetDisabled(cesarKeyBox);
            
            
            // Cesar mode check boxes
            var cesarModeNormalCheckBox = new CheckBox();
            cesarModeNormalCheckBox.Appearance = Appearance.Normal;
            cesarModeNormalCheckBox.AutoCheck = true;
            cesarModeNormalCheckBox.Visible = true;
            cesarModeNormalCheckBox.Text = "normale";
            cesarModeNormalCheckBox.Checked = true;

            var cesarModeInvCheckBox = new CheckBox();
            cesarModeInvCheckBox.Appearance = Appearance.Normal;
            cesarModeInvCheckBox.AutoCheck = true;
            cesarModeInvCheckBox.Visible = true;
            cesarModeInvCheckBox.Text = "inversée";
            cesarModeInvCheckBox.Checked = false;

            cesarModeNormalCheckBox.Click +=
                (_, _) => CesarModeNormalChecked(cesarModeNormalCheckBox, cesarModeInvCheckBox);
            cesarModeInvCheckBox.Click += (_, _) => CesarModeInvChecked(cesarModeInvCheckBox, cesarModeNormalCheckBox);

            cesarCheckBox.Click += (_,_) => CesarModeSetEnabled(cesarModeNormalCheckBox, cesarModeInvCheckBox, true);
            aeroCheckBox.Click += (_, _) => CesarModeSetEnabled(cesarModeNormalCheckBox, cesarModeInvCheckBox, false);
            morseCheckBox.Click += (_, _) => CesarModeSetEnabled(cesarModeNormalCheckBox, cesarModeInvCheckBox, false);
            navCheckBox.Click += (_, _) => CesarModeSetEnabled(cesarModeNormalCheckBox, cesarModeInvCheckBox, false);
            vigCheckBox.Click += (_, _) => CesarModeSetEnabled(cesarModeNormalCheckBox, cesarModeInvCheckBox, false);
            binCheckBox.Click += (_, _) => CesarModeSetEnabled(cesarModeNormalCheckBox, cesarModeInvCheckBox, false);
            
            
            // Aero standard check boxes
            var aeroStdIntCheckBox = new CheckBox();
            aeroStdIntCheckBox.Appearance = Appearance.Normal;
            aeroStdIntCheckBox.AutoCheck = true;
            aeroStdIntCheckBox.Visible = true;
            aeroStdIntCheckBox.Text = "international";
            aeroStdIntCheckBox.Size = new Size(120, 30);
            aeroStdIntCheckBox.Checked = true;
            aeroStdIntCheckBox.Enabled = false;

            var aeroStdFrCheckBox = new CheckBox();
            aeroStdFrCheckBox.Appearance = Appearance.Normal;
            aeroStdFrCheckBox.AutoCheck = true;
            aeroStdFrCheckBox.Visible = true;
            aeroStdFrCheckBox.Text = "francais";
            aeroStdFrCheckBox.Size = new Size(120, 30);
            aeroStdFrCheckBox.Checked = false;
            aeroStdFrCheckBox.Enabled = false;

            var aeroStdText = new Label();
            aeroStdText.Text = "Standart (code aero)";
            aeroStdText.Font = new Font("Verdana", 9, FontStyle.Bold);
            aeroStdText.Size = new Size(400, 30);


            var flowPanelAeroStd = new FlowLayoutPanel();
            flowPanelAeroStd.BorderStyle = BorderStyle.FixedSingle;
            flowPanelAeroStd.Location = new Point(250, 120);
            flowPanelAeroStd.Size = new Size(514, 65);

            aeroStdIntCheckBox.Click += (_, _) => AeroStdIntChecked(aeroStdIntCheckBox, aeroStdFrCheckBox);
            aeroStdFrCheckBox.Click += (_, _) => AeroStdFrChecked(aeroStdFrCheckBox, aeroStdIntCheckBox);

            cesarCheckBox.Click += (_, _) => AeroStdSetEnabled(aeroStdIntCheckBox, aeroStdFrCheckBox, false);
            aeroCheckBox.Click += (_, _) => AeroStdSetEnabled(aeroStdIntCheckBox, aeroStdFrCheckBox, true);
            morseCheckBox.Click += (_, _) => AeroStdSetEnabled(aeroStdIntCheckBox, aeroStdFrCheckBox, false);
            navCheckBox.Click += (_, _) => AeroStdSetEnabled(aeroStdIntCheckBox, aeroStdFrCheckBox, false);
            vigCheckBox.Click += (_, _) => AeroStdSetEnabled(aeroStdIntCheckBox, aeroStdFrCheckBox, false);
            binCheckBox.Click += (_, _) => AeroStdSetEnabled(aeroStdIntCheckBox, aeroStdFrCheckBox, false);
            
            
            // Input-Output boxes
            var inputBoxText = new TextBox();
            inputBoxText.Multiline = false;
            inputBoxText.TextAlign = HorizontalAlignment.Left;
            inputBoxText.Width = 450;
            inputBoxText.TextChanged += GetInputBoxText;

            var outputBoxText = new TextBox();
            outputBoxText.Multiline = false;
            outputBoxText.TextAlign = HorizontalAlignment.Left;
            outputBoxText.Width = 450;
            outputBoxText.TextChanged += GetOutputBoxText;

            var inputBoxTextDescription = new Label();
            inputBoxTextDescription.Text = "Fichier source:";
            inputBoxTextDescription.Font = new Font("Verdana", 9, FontStyle.Bold);
            inputBoxTextDescription.Size = new Size(280, 30);

            var outputBoxTextDescription = new Label();
            outputBoxTextDescription.Text = "Fichier cible:";
            outputBoxTextDescription.Font = new Font("Verdana", 9, FontStyle.Bold);
            outputBoxTextDescription.Size = new Size(280, 30);

            var inputBoxButton = new Button();
            inputBoxButton.Text = "...";
            inputBoxButton.Size = new Size(30, 28);
            inputBoxButton.Click += (_, _) => OpenFileDialogInput(inputBoxText);

            var outputBoxButton = new Button();
            outputBoxButton.Text = "...";
            outputBoxButton.Size = new Size(30, 28);
            outputBoxButton.Click += (_, _) => OpenFileDialogOutput(outputBoxText);

            var flowPanelPath = new FlowLayoutPanel();
            flowPanelPath.Location = new Point(10, 300);
            flowPanelPath.Size = new Size(500, 135); // 450;135
            flowPanelPath.BorderStyle = BorderStyle.FixedSingle;
            
            
            // Crypt buttons
            var crypterButton = new Button();
            crypterButton.Text = "Crypter";
            crypterButton.Font = new Font("Verdana", 20, FontStyle.Bold | FontStyle.Italic);
            crypterButton.Size = new Size(220, 66);
            crypterButton.Click += (_, _) => Crypt();

            var decrypterButton = new Button();
            decrypterButton.Text = "Décrypter";
            decrypterButton.Font = new Font("Verdana", 20, FontStyle.Bold | FontStyle.Italic);
            decrypterButton.Size = new Size(220, 66);
            decrypterButton.Click += (_, _) => Decrypt();

            var flowPanelCrypt = new FlowLayoutPanel();
            flowPanelCrypt.Location = new Point(530, 298);
            flowPanelCrypt.Size = new Size(230, 150);
            flowPanelCrypt.BorderStyle = BorderStyle.None;
            
            // Vigenere input box
            var vigTextBoxDesc = new Label();
            vigTextBoxDesc.Text = "Mot de passe (clé de vigenère)";
            vigTextBoxDesc.Font = new Font("Verdana", 9, FontStyle.Bold);
            vigTextBoxDesc.Width = 280;

            var vigTextBox = new TextBox();
            vigTextBox.Multiline = false;
            vigTextBox.TextAlign = HorizontalAlignment.Left;
            vigTextBox.Width = 330;
            vigTextBox.Enabled = false;
            vigTextBox.TextChanged += VigenereKeyBoxOnTextChanged;


            var flowPanelVigKey = new FlowLayoutPanel();
            flowPanelVigKey.Size = new Size(514, 65);
            flowPanelVigKey.Location = new Point(250, 200);
            flowPanelVigKey.BorderStyle = BorderStyle.FixedSingle;

            cesarCheckBox.Click += (_, _) => VigenereKeyBoxSetDisabled(vigTextBox);
            aeroCheckBox.Click += (_, _) => VigenereKeyBoxSetDisabled(vigTextBox);
            morseCheckBox.Click += (_, _) => VigenereKeyBoxSetDisabled(vigTextBox);
            navCheckBox.Click += (_, _) => VigenereKeyBoxSetDisabled(vigTextBox);
            vigCheckBox.Click += (_, _) => VigenereKeyBoxSetEnabled(vigTextBox);
            binCheckBox.Click += (_, _) => VigenereKeyBoxSetDisabled(vigTextBox);
            
            // logo
            PictureBox pic = new PictureBox();
            pic.Size = new Size(130, 119);
            pic.Location = new Point(50, 160);

            Bitmap img = new Bitmap("./assets/logo.bmp");
            pic.Image = img;
            
            
            // flowPanels add
            flowPanel.Controls.Add(checkBoxTextIndicator);
            flowPanel.Controls.Add(cesarCheckBox);
            flowPanel.Controls.Add(morseCheckBox);
            flowPanel.Controls.Add(aeroCheckBox);
            flowPanel.Controls.Add(navCheckBox);
            flowPanel.Controls.Add(vigCheckBox);
            flowPanel.Controls.Add(binCheckBox);
            
            flowPanelKey.Controls.Add(cesarKeyBoxText);
            flowPanelKey.Controls.Add(cesarKeyBox);
            flowPanelKey.Controls.Add(cesarModeNormalCheckBox);
            flowPanelKey.Controls.Add(cesarModeInvCheckBox);
            
            flowPanelAeroStd.Controls.Add(aeroStdText);
            flowPanelAeroStd.Controls.Add(aeroStdIntCheckBox);
            flowPanelAeroStd.Controls.Add(aeroStdFrCheckBox);
            
            flowPanelPath.Controls.Add(inputBoxTextDescription);
            flowPanelPath.Controls.Add(inputBoxText);
            flowPanelPath.Controls.Add(inputBoxButton);
            flowPanelPath.Controls.Add(outputBoxTextDescription);
            flowPanelPath.Controls.Add(outputBoxText);
            flowPanelPath.Controls.Add(outputBoxButton);
            
            flowPanelCrypt.Controls.Add(crypterButton);
            flowPanelCrypt.Controls.Add(decrypterButton);
            
            flowPanelVigKey.Controls.Add(vigTextBoxDesc);
            flowPanelVigKey.Controls.Add(vigTextBox);
            
            
            Controls.Add(flowPanel);
            Controls.Add(flowPanelKey);
            Controls.Add(flowPanelAeroStd);
            Controls.Add(flowPanelPath);
            Controls.Add(flowPanelCrypt);
            Controls.Add(flowPanelVigKey);
            Controls.Add(pic);

            ClientSize = new Size(780, 450);
            CenterToScreen();
            FormBorderStyle = FormBorderStyle.Fixed3D;

            Icon = new Icon("./assets/logo.ico");
        }
        


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        

        // Metadata updaters
        
        /// <summary>
        /// Set if we keep the special characters in the output file or not
        /// </summary>
        /// <param name="keepSpecChar">A <see cref="bool"/> which indicate if we keep the special characters or not.</param>
        /// <param name="cons">The <see cref="ToolStripMenuItem"/> which refers to the "keep the special characters" option.</param>
        /// <param name="ign">The <see cref="ToolStripMenuItem"/> which refers to the "do not keep the special characters" option.</param>
        private void SetSpecChar(bool keepSpecChar, ToolStripMenuItem cons, ToolStripMenuItem ign)
        {
            this._keepInvalidChar = keepSpecChar;

            if (_keepInvalidChar)
            {
                cons.Checked = true;
                ign.Checked = false;
            }
            else
            {
                cons.Checked = false;
                ign.Checked = true;
            }
        }

        /// <summary>
        /// Set the type of writing we will use for the output file.
        /// <value>NORMAL</value> for writing several words per line
        /// <value>WORD_BY_WORD</value> for writing one word per line. 
        /// </summary>
        /// <param name="type">A <see cref="Util.WrittingType"/> indicating the type of writing we will use.</param>
        /// <param name="normal">The <see cref="ToolStripMenuItem"/> which refers to the "normal writing" option.</param>
        /// <param name="wbw">The <see cref="ToolStripMenuItem"/> which refers to the "one word per line" options.</param>
        private void SetLineSeparator(Util.WrittingType type, ToolStripMenuItem normal, ToolStripMenuItem wbw)
        {
            this._type = type;

            if (_type == Util.WrittingType.NORMAL)
            {
                normal.Checked = true;
                wbw.Checked = false;
            }
            else
            {
                normal.Checked = false;
                wbw.Checked = true;
            }
        }

        /// <summary>
        /// Set the type of layout for the output file.
        /// </summary>
        /// <param name="codeAndTranslate">A <see cref="bool"/> indicating the type of layout.
        /// <see langword="true"/> will write the source text and its translation,
        /// <see langword="false"/> will write only the translation.</param>
        /// <param name="code">The <see cref="ToolStripMenuItem"/> which refers to the "translation only" option.</param>
        /// <param name="orig">The <see cref="ToolStripMenuItem"/> which refers to the "original text and translation" option.</param>
        private void SetCodeAndTranslate(bool codeAndTranslate, ToolStripMenuItem code, ToolStripMenuItem orig)
        {
            _codeAndTranslate = codeAndTranslate;

            if (_codeAndTranslate)
            {
                orig.Checked = true;
                code.Checked = false;
            }
            else
            {
                orig.Checked = false;
                code.Checked = true;
            }
        }

        /// <summary>
        /// Set an error if it happened during translation.
        /// </summary>
        /// <param name="error">An <see cref="Util.Error"/> containing the details of the error.</param>
        public static void SetError(Util.Error error)
        { 
            _error = error;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        // About windows
        
        /// <summary>
        /// Shows the Cesar code about window.
        /// </summary>
        private void ShowCesar()
        {
            AboutWindow about = new AboutWindow(Util.TypeC.CESAR);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the Morse code about window.
        /// </summary>
        private void ShowMorse()
        {
            AboutWindow about = new AboutWindow(Util.TypeC.MORSE);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the aeronautic code about window.
        /// </summary>
        private void ShowAero()
        {
            AboutWindow about = new AboutWindow(Util.TypeC.AERO);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the Navajo code about window.
        /// </summary>
        private void ShowNavajo()
        {
            AboutWindow about = new AboutWindow(Util.TypeC.NAVAJO);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the Vignere key about window.
        /// </summary>
        private void ShowVigenere()
        {
            AboutWindow about = new AboutWindow(Util.TypeC.VIGENERE);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the binary code about window.
        /// </summary>
        private void ShowBinaire()
        {
            AboutWindow about = new AboutWindow(Util.TypeC.BINAIRE);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows a window containing information about the version of this program and some advice to use it properly.
        /// </summary>
        private void ShowAboutInfo()
        {
            AboutWindow about = new AboutWindow(null);
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the help window.
        /// </summary>
        private void AfficheAide()
        {
            HelpWindow hw = new HelpWindow();
            hw.ShowDialog();
        }

        private void ShowVigTable()
        {
            TableVigenere table = new TableVigenere();
            table.ShowDialog();
        }
        
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        // Metadata checker
        
        /// <summary>
        /// Checks if the translation is ready to be launched
        /// </summary>
        /// <returns><see langword="true"/> if all the metadata are correct, <see langword="false"/> otherwise.</returns>
        private bool IsReady()
        {
            if (_inputDir is null || _outputDir is null)
                return false;


            if (!PathChecker.CheckPath(_inputDir, "in"))
            {
                MessageBox.Show($"L'addresse {_inputDir} est invalide, ou le fichier spécifié est introuvable.", "Erreur : fichier source incorrect" , MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (!PathChecker.CheckPath(_outputDir, "out"))
            {
                MessageBox.Show($"L'addresse {_outputDir} est invalide.", "Erreur : fichier cible incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_algorithm == Util.TypeC.CESAR && _cesarKey == null)
                return false;

            if (_algorithm == Util.TypeC.VIGENERE && _vigenereKey == null)
                return false;
            
            return true;
        }
        

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////


        // Crypto methods
        
        /// <summary>
        /// Launch the coding of the input file.
        /// </summary>
        private void Crypt()
        {
            
            if (!IsReady())
                return;


            if (_inputDir != null && _outputDir != null)
            {
                Util.MetaData met = new Util.MetaData(_inputDir, _outputDir, Util.State.CODE, 
                    _keepInvalidChar, _codeAndTranslate, _type, _cesarKey);

                ICrypt crypt = (_algorithm == Util.TypeC.AERO) ? new Aero(met, _aeroUseInt)
                    : (_algorithm == Util.TypeC.CESAR) ? new Cesar(met, _cesarLeftToRight)
                    : (_algorithm == Util.TypeC.MORSE) ? new Morse(met)
                    : (_algorithm == Util.TypeC.NAVAJO) ? new Navajo(met)
                    : (_algorithm == Util.TypeC.VIGENERE) ? new Vigenere(met, _vigenereKey)
                    : new Binaire(met);


                crypt.Translate();
            }
            else
            {
                string message = "Erreur: paramètre inexistant, veuillez entrer une valeur.";
                string source = (_inputDir is null) ? nameof(_inputDir) : nameof(_outputDir);
                string details = $"Error in Program.Crypt method: {source} should not be null.";
                Util.Error err = new Util.Error(message, source, details);
                _error = err;
            }

            CryptoStatusCheck(Util.State.CODE);
        }

        /// <summary>
        /// Launch the decoding of the input file.
        /// </summary>
        private void Decrypt()
        {
            if (!IsReady())
                return;

            if (_inputDir != null && _outputDir != null)
            {
                Util.MetaData met = new Util.MetaData(_inputDir, _outputDir, Util.State.DECODE, _keepInvalidChar,
                    _codeAndTranslate, _type, _cesarKey);

                ICrypt crypt = (_algorithm == Util.TypeC.AERO) ? new Aero(met, _aeroUseInt)
                    : (_algorithm == Util.TypeC.CESAR) ? new Cesar(met, _cesarLeftToRight)
                    : (_algorithm == Util.TypeC.MORSE) ? new Morse(met)
                    : (_algorithm == Util.TypeC.NAVAJO) ? new Navajo(met)
                    : (_algorithm == Util.TypeC.VIGENERE) ? new Vigenere(met, _vigenereKey)
                    : new Binaire(met);    

                crypt.Translate();
            }
            else
            {
                string message = "Erreur: paramètre inexistant, veuillez entrer une valeur.";
                string source = (_inputDir is null) ? nameof(_inputDir) : nameof(_outputDir);
                string details = $"Error in Program.Decrypt method: {source} should not be null.";
                Util.Error err = new Util.Error(message, source, details);
                _error = err;
            }

            CryptoStatusCheck(Util.State.DECODE);
        }

        /// <summary>
        /// Checks if an error occurs during translation.
        /// If not, displays a message indicating that the translation is done,
        /// else displays an error message indicating the problem that occurs during translation.
        /// </summary>
        /// <param name="s">A <see cref="Util.State"/> indicating if the program is actually coding or decoding the input file.</param>
        private void CryptoStatusCheck(Util.State s)
        {
            if (_error is not null)
            {
                MessageBox.Show(_error.ToString(), "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _error = null;
            }
            else
            {
                string message = (s == Util.State.CODE) ? "Cryptage" : "Decryptage";
                message += " terminé !";

                MessageBox.Show(message, "Fin de l'opération", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // Checkboxes methods
        private void CesarChecked(CheckBox sender, CheckBox aero, CheckBox morse, CheckBox nav, CheckBox vig, CheckBox bin)
        {
            aero.Checked = false;
            morse.Checked = false;
            nav.Checked = false;
            vig.Checked = false;
            bin.Checked = false;

            _algorithm = Util.TypeC.CESAR;

            if (!sender.Checked)
                sender.Checked = true;
            
        }

        private void AeroChecked(CheckBox sender,CheckBox cesar, CheckBox morse, CheckBox nav, CheckBox vig, CheckBox bin)
        {
            cesar.Checked = false;
            morse.Checked = false;
            nav.Checked = false;
            vig.Checked = false;
            bin.Checked = false;

            _algorithm = Util.TypeC.AERO;

            if (!sender.Checked)
                sender.Checked = true;
        }

        private void MorseChecked(CheckBox sender,CheckBox cesar, CheckBox aero, CheckBox nav, CheckBox vig, CheckBox bin)
        {
            cesar.Checked = false;
            aero.Checked = false;
            nav.Checked = false;
            vig.Checked = false;
            bin.Checked = false;

            _algorithm = Util.TypeC.MORSE;

            if (!sender.Checked)
                sender.Checked = true;
        }

        private void NavajoChecked(CheckBox sender, CheckBox cesar, CheckBox aero, CheckBox nav, CheckBox vig, CheckBox bin)
        {
            cesar.Checked = false;
            aero.Checked = false;
            nav.Checked = false;
            vig.Checked = false;
            bin.Checked = false;

            _algorithm = Util.TypeC.NAVAJO;

            if (!sender.Checked)
                sender.Checked = true;
        }

        private void VigenereChecked(CheckBox sender, CheckBox cesar, CheckBox aero, CheckBox morse, CheckBox nav, CheckBox bin)
        {
            cesar.Checked = false;
            aero.Checked = false;
            nav.Checked = false;
            morse.Checked = false;
            bin.Checked = false;

            _algorithm = Util.TypeC.VIGENERE;

            if (!sender.Checked)
                sender.Checked = true;
        }

        private void BinaireChecked(CheckBox sender, CheckBox cesar, CheckBox aero, CheckBox morse, CheckBox nav, CheckBox vig)
        {
            cesar.Checked = false;
            aero.Checked = false;
            morse.Checked = false;
            nav.Checked = false;
            vig.Checked = false;

            _algorithm = Util.TypeC.BINAIRE;

            if (!sender.Checked)
                sender.Checked = true;
        }
        
        private void CesarModeNormalChecked(CheckBox sender, CheckBox inv)
        {
            sender.Checked = true;
            inv.Checked = false;

            _cesarLeftToRight = true;
        }

        private void CesarModeInvChecked(CheckBox sender, CheckBox norm)
        {
            sender.Checked = true;
            norm.Checked = false;

            _cesarLeftToRight = false;
        }

        private void CesarModeSetEnabled(CheckBox normal, CheckBox inv, bool enabled)
        {
            if (enabled)
            {
                normal.Enabled = true;
                inv.Enabled = true;
            }
            else
            {
                normal.Enabled = false;
                inv.Enabled = false;
            }
        }

        private void AeroStdFrChecked(CheckBox sender, CheckBox inte)
        {
            sender.Checked = true;
            inte.Checked = false;

            _aeroUseInt = false;
        }

        private void AeroStdIntChecked(CheckBox sender, CheckBox fr)
        {
            sender.Checked = true;
            fr.Checked = false;

            _aeroUseInt = true;
        }

        private void AeroStdSetEnabled(CheckBox inte, CheckBox fr, bool enabled)
        {
            if (enabled)
            {
                inte.Enabled = true;
                fr.Enabled = true;
            }
            else
            {
                inte.Enabled = false;
                fr.Enabled = false;
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        // Text boxes methods
        
        /// <summary>
        /// Convert the text written on the <see cref="TextBox"/> 'cesarKeyBox' and if possible,
        /// convert it into an integer and put the result value in <see cref="_cesarKey"/>.
        /// </summary>
        private void CesarKeyBoxOnTextChanged(object? sender, EventArgs e)
        {
            var t = sender as TextBox;
            if (t is not null)
            {
                string s = t.Text;
                try
                {
                    int res = Int32.Parse(s);
                    if (res < 0)
                    {
                        res = 0;
                        t.Text = "0";
                    }

                    _cesarKey = res;
                }
                catch (OverflowException)
                {
                    _cesarKey = Int32.MaxValue;
                    t.Text = Int32.MaxValue.ToString();
                }
                catch (Exception)
                {
                    //_cesarKey = null;
                    _cesarKey = 0;
                    t.Text = "0";
                }
            }
        }

        /// <summary>
        /// Enable the <see cref="TextBox"/> 'cesarKeyBox' when the mode 'cesar' is set.
        /// </summary>
        private void CesarKeyBoxSetEnabled(object? sender)
        {
            switch (sender)
            {
                case TextBox k:
                    k.Enabled = true;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Disable the <see cref="TextBox"/> 'cesarKeyBox' when another mode is set.
        /// </summary>
        private void CesarKeyBoxSetDisabled(object? sender)
        {
            switch (sender)
            {
                case TextBox k:
                    k.Enabled = false;
                    _cesarKey = null;
                    break;
                default:
                    return;
            }
        }


        private void VigenereKeyBoxSetEnabled(object? sender)
        {
            switch (sender)
            {
                case TextBox k:
                    k.Enabled = true;
                    break;
                default:
                    return;
            }
        }

        private void VigenereKeyBoxSetDisabled(object? sender)
        {
            switch (sender)
            {
                case TextBox k:
                    k.Enabled = false;
                    break;
                default:
                    return;
            }
        }

        private void VigenereKeyBoxOnTextChanged(object? sender, EventArgs e)
        {
            var t = sender as TextBox;
            string new_text = "";

            List<string> dico = Util.DicoInit.Min();
            foreach (char c in t.Text)
            {
                foreach (string s in dico)
                {
                    if (s == c.ToString() || s.ToUpper() == c.ToString())
                        new_text += s;
                }
            }

            _vigenereKey = new_text;
            t.Text = new_text;

            //Console.Out.WriteLine(new_text);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////


        // Input / Output box methods
        
        /// <summary>
        /// Gets the text from the input file <see cref="TextBox"/> and put it in <see cref="_inputDir"/>.
        /// </summary>
        private void GetInputBoxText(object? sender, EventArgs e)
        {
            if (sender is not null)
            {
                var t = sender as TextBox;
                string? s = t?.Text;
                
                _inputDir = s;

                //Console.Out.WriteLine(_inputDir);
            }
        }

        
        /// <summary>
        /// Gets the text from the output file <see cref="TextBox"/> and put it in <see cref="_outputDir"/>.
        /// </summary>
        private void GetOutputBoxText(object? sender, EventArgs e)
        {
            if (sender is not null)
            {
                var t = sender as TextBox;
                string? s = t?.Text;

                _outputDir = s;

                //Console.Out.WriteLine(_outputDir);
            }
        }

        /// <summary>
        /// Opens an <see cref="OpenFileDialog"/> to select the input file and put the selected path in the input file <see cref="TextBox"/>.
        /// </summary>
        /// <param name="t">The input file <see cref="TextBox"/>.</param>
        private void OpenFileDialogInput(TextBox t)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.InitialDirectory = AppContext.BaseDirectory;
            o.Filter = "fichier texte (*.txt)|*.txt";
            o.FilterIndex = 2;
            o.RestoreDirectory = true;

            if (o.ShowDialog() == DialogResult.OK)
            {
                t.Text = o.FileName;
            }
        }

        /// <summary>
        /// Opens an <see cref="OpenFileDialog"/> to select the output file and put the selected path in the output file <see cref="TextBox"/>.
        /// </summary>
        /// <param name="t">The output file <see cref="TextBox"/>.</param>
        private void OpenFileDialogOutput(TextBox t)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.InitialDirectory = AppContext.BaseDirectory;
            o.Filter = "fichier texte (*.txt)|*.txt";
            o.FilterIndex = 2;
            o.RestoreDirectory = true;

            if (o.ShowDialog() == DialogResult.OK)
            {
                t.Text = o.FileName;
            }
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}