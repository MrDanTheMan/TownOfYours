using Microsoft.Xna.Framework;

namespace TownOfYours.Utilities
{
    public delegate void GridLocationHandler(GridLocation sender);
    public class GridLocation
    {
        public event GridLocationHandler LocationChanged;
        private Vector2 m_location;
        private Vector3 m_worldspace;
        private float m_depth;

        /// <summary>
        /// Gets grid location
        /// </summary>
        public Vector2 Location
        {
            get { return m_location; }
            set { m_location = value; UpdateLocation(); }
        }

        /// <summary>
        /// Gets world space location
        /// </summary>
        public Vector3 WorldSpace
        {
            get { return m_worldspace; }
            private set { m_worldspace = value; }
        }

        /// <summary>
        /// Gets or sets the depth
        /// </summary>
        public float Depth
        {
            get { return m_depth; }
            set { m_depth = value; UpdateLocation(); }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public GridLocation()
        {
            Location = new Vector2();
        }

        /// <summary>
        /// Alternative ctor
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public GridLocation(int column, int row, float depth=0.0f)
        {
            Depth = depth;
            Location = new Vector2(column, row);
        }

        /// <summary>
        /// Recalculates the world space position
        /// </summary>
        private void UpdateLocation()
        {
            float x = m_location.X * GlobalSettings.TILE_SIZE;
            float z = m_location.Y * GlobalSettings.TILE_SIZE;

            WorldSpace = new Vector3(x, Depth, z);
            NotifyLocationChanged();
        }

        /// <summary>
        /// Rises LocationChanged event
        /// </summary>
        private void NotifyLocationChanged()
        {
            LocationChanged?.Invoke(this);
        }
    }
}
