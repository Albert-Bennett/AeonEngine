using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Aeon.Engine.Input
{
    /// <summary>
    /// Defines the mouse input item.
    /// </summary>
    public class Mouse : BaseInputItem
    {
        float sensitivity = 50f;//Mouse speed

        Vector2 defaultPos;
        MouseState currentState;
        MouseState prevState;
        MouseState emptyState;

        Vector2 pos;
        Ray ray;
        bool createdRay = false;

        public Vector2 Pos
        {
            get { return pos; }
            set
            {
                if (!Vector2.Equals(pos, value))
                {
                    pos = value;
                    Microsoft.Xna.Framework.Input.Mouse.SetPosition((int)pos.X, (int)pos.Y);
                }
            }
        }

        /// <summary>
        /// Finds out the delta of the mouse.
        /// </summary>
        public Vector2 MouseDelta { get { return new Vector2(currentState.X - prevState.X, currentState.Y - prevState.Y); } }

        /// <summary>
        /// The value of the Mouse's scrollwheel.
        /// </summary>
        public float ScrollWheel { get { return currentState.ScrollWheelValue; } }

        /// <summary>
        /// Creates a new Mouse input device.
        /// </summary>
        public Mouse()
            : base("Mouse")
        {
            defaultPos = new Vector2(Common.Device.Viewport.Width / 2,
                Common.Device.Viewport.Height / 2);

            Reset();
        }

        protected override void _Update()
        {
            createdRay = false;

            prevState = currentState;

            SetMousePos();

            currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        void SetMousePos()
        {
            Vector2 delta = new Vector2(currentState.X - prevState.X, currentState.Y - prevState.Y);

            if (!Vector2.Equals(delta, Vector2.Zero))
            {
                delta *= 1 / sensitivity;
                pos = new Vector2(currentState.X + delta.X, currentState.Y + delta.Y);
                Microsoft.Xna.Framework.Input.Mouse.SetPosition((int)pos.X, (int)pos.Y);
            }
        }

        /// <summary>
        /// Calulates a screen space ray using the mouses position.
        /// </summary>
        /// <param name="view">The view matrix to use.</param>
        /// <param name="proj">The projection matrix to use.</param>
        /// <returns>The calculated ray.</returns>
        public Ray GetScreenSpaceRay(Matrix view, Matrix proj)
        {
            if (!createdRay)
            {
                Vector3 near = new Vector3(pos, 0);
                Vector3 far = new Vector3(pos, 1);

                Vector3 nearPoint = Common.Viewport.Unproject(near,
                    proj, view, Matrix.Identity);

                Vector3 farPoint = Common.Viewport.Unproject(far,
                    proj, view, Matrix.Identity);

                Vector3 direct = farPoint - nearPoint;
                direct.Normalize();

                ray = new Ray(nearPoint, direct);
                createdRay = true;
            }

            return ray;
        }

        ButtonState GetBtnState(MouseBtns btn, MouseState state)
        {
            if (btn == MouseBtns.Left)
                return state.LeftButton;
            else if (btn == MouseBtns.Right)
                return state.RightButton;
            else if (btn == MouseBtns.Middle)
                return state.MiddleButton;
            else if (btn == MouseBtns.Alt1)
                return state.XButton1;
            else
                return state.XButton2;
        }

        /// <summary>
        /// A check to see if a mouse button is being held down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public bool IsButtonPressed(MouseBtns button)
        {
            return GetBtnState(button, currentState) == ButtonState.Pressed ? true : false;
        }
        bool IsButtonPressed(MouseBtns button, MouseState state)
        {
            return GetBtnState(button, state) == ButtonState.Pressed ? true : false;
        }

        /// <summary>
        /// A check to see if a mouse button has being released.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public bool IsButtonReleased(MouseBtns button)
        {
            return GetBtnState(button, currentState) == ButtonState.Released ? true : false;
        }
        bool IsButtonReleased(MouseBtns button, MouseState state)
        {
            return GetBtnState(button, state) == ButtonState.Released ? true : false;
        }

        /// <summary>
        /// A check to see if a mouse button has been stroked.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public bool IsButtonStroked(MouseBtns button)
        {
            if (IsButtonReleased(button, prevState) && IsButtonPressed(button, currentState))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Resets the Mouse.
        /// </summary>
        public override void Reset()
        {
            Microsoft.Xna.Framework.Input.Mouse.SetPosition((int)defaultPos.X, (int)defaultPos.Y);

            emptyState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            currentState = prevState = emptyState;
        }

        /// <summary>
        /// Used to set the sensitivity of the Mouse.
        /// </summary>
        /// <param name="value">The value to set the sensitivity to.</param>
        public void SetMouseSensitivity(float value)
        {
            if (sensitivity != value)
                sensitivity = MathHelper.Clamp(sensitivity, 1, 100);
        }
    }
}
