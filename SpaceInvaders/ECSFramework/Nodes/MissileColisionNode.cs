using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>This node manage the data from Missile for detect collision against entities</summary>
    public class MissileCollisionNode : Node
    {
        #region Properties

        public MissileHitBox MissileHitBox { get; set; }
        public Position Position { get; set; }
        public Health Health { get; set; }
        public Side Side { get; set; }
        public Render Render { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="MissileCollisionNode" /> class.
        /// </para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="missileHitBox">The missile collision.</param>
        /// <param name="position">The position.</param>
        /// <param name="health">The health.</param>
        /// <param name="side">The side.</param>
        /// <param name="render">The render.</param>
        public MissileCollisionNode(Entity entity, MissileHitBox missileHitBox, Position position, Health health, Side side, Render render) : base(entity)
        {
            MissileHitBox = missileHitBox;
            Position = position;
            Health = health;
            Side = side;
            Render = render;
        }

        #endregion Constructor
    }
}