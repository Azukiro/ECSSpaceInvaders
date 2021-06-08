using SpaceInvaders.ECSFramework.Components;
using SpaceInvaders.ECSFramework.Nodes;
using SpaceInvaders.ECSFramework.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpaceInvaders.ECSFramework.Core
{
    /// <summary>The engine class as the function of making communication between the Game and the Systems. It manage node, entity and system.</summary>
    public class Engine
    {
        #region Fields

        private HashSet<System> systems = new HashSet<System>();
        private HashSet<Entity> entities = new HashSet<Entity>();
        private RenderSystem renderSystem;
        private ParticleRenderSystem particleRenderSystem;

        /// <summary>
        /// A dictionnary for associate an Type of node to an Dictionnary of Entity and Node. For keep in memory all of created node associate to a specific type and the entity wich get this node
        /// </summary>
        private Dictionary<Type, Dictionary<Entity, Node>> nodeDic = new Dictionary<Type, Dictionary<Entity, Node>>();

        #endregion Fields

        #region Properties

        public Graphics Graphics { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Engine" /> class.</summary>
        public Engine()
        {
            this.renderSystem = new RenderSystem(this);
            this.particleRenderSystem = new ParticleRenderSystem(this);
        }

        #endregion Constructor

        #region PublicMethods

        #region EntityManagement

        /// <summary>Adds an entity to the game.</summary>
        /// <param name="entity">The entity.</param>
        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        /// <summary>Adds a system to the game.</summary>
        /// <param name="system">The system.</param>
        public void AddSystem(System system)
        {
            systems.Add(system);
        }

        /// <summary>Remove a entity from the game. And all its associate node</summary>
        /// <param name="system">The entity.</param>
        public void RemoveEntity(Entity entity)
        {
            //
            entities.Remove(entity);
            foreach (var key in nodeDic.Keys)
            {
                //Remove all the node created with this entity
                nodeDic[key].Remove(entity);
            }
        }

        #endregion EntityManagement

        #region NodeManagement

        /// <summary>Check if the entoty have all the component of the Node Type as</summary>
        /// <typeparam name="T">Type of the node wanted</typeparam>
        /// <param name="spaceClan">The space clan.</param>
        /// <returns>Return all nodes who match on Entity with the Node Type.</returns>
        public HashSet<T> MatchComponent<T>(SpaceClan spaceClan = SpaceClan.All) where T : Node
        {
            Type askedNode = typeof(T);
            HashSet<T> result = new HashSet<T>();
            foreach (Entity entity in entities)
            {
                if (DontGenerateNode(entity, spaceClan, askedNode, ref result))
                {
                    continue;
                }
                HashSet<IComponent> components;
                if (!TryGetComponents(entity, askedNode, out components))
                {
                    continue;
                }
                try
                {
                    AddNodeToDictionnary<T>(components, entity, askedNode, ref result);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e, " : Can't generate the node");
                }
            }

            return result;
        }

        /// <summary>Adds the node to dictionnary.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="components">The components.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="askedNode">The asked node.</param>
        /// <param name="result">The result.</param>
        private void AddNodeToDictionnary<T>(HashSet<IComponent> components, Entity entity, Type askedNode, ref HashSet<T> result) where T : Node
        {
            T node = GenerateNode<T>(components, entity, askedNode);
            if (node != null)
            {
                if (nodeDic.ContainsKey(askedNode))
                {
                    nodeDic[askedNode].Add(entity, node);
                }
                else
                {
                    var dicoResult = new Dictionary<Entity, Node>();
                    dicoResult.Add(entity, node);
                    nodeDic.Add(askedNode, dicoResult);
                }

                result.Add(node);
            }
        }

        /// <summary>Generates a node using reflection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="components">The components.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="askedNode">The asked node.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private T GenerateNode<T>(HashSet<IComponent> components, Entity entity, Type askedNode) where T : Node
        {
            object[] param = new object[components.Count + 1];
            int i = 0;
            param[i] = entity;
            foreach (var item in components)
            {
                i++;
                param[i] = item;
            }
            return (T)Activator.CreateInstance(askedNode, param);
        }

        /// <summary>This method check if all the component Type include on the node are also in the entity</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="askedNode">The asked node.</param>
        /// <param name="components">A set of different component corresponding to the node find on the entity</param>
        /// <returns>true if we have all Component of the node on the entity, else return false</returns>
        private bool TryGetComponents(Entity entity, Type askedNode, out HashSet<IComponent> components)
        {
            PropertyInfo[] propertiesNode = askedNode.GetProperties();//Get all PropertyInfo of the node

            HashSet<Type> tryContructor = new HashSet<Type>();//Set for check if the constructor of the node take this different type on argument
            tryContructor.Add(typeof(Entity));//First we need to gave an entity

            components = new HashSet<IComponent>();
            foreach (PropertyInfo pI in propertiesNode)
            {
                IComponent component = entity.TryGet(pI.PropertyType);//Try to get the component corresponding to one property type of the asked node

                if (component != null)
                {
                    tryContructor.Add(pI.PropertyType);
                    components.Add(component);
                }
                else
                {
                    //If we don't find the component on the entity, we can't construct the node
                    return false;
                }
            }

            //Using Reflections for get the constructor info of our Node, with the different type of component checked
            //ConstructorInfo ctor = askedNode.GetConstructor(tryContructor.ToArray());
            //int numberOfArguments = ctor?.GetParameters().Length ?? 0;
            //if (numberOfArguments < 1)
            //{
            //    return false;
            //}

            return true;
        }

        /// <summary>This method check if we want the node and if we already generate it</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spaceClan">The space clan.</param>
        /// <param name="askComponents">The ask components.</param>
        /// <param name="result">The result.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool DontGenerateNode<T>(Entity entity, SpaceClan spaceClan, Type askedNode, ref HashSet<T> result) where T : Node
        {
            //We check if the asking clan is the clan of the entity
            if (spaceClan != SpaceClan.All)
            {
                Side side = (Side)entity.TryGet(typeof(Side));
                if (side != null)
                {
                    if (side.Clan != spaceClan)
                    {
                        return true;
                    }
                }
            }
            //We check if we already create the node corresponding to the one ask for this entity
            if (nodeDic.ContainsKey(askedNode) && nodeDic[askedNode].ContainsKey(entity))
            {
                result.Add((T)nodeDic[askedNode][entity]);
                return true;
            }

            return false;
        }

        #endregion NodeManagement

        #region GameManagement

        /// <summary>This function call the update of all game system</summary>
        /// <param name="deltaT">The delta t.</param>
        public void Update(double deltaT)
        {
            foreach (System system in systems)
            {
                system.Update(deltaT);
            }
        }

        /// <summary>Save the graphics of the window and update rendering system</summary>
        /// <param name="g">The g.</param>
        public void Draw(Graphics g)
        {
            this.Graphics = g;
            renderSystem.Update(0.0);
            particleRenderSystem.Update(0.0);
        }

        #endregion GameManagement

        #region ParticlesManagement

        /// <summary>Generate a particle manager, with all its random particle</summary>
        /// <param name="hitBox">The hitBox of the entity generate particles.</param>
        /// <param name="position">The position of the entity generate particles.</param>
        public void AddParticle(Entity entity)
        {
            HitBox hitBox = (HitBox)(entity.TryGet(typeof(HitBox)));
            Position position = (Position)(entity.TryGet(typeof(Position)));
            Random rdn = Game.game.Random;
            if (position != null && hitBox != null)
            {
                float x = position.X + hitBox.Width / 2;
                float y = position.Y + hitBox.Height / 2;

                //We got One particle manager winch manage the life of the particle, when life time over, all children of him disapear
                Entity manager = new Entity("ParticleManager");
                manager.AddComponent(new Position(x, y));
                manager.AddComponent(new TimerParticle(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
                manager.AddComponent(new MovementSpeed(120));

                var tmp = new HashSet<Entity>();
                for (int i = 0; i < 50; i++)
                {
                    //Got a random direction for the particle
                    double randomD = (rdn.NextDouble() * Math.PI * 2);
                    float direcX = (float)(Math.Cos(randomD));
                    float direcy = (float)(Math.Sin(randomD));
                    Entity particle = new Entity("Particle");
                    particle.AddComponent(new Position(x, y));
                    particle.AddComponent(new ParticleRender(ImageProcessing.RandomColor(Game.game.ForegroundColor, -50, 50, rdn), 10, 10));
                    particle.AddComponent(new ParticleDirection(direcX, direcy));
                    tmp.Add(particle);
                }

                manager.AddComponent(new ParticleManager(tmp));
                AddEntity(manager);
            }
        }

        #endregion ParticlesManagement

        #endregion PublicMethods
    }
}