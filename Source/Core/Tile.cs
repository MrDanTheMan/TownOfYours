using System.Collections.Generic;

namespace TownOfYours.Core
{
    public class Tile
    {
        /// <summary>
        /// Available tile types
        /// </summary>
        public enum TILE_TYPE
        {
            NONE,
            UNKNOWN,
            GROUND,    
        }

        /// <summary>
        /// Gets or sets this tile index 
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this tile grid column
        /// </summary>
        public int Column
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this tile grid row
        /// </summary>
        public int Row
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this tile type
        /// </summary>
        public TILE_TYPE Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets list of neighbouring tiles
        /// </summary>
        public List<Tile> Neihgbours
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public Tile()
        {
            Neihgbours = new List<Tile>();
        }
    }
}
