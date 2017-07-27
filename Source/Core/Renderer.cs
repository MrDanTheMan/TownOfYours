using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using TownOfYours.Core.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections;
using System;

namespace TownOfYours.Core
{
    public class Renderer
    {
        private ICamera m_activeCamera;
        private IComparer m_drawComparer;
        public static GraphicsDevice Device=null;

        /// <summary>
        /// Default sprite batch for rendering text to the screen
        /// </summary>
        private SpriteBatch TextSprite
        {
            get;
            set;
        }

        /// <summary>
        /// Gets sets the default font used for rendering text to screen
        /// </summary>
        public SpriteFont DefaultFont
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets active camera
        /// </summary>
        public ICamera ActiveCamera
        {
            get {return m_activeCamera; }
            set
            {
                if (m_activeCamera != null)
                {
                    m_activeCamera.CameraResize -= OnCameraResize;
                }
                m_activeCamera = value;
                if (m_activeCamera != null)
                {
                    m_activeCamera.CameraResize += OnCameraResize;
                }
            }
        }

        /// <summary>
        /// Gets debug material with boing shader
        /// </summary>
        public ShaderMaterial BoingMaterial
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public Renderer()
        {
            m_drawComparer = new DrawComparer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public void Initialize(GraphicsDevice device)
        {
            Device = device;
            TextSprite = new SpriteBatch(Device);
            BoingMaterial = new ShaderMaterial(ShaderCache.Instance.BoingShader);
            Debug.Print("Renderer: GPU name -> {0}", Device.Adapter.Description);
            Debug.Print("Renderer: High end GPU -> {0}", Device.Adapter.IsProfileSupported(GraphicsProfile.HiDef));
        }

        /// <summary>
        /// Pushes standard shader values
        /// </summary>
        /// <param name="shader">Shader handle</param>
        public void PushShaderStdValues(IRenderable renderable)
        {
            Debug.Assert(ActiveCamera != null, "No active camera assigned !");
            ShaderMaterial renderableMaterial = renderable.Material;
            if (ShaderMaterial.IsInvalid(renderableMaterial))
            {
                renderableMaterial = BoingMaterial;
            }

            foreach (EffectParameter param in renderableMaterial.Shader.Parameters)
            {
                switch (param.Name)
                {
                    case "STD_WORLD":
                        param.SetValue(renderable.Transform.Transform);
                        break;
                    case "STD_VIEW":
                        param.SetValue(ActiveCamera.ViewMatrix);
                        break;
                    case "STD_PROJECTION":
                        param.SetValue(ActiveCamera.Projection);
                        break;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// Pushes textures to the shader parameters
        /// </summary>
        /// <param name="renderable"></param>
        public void PushShaderTextures(IRenderable renderable)
        {
            if (ShaderMaterial.IsInvalid(renderable.Material))
            {
                return;
            }

            foreach (EffectParameter param in renderable.Material.Shader.Parameters)
            {
                if(param.Name == "diffTex")
                {
                    param.SetValue(TextureCache.Instance.DebugTexture);
                }
            }
        }

        /// <summary>
        /// Draw renderable object
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="shader"></param>
        public void DrawRenderable(IRenderable renderable)
        { 
            if(!renderable.States.Visible)
            {
                return;
            }

            Debug.Assert(renderable.Mesh() != null);
            Debug.Assert(renderable.Mesh().IBuffer != null);
            Debug.Assert(renderable.Mesh().VBuffer != null);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.Solid;
            Device.RasterizerState = rasterizerState;

            PushShaderStdValues(renderable);
            renderable.Material.PushShaderOverrides();

            if (ShaderMaterial.IsInvalid(renderable.Material))
            {
                BoingMaterial.Shader.CurrentTechnique.Passes[0].Apply();
            }
            else
            {
                renderable.Material.Shader.CurrentTechnique.Passes[0].Apply();
            }

            Device.SetVertexBuffer(renderable.Mesh().VBuffer);
            Device.Indices = renderable.Mesh().IBuffer;

            switch(renderable.Mesh().Primitive)
            {
                case VertexBlob.PRIMITIVE_TYPE.TRINAGLE:
                    Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, renderable.Mesh().IndexCount / 3);
                    break;
                case VertexBlob.PRIMITIVE_TYPE.LINE:
                    Device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, renderable.Mesh().IndexCount / 2);
                    break;
                default: throw new System.NotImplementedException();
            }

        }

        /// <summary>
        /// Draws renderable objects
        /// TODO: batching
        /// </summary>
        /// <param name="renderables"></param>
        public void DrawRenderables(IRenderable[] renderables)
        {
            Array.Sort(renderables, m_drawComparer);
            //Array.Reverse(renderables);
            for (int i = 0; i < renderables.Length; i++)
            {
                DrawRenderable(renderables[i]);
            }
        }

        /// <summary>
        /// Draws text to screen
        /// </summary>
        /// <param name="text">Text to render</param>
        /// <param name="position">Screen space position</param>
        public void DrawText(string text, Vector2 position)
        {
            Debug.Assert(DefaultFont != null, "Default render font is null !");
            Debug.Assert(TextSprite != null, "Default font sprite is null !");

            TextSprite.Begin();
            TextSprite.DrawString(DefaultFont, text, position, Color.White);
            TextSprite.End();
        }

        /// <summary>
        /// Handles camera resizing
        /// </summary>
        /// <param name="camera">Camera handle</param>
        /// <param name="args">Camera event arguments</param>
        private void OnCameraResize(ICamera camera, CameraEventArgs args)
        {
            Device.Viewport = new Viewport(0, 0, args.m_width, args.m_height);
        }
    }
}
