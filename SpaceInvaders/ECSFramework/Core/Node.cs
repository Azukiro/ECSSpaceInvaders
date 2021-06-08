using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Core
{
    /// <summary>Abstract class for got same Node pattern, with the entity in memory for removing</summary>
    public abstract class Node
    {
        #region ReadonlyFields

        public readonly Entity entity;

        #endregion ReadonlyFields

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Node" /> class.</summary>
        /// <param name="entity">The entity.</param>
        protected Node(Entity entity)
        {
            this.entity = entity;
        }

        #endregion Constructor
    }
}