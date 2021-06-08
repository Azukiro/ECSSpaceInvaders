using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>System for put out of the game a dead entity</summary>
    public class HealthSystem : Core.System
    {
        #region Constructor

        public HealthSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>
        ///   <para>
        /// Remove from the game a dead Entity and active a partcile animation</para>
        /// </summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            var healthNodes = engine.MatchComponent<HealthNode>();
            foreach (HealthNode healthNode in healthNodes)
            {
                int healthPv = healthNode.Health.Pv;
                if (healthPv <= 0)
                {
                    engine.RemoveEntity(healthNode.entity);
                    engine.AddParticle(healthNode.entity);
                }
            }
        }

        #endregion OverrideMethods
    }
}