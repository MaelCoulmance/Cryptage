// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

using System.ComponentModel;
using System.Windows.Forms;

namespace Cryptage
{
    /// <summary>
    /// Build the help menu window.
    /// </summary>
    partial class HelpWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AideLabel = new System.Windows.Forms.Label();
            this.aideCryptageButton = new System.Windows.Forms.Button();
            this.specialParameterButton = new System.Windows.Forms.Button();
            this.layoutButton = new System.Windows.Forms.Button();
            this.TextLabel = new System.Windows.Forms.Label();
            this.retourButton = new System.Windows.Forms.Button();
            this.suivantButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AideLabel
            // 
            this.AideLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AideLabel.Font = new System.Drawing.Font("Verdana", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AideLabel.Location = new System.Drawing.Point(345, 52);
            this.AideLabel.Name = "AideLabel";
            this.AideLabel.Size = new System.Drawing.Size(92, 64);
            this.AideLabel.TabIndex = 0;
            this.AideLabel.Text = "Aide";
            // 
            // aideCryptageButton
            // 
            this.aideCryptageButton.Location = new System.Drawing.Point(327, 154);
            this.aideCryptageButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.aideCryptageButton.Name = "aideCryptageButton";
            this.aideCryptageButton.Size = new System.Drawing.Size(130, 58);
            this.aideCryptageButton.TabIndex = 1;
            this.aideCryptageButton.Text = "Aide cryptage";
            this.aideCryptageButton.UseVisualStyleBackColor = true;
            this.aideCryptageButton.Click += new System.EventHandler(this.aideCryptageButton_Click);
            // 
            // specialParameterButton
            // 
            this.specialParameterButton.Location = new System.Drawing.Point(327, 279);
            this.specialParameterButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.specialParameterButton.Name = "specialParameterButton";
            this.specialParameterButton.Size = new System.Drawing.Size(130, 58);
            this.specialParameterButton.TabIndex = 2;
            this.specialParameterButton.Text = "Paramètres spéciaux";
            this.specialParameterButton.UseVisualStyleBackColor = true;
            this.specialParameterButton.Click += new System.EventHandler(this.specialParameterButton_Click);
            // 
            // layoutButton
            // 
            this.layoutButton.Location = new System.Drawing.Point(327, 401);
            this.layoutButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutButton.Name = "layoutButton";
            this.layoutButton.Size = new System.Drawing.Size(130, 58);
            this.layoutButton.TabIndex = 3;
            this.layoutButton.Text = "Paramètres de mise en page";
            this.layoutButton.UseVisualStyleBackColor = true;
            this.layoutButton.Click += new System.EventHandler(this.layoutButton_Click);
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.Location = new System.Drawing.Point(133, 131);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(0, 20);
            this.TextLabel.TabIndex = 4;
            this.TextLabel.Visible = false;
            this.TextLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // retourButton
            // 
            this.retourButton.Location = new System.Drawing.Point(33, 510);
            this.retourButton.Name = "retourButton";
            this.retourButton.Size = new System.Drawing.Size(100, 31);
            this.retourButton.TabIndex = 5;
            this.retourButton.Text = "retour";
            this.retourButton.UseVisualStyleBackColor = true;
            this.retourButton.Visible = false;
            this.retourButton.Click += new System.EventHandler(this.retourButton_Click);
            // 
            // suivantButton
            // 
            this.suivantButton.Location = new System.Drawing.Point(661, 510);
            this.suivantButton.Name = "suivantButton";
            this.suivantButton.Size = new System.Drawing.Size(100, 31);
            this.suivantButton.TabIndex = 6;
            this.suivantButton.Text = "suivant";
            this.suivantButton.UseVisualStyleBackColor = true;
            this.suivantButton.Click += new System.EventHandler(this.suivantButton_Click);
            this.suivantButton.Visible = false;
            // 
            // HelpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 562);
            this.Controls.Add(this.suivantButton);
            this.Controls.Add(this.retourButton);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.layoutButton);
            this.Controls.Add(this.specialParameterButton);
            this.Controls.Add(this.aideCryptageButton);
            this.Controls.Add(this.AideLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HelpWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fenêtre d\'aide";
            this.Load += new System.EventHandler(this.HelpWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button specialParameterButton;

        private System.Windows.Forms.Button aideCryptageButton;

        private System.Windows.Forms.Label AideLabel;

        private bool isSuivant = true;


        #endregion

        private Button layoutButton;
        private Label TextLabel;
        private Button retourButton;
        private Button suivantButton;

        private bool IsSpecParam = false;
    }
}