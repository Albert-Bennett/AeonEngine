using Microsoft.Xna.Framework.Input;

namespace Aeon.Engine.Input
{
    /// <summary>
    /// Defines a Keyboard input device.
    /// </summary>
    public class Keyboard : BaseInputItem
    {
        KeyboardState currentState;
        KeyboardState prevState;
        KeyboardState emptyState;

        /// <summary>
        /// Creates a new Keyboard.
        /// </summary>
        public Keyboard()
            : base("Keyboard")
        {
            emptyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        /// <summary>
        /// Updates the Keyboard.
        /// </summary>
        protected override void _Update()
        {
            prevState = currentState;

            currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        /// <summary>
        /// Checks if a key has been presssed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>If that key has been pressed.</returns>
        public bool IsKeyPressed(Keys key)
        {
            return GetBtnPressed(key, currentState);
        }

        /// <summary>
        /// Checks if a key has been released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>If that key has been pressed.</returns>
        public bool IsKeyReleased(Keys key)
        {
            return GetBtnReleased(key, currentState);
        }

        /// <summary>
        /// Checks if a key has been stroked.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>If that key has been stroked.</returns>
        public bool IsKeyStroked(Keys key)
        {
            return (!GetBtnPressed(key, prevState) && IsKeyPressed(key));
        }

        bool GetBtnPressed(Keys key, KeyboardState state)
        {
            #region Letters
            if (key == Keys.A)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A);
            else if (key == Keys.B)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.B);
            else if (key == Keys.C)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C);
            else if (key == Keys.D)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D);
            else if (key == Keys.E)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E);
            else if (key == Keys.F)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F);
            else if (key == Keys.G)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.G);
            else if (key == Keys.H)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.H);
            else if (key == Keys.I)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.I);
            else if (key == Keys.J)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.J);
            else if (key == Keys.K)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.K);
            else if (key == Keys.L)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.L);
            else if (key == Keys.M)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.M);
            else if (key == Keys.N)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.N);
            else if (key == Keys.O)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.O);
            else if (key == Keys.P)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P);
            else if (key == Keys.Q)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q);
            else if (key == Keys.R)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.R);
            else if (key == Keys.S)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S);
            else if (key == Keys.T)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.T);
            else if (key == Keys.U)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.U);
            else if (key == Keys.V)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.V);
            else if (key == Keys.W)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W);
            else if (key == Keys.X)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X);
            else if (key == Keys.Y)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Y);
            else if (key == Keys.Z)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z);
            #endregion
            #region Numbers
            else if (key == Keys.Num0)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad0);
            else if (key == Keys.Num1)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad1);
            else if (key == Keys.Num2)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad2);
            else if (key == Keys.Num3)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad3);
            else if (key == Keys.Num4)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad4);
            else if (key == Keys.Num5)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad5);
            else if (key == Keys.Num6)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad6);
            else if (key == Keys.Num7)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad7);
            else if (key == Keys.Num8)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad8);
            else if (key == Keys.Num9)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad9);
            #endregion
            #region AltKeys
            else if (key == Keys.Esc)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape);
            else if (key == Keys.Enter)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter);
            else if (key == Keys.AltL)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt);
            else if (key == Keys.AltR)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightAlt);
            else if (key == Keys.CtrlL)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl);
            else if (key == Keys.CtrlR)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl);
            else if (key == Keys.ShiftL)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift);
            else if (key == Keys.ShiftR)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift);
            else if (key == Keys.Tab)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Tab);
            else if (key == Keys.Space)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space);
            #endregion
            #region Arrows
            else if (key == Keys.Up)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up);
            else if (key == Keys.Down)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down);
            else if (key == Keys.Left)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left);
            else if (key == Keys.Right)
                return state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right);
            #endregion

            else return false;
        }
        bool GetBtnReleased(Keys key, KeyboardState state)
        {
            #region Letters
            if (key == Keys.A)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.A);
            else if (key == Keys.B)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.B);
            else if (key == Keys.C)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.C);
            else if (key == Keys.D)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.D);
            else if (key == Keys.E)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.E);
            else if (key == Keys.F)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F);
            else if (key == Keys.G)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.G);
            else if (key == Keys.H)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.H);
            else if (key == Keys.I)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.I);
            else if (key == Keys.J)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.J);
            else if (key == Keys.K)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.K);
            else if (key == Keys.L)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.L);
            else if (key == Keys.M)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.M);
            else if (key == Keys.N)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.N);
            else if (key == Keys.O)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.O);
            else if (key == Keys.P)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.P);
            else if (key == Keys.Q)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Q);
            else if (key == Keys.R)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.R);
            else if (key == Keys.S)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.S);
            else if (key == Keys.T)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.T);
            else if (key == Keys.U)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.U);
            else if (key == Keys.V)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.V);
            else if (key == Keys.W)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.W);
            else if (key == Keys.X)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.X);
            else if (key == Keys.Y)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Y);
            else if (key == Keys.Z)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Z);
            #endregion
            #region Numbers
            else if (key == Keys.Num0)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad0);
            else if (key == Keys.Num1)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad1);
            else if (key == Keys.Num2)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad2);
            else if (key == Keys.Num3)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad3);
            else if (key == Keys.Num4)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad4);
            else if (key == Keys.Num5)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad5);
            else if (key == Keys.Num6)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad6);
            else if (key == Keys.Num7)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad7);
            else if (key == Keys.Num8)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad8);
            else if (key == Keys.Num9)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.NumPad9);
            #endregion
            #region AltKeys
            else if (key == Keys.Esc)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape);
            else if (key == Keys.Enter)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Enter);
            else if (key == Keys.AltL)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftAlt);
            else if (key == Keys.AltR)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.RightAlt);
            else if (key == Keys.CtrlL)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftControl);
            else if (key == Keys.CtrlR)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.RightControl);
            else if (key == Keys.ShiftL)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftShift);
            else if (key == Keys.ShiftR)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.RightShift);
            else if (key == Keys.Tab)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Tab);
            else if (key == Keys.Space)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Space);
            #endregion
            #region Arrows
            else if (key == Keys.Up)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Up);
            else if (key == Keys.Down)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Down);
            else if (key == Keys.Left)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Left);
            else if (key == Keys.Right)
                return state.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Right);
            #endregion

            else return false;
        }

        /// <summary>
        /// Resets all of the input states for the Keyboard.
        /// </summary>
        public override void Reset()
        {
            currentState = prevState = emptyState;
        }
    }
}
