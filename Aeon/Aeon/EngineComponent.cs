using Aeon.EngineComponents;

namespace Aeon
{
    /// <summary>
    /// Defines an abstract class similar to the 
    /// component class but. This type of component is to be 
    /// used specifically for components which are to be managed by the engine. 
    /// </summary>
    public abstract class EngineComponent
    {
        string id;
        internal bool initialized = false;

        public string ID { get { return id; } }

        public EngineComponent(string id)
        {
            this.id = id;
            EngineComponentManager.AddComp(this);
        }

        internal void _Init()
        {
            initialized = true;
            Initialize();
        }

        protected virtual void Initialize() { }
    }
}
