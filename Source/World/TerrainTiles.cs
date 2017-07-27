using TownOfYours.Core;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.World
{
    public class TerrainTiles : IRenderable
    {
        /// <summary>
        /// Gets or sets this terrain tiles
        /// </summary>
        public TileGrid Tiles
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets this terrain render mesh
        /// </summary>
        public TileMesh TileGeometry
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets this terrain grid transform
        /// </summary>
        public TransformMatrix Transform
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this terrain material
        /// </summary>
        public ShaderMaterial Material
        {
            get;
            set;
        }

        /// <summary>
        /// Gets terrain grid width in collumns
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets terrain grid height in rows
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the grid lines renderable overlay
        /// </summary>
        public IRenderable GridOverlay
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets render states
        /// </summary>
        public RenderStates States
        {
            get;
            set;
        }

        /// <summary>
        /// Access to this renderable mesh
        /// </summary>
        /// <returns></returns>
        public VertexBlob Mesh()
        {
            return TileGeometry.Blob;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="columns">Terrain tile width</param>
        /// <param name="rows">Terrain tile height </param>
        /// <param name="tileSize">Tile size</param>
        /// <param name="device">Graphics device</param>
        public TerrainTiles(int columns, int rows, float tileSize)
        {
            States = new RenderStates();
            States.DrawOder = GlobalSettings.TERRAIN_LAYER_ORDER;
            Tiles = new TileGrid(columns, rows, tileSize);
            TileGeometry = new TileMesh(columns, rows, tileSize);
            Material = new ShaderMaterial(ShaderCache.Instance.GetShader("DefaultShader"));
            Transform = new TransformMatrix();
            Width = columns;
            Height = rows;
            GridOverlay = new GridLines(columns, rows, tileSize);
        }
    }
}
