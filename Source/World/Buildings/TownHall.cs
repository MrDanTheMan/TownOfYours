using TownOfYours.Core;

namespace TownOfYours.World.Buildings
{
    public class TownHall : Building
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public TownHall() : base()
        {
            Name = "Town Hall";
            RenderQuad.Material.SetValue("diffTex", TextureCache.Instance.GetTexture("Buildings/Debug/TownHall"));
        }
    }
}
