using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describes the rendering of a particle</summary>
    public class ParticleRender : IComponent
    {
        #region Properties

        public Color Color { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="ParticleRender" /> class.</summary>
        /// <param name="color">The color.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ParticleRender(Color color, float width, float height)
        {
            Color = color;
            Width = width;
            Height = height;
        }

        #endregion Constructor
    }
}