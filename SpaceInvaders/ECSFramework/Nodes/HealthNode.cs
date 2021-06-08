using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>This node manage the health of entities</summary>
    public class HealthNode : Node
    {
        #region Properties

        public Health Health { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="HealthNode" /> class.
        /// </para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="health">The health.</param>
        public HealthNode(Entity entity, Health health) : base(entity)
        {
            Health = health;
        }

        #endregion Constructor
    }
}