using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom.Primitives
{
    public class PrimitiveBase
    {
        protected VertexPositionNormalTexture[] _vertexData;
        public VertexPositionNormalTexture[] VertexData { get { return _vertexData; } }

        protected PrimitiveType _primitiveType;
        public PrimitiveType PrimitiveType { get { return _primitiveType; } }

        protected Vector3[] _vertexPositions;
        public Vector3[] VertexPositions { get { return _vertexPositions; } }

        protected Tuple<int,int>[] _edgeVertexIndexes;
        public Tuple<int, int>[] EdgeVertexIndexes { get { return _edgeVertexIndexes; } }

        protected Tuple<int, int, int>[] _triangleVertexIndexes;
        public Tuple<int, int, int>[] TriangleVertexIndexes { get { return _triangleVertexIndexes; } }
        
        protected Vector3[] _normals;
        public Vector3[] Normals { get { return _normals; } }

        protected Vector2[] _textureCoords;
        public Vector2[] TextureCoords { get { return _textureCoords; } }

        protected int _primitiveCount;
        public int PrimitiveCount { get { return _primitiveCount; } }
    }
}
