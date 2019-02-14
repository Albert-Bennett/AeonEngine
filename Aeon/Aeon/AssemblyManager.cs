using Aeon.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aeon
{
    public sealed class AssemblyManager
    {
        static AssemblyManager instance;

        static List<Assembly> assemblies = new List<Assembly>();

        public static AssemblyManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AssemblyManager();

                return instance;
            }
        }

        AssemblyManager() { }

        public static void AddAssemblyRef(string assemblyName)
        {
            Assembly assem = null;

            if (assemblyName != null)
            {
                assem = Assembly.Load(AssemblyName.GetAssemblyName(assemblyName + ".dll"));

                if (assem != null)
                    assemblies.Add(assem);
            }
        }

        public static Type GetType(string typeName)
        {
            Type t = null;

            t = Type.GetType(typeName);

            if (t == null)
                for (int i = 0; i < assemblies.Count; i++)
                    if (t == null)
                        t = assemblies[i].GetType(typeName);

            return t;
        }

        public static object CreateInstance(string typeName, ParameterCollection parameters)
        {
            Type t = null;
            object obj = null;

            t = Type.GetType(typeName);

            if (t == null)
            {
                for (int i = 0; i < assemblies.Count; i++)
                    if (t == null)
                    {
                        t = assemblies[i].GetType(typeName);

                        if (t != null)
                            if (parameters == null)
                                obj = Activator.CreateInstance(t, null);
                            else
                                obj = Activator.CreateInstance(t, parameters.Parameters.ToArray());
                    }
            }
            else
                if (parameters == null)
                    return Activator.CreateInstance(t, null);
                else
                    return Activator.CreateInstance(t, parameters.Parameters);

            if (obj == null)
                throw new ArgumentNullException(typeName + " not created");

            return obj;
        }
    }
}
