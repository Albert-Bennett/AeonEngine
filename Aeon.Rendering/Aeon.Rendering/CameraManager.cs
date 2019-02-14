using Aeon.Rendering.Cameras;

namespace Aeon.Rendering
{
    public sealed class CameraManager : EngineComponent
    {
        static CameraComponent currentCamera;

        public static CameraComponent CurrentCamera
        {
            get { return currentCamera; }
        }

        public CameraManager() : base("CameraManager") { }

        internal static void SetCamera(CameraComponent camera)
        {
            if (currentCamera != null)
            {
                if (currentCamera.ID != camera.ID)
                    currentCamera = camera;
            }
            else
                currentCamera = camera;
        }
    }
}
