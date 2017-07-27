using Microsoft.Xna.Framework;
using System.Diagnostics;
using TownOfYours.Core;
using TownOfYours.World;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.DebugTools
{
    public class DebugOverlay
    {
        /// <summary>
        /// World handle
        /// </summary>
        private GameWorld World
        {
            get;
            set;
        }

        /// <summary>
        /// Main camera handle
        /// </summary>
        private TopDownCamera Camera
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets input manager handle 
        /// </summary>
        private InputManager Input
        {
            get;
            set;
        }

        /// <summary>
        /// Show main camera world space position on tehs screen
        /// </summary>
        public bool ShowCameraPosition
        {
            get;
            set;
        }

        /// <summary>
        /// Shows mouse cursor coordinates
        /// </summary>
        public bool ShowMousePosition
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="world"></param>
        /// <param name="camera"></param>
        public DebugOverlay(GameWorld world, TopDownCamera camera, InputManager input)
        {
            World = world;
            Camera = camera;
            Input = input;

            Debug.Assert(World != null, "World is null !");
            Debug.Assert(Camera != null, "Camera is null !");
        }

        /// <summary>
        /// Draws debug overlay
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            float xOffset = 10.0f;
            float yOffset = 10.0f;

            if(ShowCameraPosition)
            {
                Debug.Assert(Camera != null, "Camera is null !");
                Vector3 pos = Camera.Transform.Translation;
                renderer.DrawText(string.Format("Camera position: x:{0} y:{1} z:{2}  zoom:{3} zoom factor:{4}", pos.X, pos.Y, pos.Z, Camera.ZoomLevel, Camera.ZoomFactor), new Vector2(xOffset, yOffset));
                yOffset += 15;
            }

            if(ShowMousePosition)
            {
                Debug.Assert(Input != null, "Input is null !");
                Debug.Assert(Camera != null, "Camera is null !");

                Vector3 ws = Camera.ScreenToWorld(new Vector2(Input.MouseX, Input.MouseY));
                renderer.DrawText(string.Format("Mouse: screen: {0}x{1} world: {2}x{3}", Input.MouseX, Input.MouseY, ws.X, ws.Z), new Vector2(xOffset, yOffset));
                yOffset += 15;
            }
        }
    }
}
