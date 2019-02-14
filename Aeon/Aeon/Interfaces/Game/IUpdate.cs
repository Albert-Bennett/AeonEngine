namespace Aeon.Interfaces.Game
{
    /// <summary>
    /// An interface that used to manage 
    /// the updating of components.
    /// </summary>
    public interface IUpdate
    {
        bool Enabled { get; }

        void Update();
        void ToggleEnabled();
    }
}
