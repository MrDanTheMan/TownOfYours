using Microsoft.Xna.Framework;

namespace TownOfYours
{
    public static class GlobalSettings
    {
        public static readonly float TILE_SIZE = 64.0f;
        public static readonly float CAM_HEIGHT = 10.0f;
        public static readonly int DEF_WORLD_SIZE_X = 64;
        public static readonly int DEF_WORLD_SIZE_Y = 64;

        public static int TERRAIN_LAYER_ORDER = 0;
        public static int TOWN_LAYER_ORDER = 1;
        public static int BUILDING_LAYER_ORDER = 2;
    }
}
