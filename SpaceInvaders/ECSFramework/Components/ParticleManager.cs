using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component manage a set of particle. For don't have a high complexity on particle management I decide to group on an unique component.</summary>
    public class ParticleManager : IComponent
    {
        #region Properties

        public HashSet<Entity> Particles { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="ParticleManager" /> class.</summary>
        /// <param name="particles">The set of particles.</param>
        public ParticleManager(HashSet<Entity> particles)
        {
            this.Particles = particles;
        }

        #endregion Constructor
    }
}