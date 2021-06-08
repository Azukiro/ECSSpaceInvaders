using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Nodes
{
    /// <summary>The RenderNode is used for define the render of an entity</summary>
    public class RenderNode : Node
    {
        #region Properties

        public Position Position { get; set; }
        public Render Render { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="RenderNode" /> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="render">The render.</param>
        public RenderNode(Entity entity, Position position, Render render) : base(entity)
        {
            this.Position = position;
            this.Render = render;
        }

        #endregion Constructor
    }
}