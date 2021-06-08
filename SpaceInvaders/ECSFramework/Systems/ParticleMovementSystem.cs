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
    /// <summary>System for manage the position and the life of particles</summary>
    public class ParticleMovementSystem : Core.System
    {
        #region Constructor

        public ParticleMovementSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Update the position of particles</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            HashSet<ParticleMovement> nodes = engine.MatchComponent<ParticleMovement>();
            foreach (ParticleMovement node in nodes)
            {
                if (node.TimerParticle.StartTimer + 1000 < DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond)//Detele particle if life time is over
                {
                    engine.RemoveEntity(node.entity);
                    return;
                }
                foreach (Entity ent in node.ParticleSystemDirection.Particles)
                {
                    Position p = (Position)ent.TryGet(typeof(Position));
                    ParticleDirection d = (ParticleDirection)ent.TryGet(typeof(ParticleDirection));
                    p.X += (float)deltaT * node.Movement.Speed * d.X;
                    p.Y += (float)deltaT * node.Movement.Speed * d.Y;
                }
            }
        }

        #endregion OverrideMethods
    }
}