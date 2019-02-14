namespace Aeon.Interfaces
{
    /// <summary>
    /// An interface used for objects which 
    /// initialize after all of the other objects have.
    /// </summary>
    public interface IPostInitialize
    {
        void PostInitialize();
    }
}
