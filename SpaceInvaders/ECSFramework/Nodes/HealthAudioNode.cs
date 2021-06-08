using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>Node for launch sound after an entity die</summary>
    public class HealthAudioNode : Node
    {
        #region Properties

        public Health Health { get; set; }
        public DeathAudio DestroyAudio { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="HealthAudioNode" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="health">The health.</param>
        /// <param name="destroyAudio">The destroy audio.</param>
        public HealthAudioNode(Entity entity, Health health, DeathAudio destroyAudio) : base(entity)
        {
            Health = health;
            DestroyAudio = destroyAudio;
        }

        #endregion Constructor
    }
}