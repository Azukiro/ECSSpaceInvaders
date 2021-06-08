using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>Node for manage the data of the particle System</summary>
    public class ParticleMovement : Node
    {
        #region Properties

        public ParticleManager ParticleSystemDirection { get; set; }
        public TimerParticle TimerParticle { get; set; }
        public MovementSpeed Movement { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="ParticleMovement" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="particleSystem">The particle system.</param>
        /// <param name="timerParticle">The timer particle.</param>
        /// <param name="movement">The movement.</param>
        public ParticleMovement(Entity entity, ParticleManager particleSystem, TimerParticle timerParticle, MovementSpeed movement) : base(entity)
        {
            ParticleSystemDirection = particleSystem;
            TimerParticle = timerParticle;
            Movement = movement;
        }

        #endregion Constructor
    }
}