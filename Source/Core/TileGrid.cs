using System.Collections.Generic;
using System.Diagnostics;

namespace TownOfYours.Core
{
    public class TileGrid
    {
        /// <summary>
        /// Gets or sets number of grid columns
        /// </summary>
        public int Columns
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets number of grid rows
        /// </summary>
        public int Rows
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets tthe size of each tile
        /// </summary>
        public float TileSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets list of tiles that belong to this grid
        /// </summary>
        private List<Tile> Tiles
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="columns">Number of grid columns</param>
        /// <param name="rows">Number of grid rows</param>
        /// <param name="size">Tile size</param>
        public TileGrid(int columns, int rows, float size)
        {
            Columns = columns;
            Rows = rows;
            TileSize = size;

            Tiles = new List<Tile>();
            List<List<int>> neighbourMap = new List<List<int>>();
            for(int i=0; i < rows; i++)
            {
                for(int j=0; j < columns;j++)
                {
                    Tile thisTile = new Tile();
                    thisTile.Column = j;
                    thisTile.Row = i;
                    thisTile.Index = (rows * i) + j;
                    thisTile.Type = Tile.TILE_TYPE.UNKNOWN;
                    Tiles.Add(thisTile);

                    // Resolve neighbour indices
                    neighbourMap.Add(new List<int>());
                    int index = i * rows + j;
                    if (i > 0) { neighbourMap[index].Add(index - columns); }
                    if (i < columns - 1) { neighbourMap[index].Add(index + columns); }
                    if (j > 0) { neighbourMap[index].Add(index - 1); }
                    if (j < columns - 1) { neighbourMap[index].Add(index + 1); }
                }
            }

            // Assing neighbour indices
            for(int i=0; i < neighbourMap.Count; i++)
            {
                foreach (int neighbourIndex in neighbourMap[i])
                {
                    Tiles[i].Neihgbours.Add(Tiles[neighbourIndex]);
                }
            }
        }

        /// <summary>
        /// Returns tile reference from the grid
        /// </summary>
        /// <param name="index">Tile index</param>
        /// <returns></returns>
        public Tile GetTile(int index)
        {
            Debug.Assert(index > 0 && index < Tiles.Count, "Tile index out of range !");
            return Tiles[index];
        }

        /// <summary>
        /// Returns tile reference from the grid
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            Debug.Assert(x >= 0 && x < Columns, "Tile x coordinate lookup is out of range");
            Debug.Assert(y >= 0 && y < Rows, "Tile y coordinate lookup is out of range");

            return Tiles[y * Rows + x];
        }
    }
}
