using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>System for determine the direction of invaders and if they need to change when they touch the border of the screen</summary>
    public class DirectionSystem : Core.System
    {
        #region Fields

        /// <summary>The automoveable
        /// winch is assign to all the invaders</summary>
        private AutoMoveable automoveable;

        #endregion Fields

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="DirectionSystem" /> class.</summary>
        /// <param name="engine">The engine.</param>
        /// <param name="autoMoveable">Need the AutoMoveable assign to the entire invader army</param>
        public DirectionSystem(Engine engine, AutoMoveable autoMoveable) : base(engine)
        {
            this.automoveable = autoMoveable;
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Updates the direction of the invader block</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            HashSet<DirectionNode> directionNodes = engine.MatchComponent<DirectionNode>(SpaceClan.Ennemies);
            bool change = false;
            foreach (DirectionNode directionNode in directionNodes)
            {
                if (directionNode.Position.X <= 0)//Invaders go to the right if one touch the border left of the screen
                {
                    change = true;
                    automoveable.direction = Direction.RIGHT;
                    break;
                }
                else if (directionNode.Position.X + directionNode.HitBox.Width >= Game.game.gameSize.Width)//Invaders go to the left  if one touch the border right of the screen
                {
                    change = true;
                    automoveable.direction = Direction.LEFT;
                    break;
                }
            }
            if (change)
            {
                MoveDown(directionNodes);
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        /// <summary>Moves down invaders, and make acceleration</summary>
        /// <param name="directionNodes">The direction nodes.</param>
        private void MoveDown(HashSet<DirectionNode> directionNodes)
        {
            foreach (DirectionNode node in directionNodes)
            {
                node.Position.Y += Properties.Settings.Default.InvaderDownPixel;
                node.Movement.Speed *= Properties.Settings.Default.InvaderIncreaseSpeed;
            }
        }

        #endregion PrivateMethods
    }
}