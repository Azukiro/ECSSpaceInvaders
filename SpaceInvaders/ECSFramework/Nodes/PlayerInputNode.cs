using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>Node for answer to player input</summary>
    public class PlayerInputNode : Node
    {
        #region Properties

        public Position Position { get; set; }
        public MovementSpeed Movement { get; set; }

        public HitBox HitBox { get; set; }
        public PlayerInput PlayerInput { get; private set; }

        public Side Side { get; set; }

        public MissileRender MissileRender { get; set; }

        public Render Render { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="PlayerInputNode" /> class.
        /// </para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="speed">The spped.</param>
        /// <param name="hitBox">The collision.</param>
        /// <param name="playerInput">The player input.</param>
        /// <param name="side">The side.</param>
        /// <param name="missileRender">The missile render.</param>
        /// <param name="render">The render.</param>
        public PlayerInputNode(Entity entity, Position position, MovementSpeed speed, HitBox hitBox, PlayerInput playerInput, Side side, MissileRender missileRender, Render render) : base(entity)
        {
            Position = position;
            Movement = speed;
            HitBox = hitBox;
            PlayerInput = playerInput;
            Side = side;
            MissileRender = missileRender;
            Render = render;
        }

        #endregion Constructor
    }
}