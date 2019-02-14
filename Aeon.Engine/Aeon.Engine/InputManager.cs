using Aeon.Engine.Input;
using Microsoft.Xna.Framework;

namespace Aeon.Engine
{
    /// <summary>
    /// A class that is used to process input from the player
    /// through the use of input devices.
    /// </summary>
    public class InputManager
    {
        static Keyboard keyboard = null;
        static Mouse mouse = null;

        /// <summary>
        /// Creates a new InputManager.
        /// </summary>
        public InputManager()
        {
            keyboard = new Keyboard();
            mouse = new Mouse();
        }

        #region Keyboard

        /// <summary>
        /// Finds out if a key on the Keyboard is being held down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyPressed(Keys key)
        {
            if (keyboard != null)
                return keyboard.IsKeyPressed(key);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a key on the Keyboard has been released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyReleased(Keys key)
        {
            if (keyboard != null)
                return keyboard.IsKeyReleased(key);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a key on the Keyboard has been stroked.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyStroked(Keys key)
        {
            if (keyboard != null)
                return keyboard.IsKeyStroked(key);
            else
                return false;
        }

        #endregion
        #region Mouse

        /// <summary>
        /// The position of the Mouse on the screen.
        /// </summary>
        public static Vector2 MousePos
        {
            get
            {
                if (mouse != null)
                    return mouse.Pos;
                else
                    return Vector2.Zero;
            }
        }

        /// <summary>
        /// A check to see if the mouse is on the screen.
        /// </summary>
        public static bool MouseIsOnScreen
        {
            get
            {
                if (MousePos.X < Common.Device.Viewport.Width && MousePos.X > Common.Device.Viewport.X)
                    if (MousePos.Y < Common.Device.Viewport.Height && MousePos.Y > Common.Device.Viewport.Y)
                        return true;

                return false;
            }
        }

        /// <summary>
        /// The delta between mouse positions.
        /// </summary>
        public static Vector2 MouseDelta
        {
            get
            {
                if (mouse != null)
                    return mouse.MouseDelta;
                else
                    return Vector2.Zero;
            }
        }

        /// <summary>
        /// Finds the Mouse's scroll wheel value.
        /// </summary>
        public static float ScrollWheelValue
        {
            get
            {
                if (mouse != null)
                    return mouse.ScrollWheel;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Finds out if a button on the Mouse is being held down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsButtonPressed(MouseBtns button)
        {
            if (mouse != null)
                return mouse.IsButtonPressed(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a button on the Mouse has been released.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsButtonReleased(MouseBtns button)
        {
            if (mouse != null)
                return mouse.IsButtonReleased(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a button on the Mouse has been stroked.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsButtonStroked(MouseBtns button)
        {
            if (mouse != null)
                return mouse.IsButtonStroked(button);
            else
                return false;
        }

        public static Ray GetScreenSpaceRay(Matrix view, Matrix projection)
        {
            return mouse.GetScreenSpaceRay(view, projection);
        }

        #endregion
        #region Helpers

        /// <summary>
        /// Sets the sensitivity for the mouse.
        /// </summary>
        /// <param name="value">The mouse's sencitivity.</param>
        public void SetMouseSensitivity(float value)
        {
            if (mouse != null)
                mouse.SetMouseSensitivity(value);
        }

        #endregion
    }
}
