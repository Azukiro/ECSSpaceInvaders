using SpaceInvaders.ECSFramework.Nodes;
using SpaceInvaders.ECSFramework.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SpaceInvaders.ECSFramework.Core;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>System for manage the position of invaders and missiles</summary>
    public class MovingSystem : Core.System
    {
        #region Constructor

        public MovingSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        public override void Update(double deltaT)
        {
            HashSet<DeplacementNode> deplacementNodes = engine.MatchComponent<DeplacementNode>();

            foreach (DeplacementNode deplacementNode in deplacementNodes)
            {
                float speed = deplacementNode.Movement.Speed;
                Position position = deplacementNode.Position;
                Direction direction = deplacementNode.AutoMoveable.direction;
                MoveEntity(speed, position, direction, deltaT);
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        private void MoveEntity(float speed, Position position, Direction direction, double deltaT)
        {
            switch (direction)
            {
                case Direction.UP:
                    position.Y -= (float)(speed * deltaT);
                    break;

                case Direction.DOWN:
                    position.Y += (float)(speed * deltaT);
                    break;

                case Direction.LEFT:
                    position.X -= (float)(speed * deltaT);
                    break;

                case Direction.RIGHT:
                    position.X += (float)(speed * deltaT);
                    break;

                default:
                    break;
            }
        }

        #endregion PrivateMethods
    }
}