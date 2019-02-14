using Aeon.EngineComponents;
using Aeon.Interfaces;
using Aeon.Interfaces.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aeon
{
    internal delegate void OnDestroyEvent(string id);

    /// <summary>
    /// Defines an GameObject. In this case an GameObject 
    /// is an abstract class which can contain Components.
    /// 
    /// All update and dispose calls for each Component 
    /// attached to this GameObject. Is managed by said GameObject.
    /// </summary>
    public abstract class GameObject
    {
        List<Component> components =
            new List<Component>();

        List<IUpdate> updateComps = new List<IUpdate>();
        List<IDispose> disposeComps = new List<IDispose>();

        Matrix transform;
        internal OnDestroyEvent OnDestroy;

        string id;
        bool enabled = true;
        bool remove = false;
        internal bool initialized = false;

        public string ID { get { return id; } }
        public bool Enabled { get { return enabled; } }
        public bool Removeable { get { return remove; } }

        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        /// <summary>
        /// Creates a new GameObject.
        /// </summary>
        /// <param name="id">The unique id for this GameObject.</param>
        public GameObject(string id)
        {
            this.id = id;
            GameObjectManager.AddGameObject(this);
            transform = Matrix.Identity;
        }

        internal void _Initialize()
        {
            initialized = true;

            Initialize();

            for (int i = 0; i < components.Count; i++)
                if (components[i] is IPostInitialize)
                    ((IPostInitialize)components[i]).PostInitialize();
        }

        protected virtual void Initialize() { }

        /// <summary>
        /// Used to add a Component to this GameObject.
        /// </summary>
        /// <param name="component">The Component to be added.</param>
        protected void AddComponent(Component component)
        {
            Component comp = null;

            comp = (from c in components
                    where c.ID == component.ID
                    select c).FirstOrDefault();

            if (comp == null)
            {
                component.Parent = this;
                component.OnDestroy += new OnDestroyEvent(RemoveComponent);

                if (component is IUpdate)
                    updateComps.Add(component as IUpdate);

                if (component is IDispose)
                    disposeComps.Add(component as IDispose);

                components.Add(component);

                component._Initialize();
            }
        }

        /// <summary>
        /// Finds a Component matching the given id.
        /// </summary>
        /// <param name="id">The id of the Component to find.</param>
        /// <returns>The result of the search.</returns>
        public Component FindComponent(string id)
        {
            Component comp = null;

            comp = (from c in components
                    where c.ID == id
                    select c).FirstOrDefault();

            return comp;
        }

        /// <summary>
        /// Finds a Component matching the given type.
        /// </summary>
        /// <param name="type">The type of the Component to find</param>
        /// <returns>The result of the search.</returns>
        public Component FindComponent(Type type)
        {
            Component comp = null;

            comp = (from c in components
                    where c.GetType() == type
                    select c).FirstOrDefault();

            return comp;
        }

        /// <summary>
        /// Removes a Component with the given id.
        /// </summary>
        /// <param name="id">The id of the Component to remove.</param>
        public void RemoveComponent(string id)
        {
            Component comp = null;

            comp = (from c in components
                    where c.ID == id
                    select c).FirstOrDefault();

            if (comp != null)
            {
                if (comp is IUpdate)
                    updateComps.Remove(comp as IUpdate);

                if (comp is IDispose)
                    disposeComps.Remove(comp as IDispose);

                components.Remove(comp);
            }
        }

        /// <summary>
        /// Removes a Component of the given type.
        /// </summary>
        /// <param name="type">The type of the Component to remove.</param>
        public void RemoveComponent(Type type)
        {
            Component comp = null;

            comp = (from c in components
                    where c.GetType() == type
                    select c).FirstOrDefault();

            if (comp != null)
                components.Remove(comp);
        }

        public void ToogleEnabled()
        {
            enabled = !enabled;

            for (int i = 0; i < components.Count; i++)
                if (components[i].Enabled != enabled)
                    components[i].ToggleEnabled();
        }

        internal void _Update()
        {
            if (enabled)
            {
                for (int i = 0; i < updateComps.Count; i++)
                    if (updateComps[i].Enabled)
                        updateComps[i].Update();

                Update();
            }
        }

        protected virtual void Update() { }

        /// <summary>
        /// Disposes of the GameObject all of the
        /// resources attached to it's Components.
        /// </summary>
        internal void _Dispose()
        {
            for (int i = 0; i < disposeComps.Count; i++)
                disposeComps[i].Dispose();
        }

        protected virtual void Dispose() { }

        /// <summary>
        /// Destroys this GameObject and all of it's Components.
        /// </summary>
        public void Destroy()
        {
            _Dispose();

            for (int i = 0; i < components.Count; i++)
                components[i].Destroy();

            disposeComps.Clear();
            updateComps.Clear();
            components.Clear();

            remove = true;

            if (OnDestroy != null)
                OnDestroy(id);
        }

        /// <summary>
        /// Used to send a message to Components.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="target">The target Component for the message (if any).</param>
        public void SendMessage(string message, params object[] parameters)
        {
            MethodInfo info = GetType().GetMethod(message);

            if (info != null)
            {
                var comps = (from c in components
                             where c.GetType().GetMethod(message) != null
                             select c).ToList();

                object obj = null;

                foreach (var comp in comps)
                    obj = comp.GetType().GetMethod(message).Invoke(comp, parameters);

            }
        }

        /// <summary>
        /// Finds base class for this object;
        /// </summary>
        /// <returns>The base class for this Component.</returns>
        public Type BaseClass()
        {
            return GetType().BaseType;
        }
    }
}
