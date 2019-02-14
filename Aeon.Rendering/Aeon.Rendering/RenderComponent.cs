namespace Aeon.Rendering
{
    /// <summary>
    /// Defines a Component which can be 
    /// rendered in both a lit and unlit scene.
    /// </summary>
    public class RenderComponent : Component
    {
        /// <summary>
        /// Creates a new RenderComponent.
        /// </summary>
        /// <param name="id">The id of this RenderComponent.</param>
        public RenderComponent(string id) : base(id) { }

        public void _Render()
        {
            Render();
        }

        public void _RenderOpaque()
        {
            RenderOpaque();
        }

        protected virtual void Render() { }
        protected virtual void RenderOpaque() { }
    }
}
