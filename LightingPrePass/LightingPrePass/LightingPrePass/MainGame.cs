using Aeon.Engine;

namespace LightingPrePass
{
    public class MainGame : Framework
    {
        protected override void Initialize()
        {
            base.Initialize();

           // common.FullScreen();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Esc))
                Exit();

            base.Update(gameTime);
        }
    }
}
