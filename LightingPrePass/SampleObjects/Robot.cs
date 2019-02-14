using Aeon;
using Aeon.Engine;
using Aeon.Rendering.Cameras;
using Aeon.Rendering.Components;
using Aeon.Rendering.Lighting;
using Microsoft.Xna.Framework;

namespace SampleObjects
{
    public class Robot : GameObject
    {
        TargetCamera cam = null;

        public Robot(string id, Vector3 position, int rotation)
            : base(id)
        {
            cam = new TargetCamera("Camera1", new Vector3(0, 0, -15), Vector3.Zero, 0.1f, 100000);
            AddComponent(cam);

            BasicLPPModel model = new BasicLPPModel(id + "Model", "Models/Robot/Robot", position);
            AddComponent(model);

            new PointLight(id + "p1", Color.White.ToVector4(),
                1, new Vector3(0, 0, 0), 100);

            new PointLight(id + "p2", Color.Red.ToVector4(),
                1f, new Vector3(0, 0, 2), 12);

            Transform *= Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))
                * Matrix.CreateTranslation(position);
        }

        protected override void Update()
        {
            Vector3 move = new Vector3();

            if (InputManager.IsKeyPressed(Keys.W))
                move.Z += 1;
            else if (InputManager.IsKeyPressed(Keys.S))
                move.Z -= 1;

            if (InputManager.IsKeyPressed(Keys.A))
                move.X -= 1;
            else if (InputManager.IsKeyPressed(Keys.S))
                move.X += 1;

            cam.Move(move);

            base.Update();
        }
    }
}
