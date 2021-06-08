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
    /// <summary>System for manage collision between entity and missile</summary>
    public class CollisionSystem : Core.System
    {
        #region Constructor

        public CollisionSystem(Engine engine) : base(engine)
        {
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Detect collision and manage pv decreasing during collision</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            var missileNodes = engine.MatchComponent<MissileCollisionNode>();
            var collisionNodes = engine.MatchComponent<CollisionNode>();

            foreach (MissileCollisionNode missileNode in missileNodes)
            {
                if (missileNode.Position.Y < 0 || missileNode.Position.Y >= Game.game.gameSize.Height)
                {
                    missileNode.Health.Pv = 0;
                    continue;
                }
                foreach (CollisionNode collisionNode in collisionNodes)
                {
                    DetectCollision(collisionNode, missileNode);
                }
                foreach (MissileCollisionNode missileNodeEnnemie in missileNodes)
                {
                    DetectCollisionBetweenMissile(missileNodeEnnemie, missileNode);
                }
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        /// <summary>Detects the collision.
        /// And manage damage</summary>
        /// <param name="collisionNode">The collision node.</param>
        /// <param name="missileNode">The missile node.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool DetectCollision(CollisionNode collisionNode, MissileCollisionNode missileNode)
        {
            if (collisionNode.Side.Clan != missileNode.Side.Clan)
            {
                Rectangle collisionNodeRect = new Rectangle((int)collisionNode.Position.X, (int)collisionNode.Position.Y, (int)collisionNode.HitBox.Width, (int)collisionNode.HitBox.Height);
                Rectangle missileNodeRect = new Rectangle((int)missileNode.Position.X, (int)missileNode.Position.Y, (int)missileNode.MissileHitBox.Width, (int)missileNode.MissileHitBox.Height);
                if (ImageProcessing.OnCollision(collisionNodeRect, missileNodeRect, collisionNode.Render.Image))
                {
                    ImageProcessing.DeleteData(collisionNodeRect, missileNodeRect, collisionNode.Render.Image, missileNode.Render.Image, out var deletedPixels);
                    DecreasePv(missileNode.Health, collisionNode.Health, deletedPixels, collisionNode.Side.Clan == SpaceClan.Bunker);
                    return true;
                }
            }
            return false;
        }

        /// <summary>Detects the collision between 2 missiles.
        /// And manage damage</summary>
        /// <param name="missileNodeEnemy">The enemy missile node.</param>
        /// <param name="missileNode">The missile node.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool DetectCollisionBetweenMissile(MissileCollisionNode missileNodeEnemy, MissileCollisionNode missileNode)
        {
            if (missileNodeEnemy.Side.Clan != missileNode.Side.Clan)
            {
                Rectangle missileNodeEnemyRect = new Rectangle((int)missileNodeEnemy.Position.X, (int)missileNodeEnemy.Position.Y, (int)missileNodeEnemy.MissileHitBox.Width, (int)missileNodeEnemy.MissileHitBox.Height);
                Rectangle missileNodeRect = new Rectangle((int)missileNode.Position.X, (int)missileNode.Position.Y, (int)missileNode.MissileHitBox.Width, (int)missileNode.MissileHitBox.Height);

                if (ImageProcessing.OnCollision(missileNodeEnemyRect, missileNodeRect, missileNodeEnemy.Render.Image))
                {
                    ImageProcessing.DeleteData(missileNodeEnemyRect, missileNodeRect, missileNodeEnemy.Render.Image, missileNode.Render.Image, out var _);

                    DecreasePv(missileNode.Health, missileNodeEnemy.Health, 0, false);
                    return true;
                }
            }
            return false;
        }

        /// <summary>Remove Pv to entities</summary>
        /// <param name="missileHealth">The missile health.</param>
        /// <param name="collisionHealth">The collision health.</param>
        /// <param name="deletedPixels">The deleted pixels.</param>
        /// <param name="bunker">if the entity attack is a bunker</param>
        private void DecreasePv(Health missileHealth, Health collisionHealth, int deletedPixels, bool bunker)
        {
            if (bunker)
            {
                collisionHealth.Pv -= deletedPixels;
                missileHealth.Pv -= deletedPixels;
            }
            else
            {
                int lessPv = missileHealth.Pv;
                if (missileHealth.Pv > collisionHealth.Pv)
                {
                    lessPv = collisionHealth.Pv;
                }
                collisionHealth.Pv -= lessPv;
                missileHealth.Pv -= lessPv;
            }
        }

        #endregion PrivateMethods
    }
}