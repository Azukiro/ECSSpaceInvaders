using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describe the direction of entity that move without player interaction</summary>
    public class AutoMoveable : IComponent
    {
        #region Properties

        public Direction direction { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="AutoMoveable" /> class.</summary>
        /// <param name="direction">The direction.</param>
        public AutoMoveable(Direction direction)
        {
            this.direction = direction;
        }

        #endregion Constructor
    }

    /// <summary>Enumeration for the differente moving direction of an entity</summary>
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }
}