using System.Collections.Generic;
using System.Linq;

namespace Aeon.Rendering
{
    public static class ModelManager
    {
        static List<RenderComponent> models = new List<RenderComponent>();

        public static void AddModel(RenderComponent model)
        {
            RenderComponent mod = (from m in models
                       where m.ID == model.ID
                       select m).FirstOrDefault();

            if (mod == null)
                models.Add(model);
        }

        public static void Remove(RenderComponent model)
        {
            models.Remove(model);
        }

        public static void RenderModels()
        {
            for (int i = 0; i < models.Count; i++)
                if (models[i].Enabled)
                    models[i]._Render();
        }

        public static void RenderOpaque()
        {
            for (int i = 0; i < models.Count; i++)
                if (models[i].Enabled)
                    models[i]._RenderOpaque();
        }
    }
}
