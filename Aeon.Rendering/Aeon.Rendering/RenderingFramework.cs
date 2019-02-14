using Aeon.EngineComponents.Interfaces;
using Aeon.Rendering.Lighting;

namespace Aeon.Rendering
{
    public sealed class RenderingFramework : EngineComponent, IRenderComponent
    {
        static CameraManager cameraManager;
        static LightManager lightManager;
        static LightingPrePass lpp;

        public int Priority { get { return 0; } }

        public RenderingFramework() : base("RenderingFramework") { }

        protected override void Initialize()
        {
            lightManager = new LightManager();
            cameraManager = new CameraManager();

            lpp = new LightingPrePass();

            base.Initialize();
        }

        public void Render()
        {
            lpp.Render();
            lpp.RenderDebug();
        }
    }
}
