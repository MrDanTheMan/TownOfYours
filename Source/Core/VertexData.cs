using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TownOfYours.Core
{
    public struct VertexData : IVertexType
    {
        public Vector3 m_position;
        public Vector4 m_color;
        public Vector2 m_texcoord0;

        VertexDeclaration IVertexType.VertexDeclaration => new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(12, VertexElementFormat.Vector4, VertexElementUsage.Color, 0),
            new VertexElement(28, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
        );
    }
}
