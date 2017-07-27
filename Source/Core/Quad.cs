using System.Diagnostics;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.Core
{
    public class Quad : IRenderable
    {
        private VertexBlob m_blob;
        /// <summary>
        /// Gets or sets quad trasform
        /// </summary>
        public TransformMatrix Transform
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets quad material
        /// </summary>
        public ShaderMaterial Material
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this quad render states
        /// </summary>
        public RenderStates States
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public Quad(float size = 16.0f, PIVOT_MODE pivot = PIVOT_MODE.CENTER)
        {
            Debug.Assert(Renderer.Device != null, "Renderer has not been initialized !");
            States = new RenderStates();
            Transform = new TransformMatrix();
            Material = new ShaderMaterial(ShaderCache.Instance.BoingShader);
            m_blob = VertexBlob.Quad(Renderer.Device, size, pivot);
        }

        /// <summary>
        /// Returns quad vertex blob
        /// </summary>
        /// <returns></returns>
        public VertexBlob Mesh()
        {
            return m_blob;
        }
    }
}
