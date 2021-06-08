using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This class describes the image of entity for his render</summary>
    public class Render : IComponent
    {
        #region Properties

        public Bitmap Image { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="Render" /> class.
        /// </para>
        /// </summary>
        /// <param name="image">The image.</param>
        public Render(Bitmap image)
        {
            Image = image;
        }

        #endregion Constructor
    }
}