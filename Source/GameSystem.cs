using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TownOfYours.Core;
using TownOfYours.Core.Data;
using TownOfYours.DebugTools;
using TownOfYours.World;

namespace TownOfYours
{
    public class GameSystem
    {
        private static GameSystem m_instance;

        /// <summary>
        /// Access to global instance of the shader cache object
        /// </summary>
        public static GameSystem Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameSystem();
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Gets MonoGame graphcis device
        /// </summary>
        public GraphicsDevice Device
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets main renderer handle
        /// </summary>
        public Renderer MainRenderer
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets MonoGame content manager handle
        /// </summary>
        public ContentManager Content
        {
            get;
            private set;
        }

#if DEBUG
        /// <summary>
        /// Gets or sets the debug information overlay
        /// </summary>
        private DebugOverlay DOverlay
        {
            get;
            set;
        }
#endif

        /// <summary>
        /// Gets input manager handle
        /// </summary>
        public InputManager Input
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets camera handle
        /// </summary>
        public TopDownCamera MainCamera
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets game world handle
        /// </summary>
        public GameWorld World
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the game director  handle
        /// </summary>
        public GameDirector Director
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value that idicate weather the game begun yet 
        /// </summary>
        public bool Begun
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        private GameSystem()
        {
            Input = new InputManager();
            MainCamera = new TopDownCamera();
            World = new GameWorld();
            Director = new GameDirector(World, MainCamera);

#if DEBUG
            DOverlay = new DebugOverlay(World, MainCamera, Input);
            DOverlay.ShowCameraPosition = true;
            DOverlay.ShowMousePosition = true;
#endif
            MainRenderer = new Renderer();
        }

        /// <summary>
        /// Sets up game systems
        /// </summary>
        public void Initialize(GraphicsDevice device, ContentManager contentManger)
        {
            Content = contentManger;
            Device = device;
        }

        /// <summary>
        /// Loads all the games content
        /// </summary>
        public void LoadGameContent()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TownOfYours.ContentXml.AssetManifest.xml");
            Debug.Assert(stream != null, "Asset manifest resource stream is null !");
            AssetManifest manifest = AssetManifest.Read(stream);

            // Make sure to load shader resource first since the
            // renderer depends on them
            ShaderCache.Instance.LoadShaderResources(manifest, Content);
            InitializeGraphics(Device);
            TextureCache.Instance.LoadTextureResources(manifest, Content);
        }

        /// <summary>
        /// Initializes game graphics
        /// </summary>
        private void InitializeGraphics(GraphicsDevice device)
        {
            MainRenderer.DefaultFont = Content.Load<SpriteFont>("Fonts/Default");
            MainRenderer.Initialize(device);
            MainRenderer.ActiveCamera = MainCamera;
            MainCamera.Resize(1280, 720);
            MainCamera.BindControlInput(Input);
            MainCamera.Transform.Translate(new Vector3(0.0f, GlobalSettings.CAM_HEIGHT, 0.0f));
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Begin()
        {
            Debug.Assert(World != null, "Game world is null !");
            Debug.Assert(Director != null, "Game director is null !");

            World.Initialize(Device, Input);
            Director.Begin();
            Begun = true;
        }

        /// <summary>
        /// Quits the game
        /// </summary>
        public void End()
        {

        }

        /// <summary>
        /// Updates the game
        /// </summary>
        public void Update(GameTime time)
        {
            float delta = (float)time.ElapsedGameTime.TotalSeconds;
            Director.Update();
            Input.Update();
            MainCamera.Update(delta);
        }

        /// <summary>
        /// Render game frame
        /// </summary>
        public void Draw(GameTime time)
        {
            Device.Clear(Color.CornflowerBlue);
            MainRenderer.DrawRenderable(World.Terrain);
            MainRenderer.DrawRenderable(World.Terrain.GridOverlay);
            MainRenderer.DrawRenderables(World.TownRenderables());

#if DEBUG 
            DOverlay.Draw(MainRenderer);
#endif
        }
    }
}
