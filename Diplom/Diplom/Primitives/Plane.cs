using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom.Primitives
{
    public class Plane : PrimitiveBase
    {
        public Plane()
        {
            _primitiveCount = 2;
            _primitiveType = PrimitiveType.TriangleList;
            _vertexPositions = new Vector3[4]
                {
                    new Vector3(-2f,0f,2f),new Vector3(2f,0f,2f),new Vector3(2f,0f,-2f),new Vector3(-2f,0f,-2f)
                };
            _normals = new Vector3[1]
                {
                    Vector3.Up
                };
            _textureCoords = new Vector2[4]
                {
                    new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f)
                };
            _edgeVertexIndexes = new Tuple<int, int>[5] 
                { 
                    new Tuple<int,int>(0,1),
                    new Tuple<int,int>(1,3),
                    new Tuple<int,int>(3,0),
                    new Tuple<int,int>(1,2),
                    new Tuple<int,int>(2,3)
                };
            _triangleVertexIndexes = new Tuple<int, int, int>[2]
                {
                    new Tuple<int, int, int>(0,1,3),
                    new Tuple<int, int, int>(3,1,2)
                };
            _vertexData = new VertexPositionNormalTexture[6]
                {
                    new VertexPositionNormalTexture(_vertexPositions[0], _normals[0], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[0], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[0], _textureCoords[3]),

                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[0], _textureCoords[3]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[0], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[0], _textureCoords[2]),
                };
        }

        public override PrimitiveBase CreateNew()
        {
            return new Plane();
        }
    }
}
