using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using TownOfYours.Core;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.World
{
    public class GameWorld
    {
        private TerrainTiles m_terrain;
        private List<Town> m_towns;

        /// <summary>
        /// Gets terrain tiles handle
        /// </summary>
        public TerrainTiles Terrain
        {
            get { return m_terrain;;}
        }

        /// <summary>
        /// Gets list of towns
        /// </summary>
        public List<Town> Towns
        {
            get { return m_towns; }
            private set { m_towns = value; }
        }

        /// <summary>
        /// Gets world tile information from under mouse
        /// </summary>
        public WorldTileInfo MouseTile
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public GameWorld()
        {

            Towns = new List<Town>();
        }

        /// <summary>
        /// Initializes game world
        /// </summary>
        /// <param name="device"></param>
        public void Initialize(GraphicsDevice device, InputManager input)
        {
            input.MouseMoved += OnMouseMove;

            m_terrain = new TerrainTiles(GlobalSettings.DEF_WORLD_SIZE_X, GlobalSettings.DEF_WORLD_SIZE_Y, GlobalSettings.TILE_SIZE);
            m_terrain.Material = new ShaderMaterial(ShaderCache.Instance.GetShader("Diff1"));
            m_terrain.Material.SetValue("diffTex", TextureCache.Instance.GetTexture("TerrainTile_01"));

            GenerateTowns(3);
        }

        /// <summary>
        /// Returns array of renderables object attached to towns
        /// </summary>
        /// <returns></returns>
        public IRenderable[] TownRenderables()
        {
            int size = Towns.Sum(o => o.Renderables.Count);
            List<IRenderable> renderables = new List<IRenderable>(size);

            foreach (Town thisTown in Towns)
            {
                renderables.AddRange(thisTown.Renderables);
            }

            return renderables.ToArray();
        }

        /// <summary>
        /// Generate Towns
        /// </summary>
        private void GenerateTowns(int count)
        {
            Debug.Assert(Terrain != null, "Cannot generates towns if there is no terrain");
            Towns.Clear();
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int locationX = rand.Next(Terrain.Width);
                int locationY = rand.Next(Terrain.Height);

                Tile townTile = Terrain.Tiles.GetTile(locationX, locationY);
                Town thisTown = new Town(townTile);
                thisTown.ControllerType = Town.CONTROLLER.AI;
                Towns.Add(thisTown);
            }
        }

        /// <summary>
        /// Handles mouse events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            //Vector3 wsMouse = Camera
        }
    }
}
