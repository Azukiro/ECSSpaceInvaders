using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>
    ///   <para>System for manage the rendering of the particle</para>
    /// </summary>
    public class ParticleRenderSystem : Core.System
    {
        #region Constructor

        public ParticleRenderSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Update render of the particle</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            Graphics graphics = engine.Graphics;
            HashSet<ParticleMovement> nodes = engine.MatchComponent<ParticleMovement>();
            foreach (ParticleMovement node in nodes)
            {
                foreach (Entity ent in node.ParticleSystemDirection.Particles)
                {
                    Position p = (Position)ent.TryGet(typeof(Position));
                    ParticleRender c = (ParticleRender)ent.TryGet(typeof(ParticleRender));
                    var color = c.Color;
                    var time = (node.TimerParticle.StartTimer + 1000) - (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);//Get a life time % of the particle
                    int a = (int)((time / 1000.0) * 254);//decrease the alpha channel of the particle for transition
                    color = Color.FromArgb(a < 0 ? 0 : a, color);
                    graphics.FillRectangle(new SolidBrush(color), new Rectangle((int)p.X, (int)p.Y, 10, 10));
                }
            }
        }

        #endregion OverrideMethods
    }
}