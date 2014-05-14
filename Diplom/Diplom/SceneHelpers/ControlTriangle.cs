using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.Intefaces;
using Diplom.Primitives;

namespace Diplom.SceneHelpers
{
    public class ControlTriangle
    {
        private BasicEffect _effect;
        private Matrix _vertexWorld = Matrix.Identity;
        private Color _usualColor = new Color(0,0,150,150);
        private Color _highlightColor = new Color(150, 0, 0, 150);

        private VertexPositionColor[] _vertexData;

        public Vector3 FirstVertex 
        {
            get { return _vertexData[0].Position; }
            set { _vertexData[0].Position = value; }
        }
        public Vector3 SecondVertex
        {
            get { return _vertexData[1].Position; }
            set { _vertexData[1].Position = value; }
        }
        public Vector3 ThirdVertex
        {
            get { return _vertexData[2].Position; }
            set { _vertexData[2].Position = value; }
        }

        public Vector3 Center { get { return (FirstVertex + SecondVertex + ThirdVertex) / 3; } }
        public float Length { get { return Vector3.Distance(FirstVertex, SecondVertex); } }
        public bool IsSelected { get { return Engine.TriangleSelectionPool.Contains(this); } }
        public Matrix VertexWorld { get { return _vertexWorld; } }

        private ControlTriangle()
        {
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
            _vertexData = new VertexPositionColor[3];

            FirstVertex = new Vector3(0f);
            SecondVertex = new Vector3(0f);
            ThirdVertex = new Vector3(0f);
        }

        public ControlTriangle(Vector3 firstVertex, Vector3 secondVertex, Vector3 thirdVertex)
            : this()
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;

            _vertexData[0] = new VertexPositionColor(FirstVertex, Color.White);
            _vertexData[1] = new VertexPositionColor(SecondVertex, Color.White);
            _vertexData[2] = new VertexPositionColor(ThirdVertex, Color.White);
        }

        public void Translate(Vector3 delta)
        {
            FirstVertex += delta;
            SecondVertex += delta;
            ThirdVertex += delta;
        }

        public void Draw()
        {
            //Engine.ActiveGraphicsDevice.BlendState = BlendState.Additive;

            _effect.World = Matrix.Identity;
            _effect.View = Engine.ActiveCamera.ViewMatrix;
            _effect.Projection = Engine.ActiveCamera.ProjectionMatrix;

            _effect.DiffuseColor = IsSelected ? _highlightColor.ToVector3() : _usualColor.ToVector3();
            _effect.EmissiveColor = _effect.DiffuseColor;

            _effect.CurrentTechnique.Passes[0].Apply();

            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexData, 0, 1);

            //Engine.ActiveGraphicsDevice.BlendState = BlendState.Opaque;
        }
    }
}
