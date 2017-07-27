using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.Core
{
    public class TopDownCamera : ICamera
    {
        private static readonly int PAN_AREA = 25;
        private int m_width;
        private int m_height;
        private float m_aspect;
        private int m_zoom = 0;
        private Vector2 m_movement;
        private InputManager m_input;

        public event CameraEventHandler CameraResize;
        private readonly int MAX_ZOOM = 64;
        private readonly int ZOOM_STEP=(int)GlobalSettings.TILE_SIZE;

        /// <summary>
        /// Gets or sets this camera view matrix
        /// </summary>
        public Matrix ViewMatrix
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this camera projection matrix
        /// </summary>
        public Matrix Projection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this camera transform
        /// </summary>
        public TransformMatrix Transform
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this camera movement speed
        /// </summary>
        public float Speed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets zoom factor
        /// </summary>
        public int ZoomFactor
        {
            get { return m_zoom * ZOOM_STEP; }
        }

        /// <summary>
        /// Gets camera zoom level
        /// </summary>
        public int ZoomLevel
        {
            get { return m_zoom; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public TopDownCamera()
        {
            ViewMatrix = Matrix.Identity;
            Projection = Matrix.Identity;
            Transform = new TransformMatrix();
            m_movement = new Vector2();
            Speed = 1024;
        }

        /// <summary>
        /// Resizes camera
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_aspect = (float)m_height / (float)m_width;
            UpdateProjection();

            CameraEventArgs args = new CameraEventArgs
            {
                m_width = width,
                m_height = height,
            };

            CameraResize?.Invoke(this, args);
        }

        /// <summary>
        /// Recalculates camera proejection matrix
        /// </summary>
        public void UpdateProjection()
        {
            Projection = Matrix.CreateOrthographic(m_width + ZoomFactor, m_height + (ZoomFactor * m_aspect), 0.001f, 1000.0f);
        }

        /// <summary>
        /// Update should be call once per frame before rendering calls
        /// </summary>
        public void Update(float deltaTime)
        {
            HandleCameraPan();
            Transform.Translate(new Vector3(m_movement.X, 0.0f, m_movement.Y) * deltaTime);
            Vector3 lookat = new Vector3(Transform.Transform.Translation.X, 0.0f, Transform.Transform.Translation.Z);
            ViewMatrix = Matrix.CreateLookAt(Transform.Transform.Translation, lookat, Vector3.Forward);
            m_movement.X = 0.0f;
            m_movement.Y = 0.0f;
        }

        /// <summary>
        /// Moves the camera so that the given position is at the center of the screen
        /// </summary>
        /// <param name="position">Camera center position</param>
        public void Center(Vector3 position)
        {
            Transform.SetTranslation(new Vector3(position.X, GlobalSettings.CAM_HEIGHT, position.Z));
        }

        /// <summary>
        /// Subscribes to input manager events 
        /// </summary>
        /// <param name="input"></param>
        public void BindControlInput(InputManager input)
        {
            m_input = input;
            input.KeyDown += HandleInput;
            input.KeyChanged += HandleInputEnd;
            input.MouseWheelMoved += HandleCameraZoom;
        }

        /// <summary>
        /// Converts screen coordinates to world space position
        /// </summary>
        /// <param name="screenCoords"></param>
        /// <returns></returns>
        public Vector3 ScreenToWorld(Vector2 screenCoords)
        {
            Vector3 output = new Vector3(screenCoords.X, 0.0f, screenCoords.Y);
            output.X += Transform.Translation.X - (m_width / 2);
            output.Y = 0.0f;
            output.Z += Transform.Translation.Z - (m_height / 2);

            return output;
        }

        /// <summary>
        /// Zooms out the camera
        /// </summary>
        private void ZoomOut()
        {
            if(m_zoom < MAX_ZOOM)
            {
                m_zoom++;
                UpdateProjection();
            }
        }

        /// <summary>
        /// Zooms in the camera
        /// </summary>
        private void ZoomIn()
        {
            if(m_zoom > 0)
            {
                m_zoom--;
                UpdateProjection();
            }
        }

        /// <summary>
        /// Translates user keyboard input to resolve camera transform
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleInput(object sender, KeyboardEventArgs args)
        {
            switch (args.m_key)
            {
                case Keys.W:
                    m_movement.Y -= Speed;
                    break;
                case Keys.S:
                    m_movement.Y += Speed;
                    break;
                case Keys.A:
                    m_movement.X -= Speed;
                    break;
                case Keys.D:
                    m_movement.X += Speed;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleInputEnd(object sender, KeyboardEventArgs args)
        {
            
        }

        /// <summary>
        /// Handles camera paning based on the mouse position
        /// </summary>
        private void HandleCameraPan()
        {
            if (m_input == null)
            {
                return;
            }

            /// Exit early if mouse is outside the window
            Rectangle windowRect = new Rectangle(0, 0, m_width, m_height);
            if (!windowRect.Contains(new Point((int)m_input.MouseX, (int)m_input.MouseY)))
            {
                return;
            }

            float panSpeed = Speed*2;
            if (m_input.MouseX < PAN_AREA)
            {
                m_movement.X -= panSpeed;
            }

            if (m_input.MouseX > m_width - PAN_AREA)
            {
                m_movement.X += panSpeed;
            }

            if (m_input.MouseY < PAN_AREA)
            {
                m_movement.Y -= panSpeed;
            }

            if (m_input.MouseY > m_height - PAN_AREA)
            {
                m_movement.Y += panSpeed;
            }
        }

        /// <summary>
        /// Translates mouse wheel actions into camera zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="action"></param>
        private void HandleCameraZoom(object sender, MOUSE_WHEEL_ACTION action)
        {
            Rectangle windowRect = new Rectangle(0, 0, m_width, m_height);
            if (!windowRect.Contains(new Point((int)m_input.MouseX, (int)m_input.MouseY)))
            {
                return;
            }

            switch(action)
            {
                case MOUSE_WHEEL_ACTION.UP:
                    ZoomIn();
                    break;
                case MOUSE_WHEEL_ACTION.DOWN:
                    ZoomOut();
                    break;
                default: return;
            }
        }
    }
}
