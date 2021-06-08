using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ECSFramework.Core
{
    /// <summary>Class corresponding to the element of my set composed of all these datas through the components</summary>
    public class Entity
    {
        #region Properties

        public HashSet<IComponent> components { get; private set; } = new HashSet<IComponent>();

        public string Name { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Entity" /> class.</summary>
        /// <param name="name">The name.</param>
        public Entity(string name)
        {
            Name = name;
        }

        #endregion Constructor

        #region PublicMethods

        /// <summary>Add component to the entity.</summary>
        /// <param name="component">The component.</param>
        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        /// <summary>Add a list of component to your entity.</summary>
        /// <param name="comps">The components.</param>
        public void AddComponents(params IComponent[] comps)
        {
            foreach (var component in comps)
            {
                components.Add(component);
            }
        }

        /// <summary>Try to get a component by his type</summary>
        /// <param name="t">The t.</param>
        /// <returns>Return a component if his check the type or null</returns>
        public IComponent TryGet(Type t)
        {
            foreach (IComponent compo in components)
            {
                //if the component check the asking type return him
                if (compo.GetType() == t)
                {
                    return compo;
                }
            }
            return null; ;
        }

        #endregion PublicMethods
    }
}