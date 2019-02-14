using System;

namespace Aeon
{
    /// <summary>
    /// Defines a component that can be attached to an GameObject.
    /// </summary>
    public abstract class Component
    {
        GameObject parent;

        string id;
        bool enabled = true;
        bool remove;

        public GameObject Parent
        {
            get { return parent; }
            internal set { parent = value; }
        }

        public string ID { get { return id; } }
        public bool Enabled { get { return enabled; } }
        public bool Removeable { get { return remove; } }

        internal OnDestroyEvent OnDestroy;

        /// <summary>
        /// Creates a new Component.
        /// </summary>
        /// <param name="id">The unique id for the Component.</param>
        public Component(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// Initializes the Component.
        /// </summary>
        internal void _Initialize()
        {
            Initialize();
        }

        protected virtual void Initialize() { }

        /// <summary>
        /// Toggles the enabled property of this Component.
        /// </summary>
        public void ToggleEnabled()
        {
            enabled = !enabled;
        }

        /// <summary>
        /// Changes the remove property of 
        /// this Component to false.
        /// </summary>
        public virtual void Destroy()
        {
            remove = true;

            OnDestroy(id);
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
