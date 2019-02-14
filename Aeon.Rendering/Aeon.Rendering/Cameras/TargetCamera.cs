using Microsoft.Xna.Framework;

namespace Aeon.Rendering.Cameras
{
    /// <summary>
    /// Defines a target camera.
    /// </summary>
    public class TargetCamera : CameraComponent
    {
        /// <summary>
        /// Creates a new TargetCamera.
        /// </summary>
        /// <param name="pos">The TargetCamera's position.</param>
        /// <param name="target">The TargetCamera's look at target.</param>
        /// <param name="nearPlane">The TargetCamera's near plane.</param>
        /// <param name="farPlane">The TargetCamera's far plane.</param>
        public TargetCamera(string id, Vector3 pos, Vector3 target,
            float nearPlane, float farPlane)
            : base(id, nearPlane, farPlane)
        {
            this.pos = pos;
            this.Target = target;

            originalPos = pos;
        }

        public void Move(Vector3 pos)
        {
            this.pos += pos;
        }

        protected override void _Update()
        {
            View = Matrix.CreateLookAt(pos, Target, Vector3.Up);
        }
    }
}
