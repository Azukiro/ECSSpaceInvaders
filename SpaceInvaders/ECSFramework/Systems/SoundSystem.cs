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
    /// <summary>
    ///   <para>System for the sound management</para>
    /// </summary>
    public class SoundSystem : Core.System
    {
        #region Constructor

        public SoundSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Play the destroy sound if an entity die</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            var nodes = engine.MatchComponent<HealthAudioNode>();

            foreach (HealthAudioNode node in nodes)
            {
                if (node.Health.Pv <= 0)
                {
                    SoundUtils.PlaySoundFromRessources(node.DestroyAudio.Audio);
                }
            }
        }

        #endregion OverrideMethods
    }
}