using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This timer describes the born of a particle for calcul its death with its life time</summary>
    public class TimerParticle : IComponent
    {
        #region Properties

        public long StartTimer { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="TimerParticle" /> class.</summary>
        /// <param name="startTimer">The start timer.</param>
        public TimerParticle(long startTimer)
        {
            StartTimer = startTimer;
        }

        #endregion Constructor
    }
}