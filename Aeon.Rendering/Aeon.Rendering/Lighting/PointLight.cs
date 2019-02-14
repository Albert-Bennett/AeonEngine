using Microsoft.Xna.Framework;

namespace Aeon.Rendering.Lighting
{
    public class PointLight : LightComponent
    {
        float radius;

        Matrix world;

        public float Radius
        {
            get { return radius; }
            set { SetWorld(value, Vector3.Zero); }
        }

        public virtual Matrix World
        {
            get { return world; }
        }

        public PointLight(string id)
            : base(id)
        {
            radius = 10;
        }

        public PointLight(string id, Vector4 colour,
            float intensity, Vector3 position, float radius) :
            base(id, colour, intensity)
        {
            SetWorld(radius, position);
        }

        void SetWorld(float radius, Vector3 pos)
        {
            Vector3 trans = world.Translation;
            world -= Matrix.CreateTranslation(trans);

            Matrix scale = Matrix.CreateScale(this.radius);
            world -= scale;

            this.radius += radius;

            world = Matrix.CreateScale(this.radius) *
                Matrix.CreateTranslation(trans + pos);
        }

        public void Move(Vector3 position)
        {
            SetWorld(0, position);
        }
    }
}
