using Microsoft.Xna.Framework;
using TownOfYours.World.Buildings;
using TownOfYours.Core;

namespace TownOfYours.World
{
    public struct WorldTileInfo
    {
        Tile m_terrainTile;
        TownTile m_townTile;
        Building m_building;
        Vector2 m_gridPosition;
        Vector3 m_worldPosition;
    }
}
