using Microsoft.Xna.Framework;
using TownOfYours.Core;

namespace TownOfYours.World
{
    public class TownTile : Tile
    {
        /// <summary>
        /// Gets this tile renderable
        /// </summary>
        public Quad RenderQuad
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets this tile highlight color value
        /// </summary>
        public Color Highlight
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="tile"></param>
        public TownTile(Tile tile)
        {
            Column = tile.Column;
            Row = tile.Row;
            Index = tile.Index;
            RenderQuad = new Quad(GlobalSettings.TILE_SIZE, PIVOT_MODE.DEFAULT);
            RenderQuad.States.DrawOder = GlobalSettings.TOWN_LAYER_ORDER;
            RenderQuad.Transform.SetTranslation(new Vector3(Column * GlobalSettings.TILE_SIZE, 0.0f, Row * GlobalSettings.TILE_SIZE));
        }
    }
}
