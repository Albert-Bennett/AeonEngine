using Aeon.EngineComponents;
using Microsoft.Xna.Framework;
using System;

namespace Aeon.Engine
{
    /// <summary>
    /// A framework that is used to glue togeather all other parts of the game engine.
    /// </summary>
    public class Framework : Microsoft.Xna.Framework.Game
    {
        static EngineComponentManager compManager;

        protected Common common;


        /// <summary>
        /// Creates a new Aeon framework.
        /// </summary>
        public Framework()
        {
            common = new Common(this);

            Common.ContentManager.RootDirectory = "Content\\";
            common.ViewChanged += new ViewportChanged(ViewportChanged);

            compManager = new EngineComponentManager();
        }

        /// <summary>
        /// Sets the framerate for the game.
        /// </summary>
        /// <param name="frames">The target number of frames.</param>
        protected void SetFrameRate(int frames)
        {
            TimeSpan time = TimeSpan.FromSeconds(1f / frames);

            this.TargetElapsedTime = time;
        }

        protected override void Initialize()
        {
            common.Initialize();

            Common.SetResolution(new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth,
                GraphicsDeviceManager.DefaultBackBufferHeight));

            new GameObjectManager();
            new InputManager();

            CreateFrameworks();

            compManager.Initialize();

            base.Initialize();

            compManager.PostInitialize();
        }

        void CreateFrameworks()
        {
            try
            {
                FrameworkCreation frame = Common.ContentManager.Load<FrameworkCreation>("Aeon");

                foreach (string s in frame.AssemblyRefferences)
                    AssemblyManager.AddAssemblyRef(s);

                foreach (string s in frame.EngineComponents)
                    AssemblyManager.CreateInstance(s, null);
            }
            catch (Exception ex)
            {
                throw new Exception("The was no Aeon.Aini file found");
            }
        }

        void ViewportChanged()
        {
            Common.SetResolution(new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth,
                GraphicsDeviceManager.DefaultBackBufferHeight));

            compManager.SendMessage("ViewChanged", null);
        }

        /// <summary>
        /// Updates the framework.
        /// </summary>
        /// <param name="gameTime">The GameTime to use for the update.</param>
        protected override void Update(GameTime gameTime)
        {
            common.SetElaspedTimeDelta(gameTime.ElapsedGameTime);

            compManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// Used to draw objects.
        /// </summary>
        /// <param name="gameTime">The GameTime to use for the draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            compManager.Render();
        }

        /// <summary>
        /// Disposes of the Framework.
        /// </summary>
        /// <param name="disposing">Weather or not to dispose.</param>
        protected override void Dispose(bool disposing)
        {
            compManager.Dispose();
            compManager = null;

            base.Dispose(disposing);
        }
    }
}
