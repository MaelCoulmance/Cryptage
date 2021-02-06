// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.


using System;
using System.Windows.Forms;

namespace Cryptage
{
    public partial class HelpWindow : Form
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void HelpWindow_Load(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void layoutButton_Click(object sender, EventArgs e)
        {
            this.layoutButton.Visible = false;
            this.specialParameterButton.Visible = false;
            this.aideCryptageButton.Visible = false;

            this.TextLabel.Text = Ressources.HELP_MENU_LAYOUT1;
            this.TextLabel.Location = new System.Drawing.Point(133, 111);
            this.TextLabel.Visible = true;

            this.retourButton.Visible = true;
            this.suivantButton.Text = "suivant";
            this.suivantButton.Visible = true;
            this.isSuivant = true;

            IsSpecParam = false;
        }

        private void aideCryptageButton_Click(object sender, EventArgs e)
        {
            this.layoutButton.Visible = false;
            this.specialParameterButton.Visible = false;
            this.aideCryptageButton.Visible = false;

            this.TextLabel.Text = Ressources.HELP_MENU_AIDE_CRYPTAGE;
            this.TextLabel.Location = new System.Drawing.Point(102, 173);
            this.TextLabel.Visible = true;

            this.retourButton.Visible = true;
            IsSpecParam = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void specialParameterButton_Click(object sender, EventArgs e)
        {
            this.layoutButton.Visible = false;
            this.specialParameterButton.Visible = false;
            this.aideCryptageButton.Visible = false;

            this.TextLabel.Text = Ressources.HELP_MENU_SPECIAL_PARAM1;
            this.TextLabel.Location = new System.Drawing.Point(104, 107);
            this.TextLabel.Visible = true;

            this.retourButton.Visible = true;
            this.suivantButton.Text = "suivant";
            this.suivantButton.Visible = true;
            this.isSuivant = true;

            IsSpecParam = true;
        }

        private void retourButton_Click(object sender, EventArgs e)
        {
            this.TextLabel.Visible = false;
            this.retourButton.Visible = false;
            this.suivantButton.Visible = false;

            this.layoutButton.Visible = true;
            this.specialParameterButton.Visible = true;
            this.aideCryptageButton.Visible = true;
        }

        private void suivantButton_Click(object sender, EventArgs e)
        {
            if (this.isSuivant)
            {
                this.TextLabel.Text = (IsSpecParam) ? Ressources.HELP_MENU_SPECIAL_PARAM2 : Ressources.HELP_MENU_LAYOUT2;
                this.suivantButton.Text = "precedant";
                this.isSuivant = false;
            } else
            {
                this.TextLabel.Text = (IsSpecParam) ? Ressources.HELP_MENU_SPECIAL_PARAM1 : Ressources.HELP_MENU_LAYOUT1;
                this.suivantButton.Text = "suivant";
                this.isSuivant = true;
            }
        }
    }
}