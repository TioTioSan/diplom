using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom.Primitives
{
    public class Cylinder : PrimitiveBase
    {
        public Cylinder()
        {
            _primitiveCount = 40;
            _primitiveType = PrimitiveType.TriangleList;
            _vertexPositions = new Vector3[23]
                {
                    Vector3.Zero,
                    new Vector3(0.000000f,-1.000000f,0.000000f),new Vector3(0.000000f,1.000000f,0.000000f),
                    new Vector3(0.000000f,-1.000000f,-1.000000f),new Vector3(0.000000f,1.000000f,-1.000000f),
                    new Vector3(0.587785f,-1.000000f,-0.809017f),new Vector3(0.587785f,1.000000f,-0.809017f),
                    new Vector3(0.951057f,-1.000000f,-0.309017f),new Vector3(0.951057f,1.000000f,-0.309017f),
                    new Vector3(0.951056f,-1.000000f,0.309017f),new Vector3(0.951056f,1.000000f,0.309017f),
                    new Vector3(0.587785f,-1.000000f,0.809017f),new Vector3(0.587785f,1.000000f,0.809017f),
                    new Vector3(-0.000000f,-1.000000f,1.000000f),new Vector3(-0.000000f,1.000000f,1.000000f),
                    new Vector3(-0.587785f,-1.000000f,0.809017f),new Vector3(-0.587785f,1.000000f,0.809017f),
                    new Vector3(-0.951056f,-1.000000f,0.309017f),new Vector3(-0.951056f,1.000000f,0.309017f),
                    new Vector3(-0.951056f,-1.000000f,-0.309017f),new Vector3(-0.951056f,1.000000f,-0.309017f),
                    new Vector3(-0.587785f,-1.000000f,-0.809017f),new Vector3(-0.587785f,1.000000f,-0.809017f)
                };
            _normals = new Vector3[13]
                {
                    Vector3.Zero,
                    new Vector3(0.000000f,-1.000000f,0.000000f),new Vector3(0.000000f,1.000000f,0.000000f),
                    new Vector3(0.309017f,0.000000f,-0.951057f),new Vector3(0.809017f,-0.000000f,-0.587785f),
                    new Vector3(1.000000f,-0.000000f,0.000000f),new Vector3(0.809017f,-0.000000f,0.587785f),
                    new Vector3(0.309017f,-0.000000f,0.951057f),new Vector3(-0.309017f,0.000000f,0.951056f),
                    new Vector3(-0.809017f,-0.000000f,0.587785f),new Vector3(-1.000000f,-0.000000f,0.000000f),
                    new Vector3(-0.809017f,0.000000f,-0.587785f),new Vector3(-0.309017f,-0.000000f,-0.951057f)
                };
            _textureCoords = new Vector2[1]
                {
                    new Vector2(0f, 0f)
                };
            _edgeVertexIndexes = new Tuple<int, int>[120] 
		        { 
                    new Tuple<int,int>(1,3),new Tuple<int,int>(3,5),new Tuple<int,int>(5,1),new Tuple<int,int>(2,6),
                    new Tuple<int,int>(6,4),new Tuple<int,int>(4,2),new Tuple<int,int>(4,6),new Tuple<int,int>(6,5),
                    new Tuple<int,int>(5,4),new Tuple<int,int>(1,5),new Tuple<int,int>(5,7),new Tuple<int,int>(7,1),
                    new Tuple<int,int>(2,8),new Tuple<int,int>(8,6),new Tuple<int,int>(6,2),new Tuple<int,int>(6,8),
                    new Tuple<int,int>(8,7),new Tuple<int,int>(7,6),new Tuple<int,int>(1,7),new Tuple<int,int>(7,9),
                    new Tuple<int,int>(9,1),new Tuple<int,int>(2,10),new Tuple<int,int>(10,8),new Tuple<int,int>(8,2),
                    new Tuple<int,int>(8,10),new Tuple<int,int>(10,9),new Tuple<int,int>(9,8),new Tuple<int,int>(1,9),
                    new Tuple<int,int>(9,11),new Tuple<int,int>(11,1),new Tuple<int,int>(2,12),new Tuple<int,int>(12,10),
                    new Tuple<int,int>(10,2),new Tuple<int,int>(10,12),new Tuple<int,int>(12,11),new Tuple<int,int>(11,10),
                    new Tuple<int,int>(1,11),new Tuple<int,int>(11,13),new Tuple<int,int>(13,1),new Tuple<int,int>(2,14),
                    new Tuple<int,int>(14,12),new Tuple<int,int>(12,2),new Tuple<int,int>(12,14),new Tuple<int,int>(14,13),
                    new Tuple<int,int>(13,12),new Tuple<int,int>(1,13),new Tuple<int,int>(13,15),new Tuple<int,int>(15,1),
                    new Tuple<int,int>(2,16),new Tuple<int,int>(16,14),new Tuple<int,int>(14,2),new Tuple<int,int>(14,16),
                    new Tuple<int,int>(16,15),new Tuple<int,int>(15,14),new Tuple<int,int>(1,15),new Tuple<int,int>(15,17),
                    new Tuple<int,int>(17,1),new Tuple<int,int>(2,18),new Tuple<int,int>(18,16),new Tuple<int,int>(16,2),
                    new Tuple<int,int>(16,18),new Tuple<int,int>(18,17),new Tuple<int,int>(17,16),new Tuple<int,int>(1,17),
                    new Tuple<int,int>(17,19),new Tuple<int,int>(19,1),new Tuple<int,int>(2,20),new Tuple<int,int>(20,18),
                    new Tuple<int,int>(18,2),new Tuple<int,int>(18,20),new Tuple<int,int>(20,19),new Tuple<int,int>(19,18),
                    new Tuple<int,int>(1,19),new Tuple<int,int>(19,21),new Tuple<int,int>(21,1),new Tuple<int,int>(2,22),
                    new Tuple<int,int>(22,20),new Tuple<int,int>(20,2),new Tuple<int,int>(20,22),new Tuple<int,int>(22,21),
                    new Tuple<int,int>(21,20),new Tuple<int,int>(1,21),new Tuple<int,int>(21,3),new Tuple<int,int>(3,1),
                    new Tuple<int,int>(2,4),new Tuple<int,int>(4,22),new Tuple<int,int>(22,2),new Tuple<int,int>(22,4),
                    new Tuple<int,int>(4,3),new Tuple<int,int>(3,22),new Tuple<int,int>(3,4),new Tuple<int,int>(4,5),
                    new Tuple<int,int>(5,3),new Tuple<int,int>(5,6),new Tuple<int,int>(6,7),new Tuple<int,int>(7,5),
                    new Tuple<int,int>(7,8),new Tuple<int,int>(8,9),new Tuple<int,int>(9,7),new Tuple<int,int>(9,10),
                    new Tuple<int,int>(10,11),new Tuple<int,int>(11,9),new Tuple<int,int>(11,12),new Tuple<int,int>(12,13),
                    new Tuple<int,int>(13,11),new Tuple<int,int>(13,14),new Tuple<int,int>(14,15),new Tuple<int,int>(15,13),
                    new Tuple<int,int>(15,16),new Tuple<int,int>(16,17),new Tuple<int,int>(17,15),new Tuple<int,int>(17,18),
                    new Tuple<int,int>(18,19),new Tuple<int,int>(19,17),new Tuple<int,int>(19,20),new Tuple<int,int>(20,21),
                    new Tuple<int,int>(21,19),new Tuple<int,int>(21,22),new Tuple<int,int>(22,3),new Tuple<int,int>(3,21)
                };
            _triangleVertexIndexes = new Tuple<int, int, int>[40]
                {
                    new Tuple<int, int, int>(1,3,5),new Tuple<int, int, int>(2,6,4),new Tuple<int, int, int>(4,6,5),
                    new Tuple<int, int, int>(1,5,7),new Tuple<int, int, int>(2,8,6),new Tuple<int, int, int>(6,8,7),
                    new Tuple<int, int, int>(1,7,9),new Tuple<int, int, int>(2,10,8),new Tuple<int, int, int>(8,10,9),
                    new Tuple<int, int, int>(1,9,11),new Tuple<int, int, int>(2,12,10),new Tuple<int, int, int>(10,12,11),
                    new Tuple<int, int, int>(1,11,13),new Tuple<int, int, int>(2,14,12),new Tuple<int, int, int>(12,14,13),
                    new Tuple<int, int, int>(1,13,15),new Tuple<int, int, int>(2,16,14),new Tuple<int, int, int>(14,16,15),
                    new Tuple<int, int, int>(1,15,17),new Tuple<int, int, int>(2,18,16),new Tuple<int, int, int>(16,18,17),
                    new Tuple<int, int, int>(1,17,19),new Tuple<int, int, int>(2,20,18),new Tuple<int, int, int>(18,20,19),
                    new Tuple<int, int, int>(1,19,21),new Tuple<int, int, int>(2,22,20),new Tuple<int, int, int>(20,22,21),
                    new Tuple<int, int, int>(1,21,3),new Tuple<int, int, int>(2,4,22),new Tuple<int, int, int>(22,4,3),
                    new Tuple<int, int, int>(3,4,5),new Tuple<int, int, int>(5,6,7),new Tuple<int, int, int>(7,8,9),
                    new Tuple<int, int, int>(9,10,11),new Tuple<int, int, int>(11,12,13),new Tuple<int, int, int>(13,14,15),
                    new Tuple<int, int, int>(15,16,17),new Tuple<int, int, int>(17,18,19),new Tuple<int, int, int>(19,20,21),
                    new Tuple<int, int, int>(21,22,3)
                };
            _vertexData = new VertexPositionNormalTexture[120]
		        {
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[3], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[5], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[6], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[4], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[4], _normals[3], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[6], _normals[3], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[5], _normals[3], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[5], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[7], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[8], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[6], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[6], _normals[4], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[8], _normals[4], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[7], _normals[4], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[7], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[9], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[10], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[8], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[8], _normals[5], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[10], _normals[5], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[9], _normals[5], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[9], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[11], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[12], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[10], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[10], _normals[6], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[12], _normals[6], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[11], _normals[6], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[11], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[13], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[14], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[12], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[12], _normals[7], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[14], _normals[7], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[13], _normals[7], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[13], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[15], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[16], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[14], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[14], _normals[8], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[16], _normals[8], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[15], _normals[8], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[15], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[17], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[18], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[16], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[16], _normals[9], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[18], _normals[9], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[17], _normals[9], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[17], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[19], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[20], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[18], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[18], _normals[10], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[20], _normals[10], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[19], _normals[10], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[19], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[21], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[22], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[20], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[20], _normals[11], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[22], _normals[11], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[21], _normals[11], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[1], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[21], _normals[1], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[3], _normals[1], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[2], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[4], _normals[2], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[22], _normals[2], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[22], _normals[12], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[4], _normals[12], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[3], _normals[12], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[3], _normals[3], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[4], _normals[3], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[5], _normals[3], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[5], _normals[4], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[6], _normals[4], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[7], _normals[4], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[7], _normals[5], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[8], _normals[5], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[9], _normals[5], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[9], _normals[6], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[10], _normals[6], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[11], _normals[6], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[11], _normals[7], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[12], _normals[7], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[13], _normals[7], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[13], _normals[8], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[14], _normals[8], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[15], _normals[8], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[15], _normals[9], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[16], _normals[9], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[17], _normals[9], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[17], _normals[10], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[18], _normals[10], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[19], _normals[10], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[19], _normals[11], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[20], _normals[11], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[21], _normals[11], _textureCoords[0]),
                    new VertexPositionNormalTexture(_vertexPositions[21], _normals[12], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[22], _normals[12], _textureCoords[0]),new VertexPositionNormalTexture(_vertexPositions[3], _normals[12], _textureCoords[0])
                }; 
        }

        public override PrimitiveBase CreateNew()
        {
            return new Cylinder();
        }
    }
}
