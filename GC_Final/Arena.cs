using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_Final
{
    class Arena
    {
        int triCount;
        VertexPositionTexture[] verts;
        VertexBuffer vertexBuffer;
        SamplerState clampTextureAddressMode;
        protected GraphicsDevice device;
        protected Texture2D texture;
        BasicEffect effect;
        //private VertexBuffer cubeVertexBuffer;
        private List<VertexPositionTexture> vertices = new List<VertexPositionTexture>();
        int max = 100, min = -100;

        float[] coords = new float[5] { 0, 0.33f, 0.5f, 0.66f, 1 };

        #region Constructor
        public Arena(GraphicsDevice graphicsDevice, Texture2D texture, int min, int max)
        {
            device = graphicsDevice;
            this.texture = texture;

            verts = new VertexPositionTexture[36];

            triCount = 0;
            //1
            verts[2] = new VertexPositionTexture(
                new Vector3(max, max, min), new Vector2(0, 0));
            verts[1] = new VertexPositionTexture(
                new Vector3(min, max, min), new Vector2(0.33f, 0));
            verts[0] = new VertexPositionTexture(
                new Vector3(min, min, min), new Vector2(0.33f, 0.5f));
            triCount++;
            //2

            verts[5] = new VertexPositionTexture(
                new Vector3(max, min, min), new Vector2(0, 0.5f));
            verts[4] = new VertexPositionTexture(
                new Vector3(max, max, min), new Vector2(0, 0));
            verts[3] = new VertexPositionTexture(
                new Vector3(min, min, min), new Vector2(0.33f, 0.5f));
            triCount++;

            //3
            //Side 2 t1
            verts[8] = new VertexPositionTexture(
                new Vector3(max, max, max), new Vector2(0, 0.5f));
            verts[7] = new VertexPositionTexture(
                new Vector3(max, max, min), new Vector2(0, 1));
            verts[6] = new VertexPositionTexture(
                new Vector3(max, min, min), new Vector2(0.33f, 1));
            triCount++;
            //4

            verts[11] = new VertexPositionTexture(
                new Vector3(max, min, max), new Vector2(0.33f, 0.5f));
            verts[10] = new VertexPositionTexture(
                new Vector3(max, max, max), new Vector2(0, 0.5f));
            verts[9] = new VertexPositionTexture(
                new Vector3(max, min, min), new Vector2(0.33f, 1));
            triCount++;

            //Side 3 t1
            verts[14] = new VertexPositionTexture(
                new Vector3(min, max, max), new Vector2(0.33f, 0));
            verts[13] = new VertexPositionTexture(
                new Vector3(max, max, max), new Vector2(0.66f, 0));
            verts[12] = new VertexPositionTexture(
                new Vector3(max, min, max), new Vector2(0.66f, 0.5f));
            triCount++;

            //Second Triangle (Based on Triangle Strip)
            verts[17] = new VertexPositionTexture(
                new Vector3(min, min, max), new Vector2(0.33f, 0.5f));
            verts[16] = new VertexPositionTexture(
                new Vector3(min, max, max), new Vector2(0.33f, 0));
            verts[15] = new VertexPositionTexture(
                new Vector3(max, min, max), new Vector2(0.66f, 0.5f));
            triCount++;

            //side4 t1
            verts[20] = new VertexPositionTexture(
                new Vector3(min, min, min), new Vector2(0.66f, 0.5f));
            verts[19] = new VertexPositionTexture(
                new Vector3(min, max, max), new Vector2(1, 0));//correct
            verts[18] = new VertexPositionTexture(
                new Vector3(min, min, max), new Vector2(1, 0.5f));
            triCount++;

            verts[23] = new VertexPositionTexture(
                new Vector3(min, max, max), new Vector2(1, 0));//c
            verts[22] = new VertexPositionTexture(
                new Vector3(min, min, min), new Vector2(0.66f, 0.5f));
            verts[21] = new VertexPositionTexture(
                new Vector3(min, max, min), new Vector2(0.66f, 0));
            triCount++;



            //top            
            verts[26] = new VertexPositionTexture(
                new Vector3(max, max, max), new Vector2(0.66f, 0.5f));
            verts[25] = new VertexPositionTexture(
                new Vector3(min, max, max), new Vector2(0.66f, 1));
            verts[24] = new VertexPositionTexture(
                new Vector3(min, max, min), new Vector2(1, 1));
            triCount++;

            verts[29] = new VertexPositionTexture(
                new Vector3(max, max, min), new Vector2(1, 0.5f));//c            
            verts[28] = new VertexPositionTexture(
                new Vector3(max, max, max), new Vector2(0.66f, 0.5f));
            verts[27] = new VertexPositionTexture(
                new Vector3(min, max, min), new Vector2(1, 1));
            triCount++;


            //bottom
            verts[32] = new VertexPositionTexture(
                new Vector3(min, min, min), new Vector2(0.33f, 0.5f));
            verts[31] = new VertexPositionTexture(
                new Vector3(min, min, max), new Vector2(0.66f, 0.5f));
            verts[34] = new VertexPositionTexture(
                new Vector3(max, min, max), new Vector2(0.66f, 1));
            triCount++;

            verts[35] = new VertexPositionTexture(
                new Vector3(min, min, min), new Vector2(0.33f, 0.5f));
            verts[34] = new VertexPositionTexture(
                new Vector3(max, min, max), new Vector2(0.66f, 1));
            verts[33] = new VertexPositionTexture(
                new Vector3(max, min, min), new Vector2(0.33f, 1));
            triCount++;



            // Set vertex data in VertexBuffer (Graph Device, type of Vertex, how many, special buffer settings)
            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
            vertexBuffer.SetData(verts);

            // Initialize the BasicEffect (using a base Effect included to run basic HLSL)
            effect = new BasicEffect(device);

            //Configured to handle Textures not powers of 2 error when using Reach graphics mode
            clampTextureAddressMode = new SamplerState
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp
            };

            //***************************************************************************************
            //***************************************************************************************


            // Load texture
            //texture = Content.Load<Texture2D>(@"Textures\trees");
            //texture = Content.Load<Texture2D>(@"Textures\mk");
            //texture = Content.Load<Texture2D>(@"Textures\combo");
            //worldScaling *= Matrix.CreateScale(1.5f);
        }
        #endregion

        public virtual void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = texture;
            Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
            Matrix scale = Matrix.CreateScale(0.5f);
            Matrix translate = Matrix.CreateTranslation(0, 0, 0);
            Matrix rot = Matrix.CreateRotationY(0);
            Matrix zrot = Matrix.CreateRotationZ(0);
            effect.World = center * rot * zrot * scale * translate;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(vertexBuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount / 3);
            }
        }

    }
}