using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Core
{
    /// <summary>Abstract class for create all the system of the game</summary>
    public abstract class System
    {
        #region ProtectedFields

        protected Engine engine;

        #endregion ProtectedFields

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="System" /> class.</summary>
        /// <param name="engine">The engine.</param>
        public System(Engine engine)
        {
            this.engine = engine;
        }

        #endregion Constructor

        #region AbstractMethods

        /// <summary>Abstract method for update component by different system during the game update</summary>
        /// <param name="deltaT">The delta t.</param>
        public abstract void Update(double deltaT);

        #endregion AbstractMethods
    }
}