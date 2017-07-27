using Microsoft.Xna.Framework.Graphics;

namespace TownOfYours.Core.Interfaces
{
    public interface IRenderable
    {
        /// <summary>
        /// Gets or sets this renderbale transform matrix
        /// </summary>
        TransformMatrix Transform
        {
            get;
        }

        /// <summary>
        /// Gets or sets renderable shader overrides
        /// </summary>
        ShaderMaterial Material
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this renderable states
        /// </summary>
        RenderStates States
        {
            get;
            set;
        }

        /// <summary>
        /// Access to this renderable mesh
        /// </summary>
        /// <returns>Mesh</returns>
        VertexBlob Mesh();
    }
}
