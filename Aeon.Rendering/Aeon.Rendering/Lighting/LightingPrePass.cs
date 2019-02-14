using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aeon.Rendering.Lighting
{
    internal sealed class LightingPrePass
    {
        FSQuad screenQuad;

        RenderTarget2D opaqueData;
        RenderTarget2D depthData;
        RenderTarget2D lightTarget;
        RenderTarget2D colourMap;
        RenderTarget2D composed;

        Effect clearGBuffer;
        Effect pointLight;
        Effect compose;

        Model pointLightModel;

        BlendState lightingBlend;
        DepthStencilState depthStencil;

        public RenderTarget2D DepthData { get { return depthData; } }
        public RenderTarget2D Composed { get { return composed; } }

        public LightingPrePass()
        {
            screenQuad = new FSQuad();

            pointLightModel = Common.ContentManager.Load<Model>("Aeon/Models/pointLightModel");
            pointLight = Common.ContentManager.Load<Effect>("Aeon/Shaders/PointLight");

            clearGBuffer = Common.ContentManager.Load<Effect>("Aeon/Shaders/ClearGBuffer");
            compose = Common.ContentManager.Load<Effect>("Aeon/Shaders/Compose");

            int width = Common.Viewport.Width;
            int height = Common.Viewport.Height;

            opaqueData = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Rgba64, DepthFormat.Depth24Stencil8);

            depthData = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Rgba64, DepthFormat.Depth24Stencil8);

            colourMap = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Rgba64, DepthFormat.Depth24Stencil8);

            lightTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Rgba64, DepthFormat.Depth24Stencil8);

            composed = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Rgba64, DepthFormat.None);

            lightingBlend = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.One,
                AlphaDestinationBlend = Blend.One,
                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.One,
                ColorDestinationBlend = Blend.One
            };
        }

        public void Render()
        {
            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.DepthStencilState = DepthStencilState.Default;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;

            ClearGBuffer();
            RenderOpaque();
            RenderLights();

            Compose();
        }

        void RenderOpaque()
        {
            Common.Device.DepthStencilState = DepthStencilState.Default;
            ModelManager.RenderOpaque();
        }

        void ClearGBuffer()
        {
            Common.Device.DepthStencilState = DepthStencilState.DepthRead;
            Common.Device.SetRenderTargets(opaqueData, depthData);
            clearGBuffer.CurrentTechnique.Passes[0].Apply();

            screenQuad.Render();
        }

        void RenderLights()
        {
            Common.Device.SetRenderTarget(lightTarget);

            Common.Device.Clear(Color.Transparent);

            Common.Device.BlendState = lightingBlend;
            Common.Device.DepthStencilState = DepthStencilState.DepthRead;

            Matrix invView = Matrix.Invert(CameraManager.CurrentCamera.View);
            Matrix invViewProj = Matrix.Invert(CameraManager.CurrentCamera.View * CameraManager.CurrentCamera.Projection);

            RenderPointLights(invView, invViewProj);

            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;
            Common.Device.DepthStencilState = DepthStencilState.Default;
        }

        void RenderPointLights(Matrix invView, Matrix invViewProj)
        {
            List<PointLight> lights = LightManager.GetLightsByType<PointLight>();

            if (lights.Count > 0)
            {
                pointLight.Parameters["IView"].SetValue(invView);
                pointLight.Parameters["IViewProj"].SetValue(invViewProj);
                pointLight.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.LocalTransform.Translation);
                pointLight.Parameters["GDSize"].SetValue(Common.Resolution);

                pointLight.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
                pointLight.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

                Common.Device.SetVertexBuffer(pointLightModel.Meshes[0].MeshParts[0].VertexBuffer,
                    pointLightModel.Meshes[0].MeshParts[0].VertexOffset);

                Common.Device.Indices = pointLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                foreach (PointLight l in lights)
                {
                    pointLight.Parameters["World"].SetValue(l.World);
                    pointLight.Parameters["Pos"].SetValue(l.World.Translation);
                    pointLight.Parameters["Colour"].SetValue(l.Colour);
                    pointLight.Parameters["Radius"].SetValue(l.Radius);
                    pointLight.Parameters["Intensity"].SetValue(l.Intensity);
                    pointLight.Parameters["Opaque"].SetValue(opaqueData);
                    pointLight.Parameters["DepthMap"].SetValue(depthData);

                    Common.Device.RasterizerState = RasterizerState.CullClockwise;

                    pointLight.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        pointLightModel.Meshes[0].MeshParts[0].NumVertices,
                        pointLightModel.Meshes[0].MeshParts[0].StartIndex,
                        pointLightModel.Meshes[0].MeshParts[0].PrimitiveCount);
                }
            }
        }

        void Compose()
        {
            RenderTextures();

            Common.Device.SetRenderTarget(composed);

            Common.Device.Clear(Color.Transparent);
            compose.Parameters["LightMap"].SetValue(lightTarget);
            compose.Parameters["ColourMap"].SetValue(colourMap);
            compose.Parameters["TextureSize"].SetValue(Common.Resolution);

            compose.CurrentTechnique.Passes[0].Apply();
            screenQuad.Render();

            Common.Device.SetRenderTargets(null); 
        }

        void RenderTextures()
        {
            Common.Device.SetRenderTarget(colourMap);

            Common.Device.Clear(Color.Transparent);
            ModelManager.RenderModels();

            screenQuad.Render();

            Common.Device.SetRenderTarget(null);
        }

        public void RenderDebug()
        {
            Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
                SamplerState.PointClamp, null, null);

            Common.Batch.Draw(composed, Common.Viewport.Bounds, Color.White);

            int width = Common.Viewport.Bounds.Width / 6;
            int height = Common.Viewport.Bounds.Height / 6;

            Rectangle rect = new Rectangle(0, 0, width, height);

            Common.Batch.Draw(opaqueData, rect, Color.White);

            rect.Y += height;
            Common.Batch.Draw(depthData, rect, Color.White);

            rect.Y = 0;
            rect.X += width;
            Common.Batch.Draw(lightTarget, rect, Color.White);

            rect.X += width;
            Common.Batch.Draw(colourMap, rect, Color.White);

            Common.Batch.End();
        }
    }
}
