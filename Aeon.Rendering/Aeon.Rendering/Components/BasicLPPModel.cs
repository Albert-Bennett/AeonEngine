using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aeon.Rendering.Components
{
    public class BasicLPPModel : RenderComponent
    {
        Model model;
        Matrix[] transforms;

        Effect effect;

        Texture2D normalMap;
        Texture2D specMap;
        Texture2D tex;

        public Texture2D Texture
        {
            get { return tex; }
            set { tex = value; }
        }

        public Texture2D NormalMap
        {
            get { return normalMap; }
            set { normalMap = value; }
        }

        public Texture2D SpecMap
        {
            get { return specMap; }
            set { specMap = value; }
        }

        public BasicLPPModel(string id, string filepath, Vector3 position)
            : base(id)
        {
            model = Common.ContentManager.Load<Model>(filepath);

            transforms = new Matrix[model.Bones.Count];
            effect = Common.ContentManager.Load<Effect>("Aeon/Shaders/Lpp");
            GetTextures();

            if (Parent != null)
                Parent.Transform *= Matrix.CreateTranslation(position);

            ModelManager.AddModel(this);
        }

        void GetTextures()
        {
            foreach (Microsoft.Xna.Framework.Graphics.ModelMesh mesh in model.Meshes)
                foreach (Microsoft.Xna.Framework.Graphics.ModelMeshPart part in mesh.MeshParts)
                {
                    tex = part.Effect.Parameters["Texture"].GetValueTexture2D();
                    normalMap = part.Effect.Parameters["NormalMap"].GetValueTexture2D();
                    specMap = part.Effect.Parameters["SpecularMap"].GetValueTexture2D();
                }
        }

        protected override void Render()
        {
            foreach (Microsoft.Xna.Framework.Graphics.ModelMesh mesh in model.Meshes)
                foreach (Microsoft.Xna.Framework.Graphics.ModelMeshPart part in mesh.MeshParts)
                {
                    Common.Device.SetVertexBuffer(part.VertexBuffer, part.VertexOffset);
                    Common.Device.Indices = part.IndexBuffer;

                    effect.Parameters["Texture"].SetValue(tex);

                    effect.CurrentTechnique = effect.Techniques["Render"];
                    effect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, part.NumVertices,
                        part.StartIndex, part.PrimitiveCount);
                }
        }

        protected override void RenderOpaque()
        {
            model.CopyBoneTransformsTo(transforms);

            foreach (Microsoft.Xna.Framework.Graphics.ModelMesh mesh in model.Meshes)
                foreach (Microsoft.Xna.Framework.Graphics.ModelMeshPart part in mesh.MeshParts)
                {
                    Common.Device.SetVertexBuffer(part.VertexBuffer, part.VertexOffset);
                    Common.Device.Indices = part.IndexBuffer;

                    Matrix world = Matrix.Identity;

                    if (Parent != null)
                        world = transforms[mesh.ParentBone.Index] * Parent.Transform;

                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
                    effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

                    effect.Parameters["WorldView"].SetValue(Matrix.Transpose(Matrix.Invert(world
                        * CameraManager.CurrentCamera.View)));

                    effect.Parameters["NormalMap"].SetValue(normalMap);
                    effect.Parameters["SpecularMap"].SetValue(specMap);

                    effect.CurrentTechnique = effect.Techniques["Opaque"];
                    effect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, part.NumVertices,
                        part.StartIndex, part.PrimitiveCount);
                }
        }

        public Matrix GetBoneTransform(int index)
        {
            return transforms[index];
        }

        public Matrix[] GetBoneTransforms()
        {
            return transforms;
        }

        public override void Destroy()
        {
            ModelManager.Remove(this);

            base.Destroy();
        }
    }
}
