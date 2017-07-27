using System;
using Microsoft.Xna.Framework;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.Core
{
    public class GridLines : IRenderable
    {
        private VertexBlob m_blob;

        /// <summary>
        /// Gets this grid transform
        /// </summary>
        public TransformMatrix Transform
        {
            get;
            set;
        }

        /// <summary>
        /// Gets this grid render material
        /// </summary>
        public ShaderMaterial Material
        {
            get;
            set;
        }

        /// <summary>
        /// Gets render states
        /// </summary>
        public RenderStates States
        {
            get;
            set;
        }

        /// <summary>
        /// Returns handle to this grid vertex blob
        /// </summary>
        /// <returns></returns>
        public VertexBlob Mesh()
        {
            return m_blob;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="spacing"></param>
        public GridLines(int columns, int rows, float spacing)
        {
            States = new RenderStates();
            m_blob = new VertexBlob();
            m_blob.Primitive = VertexBlob.PRIMITIVE_TYPE.LINE;

            for (int i = 0; i < rows; i++)
            {
                VertexData p1 = new VertexData() { m_position = new Vector3(0, 0, i * spacing) };
                VertexData p2 = new VertexData() { m_position = new Vector3(columns * spacing, 0, i * spacing) };

                m_blob.AddVertex(p1);
                m_blob.AddVertex(p2);
                m_blob.AddIndex(i * 2);
                m_blob.AddIndex((i * 2) + 1);
            }

            int vertexOffset = rows * 2;

            for (int i = 0; i < rows; i++)
            {
                VertexData p1 = new VertexData() { m_position = new Vector3(i * spacing, 0, 0) };
                VertexData p2 = new VertexData() { m_position = new Vector3(i * spacing, 0, rows * spacing) };

                m_blob.AddVertex(p1);
                m_blob.AddVertex(p2);
                m_blob.AddIndex((i * 2) + vertexOffset);
                m_blob.AddIndex((i * 2) + 1 + vertexOffset);
            }

            m_blob.Update(Renderer.Device);
            Transform = new TransformMatrix();
            Material = new ShaderMaterial(ShaderCache.Instance.BoingShader);
        }
    }
}
