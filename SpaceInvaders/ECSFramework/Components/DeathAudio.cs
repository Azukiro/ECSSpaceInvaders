using SpaceInvaders.ECSFramework.Core;
using System;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describes the sound an entity when it die</summary>
    public class DeathAudio : IComponent
    {
        #region Properties

        public String Audio { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="DeathAudio" /> class.</summary>
        /// <param name="audio">The audio name.</param>
        public DeathAudio(string audio)
        {
            Audio = audio;
        }

        #endregion Constructor
    }
}