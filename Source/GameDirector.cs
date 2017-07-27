using System.Diagnostics;
using TownOfYours.Core;
using TownOfYours.Core.Interfaces;
using TownOfYours.World;

namespace TownOfYours
{
    public class GameDirector
    {
        /// <summary>
        /// Gets world attached to this director
        /// </summary>
        public GameWorld GameWorld
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the game camera handle
        /// </summary>
        public TopDownCamera GameCamera
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="world"></param>
        public GameDirector(GameWorld world, TopDownCamera camera)
        {
            Debug.Assert(world != null, "World is null !");
            GameWorld = world;
            GameCamera = camera;
        }

        /// <summary>
        /// Begins directin the game
        /// </summary>
        public void Begin()
        {
            Debug.Assert(GameWorld.Towns.Count > 0, "There are no towns in this game !");
            GameCamera.Center(GameWorld.Towns[0].Location.WorldSpace);
        }

        /// <summary>
        /// Updates the game direction
        /// </summary>
        public void Update()
        {

        }
    }
}
