using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Diplom.SceneHelpers
{
    public class IndexedVertexModel
    {
        public readonly VertexPositionNormalTexture[] Vertices;
        public readonly short[] Indices;

        public IndexedVertexModel(Vector3[] positions, Vector3[] normals, short[] indices)
        {
            Indices = indices;
            var texdata = new[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };
            Vertices = new VertexPositionNormalTexture[positions.Length];
            for (int i = 0; i < positions.Length; i++)
                Vertices[i] = new VertexPositionNormalTexture(positions[i], normals[i], texdata[i % 3]);
        }
    }
}
