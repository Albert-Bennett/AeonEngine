using System;
using System.Collections.Generic;
using System.Linq;

namespace Aeon.Rendering.Lighting
{
    public sealed class LightManager
    {
        static List<LightComponent> lights = new List<LightComponent>();

        internal static List<LightComponent> SceneLights
        {
            get
            {
                return (from l in lights
                        where l.Enabled == true
                        select l).ToList();
            }
        }

        /// <summary>
        /// Find a list of lights of a given type.
        /// </summary>
        /// <typeparam name="T">The type of light to find.</typeparam>
        /// <returns>The result of the findings.</returns>
        public static List<T> GetLightsByType<T>() where T : LightComponent
        {
            List<LightComponent> lights = SceneLights;

            List<T> res = new List<T>();

            foreach (LightComponent l in lights)
                if (l.GetType() == typeof(T))
                    res.Add(l as T);

            return res;
        }

        public static void AddLight(LightComponent light)
        {
            LightComponent temp = (from l in lights
                                   where l.ID == light.ID
                                   select l).FirstOrDefault();

            if (temp == null)
                lights.Add(light);
        }

        public static void AddLight(string typeName, object[] parameters)
        {
            Type t = null;

            t = Type.GetType(typeName);

            if (t == null)
                t = AssemblyManager.GetType(typeName);

            if (t != null)
            {
                object obj = Activator.CreateInstance(t, parameters);

                if (obj is LightComponent)
                    lights.Add(obj as LightComponent);
                else
                    throw new System.ArgumentNullException("The type name inputted isn't valid");
            }
        }

        public static void Remove(LightComponent light)
        {
            try
            {
                lights.Remove(light);
            }
            catch
            {
                throw new ArgumentNullException("A light with the given id dosen't exist.");
            }
        }
    }
}
