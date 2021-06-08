using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>Node for manage the invader direction</summary>
    public class DirectionNode : Node
    {
        #region Properties

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public Position Position { get; set; }

        /// <summary>Gets or sets the collision.</summary>
        /// <value>The collision.</value>
        public HitBox HitBox { get; set; }

        public MovementSpeed Movement { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="DirectionNode" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="hitBox">The hitBox.</param>
        /// <param name="movement">The movement.</param>
        public DirectionNode(Entity entity, Position position, HitBox hitBox, MovementSpeed movement) : base(entity)
        {
            Position = position;
            HitBox = hitBox;
            Movement = movement;
        }

        #endregion Constructor
    }
}