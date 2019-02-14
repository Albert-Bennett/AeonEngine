using Aeon.Interfaces.Game;

namespace Aeon.Engine.Input
{
    /// <summary>
    /// Defines a basic input device.
    /// </summary>
    public abstract class BaseInputItem : EngineComponent, IUpdate
    {
        bool enabled = true;

        public bool Enabled { get { return enabled; } }

        /// <summary>
        /// Creates a new BaseInputItem.
        /// </summary>
        /// <param name="name">The name of the input device.</param>
        public BaseInputItem(string name) : base(name) { }

        protected virtual void PreUpdate() { }
        protected virtual void _Update() { }
        protected virtual void _PostUpdate() { }
        public virtual void Reset() { }

        public void PostUpdate()
        {
            _PostUpdate();
        }

        public void Update()
        {
            PreUpdate();
            _Update();
        }

        public void ToggleEnabled()
        {
            enabled = false;
        }
    }
}
