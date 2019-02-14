using Aeon.EngineComponents.Interfaces;
using Aeon.Interfaces;
using Aeon.Interfaces.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aeon.EngineComponents
{
    /// <summary>
    /// Defines a class which is used to manage 
    /// the basic functions of every EngineComponent.
    /// </summary>
    public sealed class EngineComponentManager : IDispose
    {
        static List<EngineComponent> comps = new List<EngineComponent>();

        static List<IRenderComponent> renderComps = new List<IRenderComponent>();
        static List<IUpdate> updateComps = new List<IUpdate>();
        static List<IDispose> disposableComps = new List<IDispose>();

        static bool initialized = false;

        internal static void AddComp(EngineComponent comp)
        {
            EngineComponent c = (from com in comps
                                 where com.ID == comp.ID
                                 select com).FirstOrDefault();

            if (c == null)
            {
                if (initialized)
                    comp._Init();

                comps.Add(comp);

                if (comp is IRenderComponent)
                    renderComps.Add(comp as IRenderComponent);

                if (comp is IUpdate)
                    updateComps.Add(comp as IUpdate);

                if (comp is IDispose)
                    disposableComps.Add(comp as IDispose);
            }
        }

        public static EngineComponent Find(string id)
        {
            EngineComponent e = (from en in comps
                                 where en.ID == id
                                 select en).FirstOrDefault();

            return e;
        }

        public static EngineComponent Find(Type type)
        {
            EngineComponent e = (from en in comps
                                 where en.GetType() == type
                                 select en).FirstOrDefault();

            return e;
        }

        public void Initialize()
        {
            initialized = true;

            for (int i = 0; i < comps.Count; i++)
                comps[i]._Init();
        }

        public void PostInitialize()
        {
            for (int i = 0; i < comps.Count; i++)
                if (comps[i] is IPostInitialize)
                    ((IPostInitialize)comps[i]).PostInitialize();
        }

        public void Update()
        {
            for (int i = 0; i < updateComps.Count; i++)
                if (updateComps[i].Enabled)
                    updateComps[i].Update();
        }

        public void Render()
        {
            int prior = 0;
            int i = 0;

            while (i < renderComps.Count)
            {
                for (int j = 0; j < renderComps.Count; j++)
                    if (renderComps[j].Priority == prior)
                    {
                        renderComps[j].Render();
                        i++;
                    }

                prior++;
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < disposableComps.Count; i++)
                disposableComps[i].Dispose();

            comps.Clear();
            updateComps.Clear();
            disposableComps.Clear();
            renderComps.Clear();
        }

        public void SendMessage(string message, params object[] parameters)
        {
            MethodInfo info = GetType().GetMethod(message);

            if (info != null)
                info.Invoke(this, parameters);
            else
            {
                    var cs = (from c in comps
                                 where c.GetType().GetMethod(message) != null
                                 select c).ToList();

                    foreach (var c in cs)
                        c.GetType().GetMethod(message).Invoke(c, parameters);
            }
        }
    }
}
