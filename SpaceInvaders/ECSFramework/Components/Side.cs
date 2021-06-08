using SpaceInvaders.ECSFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Components
{
    /// <summary>This component describes the clan of an Entity (Enemy, Ally, Bunker)</summary>
    public class Side : IComponent
    {
        #region Properties

        public SpaceClan Clan { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Side" /> class.</summary>
        /// <param name="clan">The clan.</param>
        public Side(SpaceClan clan)
        {
            this.Clan = clan;
        }

        #endregion Constructor
    }

    /// <summary>This enum describes the different clan</summary>
    public enum SpaceClan
    {
        Allies,
        Ennemies,
        Bunker,
        All,
    }
}