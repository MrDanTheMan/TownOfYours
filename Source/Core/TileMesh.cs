using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TownOfYours.Core
{
    public class TileMesh
    {
        /// <summary>
        /// Gets or sets mesh geometry blob
        /// </summary>
        public VertexBlob Blob
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="columns">Width in tiles</param>
        /// <param name="rows">Height in tiles</param>
        /// <param name="size">Tile size</param>
        /// <param name="device">Graphics device</param>
        public TileMesh(int columns, int rows, float size)
        {
            Debug.Assert(Renderer.Device != null, "Renderer has not been initialized !");

            Blob = new VertexBlob();
            Blob.Primitive = VertexBlob.PRIMITIVE_TYPE.TRINAGLE;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int beginIndex = Blob.VertexCount;
                    float beginX = j * size;
                    float beginY = i * size;

                    VertexData v1 = new VertexData()
                    {
                        m_position = new Vector3(beginX, 0.0f, beginY),
                        m_color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        m_texcoord0 = new Vector2(0.0f, 0.0f)
                    };

                    VertexData v2 = new VertexData()
                    {
                        m_position = new Vector3(beginX + size, 0.0f, beginY),
                        m_color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        m_texcoord0 = new Vector2(1.0f, 0.0f)
                    };

                    VertexData v3 = new VertexData()
                    {
                        m_position = new Vector3(beginX + size, 0.0f, beginY + size),
                        m_color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        m_texcoord0 = new Vector2(1.0f, 1.0f)
                    };

                    VertexData v4 = new VertexData()
                    {
                        m_position = new Vector3(beginX, 0.0f, beginY + size),
                        m_color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        m_texcoord0 = new Vector2(0.0f, 1.0f)
                    };

                    Blob.AddVertex(v1);
                    Blob.AddVertex(v2);
                    Blob.AddVertex(v3);
                    Blob.AddVertex(v4);

                    Blob.AddTriangle(beginIndex, beginIndex + 1, beginIndex + 2);
                    Blob.AddTriangle(beginIndex, beginIndex + 2, beginIndex + 3);
                }
            }

            Blob.Update(Renderer.Device);
        }
    }
}
