using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace Aeon
{
    /// <summary>
    /// An event to deal with the changing of the viewports bounds.
    /// </summary>
    public delegate void ViewportChanged();

    /// <summary>
    /// An event to deal with the exiting of the game.
    /// </summary>
    public delegate void ExitingEvent();

    /// <summary>
    /// Defines the common elements in Aeon i.e GraphicsDevice.
    /// </summary>
    public sealed class Common
    {
        static Game game;
        static TimeSpan elapsedTimeDelta;
        static TimeSpan totalSecs = TimeSpan.Zero;
        static SpriteBatch batch;
        static GraphicsDeviceManager deviceManager;
        static VideoPlayer videoPlayer;
        static ContentManager content;

        static Vector2 scrnResolution;

        static Vector2 OnePrecent
        {
            get
            {
                Vector2 vec = new Vector2();

                vec.X = (float)Math.Round((float)Viewport.Width / 100f);
                vec.Y = (float)Math.Round((float)Viewport.Height / 100f);

                return vec;
            }
        }

        /// <summary>
        /// An event to be thrown when the viewport has been changed.
        /// </summary>
        public event ViewportChanged ViewChanged;

        /// <summary>
        /// An event to be thrown when the game is being exited.
        /// </summary>
        public static event ExitingEvent OnExit;

        /// <summary>
        /// The game.
        /// </summary>
        public static Game Game { get { return game; } }

        /// <summary>
        /// The common GraphicsDevice.
        /// </summary>
        public static GraphicsDevice Device { get { return Game.GraphicsDevice; } }

        /// <summary>
        /// The common GraphicsDeviceManager.
        /// </summary>
        public static GraphicsDeviceManager DeviceManager { get { return deviceManager; } }

        /// <summary>
        /// The previous size of the viewport.
        /// </summary>
        public static Vector2 PreviousViewportSize { get; private set; }

        public static Vector2 Resolution
        {
            get { return scrnResolution; }
            private set { scrnResolution = value; }
        }

        /// <summary>
        /// The common Viewport.
        /// </summary>
        public static Viewport Viewport { get { return Device.Viewport; } }

        /// <summary>
        /// The common ContentManager.
        /// </summary>
        public static ContentManager ContentManager { get { return content; } }

        /// <summary>
        /// The common SpriteBatch.
        /// </summary>
        public static SpriteBatch Batch { get { return batch; } }

        /// <summary>
        /// The common ElapsedTimeDelta.
        /// </summary>
        public static TimeSpan ElapsedTimeDelta { get { return Common.elapsedTimeDelta; } }

        /// <summary>
        /// Returns the video player for the game.
        /// </summary>
        public static VideoPlayer VideoPlayer { get { return videoPlayer; } }

        /// <summary>
        /// The total amount of seconds that the game has been running for.
        /// </summary>
        public static TimeSpan TotalPlayTimeSecs { get { return Common.totalSecs; } }

        /// <summary>
        /// Creates a new Common.
        /// </summary>
        /// <param name="game">Game.</param>
        public Common(Game game)
        {
            deviceManager = new GraphicsDeviceManager(game);
            Common.game = game;
            Common.content = game.Content;
            Common.content.RootDirectory = "Content\\";
        }

        public void Initialize()
        {
            batch = new SpriteBatch(game.GraphicsDevice);
            PreviousViewportSize = new Vector2(Viewport.Width, Viewport.Height);
            videoPlayer = new VideoPlayer();
        }

        /// <summary>
        /// Changes the game to full screen.
        /// </summary>
        public void FullScreen()
        {
            if (ViewChanged != null)
            {
                PreviousViewportSize = new Vector2(Viewport.Width, Viewport.Height);

                DeviceManager.IsFullScreen = true;
                DeviceManager.ApplyChanges();

                if (ViewChanged != null)
                    ViewChanged();
            }
        }

        public static void SetResolution(Vector2 resolution)
        {
            Common.scrnResolution = resolution;
        }

        /// <summary>
        /// Sets the elasped time delta.
        /// </summary>
        /// <param name="time">The time to set to.</param>
        public void SetElaspedTimeDelta(TimeSpan time)
        {
            elapsedTimeDelta = time;
            totalSecs += time;
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        public static void ExitGame()
        {
            if (OnExit != null)
                OnExit();
        }

        /// <summary>
        /// Finds a Vector2 representing co-ordinates in 
        /// relation to the screen size in stead of in pixels.
        /// </summary>
        /// <param name="precentages">The precentage values to be used.</param>
        /// <returns>The result of the calculation.</returns>
        public static Vector2 GetPositionalCoordinates(Vector2 precentages)
        {
            if (precentages != Vector2.Zero)
            {
                Vector2 vec = OnePrecent;

                vec.X *= precentages.X;
                vec.Y *= precentages.Y;

                return vec;
            }

            return precentages;
        }

        /// <summary>
        /// Generates a Rectangle from a reference Rectangle.
        /// </summary>
        /// <param name="value">The Rectangle to be used as a reference.</param>
        /// <param name="rect">The generated Rectangle.</param>
        public static Rectangle GetRelativeRectangle(Rectangle value)
        {
            if (value == new Rectangle(0, 0, 100, 100))
                return Viewport.Bounds;
            else
            {
                Rectangle rect = new Rectangle();

                Vector2 xy = GetPositionalCoordinates(new Vector2(value.X, value.Y));

                rect.X = (int)xy.X;
                rect.Y = (int)xy.Y;

                Vector2 vec = OnePrecent;

                rect.Width = (int)Math.Round(vec.X * value.Width);
                rect.Height = (int)Math.Round(vec.Y * value.Height);

                return rect;
            }
        }
    }
}
