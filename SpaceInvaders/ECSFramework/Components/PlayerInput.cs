using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describes the keyboard key for control the player</summary>
    public class PlayerInput : IComponent
    {
        #region Properties

        public Keys LeftKey { get; private set; }
        public Keys RigthKey { get; private set; }
        public Keys ShootKey { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PlayerInput" /> class.</summary>
        /// <param name="leftKey">The key for move on left the player.</param>
        /// <param name="rightKey">The key for move on right the player.</param>
        /// <param name="shootKey">The key for shoot with the player.</param>
        public PlayerInput(Keys leftKey, Keys rightKey, Keys shootKey)
        {
            LeftKey = leftKey;
            RigthKey = rightKey;
            ShootKey = shootKey;
        }

        #endregion Constructor
    }
}