using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>Sytem for manage the rendering of entities</summary>
    public class RenderSystem : Core.System
    {
        #region Constructor

        public RenderSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Update the interface</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            Graphics graphics = engine.Graphics;//Get graphics set during the paint method of the window
            HashSet<RenderNode> renderNodes = engine.MatchComponent<RenderNode>();

            foreach (RenderNode renderNode in renderNodes)
            {
                var render = renderNode.Render;
                DrawBitmap(render, graphics, renderNode.Position);
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        /// <summary>Draws the bitmap of the entity</summary>
        /// <param name="render">Information about the bitmap.</param>
        /// <param name="g">The graphics.</param>
        /// <param name="position">The position.</param>
        private void DrawBitmap(Render render, Graphics graphics, Position position)
        {
            if (render == null)
            {
                return;
            }

            ImageProcessing.AsCurrentImageColor(render.Image);//Transform the bitmap color to the one on the parameter
            graphics?.DrawImage(render.Image, (int)position.X, (int)position.Y, render.Image.Width, render.Image.Height);
        }

        #endregion PrivateMethods
    }
}