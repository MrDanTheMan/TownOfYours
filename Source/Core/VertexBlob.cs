using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TownOfYours.Core
{
    public enum PIVOT_MODE
    {
        DEFAULT,
        CENTER,
    }

    public class VertexBlob
    {
        private readonly List<VertexData> m_vertices;
        private readonly List<int> m_indices;

        public enum PRIMITIVE_TYPE
        {
            TRINAGLE,
            LINE,
        }

        /// <summary>
        /// Gets or sets this blob vertex buffer
        /// </summary>
        public VertexBuffer VBuffer
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets this blob index buffer
        /// </summary>
        public IndexBuffer IBuffer
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets primitive type of this vertex blob
        /// </summary>
        public PRIMITIVE_TYPE Primitive
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public VertexBlob()
        {
            m_vertices = new List<VertexData>();
            m_indices = new List<int>();
        }

        /// <summary>
        /// Clears this blob
        /// </summary>
        public void Clear()
        {
            m_vertices.Clear();
            m_indices.Clear();
        }

        /// <summary>
        /// Adds new vertex
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(VertexData vertex)
        {
            m_vertices.Add(vertex);
        }

        /// <summary>
        /// Gets number of allocated vertices
        /// </summary>
        public int VertexCount
        {
            get { return m_vertices.Count; }
        }

        /// <summary>
        /// Gets number of allocated indices
        /// </summary>
        public int IndexCount
        {
            get { return m_indices.Count; }
        }

        /// <summary>
        /// Adds new triangle indices
        /// </summary>
        /// <param name="p1">Triangle vertex index</param>
        /// <param name="p2">Triangle vertex index</param>
        /// <param name="p3">Triangle vertex index</param>
        public void AddTriangle(int p1, int p2, int p3)
        {
            m_indices.Add(p1);
            m_indices.Add(p2);
            m_indices.Add(p3);
        }

        /// <summary>
        /// Adds new index
        /// </summary>
        /// <param name="i"></param>
        public void AddIndex(int i)
        {
            m_indices.Add(i);
        }

        /// <summary>
        /// Rebuilds internal buffers
        /// </summary>
        /// <param name="device">Graphics device</param>
        public void Update(GraphicsDevice device)
        {
            VBuffer = new VertexBuffer(device, typeof(VertexData), m_vertices.Count, BufferUsage.None);
            VBuffer.SetData(m_vertices.ToArray());

            IBuffer = new IndexBuffer(device, typeof(int), m_indices.Count, BufferUsage.None);
            IBuffer.SetData(m_indices.ToArray());
        }

        /// <summary>
        /// Creates traingle blob
        /// Useful for debugging
        /// </summary>
        /// <param name="device">Graphics device</param>
        /// <param name="scale">Triangle scale</param>
        /// <returns>Traingle vertex blob</returns>
        public static VertexBlob Triangle(GraphicsDevice device, float scale=16.0f)
        {
            VertexBlob output = new VertexBlob();
            VertexData v1 = new VertexData { m_position = new Vector3(10.0f, 0.0f, 0.0f) };
            VertexData v2 = new VertexData { m_position = new Vector3(1.0f * scale, 0.0f, 1.0f * scale) };
            VertexData v3 = new VertexData { m_position = new Vector3(-1.0f * scale, 0.0f, 1.0f * scale) };

            output.AddVertex(v1);
            output.AddVertex(v2);
            output.AddVertex(v3);
            output.AddTriangle(0, 1, 2);

            output.Primitive = PRIMITIVE_TYPE.TRINAGLE;
            output.Update(device);
            return output;
        }

        /// <summary>
        /// Creates quad blob
        /// </summary>
        /// <param name="device">Graphics device</param>
        /// <param name="size">Quads size</param>
        /// <returns></returns>
        public static VertexBlob Quad(GraphicsDevice device, float size = 16, PIVOT_MODE pivot=PIVOT_MODE.DEFAULT)
        {
            VertexBlob output = new VertexBlob();
            VertexData v1 = new VertexData
            {
                m_position = new Vector3(size * -0.5f, 0.0f, size * 0.5f),
                m_texcoord0 = new Vector2(0, 1)
            };

            VertexData v2 = new VertexData
            {
                m_position = new Vector3(size * 0.5f, 0.0f, size * 0.5f),
                m_texcoord0 = new Vector2(1, 1)
            };

            VertexData v3 = new VertexData
            {
                m_position = new Vector3(size * 0.5f, 0.0f, size * -0.5f),
                m_texcoord0 = new Vector2(1, 0)
            };

            VertexData v4 = new VertexData
            {
                m_position = new Vector3(size * -0.5f, 0.0f, size * -0.5f),
                m_texcoord0 = new Vector2(0, 0)
            };

            // Default puviot will revert to the left upper corner of the quad
            if(pivot == PIVOT_MODE.DEFAULT)
            {
                v1.m_position.X += size * 0.5f;
                v1.m_position.Z += size * 0.5f;
                v2.m_position.X += size * 0.5f;
                v2.m_position.Z += size * 0.5f;
                v3.m_position.X += size * 0.5f;
                v3.m_position.Z += size * 0.5f;
                v4.m_position.X += size * 0.5f;
                v4.m_position.Z += size * 0.5f;
            }

            output.AddVertex(v1);
            output.AddVertex(v2);
            output.AddVertex(v3);
            output.AddVertex(v4);

            output.AddTriangle(0, 1, 2);
            output.AddTriangle(0, 2, 3);
            output.Primitive = PRIMITIVE_TYPE.TRINAGLE;
            output.Update(device);

            return output;
        }
    }
}
