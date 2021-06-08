using SpaceInvaders.ECSFramework.Core;
using System;
using System.Drawing;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describe the render of a missile, with his Image, his direction, and his audio sound when his shoot</summary>
    public class MissileRender : IComponent
    {
        #region Properties

        public Bitmap Image { get; set; }

        public string Audio { get; private set; }

        public Direction Direction { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="MissileRender" /> class.</summary>
        /// <param name="image">The image.</param>
        /// <param name="audio">The audio name.</param>
        /// <param name="direction">The direction.</param>
        public MissileRender(Bitmap image, string audio, Direction direction)
        {
            Image = image;
            Audio = audio;
            Direction = direction;
        }

        #endregion Constructor
    }
}