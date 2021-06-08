using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    partial class GameForm
    {
        #region Field
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private IContainer components = null;
        private Timer WorldClock;

        private Button playerColorButton;
        private Button backgroundColorButton;
        private Button classicPlayButton;
        private Button versusPlayButon;
        private Button coopPlayButton;


        private ColorDialog colorDialog;
        private Label pv1Label;
        private Label pv2VersusLabel;
        private Label pv2CoopLabel;
        private Label difficultyLabel;
        private ListBox difficultyListBox;
        private Label endLabel;
        #endregion Field

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.WorldClock = new Timer(this.components);
           
            //Button
            this.playerColorButton = new Button();
            this.backgroundColorButton = new Button();
            this.classicPlayButton = new Button();
            this.versusPlayButon = new Button();
            this.coopPlayButton = new Button();

            //ColorDialog
            this.colorDialog = new ColorDialog();

            //Label
            this.pv1Label = new Label();
            this.pv2VersusLabel = new Label();
            this.pv2CoopLabel = new Label();
            this.difficultyLabel = new Label();
            this.endLabel = new Label();

            //ListBox 
            this.difficultyListBox = new System.Windows.Forms.ListBox();

            //Add to Controls
            this.Controls.Add(this.versusPlayButon);
            this.Controls.Add(this.coopPlayButton);
            this.Controls.Add(this.pv2CoopLabel);
            this.Controls.Add(this.pv2VersusLabel);
            this.Controls.Add(this.difficultyLabel);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.pv1Label);
            this.Controls.Add(this.classicPlayButton);
            this.Controls.Add(this.backgroundColorButton);
            this.Controls.Add(this.playerColorButton);
            this.Controls.Add(this.difficultyListBox);
            this.SuspendLayout();
            this.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));//Bind the background and player color to the control
            this.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));


            // 
            // WorldClock
            // 
            this.WorldClock.Interval = 30;
            this.WorldClock.Tick += new System.EventHandler(this.WorldClock_Tick);

            //Button
            // 
            // playerColor
            // 
            this.playerColorButton.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));//Bind the background and player color to the control
            this.playerColorButton.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.playerColorButton.Location = new System.Drawing.Point(328, 255);
            this.playerColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playerColorButton.Name = "playerColor";
            this.playerColorButton.Size = new System.Drawing.Size(198, 26);
            this.playerColorButton.TabIndex = 0;
            this.playerColorButton.Text = "Choose Player Color";
            this.playerColorButton.UseVisualStyleBackColor = true;
            this.playerColorButton.FlatStyle = FlatStyle.Flat;
            this.playerColorButton.FlatAppearance.BorderSize = 0;
            this.playerColorButton.TabStop = false;
            this.playerColorButton.Click += PlayerColorButton;

            // 
            // backgroudColor
            // 
            this.backgroundColorButton.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.backgroundColorButton.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.backgroundColorButton.Location = new System.Drawing.Point(328, 255+27);
            this.backgroundColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backgroundColorButton.Name = "backgroudColor";
            this.backgroundColorButton.Size = new System.Drawing.Size(198, 26);
            this.backgroundColorButton.TabIndex = 1;
            this.backgroundColorButton.Text = "Choose background color";
            this.backgroundColorButton.UseVisualStyleBackColor = true;
            this.backgroundColorButton.FlatStyle = FlatStyle.Flat;
            this.backgroundColorButton.FlatAppearance.BorderSize = 0;
            this.backgroundColorButton.TabStop = false;
            this.backgroundColorButton.Click += BackgroundColorButton_Color;
            // 
            // button3
            // BackColor
            this.classicPlayButton.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.classicPlayButton.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.classicPlayButton.Location = new System.Drawing.Point(328, 255 + 27+27);
            this.classicPlayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.classicPlayButton.Name = "button3";
            this.classicPlayButton.Size = new System.Drawing.Size(198, 26);
            this.classicPlayButton.TabIndex = 2;
            this.classicPlayButton.Text = "Play classic mode";
            this.classicPlayButton.UseVisualStyleBackColor = true;
            this.classicPlayButton.Click += ClassicPlayButton_Click;
            this.classicPlayButton.FlatStyle = FlatStyle.Flat;
            this.classicPlayButton.FlatAppearance.BorderSize = 0;
            this.classicPlayButton.TabStop = false;

            this.versusPlayButon.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.versusPlayButon.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.versusPlayButon.Location = new System.Drawing.Point(328, 255 + 27+27+27);
            this.versusPlayButon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.versusPlayButon.Name = "button4";
            this.versusPlayButon.Size = new System.Drawing.Size(198, 26);
            this.versusPlayButon.TabIndex = 5;
            this.versusPlayButon.Text = "Play versus mode";
            this.versusPlayButon.UseVisualStyleBackColor = true;
            this.versusPlayButon.Click += new System.EventHandler(this.VersusPlayButton_Click);
            this.versusPlayButon.TabStop = false;
            this.versusPlayButon.FlatAppearance.BorderSize = 0;
            this.versusPlayButon.FlatStyle = FlatStyle.Flat;


            this.coopPlayButton.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.coopPlayButton.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.coopPlayButton.Location = new System.Drawing.Point(328, 255 + 27+27+27+27);
            this.coopPlayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.coopPlayButton.Name = "Coop";
            this.coopPlayButton.Size = new System.Drawing.Size(198, 26);
            this.coopPlayButton.TabIndex = 5;
            this.coopPlayButton.Text = "Play coop mode";
            this.coopPlayButton.UseVisualStyleBackColor = true;
            this.coopPlayButton.Click += new System.EventHandler(this.CoopPlayButton_Click);
            this.coopPlayButton.TabStop = false;
            this.coopPlayButton.FlatStyle = FlatStyle.Flat;
            this.coopPlayButton.FlatAppearance.BorderSize = 0;


            this.difficultyLabel.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.difficultyLabel.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.difficultyLabel.AutoSize = true;
            this.difficultyLabel.Location = new System.Drawing.Point(328, 255 + 27 +27+27+27+27);
            this.difficultyLabel.Name = "label2";
            this.difficultyLabel.Size = new System.Drawing.Size(198, 26);
            this.difficultyLabel.TabIndex = 3;
            this.difficultyLabel.Text = "Difficulty:";

            this.difficultyListBox.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.difficultyListBox.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.difficultyListBox.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right);
            this.difficultyListBox.Items.AddRange(new object[] {"Easy",
                        "Normal",
                        "Hard",
                        "Legendary"});
            this.difficultyListBox.Location = new System.Drawing.Point(328, 255 + 27 + 27 + 27 + 27 + 27+27);
            this.difficultyListBox.Size = new System.Drawing.Size(198, 70);
            this.difficultyListBox.TabIndex = 7;
            this.difficultyListBox.SelectedItem = Properties.Settings.Default.Difficulty;
            this.difficultyListBox.SelectedValueChanged += DifficultyListBox_SelectedValueChanged;
            difficultyListBox.KeyDown += GameForm_KeyDown;
            difficultyListBox.KeyUp += GameForm_KeyUp;
            // 
            // label1
            // 



            pv1Label.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            pv1Label.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            pv1Label.Visible = false;
            this.pv1Label.AutoSize = true;
            this.pv1Label.Location = new System.Drawing.Point(12, 719);
            this.pv1Label.Name = "label1";
            this.pv1Label.Size = new System.Drawing.Size(34, 17);
            this.pv1Label.TabIndex = 3;
            this.pv1Label.Text = "PV :";



            this.endLabel.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            this.endLabel.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            this.endLabel.Visible = false;
            this.endLabel.Enabled = false;
            this.endLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(0, 0);
            this.endLabel.Name = "label1";
            this.endLabel.Font = new Font(FontFamily.GenericSansSerif, 100f);
            this.endLabel.TabIndex = 3;

            pv2VersusLabel.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            pv2VersusLabel.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            pv2VersusLabel.DataBindings.Add(new Binding("Visible", game, "Versus"));
            pv2VersusLabel.DataBindings.Add(new Binding("Enabled", game, "Versus"));
            
            this.pv2VersusLabel.AutoSize = true;
            this.pv2VersusLabel.Location = new System.Drawing.Point(12, 9);
            this.pv2VersusLabel.Name = "label2";
            this.pv2VersusLabel.Size = new System.Drawing.Size(34, 17);
            this.pv2VersusLabel.TabIndex = 3;
            this.pv2VersusLabel.Text = "PV J2:";


            pv2CoopLabel.DataBindings.Add(new Binding("ForeColor", game, "ForegroundColor"));
            pv2CoopLabel.DataBindings.Add(new Binding("BackColor", game, "BackgroundColor"));
            pv2CoopLabel.DataBindings.Add(new Binding("Visible", game, "Coop"));
            pv2CoopLabel.DataBindings.Add(new Binding("Enabled", game, "Coop"));

            this.pv2CoopLabel.AutoSize = true;
            this.pv2CoopLabel.Location = new System.Drawing.Point(727, 719);
            this.pv2CoopLabel.Name = "label2";
            this.pv2CoopLabel.Size = new System.Drawing.Size(34, 17);
            this.pv2CoopLabel.TabIndex = 3;
            this.pv2CoopLabel.Text = "PV J2:";

            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 745);
            this.BackgroundImage = (Properties.Resources.cosmo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Space Invaders";
            this.ResumeLayout(false);
            this.PerformLayout();


        }






        #endregion

        
    }
}

