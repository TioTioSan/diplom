using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom.Primitives
{
    public class Cube : PrimitiveBase
    {
        public Cube()
        {
            _primitiveCount = 12;
            _primitiveType = PrimitiveType.TriangleList;
            _vertexPositions = new Vector3[8]
                {
                    new Vector3(-1f,1f,1f),new Vector3(1f,1f,1f),new Vector3(1f,1f,-1f),new Vector3(-1f,1f,-1f),
                    new Vector3(-1f,-1f,1f),new Vector3(1f,-1f,1f),new Vector3(1f,-1f,-1f),new Vector3(-1f,-1f,-1f)
                };
            _normals = new Vector3[6]
                {
                    Vector3.Up, Vector3.Backward, Vector3.Left, Vector3.Forward, Vector3.Right, Vector3.Down
                };
            _textureCoords = new Vector2[4]
                {
                    new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f)
                };
            _edgeVertexIndexes = new Tuple<int, int>[18] 
                { 
                    new Tuple<int,int>(0,1),
                    new Tuple<int,int>(0,3),
                    new Tuple<int,int>(0,4),
                    new Tuple<int,int>(0,7),
                    new Tuple<int,int>(6,2),
                    new Tuple<int,int>(6,3),
                    new Tuple<int,int>(6,4),
                    new Tuple<int,int>(6,5),
                    new Tuple<int,int>(6,7),
                    new Tuple<int,int>(1,2),
                    new Tuple<int,int>(1,3),
                    new Tuple<int,int>(1,4),
                    new Tuple<int,int>(1,5),
                    new Tuple<int,int>(7,3),
                    new Tuple<int,int>(7,4),
                    new Tuple<int,int>(2,3),
                    new Tuple<int,int>(2,5),
                    new Tuple<int,int>(4,5)
                };
            _triangleVertexIndexes = new Tuple<int, int, int>[12]
                {
                    new Tuple<int, int, int>(0,1,3),
                    new Tuple<int, int, int>(3,1,2),
                    new Tuple<int, int, int>(3,2,6),
                    new Tuple<int, int, int>(3,6,7),
                    new Tuple<int, int, int>(3,7,0),
                    new Tuple<int, int, int>(0,7,4),
                    new Tuple<int, int, int>(0,4,1),
                    new Tuple<int, int, int>(1,4,5),
                    new Tuple<int, int, int>(1,5,2),
                    new Tuple<int, int, int>(2,5,6),
                    new Tuple<int, int, int>(6,5,4),
                    new Tuple<int, int, int>(6,4,7)
                };
            _vertexData = new VertexPositionNormalTexture[36]
                {
                    //up
                    new VertexPositionNormalTexture(_vertexPositions[0], _normals[0], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[0], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[0], _textureCoords[3]),

                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[0], _textureCoords[3]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[0], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[0], _textureCoords[2]),

                    //back
                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[1], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[6], _normals[1], _textureCoords[2]),

                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[6], _normals[1], _textureCoords[2]),
                    new VertexPositionNormalTexture(_vertexPositions[7], _normals[1], _textureCoords[3]),

                    //left
                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[7], _normals[2], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[0], _normals[2], _textureCoords[3]),

                    new VertexPositionNormalTexture(_vertexPositions[0], _normals[2], _textureCoords[3]),
                    new VertexPositionNormalTexture(_vertexPositions[7], _normals[2], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[4], _normals[2], _textureCoords[2]),

                    //front
                    new VertexPositionNormalTexture(_vertexPositions[0], _normals[3], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[4], _normals[3], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[3], _textureCoords[2]),

                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[3], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[4], _normals[3], _textureCoords[2]),
                    new VertexPositionNormalTexture(_vertexPositions[5], _normals[3], _textureCoords[3]),

                    //right
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[4], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[5], _normals[4], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[4], _textureCoords[3]),

                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[4], _textureCoords[3]),
                    new VertexPositionNormalTexture(_vertexPositions[5], _normals[4], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[6], _normals[4], _textureCoords[2]),

                    //bottom
                    new VertexPositionNormalTexture(_vertexPositions[6], _normals[5], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[5], _normals[5], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[4], _normals[5], _textureCoords[3]),

                    new VertexPositionNormalTexture(_vertexPositions[6], _normals[5], _textureCoords[3]),
                    new VertexPositionNormalTexture(_vertexPositions[4], _normals[5], _textureCoords[1]),
                    new VertexPositionNormalTexture(_vertexPositions[7], _normals[5], _textureCoords[2])
                };
        }

        public override PrimitiveBase CreateNew()
        {
            return new Cube();
        }
    }
}
