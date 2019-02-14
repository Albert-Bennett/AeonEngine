using Microsoft.Xna.Framework;

namespace Aeon.Rendering.Lighting
{
    public abstract class LightComponent : Component
    {
        Vector4 colour;

        float intensity;

        public Vector4 Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        public float Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        public LightComponent(string id):base(id)
        {
            colour = Color.White.ToVector4();
            intensity = 1;

            LightManager.AddLight(this);
        }

        public LightComponent(string id, Vector4 colour, float intensity)
            : base(id)
        {
            this.colour = colour;
            this.intensity = intensity;

            LightManager.AddLight(this);
        }

        public override void Destroy()
        {
            LightManager.Remove(this);

            base.Destroy();
        }
    }
}
