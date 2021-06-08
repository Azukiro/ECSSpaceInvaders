using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>Node for manage the collision of entity agains missile</summary>
    public class CollisionNode : Node
    {
        #region Properties

        public Position Position { get; set; }
        public Render Render { get; set; }
        public Health Health { get; set; }
        public Side Side { get; set; }
        public HitBox HitBox { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="CollisionNode" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="render">The render.</param>
        /// <param name="health">The health.</param>
        /// <param name="side">The side.</param>
        /// <param name="hitBox">The hitBox.</param>
        public CollisionNode(Entity entity, Position position, Render render, Health health, Side side, HitBox hitBox) : base(entity)
        {
            this.Position = position;
            this.Render = render;
            this.Health = health;
            this.Side = side;
            HitBox = hitBox;
        }

        #endregion Constructor
    }
}