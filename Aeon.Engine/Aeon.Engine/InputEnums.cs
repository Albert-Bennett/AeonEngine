namespace Aeon.Engine
{
    /// <summary>
    /// Used to define what player is
    /// using the controller. 
    /// </summary>
    public enum PlayerIndex
    {
        One = 0,
        Two = 1,
        Three = 2,
        Four = 3
    }

    /// <summary>
    /// Various buttons that are on the GamePad.
    /// </summary>
    public enum ControlPadBtns
    {
        None = 0,

        /// <summary>
        /// Represents the Start button on the GamePad.
        /// </summary>
        Start,

        /// <summary>
        /// Represents the Back button on the GamePad.
        /// </summary>
        Back,

        /// <summary>
        /// Represents the A button on the GamePad.
        /// </summary>
        A,

        /// <summary>
        /// Represents the B button on the GamePad.
        /// </summary>
        B,

        /// <summary>
        /// Represents the X button on the GamePad.
        /// </summary>
        X,

        /// <summary>
        /// Represents the Y button on the GamePad.
        /// </summary>
        Y,

        /// <summary>
        /// Represents the Left Shoulder button on the GamePad.
        /// </summary>
        LeftShoulder,

        /// <summary>
        /// Represents the Left thumbstick button on the GamePad.
        /// </summary>
        LeftStick,

        /// <summary>
        /// Represents the Left Trigger on the GamePad.
        /// </summary>
        LeftTrigger,

        /// <summary>
        /// Represents the Left thumbstick is in it's up direction on the GamePad.
        /// </summary>
        LeftThumbStickUp,

        /// <summary>
        /// Represents the Left thumbstick is in it's down direction on the GamePad.
        /// </summary>
        LeftThumbStickDown,

        /// <summary>
        /// Represents the Left thumbstick is in it's left direction on the GamePad.
        /// </summary>
        LeftThumbStickLeft,

        /// <summary>
        /// Represents the Left thumbstick is in it's right direction on the GamePad.
        /// </summary>
        LeftThumbStickRight,

        /// <summary>
        /// Represents the Right Shoulder button on the GamePad.
        /// </summary>
        RightShoulder,

        /// <summary>
        /// Represents the Right thumbstick button on the GamePad.
        /// </summary>
        RightStick,

        /// <summary>
        /// Represents the Right Trigger on the GamePad.
        /// </summary>
        RightTrigger,

        /// <summary>
        /// Represents the Right thumbstick is in it's up direction on the GamePad.
        /// </summary>
        RightThumbStickUp,

        /// <summary>
        /// Represents the Right thumbstick is in it's down direction on the GamePad.
        /// </summary>
        RightThumbStickDown,

        /// <summary>
        /// Represents the Right thumbstick is in it's left direction on the GamePad.
        /// </summary>
        RightThumbStickLeft,

        /// <summary>
        /// Represents the Right thumbstick is in it's right direction on the GamePad.
        /// </summary>
        RightThumbStickRight,

        /// <summary>
        /// Represents the Left button on the dpad on the GamePad.
        /// </summary>
        LeftPad,

        /// <summary>
        /// Represents the Right button on the dpad on the GamePad.
        /// </summary>
        RightPad,

        /// <summary>
        /// Represents the Up button on the dpad on the GamePad.
        /// </summary>
        UpPad,

        /// <summary>
        /// Represents the Down button on the dpad on the GamePad.
        /// </summary>
        DownPad,

        Count
    }

    /// <summary>
    /// The indexes of the GamePad's triggers.
    /// </summary>
    public enum TriggersIdx
    {
        /// <summary>
        /// Used to represent elements on the left side of the GamePad.
        /// </summary>
        Left = 0,

        /// <summary>
        /// Used to represent elements on the right side of the GamePad.
        /// </summary>
        Right = 1,

        Count
    }

    /// <summary>
    /// A set of keys that this Keyboard class can detect.
    /// </summary>
    public enum Keys
    {
        /// <summary>
        /// Represents the A key.
        /// </summary>
        A,

        /// <summary>
        /// Represents the B key.
        /// </summary>
        B,

        /// <summary>
        /// Represents the C key.
        /// </summary>
        C,

        /// <summary>
        /// Represents the D key.
        /// </summary>
        D,

        /// <summary>
        /// Represents the E key.
        /// </summary>
        E,

        /// <summary>
        /// Represents the F key.
        /// </summary>
        F,

        /// <summary>
        /// Represents the G key.
        /// </summary>
        G,

        /// <summary>
        /// Represents the H key.
        /// </summary>
        H,

        /// <summary>
        /// Represents the I key.
        /// </summary>
        I,

        /// <summary>
        /// Represents the J key.
        /// </summary>
        J,

        /// <summary>
        /// Represents the K key.
        /// </summary>
        K,

        /// <summary>
        /// Represents the L key.
        /// </summary>
        L,

        /// <summary>
        /// Represents the M key.
        /// </summary>
        M,

        /// <summary>
        /// Represents the N key.
        /// </summary>
        N,

        /// <summary>
        /// Represents the O key.
        /// </summary>
        O,

        /// <summary>
        /// Represents the P key.
        /// </summary>
        P,

        /// <summary>
        /// Represents the Q key.
        /// </summary>
        Q,

        /// <summary>
        /// Represents the R key.
        /// </summary>
        R,

        /// <summary>
        /// Represents the S key.
        /// </summary>
        S,

        /// <summary>
        /// Represents the T key.
        /// </summary>
        T,

        /// <summary>
        /// Represents the U key.
        /// </summary>
        U,

        /// <summary>
        /// Represents the V key.
        /// </summary>
        V,

        /// <summary>
        /// Represents the W key.
        /// </summary>
        W,

        /// <summary>
        /// Represents the X key.
        /// </summary>
        X,

        /// <summary>
        /// Represents the Y key.
        /// </summary>
        Y,

        /// <summary>
        /// Represents the Z key.
        /// </summary>
        Z,

        /// <summary>
        /// Represents the Tab key.
        /// </summary>
        Tab,

        /// <summary>
        /// Represents the Left Shift key.
        /// </summary>
        ShiftL,

        /// <summary>
        /// Represents the Right Shift key.
        /// </summary>
        ShiftR,

        /// <summary>
        /// Represents the Left Control key.
        /// </summary>
        CtrlL,

        /// <summary>
        /// Represents the Right Control key.
        /// </summary>
        CtrlR,

        /// <summary>
        /// Represents the Left Alt key.
        /// </summary>
        AltL,

        /// <summary>
        /// Represents the Right Alt key.
        /// </summary>
        AltR,

        /// <summary>
        /// Represents the Escape key.
        /// </summary>
        Esc,

        /// <summary>
        /// Represents the Enter key.
        /// </summary>
        Enter,

        /// <summary>
        /// Represents the Spacebar.
        /// </summary>
        Space,

        /// <summary>
        /// Represents the number one key.
        /// </summary>
        Num1,

        /// <summary>
        /// Represents the number two key.
        /// </summary>
        Num2,

        /// <summary>
        /// Represents the number three key.
        /// </summary>
        Num3,

        /// <summary>
        /// Represents the number four key.
        /// </summary>
        Num4,

        /// <summary>
        /// Represents the number five key.
        /// </summary>
        Num5,

        /// <summary>
        /// Represents the number six key.
        /// </summary>
        Num6,

        /// <summary>
        /// Represents the number seven key.
        /// </summary>
        Num7,

        /// <summary>
        /// Represents the number eight key.
        /// </summary>
        Num8,

        /// <summary>
        /// Represents the number nine key.
        /// </summary>
        Num9,

        /// <summary>
        /// Represents the number zero key.
        /// </summary>
        Num0,

        /// <summary>
        /// Represents the up arrow key.
        /// </summary>
        Up,

        /// <summary>
        /// Represents the down arrow key.
        /// </summary>
        Down,

        /// <summary>
        /// Represents the left arrow key.
        /// </summary>
        Left,

        /// <summary>
        /// Represents the right arrow key.
        /// </summary>
        Right
    }

    /// <summary>
    /// Defines a the buttons that are on a mouse.
    /// </summary>
    public enum MouseBtns
    {
        /// <summary>
        /// Represents the left button.
        /// </summary>
        Left,

        /// <summary>
        /// Represents the right button.
        /// </summary>
        Right,

        /// <summary>
        /// Represents the middle button.
        /// </summary>
        Middle,

        /// <summary>
        /// Represents an alternate side button.
        /// </summary>
        Alt1,

        /// <summary>
        /// Represents an alternate side button.
        /// </summary>
        Alt2
    }

}