using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describe the hitbox of an entity</summary>
    public class HitBox : IComponent
    {
        #region Properties

        public float Width { get; private set; }
        public float Height { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="HitBox" /> class.
        /// </para>
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public HitBox(float width, float height)
        {
            Width = width;
            Height = height;
        }

        #endregion Constructor
    }
}