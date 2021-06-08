using SpaceInvaders.ECSFramework;
using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using SpaceInvaders.ECSFramework.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>Main class of the project winch manage the start of the game.</summary>
    public class Game : INotifyPropertyChanged
    {
        #region Fields

        private Health playerHealth;
        private Health player2Health;
        private Engine engine;

        #endregion Fields

        #region Property

        public string PvText { get { return "PV :" + (playerHealth?.Pv / Properties.Settings.Default.RatioLive); } }
        public string PvText2 { get { return "PV :" + (player2Health.Pv / Properties.Settings.Default.RatioLive); } }

        /// <summary>Property for set the same AutoMoveable to all Enemy for the return when a border of the window is hiting</summary>
        /// <value>The ennemies automatic moveable.</value>
        public AutoMoveable EnnemiesAutoMoveable { get; set; }

        /// <summary>Gets the random for all the project, for have just one instance of Random.</summary>
        /// <value>The random.</value>
        public Random Random { get; private set; }

        #endregion Property

        #region PropertyChanged

        #region ForegroundColor

        private Color _ForegroundColor = Color.Red;

        public Color ForegroundColor
        {
            get { return _ForegroundColor; }
            set
            {
                _ForegroundColor = value;
                OnPropertyChanged("ForegroundColor");
            }
        }

        #endregion ForegroundColor

        #region BackgroundColor

        private Color _BackgroundColor = Color.Black;

        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                _BackgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        #endregion BackgroundColor

        #region Versus

        private bool _Versus = false;

        public bool Versus
        {
            get { return _Versus; }
            set
            {
                if (_Versus != value)
                {
                    _Versus = value;
                    OnPropertyChanged("Versus");
                }
            }
        }

        #endregion Versus

        #region Coop

        private bool _Coop = false;

        public bool Coop
        {
            get { return _Coop; }
            set
            {
                if (_Coop != value)
                {
                    _Coop = value;
                    OnPropertyChanged("Coop");
                }
            }
        }

        #endregion Coop

        #region EventPropertyChanged

        /// <summary>Called when [property changed].</summary>
        /// <param name="property">The property.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            //tell to all the project that a property have change for actualising binding
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion EventPropertyChanged

        #endregion PropertyChanged

        #region game technical elements

        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion game technical elements

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        #endregion static fields (helpers)

        #region GameStateEvent

        private GameState _GameState;

        public GameState GameState
        {
            get { return _GameState; }
            set
            {
                _GameState = value;
                RaiseGameStateChangeEvent();
            }
        }

        //Special event for modify the window on the form.cs when the game state change
        public delegate void GameStateChangeEventHandler(object sender, GameStateChangeHandler e);

        public event EventHandler<GameStateChangeHandler> GameStateChangeEvent;

        /// <summary>Raises the GameStateChange event.
        /// With invoking its delegate</summary>
        protected virtual void RaiseGameStateChangeEvent()
        {
            GameStateChangeEvent?.Invoke(this, new GameStateChangeHandler(GameState));
        }

        #endregion GameStateEvent

        #region constructors

        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        ///
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            game.ForegroundColor = System.Drawing.ColorTranslator.FromHtml(Properties.Settings.Default.PlayersColor);
            game.BackgroundColor = System.Drawing.ColorTranslator.FromHtml(Properties.Settings.Default.BackgroundColor);
            game._GameState = GameState.PAUSE;
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
            engine = new Engine();
            Random = new Random();
        }

        #endregion constructors

        #region PublicMethods

        #region EngineMethods

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            if (keyPressed.Contains(Keys.P))
            {
                if (GameState == GameState.PAUSE)
                {
                    GameState = GameState.PLAY;
                }
                else if (GameState == GameState.PLAY)
                {
                    GameState = GameState.PAUSE;
                }
                RaiseGameStateChangeEvent();
                ReleaseKey(Keys.P);
            }

            if (GameState == GameState.PLAY)
            {
                engine.Update(deltaT);
            }
        }

        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        ///
        public void Draw(Graphics g)
        {
            if (GameState == GameState.PLAY)
            {
                engine.Draw(g);
            }
        }

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        #endregion EngineMethods

        /// <summary>Loads the game with the Engine and all systems and entities necessary.</summary>

        public void Load()
        {
            SoundUtils.AmbianceMusic();//Generate the background sound
            EnnemiesAutoMoveable = new AutoMoveable(Direction.LEFT);
            if (Versus)
            {
                LoadVersus();
            }
            else if (Coop)
            {
                LoadCoop();
            }
            else
            {
                LoadClassic();
            }
            GenerateSystems();
            _GameState = GameState.PLAY;
        }

        /// <summary>Gets the winner.</summary>
        /// <returns>The winner number</returns>
        public int GetNumberwinner()
        {
            if (playerHealth.Pv <= 0)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        #endregion PublicMethods

        #region PrivateMethods

        /// <summary>Generates the systems.</summary>
        private void GenerateSystems()
        {
            engine.AddSystem(new PlayerInputSystem(engine));
            engine.AddSystem(new CollisionSystem(engine));
            engine.AddSystem(new SoundSystem(engine));
            engine.AddSystem(new HealthSystem(engine));

            if (!Versus)
            {
                //Don't need invaders systems
                engine.AddSystem(new DirectionSystem(engine, EnnemiesAutoMoveable));
                engine.AddSystem(new ShootSystem(engine));
            }
            engine.AddSystem(new MovingSystem(engine));
            engine.AddSystem(new ParticleMovementSystem(engine));
            engine.AddSystem(new EndGameSystem(engine));
        }

        private HashSet<Bitmap> GetInvaderBitmapHashSet()
        {
            HashSet<Bitmap> bitmaps = new HashSet<Bitmap>();
            bitmaps.Add(Properties.Resources.ship8);
            bitmaps.Add(Properties.Resources.ship2);
            bitmaps.Add(Properties.Resources.ship4);
            bitmaps.Add(Properties.Resources.ship5);
            return bitmaps;
        }

        /// <summary>Generates the invaders in line.</summary>
        /// <param name="bitmaps">All the bitmap we want for your invaders.</param>
        private void GenerateInvaders(HashSet<Bitmap> bitmaps)
        {
            int gameWidth = gameSize.Width;
            int maxShipWidth = bitmaps.Max(bit => bit.Width);
            int margin = 5;
            int nbShip = 7;
            int shipsWidth = (maxShipWidth + (margin * 2)) * nbShip;
            float y = 10;
            Bitmap[] missileBitmaps = new Bitmap[] { Properties.Resources.shoot1, Properties.Resources.shoot2, Properties.Resources.shoot3, Properties.Resources.shoot4 };
            foreach (Bitmap bitmap in bitmaps)
            {
                Bitmap missileBitmap = missileBitmaps[Random.Next(missileBitmaps.Length)];//Get a random missileBitmap for each invader type
                int shipWidth = bitmap.Width;
                float area = maxShipWidth + (margin * 2);
                float padding = (maxShipWidth + (margin * 2) - shipWidth) / 2;
                for (int i = 0; i < nbShip; i++)
                {
                    float x = padding + area * i;
                    AddInvader(x, y, bitmap, missileBitmap);
                }
                y += 10;
                y += bitmap.Height;
            }
        }

        /// <summary>Adds the invader to the game.</summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="missileRender">The missile render.</param>
        private void AddInvader(float x, float y, Bitmap bitmap, Bitmap missileRender)
        {
            var invader = new Entity("Invader");
            invader.AddComponents(
                new Position(x, y),
                new Render(bitmap),
                new MissileRender(missileRender, Properties.Settings.Default.ShootSound, Direction.DOWN),
                new MovementSpeed(Properties.Settings.Default.InvadersSpeed),
                new Health(Properties.Settings.Default.InvaderLive * Properties.Settings.Default.RatioLive),
                new DeathAudio(Properties.Settings.Default.InvaderKilledSound),
                new Side(SpaceClan.Ennemies),
                new HitBox(bitmap.Width, bitmap.Height),
                EnnemiesAutoMoveable//Get the common AutoMoveable
                );

            engine.AddEntity(invader);
        }

        /// <summary>Adds a player to the game.</summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y positon.</param>
        /// <param name="input">The input of the player.</param>
        /// <param name="health">The health.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="missileRender">The missile render.</param>
        /// <param name="spaceClan">The space clan.</param>
        private void AddPlayer(float x, float y, PlayerInput input, Health health, Bitmap bitmap, MissileRender missileRender, SpaceClan spaceClan)
        {
            var player = new Entity("Player");
            player.AddComponents(
                new Position(x, y),
                new Render(bitmap),
                missileRender,
                new MovementSpeed(Properties.Settings.Default.PlayerSpeed),
                health,
                input,
                new DeathAudio(Properties.Settings.Default.ExplosionSound),
                new Side(spaceClan),
                new HitBox(Properties.Resources.ship1.Width, Properties.Resources.ship1.Height)

            );

            engine.AddEntity(player);
        }

        /// <summary>Generates the bunkers center on the screen.</summary>
        /// <param name="y">The y.</param>
        private void GenerateBunkers(float y)
        {
            int bunkerWidth = Properties.Resources.bunker.Width;
            int gameWidth = gameSize.Width;
            float area = gameWidth / 3;
            float margin = (area - bunkerWidth) / 2;
            for (int i = 0; i < 3; i++)
            {
                float x = margin + area * i;
                AddBunker(x, y);
            }
        }

        /// <summary>Adds the bunker.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private void AddBunker(float x, float y)
        {
            var bunker = new Entity("Bunker");

            bunker.AddComponents(
                new Side(SpaceClan.Bunker),
                new Render(Properties.Resources.bunker),
                new Position(x, y),
                new HitBox(Properties.Resources.bunker.Width, Properties.Resources.bunker.Height),
                new Health(ImageProcessing.GetNbPixels(Properties.Resources.bunker))
                );

            engine.AddEntity(bunker);
        }

        /// <summary>Loads a classic game.</summary>
        private void LoadClassic()
        {
            playerHealth = new Health(Properties.Settings.Default.PlayerLive * Properties.Settings.Default.RatioLive);
            MissileRender missileRender = new MissileRender(Properties.Resources.shoot1, Properties.Settings.Default.ShootSound, Direction.UP);
            AddPlayer(280, 500, new PlayerInput(Keys.Left, Keys.Right, Keys.Space), playerHealth, Properties.Resources.ship1, missileRender, SpaceClan.Allies);

            GenerateInvaders(GetInvaderBitmapHashSet());

            GenerateBunkers(300);
        }

        /// <summary>Loads a coop game.</summary>
        private void LoadCoop()
        {
            playerHealth = new Health(Properties.Settings.Default.PlayerLive * Properties.Settings.Default.RatioLive);
            player2Health = new Health(Properties.Settings.Default.PlayerLive * Properties.Settings.Default.RatioLive);
            MissileRender missileRender = new MissileRender(Properties.Resources.shoot1, Properties.Settings.Default.ShootSound, Direction.UP);
            MissileRender missileRender2 = new MissileRender(Properties.Resources.shoot1, Properties.Settings.Default.ShootSound, Direction.UP);

            //Keys.Oem5 :=  *
            AddPlayer(180, 400, new PlayerInput(Keys.Left, Keys.Right, Keys.Oem5), playerHealth, Properties.Resources.ship1, missileRender, SpaceClan.Allies);
            AddPlayer(380, 400, new PlayerInput(Keys.Q, Keys.D, Keys.Space), player2Health, Properties.Resources.ship1, missileRender2, SpaceClan.Allies);

            GenerateInvaders(GetInvaderBitmapHashSet());
            GenerateBunkers(300);
        }

        /// <summary>Loads a versus game.</summary>
        private void LoadVersus()
        {
            //Player 1
            playerHealth = new Health(Properties.Settings.Default.PlayerLive * Properties.Settings.Default.RatioLive);
            MissileRender missileRender = new MissileRender(Properties.Resources.shoot1, Properties.Settings.Default.ShootSound, Direction.UP);

            //Keys.Oem5 :=  *
            AddPlayer(180, gameSize.Height - Properties.Resources.ship1.Height - 20, new PlayerInput(Keys.Left, Keys.Right, Keys.Oem5), playerHealth, Properties.Resources.ship1, missileRender, SpaceClan.Allies);

            //Player 2
            Bitmap render = Properties.Resources.ship1;
            render.RotateFlip(RotateFlipType.RotateNoneFlipY);

            Bitmap renderMissile = Properties.Resources.shoot3;
            renderMissile.RotateFlip(RotateFlipType.RotateNoneFlipY);
            MissileRender missileRender2 = new MissileRender(renderMissile, Properties.Settings.Default.ShootSound, Direction.DOWN);

            player2Health = new Health(Properties.Settings.Default.PlayerLive * Properties.Settings.Default.RatioLive);
            AddPlayer(180, 0, new PlayerInput(Keys.Q, Keys.D, Keys.Space), player2Health, render, missileRender2, SpaceClan.Ennemies);
        }

        #endregion PrivateMethods
    }

    /// <summary>Enum for following the evluation of the game</summary>
    public enum GameState
    {
        PAUSE,
        PLAY,
        WIN,
        LOOSE,
    }

    /// <summary>Class for manage the GameState event</summary>
    public class GameStateChangeHandler : EventArgs
    {
        public GameStateChangeHandler(GameState gameState)
        {
            this.State = gameState;
        }

        public GameState State { get; private set; }
    }
}