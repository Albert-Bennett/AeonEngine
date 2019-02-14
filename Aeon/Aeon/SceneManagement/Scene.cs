using Aeon.Collections;
using System;
using System.Collections.Generic;

namespace Aeon.SceneManagement
{
    public sealed class Scene
    {
        List<object> sceneObjects = new List<object>();
        SceneInfo info;
        int idx;

        public Scene(string filepath, int index)
        {
            idx = index;

            info = Common.ContentManager.Load<SceneInfo>(filepath);
        }

        internal void Load()
        {
            for (int i = 0; i < info.Objects.Count; i++)
            {
                AeonKeyValuePair<string, ParameterCollection> value = info.Objects.GetValueByIndex(i);

                sceneObjects.Add(AssemblyManager.CreateInstance(value.Key, value.Value));
            }
        }

        internal void Unload()
        {
            for(int i=0;i<sceneObjects.Count;i++)
            {
                if (sceneObjects[i] is Component)
                    ((Component)sceneObjects[i]).Destroy();
                else if (sceneObjects[i] is GameObject)
                    ((GameObject)sceneObjects[i]).Destroy();

                sceneObjects.Clear();
            }
        }

        internal void Destroy()
        {
            for (int i = 0; i < sceneObjects.Count; i++)
                if (sceneObjects[i] is Component)
                    ((Component)sceneObjects[i]).Destroy();
                else if (sceneObjects[i] is GameObject)
                    ((GameObject)sceneObjects[i]).Destroy();

            sceneObjects.Clear();
        }
    }
}
