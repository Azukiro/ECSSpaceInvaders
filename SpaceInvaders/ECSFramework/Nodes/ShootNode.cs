using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>ShootNode is used for get all parameter for generate new invader's missile</summary>
    public class ShootNode : Node
    {
        #region Properties

        public Position Position { get; set; }

        public HitBox HitBox { get; set; }

        public MissileRender MissileRender { get; set; }

        public Side Side { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="ShootNode" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="hitBox">The collision.</param>
        /// <param name="missileRender">The missile render.</param>
        /// <param name="side">The side.</param>
        public ShootNode(Entity entity, Position position, HitBox hitBox, MissileRender missileRender, Side side) : base(entity)
        {
            Position = position;
            HitBox = hitBox;
            MissileRender = missileRender;
            Side = side;
        }

        #endregion Constructor
    }
}