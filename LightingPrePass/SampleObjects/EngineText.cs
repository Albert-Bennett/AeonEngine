using Aeon;
using Aeon.Engine;
using Aeon.Rendering.Cameras;
using Aeon.Rendering.Components;
using Aeon.Rendering.Lighting;
using Microsoft.Xna.Framework;

namespace SampleObjects
{
    public class EngineText : GameObject
    {
        TargetCamera cam;
        PointLight p1;
        PointLight p2;
        PointLight p3;

        public EngineText(string id)
            : base(id)
        {
            BasicLPPModel model = new BasicLPPModel(id + "Model", "Models/Text/Text", Vector3.Zero);
            AddComponent(model);

            cam = new TargetCamera("Camera", new Vector3(0, 0, -15), Vector3.Zero, 0.1f, 100000);
            AddComponent(cam);

           p2= new PointLight(id + "p2", Color.OrangeRed.ToVector4(),
                0.5f, new Vector3(-2, 3, -6), 20);

           p1= new PointLight(id + "p1", Color.White.ToVector4(),
                1, new Vector3(0, 0, -9), 12);

           p3= new PointLight(id + "p3", Color.DarkRed.ToVector4(),
                0.5f, new Vector3(0, 0, -6), 30);

            Transform *= Matrix.CreateTranslation(6, -7, 0);

            BasicLPPModel model2 = new BasicLPPModel(id + "Model1", "Models/Text/Text", new Vector3(0,-4, -5));
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

            if (InputManager.IsKeyStroked(Keys.Num1))
                p1.ToggleEnabled();

            if (InputManager.IsKeyStroked(Keys.Num2))
                p2.ToggleEnabled();

            if (InputManager.IsKeyStroked(Keys.Num3))
                p3.ToggleEnabled();

            cam.Move(move);

            base.Update();
        }
    }
}
