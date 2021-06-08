using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>Node for the invaders and missiles deplacement</summary>
    public class DeplacementNode : Node
    {
        #region Properties

        public Position Position { get; set; }
        public MovementSpeed Movement { get; set; }
        public AutoMoveable AutoMoveable { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="DeplacementNode" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="movement">The movement.</param>
        /// <param name="autoMoveable">The automatic moveable.</param>
        public DeplacementNode(Entity entity, Position position, MovementSpeed movement, AutoMoveable autoMoveable) : base(entity)
        {
            this.Position = position;
            this.Movement = movement;
            this.AutoMoveable = autoMoveable;
        }

        #endregion Constructor
    }
}