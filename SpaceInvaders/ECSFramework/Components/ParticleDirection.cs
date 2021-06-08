using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describe the direction in X and Y of particle</summary>
    public class ParticleDirection : IComponent
    {
        #region Properties

        public float X { get; private set; }
        public float Y { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="ParticleDirection" /> class.</summary>
        /// <param name="x">The direction on X axe.</param>
        /// <param name="y">The direction on Y axe.</param>
        public ParticleDirection(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion Constructor
    }
}