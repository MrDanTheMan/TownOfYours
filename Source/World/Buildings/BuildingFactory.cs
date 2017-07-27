using System;
using Microsoft.Xna.Framework;

namespace TownOfYours.World.Buildings
{

    public static class BuildingFactory
    {
        /// <summary>
        /// Defines building types
        /// </summary>
        public enum BUILDING_TYPE
        {
            TownHall,
        }

        /// <summary>
        /// Creates instance of a building
        /// </summary>
        /// <param name="type">Building type</param>
        /// <param name="location">Building grid location</param>
        /// <returns>Instance of a new building</returns>
        public static Building Create(BUILDING_TYPE type, Vector2 location)
        {
            Building output = null;

            switch (type)
            {
                    case BUILDING_TYPE.TownHall:
                        output = new TownHall();
                        break;
                default: throw  new NotImplementedException();
            }

            output.Location.Location = location;
            return output;
        }
    }
}
