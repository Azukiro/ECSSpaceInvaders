using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class GameForm : Form
    {
        #region fields

        /// <summary>
        /// Instance of the game
        /// </summary>
        private Game game;

        #endregion fields

        #region time management

        /// <summary>
        /// Game watch
        /// </summary>
        private Stopwatch watch = new Stopwatch();

        /// <summary>
        /// Last update time
        /// </summary>
        private long lastTime = 0;

        #endregion time management

        #region constructor

        /// <summary>
        /// Create form, create game
        /// </summary>
        public GameForm()
        {
            game = Game.CreateGame(this.ClientSize);
            game.GameStateChangeEvent += Game_GameStateChangeEvent;
            InitializeComponent();
            game.gameSize = this.ClientSize;
            watch.Start();
            WorldClock.Start();
        }

        private void Game_GameStateChangeEvent(object sender, GameStateChangeHandler e)
        {
            if (e.State == GameState.PAUSE)
            {
                playerColorButton.Visible = true;
                backgroundColorButton.Visible = true;
                difficultyListBox.Visible = true;
                playerColorButton.Enabled = true;
                backgroundColorButton.Enabled = true;
                difficultyListBox.Enabled = true;
                difficultyLabel.Visible = true;
            }
            else if (e.State == GameState.PLAY)
            {
                playerColorButton.Visible = false;
                backgroundColorButton.Visible = false;
                difficultyListBox.Visible = false;
                playerColorButton.Enabled = false;
                backgroundColorButton.Enabled = false;
                difficultyListBox.Enabled = false;
                difficultyLabel.Visible = false;
            }
            else if (e.State == GameState.LOOSE)
            {
                endLabel.Visible = true;
                endLabel.Enabled = true;
                endLabel.Text = "Game\n Over";
            }
            else if (e.State == GameState.WIN)
            {
                endLabel.Visible = true;
                endLabel.Enabled = true;
                if (game.Versus)
                {
                    endLabel.Text = "Win" + game.GetNumberwinner();
                }
                else
                {
                    endLabel.Text = "Win";
                }
            }
        }

        #endregion constructor

        #region events

        /// <summary>
        /// Paint event of the form, see msdn for help => paint game with double buffering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = bg.Graphics;
            g.Clear(game.BackgroundColor);
            game.Draw(g);
            if (game.GameState == GameState.PAUSE)
            {
                g.DrawImage(Properties.Resources.cosmo, 0, 0, 606, 606);
            }

            bg.Render();
            bg.Dispose();

            pv1Label.Text = game.PvText;
            if (pv2VersusLabel.Enabled)
            {
                pv2VersusLabel.Text = game.PvText2;
            }
            else if (pv2CoopLabel.Enabled)
            {
                pv2CoopLabel.Text = game.PvText2;
            }
        }

        /// <summary>
        /// Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e)
        {
            // lets do 5 ms update to avoid quantum effects
            int maxDelta = 5;

            // get time with millisecond precision
            long nt = watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = (nt - lastTime);

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                game.Update(maxDelta / 1000.0);

            game.Update(deltaT / 1000.0);

            // remember the time of this update
            lastTime = nt;

            Invalidate();
        }

        /// <summary>
        /// Key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.keyPressed.Add(e.KeyCode);
        }

        /// <summary>
        /// Key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.keyPressed.Remove(e.KeyCode);
        }

        #endregion events

        /// <summary>Handles the SelectedValueChanged event of the DifficultyListBox control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void DifficultyListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox difficultyListBox = (ListBox)sender;
            if (difficultyListBox != null)
            {
                if (difficultyListBox.SelectedItem is string val)
                {
                    Properties.Settings.Default.Difficulty = val;
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>Players the color button.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void PlayerColorButton(object sender, EventArgs e)
        {
            var result = colorDialog.ShowDialog();
            if ((result == DialogResult.OK || result == DialogResult.Yes) && game.ForegroundColor != colorDialog.Color)
            {
                Color c = colorDialog.Color;
                game.ForegroundColor = colorDialog.Color;
                Properties.Settings.Default.PlayersColor = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");//Transform the color to an hexa color

                Properties.Settings.Default.Save();
            }
            this.ActiveControl = null;//remove the button focus
        }

        /// <summary>Handles the Click event of the classicPlayButton control.
        /// And launch the game</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ClassicPlayButton_Click(object sender, EventArgs e)
        {
            this.Paint += this.GameForm_Paint;
            this.KeyDown += this.GameForm_KeyDown;
            this.KeyUp += this.GameForm_KeyUp;
            this.playerColorButton.Visible = false;
            this.backgroundColorButton.Visible = false;
            this.classicPlayButton.Visible = false;
            this.versusPlayButon.Visible = false;
            this.coopPlayButton.Visible = false;
            this.difficultyListBox.Visible = false;
            this.playerColorButton.Enabled = false;
            this.backgroundColorButton.Enabled = false;
            this.classicPlayButton.Enabled = false;
            this.versusPlayButon.Enabled = false;
            this.coopPlayButton.Enabled = false;
            this.difficultyListBox.Enabled = false;
            this.difficultyLabel.Visible = false;
            this.pv1Label.Visible = true;
            game.Load();
        }

        /// <summary>Handles the Color event of the BackgroundColorButton control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void BackgroundColorButton_Color(object sender, EventArgs e)
        {
            var result = colorDialog.ShowDialog();
            if ((result == DialogResult.OK || result == DialogResult.Yes) && game.BackgroundColor != colorDialog.Color)
            {
                Color c = colorDialog.Color;
                game.BackgroundColor = c;
                Properties.Settings.Default.BackgroundColor = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
                Properties.Settings.Default.Save();
            }
            this.ActiveControl = null;//remove the button focus
        }

        /// <summary>Handles the Click event of the VersusPlayButton control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void VersusPlayButton_Click(object sender, EventArgs e)
        {
            game.Versus = true;
            ClassicPlayButton_Click(sender, e);
        }

        /// <summary>Handles the Click event of the CoopPlayButton control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void CoopPlayButton_Click(object sender, EventArgs e)
        {
            game.Coop = true;
            ClassicPlayButton_Click(sender, e);
        }
    }
}