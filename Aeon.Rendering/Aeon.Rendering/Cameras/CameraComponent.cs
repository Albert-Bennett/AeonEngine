using Aeon.Interfaces.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aeon.Rendering.Cameras
{
    /// <summary>
    /// Defines an abstract class which 
    /// can be used for the basis of all cameras.
    /// </summary>
    public abstract class CameraComponent : Component, IUpdate
    {
        protected Vector3 pos;
        protected Vector3 rot = new Vector3();
        protected Vector3 originalPos;

        Matrix view;
        Matrix projection;

        BoundingSphere bounds;

        /// <summary>
        /// This BaseCamera's look at target.
        /// </summary>
        public Vector3 Target { get; protected set; }

        /// <summary>
        /// A matrix used to define it's position and rotation.
        /// </summary>
        /// <summary>
        /// The camera's transformation matrix.
        /// </summary>
        public Matrix LocalTransform
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) *
                    Matrix.CreateTranslation(pos);
            }
        }

        /// <summary>
        /// This BaseCamera's view matrix as a Vector3.
        /// </summary>
        public Vector3 ViewVec { get; protected set; }

        /// <summary>
        /// This BaseCamera's view matrix.
        /// </summary>
        public Matrix View
        {
            get { return view; }

            protected set
            {
                view = value;
                GenerateFrustum();
            }
        }

        /// <summary>
        /// This BaseCamera's projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }

            protected set
            {
                projection = value;
                GenerateFrustum();
            }
        }

        /// <summary>
        /// The BaseCamera's far plane value.
        /// </summary>
        public float FarPlane { get; protected set; }

        /// <summary>
        /// The BaseCamera's near plane value.
        /// </summary>
        public float NearPlane { get; protected set; }

        protected GraphicsDevice Device { get; private set; }

        protected BoundingFrustum ViewFrustum { get; private set; }

        public BoundingSphere Bounds { get { return bounds; } }

        /// <summary>
        /// Creates a new BaseCamera.
        /// </summary>
        /// <param name="nearPlane">The BaseCamera's near plane value.</param>
        /// <param name="farPlane">The BaseCamera's far plane value.</param>
        public CameraComponent(string id, float nearPlane, float farPlane)
            : base(id)
        {
            Device = Common.Device;

            NearPlane = nearPlane;
            FarPlane = farPlane;

            GeneratePerspectiveProjection(Microsoft.Xna.Framework.MathHelper.PiOver4, NearPlane, FarPlane);

            UpdateBounds();

            CameraManager.SetCamera(this);
        }

        void GeneratePerspectiveProjection(float fieldOfView, float nearPlane, float farPlane)
        {
            PresentationParameters pp = Device.PresentationParameters;

            float aspectRatio = Device.Viewport.AspectRatio;

            Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);
        }

        void GenerateFrustum()
        {
            ViewFrustum = new BoundingFrustum(view * projection);
        }

        public void Update()
        {
            UpdateBounds();

            _Update();
        }

        void UpdateBounds()
        {
            bounds = new BoundingSphere(pos, NearPlane * 2);
        }

        protected virtual void _Update() { }

        /// <summary>
        /// Sets the position of this camera.
        /// </summary>
        /// <param name="newPos">The new position for the camera.</param>
        public void SetPos(Vector3 newPos)
        {
            pos = newPos;
        }

        /// <summary>
        /// Check to see if a bounding volume is within the bounds of the view frustum
        /// </summary>
        /// <param name="box">BoundingBox to be checked</param>
        /// <returns>Result</returns>
        public bool IsInView(BoundingBox box)
        {
            return ViewFrustum.Contains(box) != ContainmentType.Disjoint;
        }

        /// <summary>
        /// Check to see if a bounding volume is within the bounds of the view frustum
        /// </summary>
        /// <param name="sphere">BoundingSphere to be checked</param>
        /// <returns>Result</returns>
        public bool IsInView(BoundingSphere sphere)
        {
            return ViewFrustum.Contains(sphere) != ContainmentType.Disjoint;
        }

        /// <summary>
        /// Changes this look at target.
        /// </summary>
        /// <param name="target">The position to look at.</param>
        public void ChangeTarget(Vector3 target)
        {
            Target = target;
        }
    }
}
