using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Core;
using SpaceInvaders.ECSFramework.Nodes;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.ECSFramework.Systems
{
    /// <summary>
    ///   <para>System for generate invader's missiles</para>
    /// </summary>
    public class ShootSystem : Core.System
    {
        #region Fields

        /// <summary>
        ///   <para>
        /// Keep in memory if a missile is dead or not</para>
        /// </summary>
        private Dictionary<Entity, Health> missiles;

        #endregion Fields

        #region Constructor

        public ShootSystem(Engine engine) : base(engine)
        {
            missiles = new Dictionary<Entity, Health>();
        }

        #endregion Constructor

        #region OverrideMethods

        /// <summary>Generate random missile for the invaders</summary>
        /// <param name="deltaT">The delta t.</param>
        public override void Update(double deltaT)
        {
            Random rdn = Game.game.Random;
            var nodes = engine.MatchComponent<ShootNode>(SpaceClan.Ennemies);//Just get the ennemies for shooting
            int difficulty = GetRatioDifficulty();
            foreach (ShootNode node in nodes)
            {
                if ((missiles.ContainsKey(node.entity) && missiles[node.entity].Pv <= 0) || !missiles.ContainsKey(node.entity))//If the missile of the current invader is dead
                {
                    //0.0005% de chance de générer un missile à chaque frame
                    if (rdn.Next(difficulty) == 1)
                    {
                        GenerateMissile(node);
                    }
                }
            }
        }

        #endregion OverrideMethods

        #region PrivateMethods

        /// <summary>Generates a new missile for the invader</summary>
        /// <param name="node">Shoot information</param>
        private void GenerateMissile(ShootNode node)
        {
            Health health = new Health(Properties.Settings.Default.MissileLive * Properties.Settings.Default.RatioLive);
            Entity entity = new Entity("EnnemyMissile");
            entity.AddComponents(
                 new Side(node.Side.Clan),
                 new Position(node.Position.X + (node.HitBox.Width / 2), node.Position.Y + (node.HitBox.Height / 2)),
                 new Render(node.MissileRender.Image),
                 new AutoMoveable(node.MissileRender.Direction),
                 new MovementSpeed(Properties.Settings.Default.MissileSpeed),
                 new Health(Properties.Settings.Default.MissileLive * Properties.Settings.Default.RatioLive),
                 new MissileHitBox(node.MissileRender.Image.Width, node.MissileRender.Image.Height),
                 health

             );

            missiles[entity] = health;
            engine.AddEntity(entity);

            SoundUtils.PlaySoundFromRessources(node.MissileRender.Audio);
        }

        private int GetRatioDifficulty()
        {
            switch (Properties.Settings.Default.Difficulty)
            {
                case "Easy":
                    return 5000;

                case "Normal":
                    return 3500;

                case "Hard":
                    return 2000;

                case "Legendary":
                    return 1000;

                default:
                    return 0;
            }
        }

        #endregion PrivateMethods
    }
}