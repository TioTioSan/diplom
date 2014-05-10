using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom.Primitives
{
    public class Primitive
    {
        protected VertexPositionNormalTexture[] _vertexData;
        public VertexPositionNormalTexture[] VertexData { get { return _vertexData; } }

        protected PrimitiveType _primitiveType;
        public PrimitiveType PrimitiveType { get { return _primitiveType; } }

        protected Vector3[] _vertexPositions;
        public Vector3[] VertexPositions { get { return _vertexPositions; } }
        
        protected Vector3[] _normals;
        public Vector3[] Normals { get { return _normals; } }

        protected Vector2[] _textureCoords;
        public Vector2[] TextureCoords { get { return _textureCoords; } }

        protected int _primitiveCount;
        public int PrimitiveCount { get { return _primitiveCount; } }
    }
}
