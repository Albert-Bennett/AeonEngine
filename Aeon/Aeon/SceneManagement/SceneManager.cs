using System.Collections.Generic;

namespace Aeon.SceneManagement
{
    public class SceneManager : EngineComponent
    {
        static List<Scene> scenes = new List<Scene>();
        static Scene current;
        static int idx = 0;

        public SceneManager() : base("SceneManager")
        {
            try
            {
                ScenesInfo info = Common.ContentManager.Load<ScenesInfo>("Scenes");

                if (info.AssemblyRef != null)
                    AssemblyManager.AddAssemblyRef(info.AssemblyRef);

                foreach (string str in info.SceneFilepaths)
                    scenes.Add(new Scene(str, scenes.Count));
            }
            catch { }

            LoadScene(0);
        }

        public static void LoadScene(int i)
        {
            if (scenes.Count > i)
            {
                if (current != null)
                {
                    current.Unload();
                    current = null;
                }

                current = scenes[i];
                current.Load();
                idx = i;
            }
        }
    }
}
