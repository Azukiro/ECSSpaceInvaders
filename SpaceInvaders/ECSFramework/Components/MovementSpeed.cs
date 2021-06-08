using SpaceInvaders.ECSFramework.Core;

namespace SpaceInvaders.ECSFramework.Components
{
    public class MovementSpeed : IComponent
    {
        #region Properties

        public float Speed { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="MovementSpeed" /> class.</summary>
        /// <param name="speed">The speed.</param>
        public MovementSpeed(float speed)
        {
            this.Speed = speed;
        }

        #endregion Constructor
    }
}