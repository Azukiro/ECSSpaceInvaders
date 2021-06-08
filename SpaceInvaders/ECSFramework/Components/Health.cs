using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describes the health of an entity</summary>
    public class Health : IComponent
    {
        #region Properties

        public int Pv { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="Health" /> class.
        /// </para>
        /// </summary>
        /// <param name="pv">The pv.</param>
        public Health(int pv)
        {
            this.Pv = pv;
        }

        #endregion Constructor
    }
}