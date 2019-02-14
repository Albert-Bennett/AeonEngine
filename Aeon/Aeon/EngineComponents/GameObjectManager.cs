using Aeon.Interfaces.Game;
using System.Collections.Generic;
using System.Linq;

namespace Aeon.EngineComponents
{
    /// <summary>
    /// Defines a class that is used to manage GameObjects.
    /// </summary>
    public sealed class GameObjectManager : EngineComponent, IUpdate, IDispose
    {
        static List<GameObject> GameObjects = new List<GameObject>();
        static List<IDispose> disposeGameObjects = new List<IDispose>();

        static bool enabled = true;

        public bool Enabled { get { return enabled; } }

        public GameObjectManager() : base("GameObjectManager") { }

        internal static void AddGameObject(GameObject GameObject)
        {
            GameObject act = (from a in GameObjects
                         where a.ID == GameObject.ID
                         select a).FirstOrDefault();

            if (act == null)
            {
                GameObject.OnDestroy += new OnDestroyEvent(DestroyGameObject);

                if (!GameObject.initialized)
                    GameObject._Initialize();

                GameObjects.Add(GameObject);

                if (GameObject is IDispose)
                    disposeGameObjects.Add(GameObject as IDispose);
            }
        }

        protected override void Initialize()
        {
            foreach (GameObject a in GameObjects)
                a._Initialize();

            base.Initialize();
        }

        static void DestroyGameObject(string id)
        {
            GameObject GameObject = FindGameObject(id);

            if (GameObject is IDispose)
                disposeGameObjects.Remove(GameObject as IDispose);

            GameObjects.Remove(GameObject);
        }

        /// <summary>
        /// Finds a specific GameObject that matches the given id.
        /// </summary>
        /// <param name="id">The id of an GameObject.</param>
        /// <returns>The result of the search.</returns>
        public static GameObject FindGameObject(string id)
        {
            GameObject GameObject = (from a in GameObjects
                           where a.ID == id
                           select a).FirstOrDefault();

            return GameObject;
        }

        public void Update()
        {
            for (int i = 0; i < GameObjects.Count; i++)
                if (GameObjects[i].Enabled)
                    GameObjects[i]._Update();
        }

        public void Dispose()
        {
            for (int i = 0; i < disposeGameObjects.Count; i++)
                disposeGameObjects[i].Dispose();

            disposeGameObjects.Clear();
            GameObjects.Clear();
        }

        public void ToggleEnabled()
        {
            enabled = !enabled;
        }
    }
}
