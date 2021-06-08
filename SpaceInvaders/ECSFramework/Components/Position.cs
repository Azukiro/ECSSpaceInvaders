using SpaceInvaders.ECSFramework.Core;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describes the position of an entity</summary>
    public class Position : IComponent
    {
        #region Properties

        public float X { get; set; }
        public float Y { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Position" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion Constructor
    }
}