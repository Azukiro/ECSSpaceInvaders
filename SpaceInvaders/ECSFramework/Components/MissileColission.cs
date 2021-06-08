using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describe the hitBox of a missile. (It's different than HitBox for make difference between Missile and Other Entity)</summary>
    public class MissileHitBox : IComponent
    {
        #region Properties

        public float Width { get; private set; }
        public float Height { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="MissileHitBox" /> class.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public MissileHitBox(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #endregion Constructor
    }
}