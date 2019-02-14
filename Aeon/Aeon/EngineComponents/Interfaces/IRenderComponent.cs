namespace Aeon.EngineComponents.Interfaces
{
    /// <summary>
    /// Defines an EngineComponent that can be rendered.
    /// </summary>
    public interface IRenderComponent
    {
        int Priority { get; }

        void Render();
    }
}
