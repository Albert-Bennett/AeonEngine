using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aeon.Rendering
{
    /// <summary>
    /// Defines a quad renderer.
    /// </summary>
    public class FSQuad
    {
        VertexBuffer vb;
        IndexBuffer ib;

        public FSQuad()
        {
            VertexPositionTexture[] vertices =
            {
                            new VertexPositionTexture(
                                new Vector3(1,-1,0),new Vector2(1,1)),
                            new VertexPositionTexture(
                                new Vector3(-1,-1,0), new Vector2(0,1)),
                            new VertexPositionTexture(
                                new Vector3(-1,1,0), new Vector2(0,0)),
                            new VertexPositionTexture(
                                new Vector3(1,1,0), new Vector2(1,0))
            };

            vb = new VertexBuffer(Common.Device, VertexPositionTexture.VertexDeclaration, vertices.Length, BufferUsage.None);
            vb.SetData<VertexPositionTexture>(vertices);

            ushort[] indices = new ushort[] { 0, 1, 2, 2, 3, 0 };

            ib = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Length, BufferUsage.None);

            ib.SetData<ushort>(indices);
        }

        public void Render()
        {
            ReadyBuffers();

            RenderQuad();
        }

        public void ReadyBuffers()
        {
            Common.Device.SetVertexBuffer(vb);
            Common.Device.Indices = ib;
        }

        public void RenderQuad()
        {
            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                 0, 0, 4, 0, 2);
        }
    }
}
