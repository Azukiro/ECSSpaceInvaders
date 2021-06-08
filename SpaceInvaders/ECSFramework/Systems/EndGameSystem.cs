using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>System manage the issue of the game</summary>
    public class EndGameSystem : Core.System
    {
        #region Constructor

        public EndGameSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>
        ///   <para>Update the status of the game</para>
        /// </summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            var playerNodes = engine.MatchComponent<PlayerInputNode>();
            if (!Game.game.Versus)
            {
                if (playerNodes.Count == 0)//If the player is dead it's a loose
                {
                    Game.game.GameState = GameState.LOOSE;
                    return;
                }

                HasEnnemyWin(playerNodes);
            }
            else
            {
                EndVersus(playerNodes);
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        /// <summary>
        ///   <para>Determine if a player win the versus</para>
        /// </summary>
        /// <param name="playerNodes">The player nodes.</param>
        private void EndVersus(HashSet<PlayerInputNode> playerNodes)
        {
            if (playerNodes.Count == 1)//If just 1 player left it's a Win
            {
                Game.game.GameState = GameState.WIN;
            }
        }

        /// <summary>Determines ennemies have win</summary>
        /// <param name="playerNodes">The player nodes.</param>
        private void HasEnnemyWin(HashSet<PlayerInputNode> playerNodes)
        {
            var nodeEnnemies = engine.MatchComponent<ShootNode>(SpaceClan.Ennemies);
            if (nodeEnnemies.Count == 0)//If all ennemie are dead it's a Win
            {
                Game.game.GameState = GameState.WIN;
                return;
            }

            foreach (ShootNode node in nodeEnnemies)
            {
                if (playerNodes.Any(p => p.Position.Y <= node.Position.Y))//If 1 invader have the same Y than the player it's a loose
                {
                    Game.game.GameState = GameState.LOOSE;
                    return;
                }
            }
        }

        #endregion PrivateMethods
    }
}