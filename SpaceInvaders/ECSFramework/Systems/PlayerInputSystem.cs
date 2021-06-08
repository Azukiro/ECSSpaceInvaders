using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System.Collections.Generic;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>
    ///   <para>System for manage the input of the player </para>
    /// </summary>
    public class PlayerInputSystem : Core.System
    {
        #region Fields

        //Maps are use because we can have more than 1 player

        /// <summary>
        /// <para>
        /// Map between the player and his missile, for detect if missile is available
        /// </para>
        /// </summary>
        private Dictionary<PlayerInput, Entity> missiles = new Dictionary<PlayerInput, Entity>();

        /// <summary>
        ///   <para>Map between player and his orientation</para>
        /// </summary>
        private Dictionary<PlayerInput, Direction> orientations = new Dictionary<PlayerInput, Direction>();

        #endregion Fields

        #region Constructor

        public PlayerInputSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Update the player state with the input</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            var playerInputNodes = engine.MatchComponent<PlayerInputNode>();

            foreach (var playerInputNode in playerInputNodes)
            {
                PlayerInput playerInput = playerInputNode.PlayerInput;

                if (!orientations.ContainsKey(playerInput))
                {
                    orientations.Add(playerInput, Direction.DOWN);
                }

                if (Game.game.keyPressed.Contains(playerInput.LeftKey))
                {
                    MoveLeft(playerInputNode, deltaT);
                }
                else if (Game.game.keyPressed.Contains(playerInput.RigthKey))
                {
                    MoveRight(playerInputNode, deltaT);
                }

                if (Game.game.keyPressed.Contains(playerInput.ShootKey))
                {
                    Shoot(playerInputNode);
                }
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        /// <summary>Moves to the left.</summary>
        /// <param name="node">Player informations</param>
        /// <param name="deltaT">The delta t.</param>
        private void MoveLeft(PlayerInputNode playerInputNode, double deltaT)
        {
            Position playerPosition = playerInputNode.Position;
            MovementSpeed playerMovement = playerInputNode.Movement;
            Render playerRender = playerInputNode.Render;

            playerPosition.X -= (float)(playerMovement.Speed * deltaT);
            if (playerPosition.X <= 0)
            {
                playerPosition.X = 0;
            }

            //If direction change make a Flip of the bitmap
            if (orientations[playerInputNode.PlayerInput] == Direction.RIGHT)
            {
                playerRender.Image = ImageProcessing.FlipBitmap(playerRender.Image);
            }

            orientations[playerInputNode.PlayerInput] = Direction.LEFT;
        }

        /// <summary>Moves to the right.</summary>
        /// <param name="node">Player informations</param>
        /// <param name="deltaT">The delta t.</param>
        private void MoveRight(PlayerInputNode playerInputNode, double deltaT)
        {
            Position playerPosition = playerInputNode.Position;
            MovementSpeed playerMovement = playerInputNode.Movement;
            Render playerRender = playerInputNode.Render;
            HitBox playerHitBox = playerInputNode.HitBox;

            playerPosition.X += (float)(playerMovement.Speed * deltaT);
            if (playerPosition.X + playerHitBox.Width >= Game.game.gameSize.Width)
            {
                playerPosition.X = Game.game.gameSize.Width - playerHitBox.Width;
            }
            //If direction change make a Flip of the bitmap
            if (orientations[playerInputNode.PlayerInput] == Direction.LEFT)
            {
                playerRender.Image = ImageProcessing.FlipBitmap(playerRender.Image);
            }

            orientations[playerInputNode.PlayerInput] = Direction.RIGHT;
        }

        /// <summary>
        /// Invoke the shoot action of the player
        /// </summary>
        /// <param name="node">Player informations</param>
        /// <returns></returns>
        private void Shoot(PlayerInputNode playerInputNode)
        {
            if (missiles.ContainsKey(playerInputNode.PlayerInput))
            {
                if (((Health)missiles[playerInputNode.PlayerInput].TryGet(typeof(Health))).Pv <= 0)
                {
                    missiles[playerInputNode.PlayerInput] = GenerateMissile(playerInputNode);
                }
            }
            else
            {
                missiles.Add(playerInputNode.PlayerInput, GenerateMissile(playerInputNode));
            }
        }

        /// <summary>Generates a new missile for the player</summary>
        /// <param name="node">Player information</param>
        /// <returns>
        ///   <para>Return the entity of the player for detect if the missile is dead</para>
        /// </returns>
        private Entity GenerateMissile(PlayerInputNode node)
        {
            Entity entity = new Entity("PlayerMissile");

            entity.AddComponents(
                new Side(node.Side.Clan),
                new Position(node.Position.X + (node.HitBox.Width / 2), node.Position.Y + (node.HitBox.Height / 2)),
                new Render(node.MissileRender.Image),
                new AutoMoveable(node.MissileRender.Direction),
                new MovementSpeed(Properties.Settings.Default.MissileSpeed),
                new Health(Properties.Settings.Default.MissileLive * Properties.Settings.Default.RatioLive),
                new MissileHitBox(node.MissileRender.Image.Width, node.MissileRender.Image.Height)

            );
            engine.AddEntity(entity);
            SoundUtils.PlaySoundFromRessources(node.MissileRender.Audio);
            return entity;
        }

        #endregion PrivateMethods
    }
}