using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.Primitives;
using System.Runtime.Serialization;

namespace Diplom.SceneHelpers
{
    [Serializable]
    public class ControlTriangle
    {
        [NonSerialized]
        private BasicEffect _effect;
        [NonSerialized]
        private Matrix _vertexWorld = Matrix.Identity;
        [NonSerialized]
        private Color _usualColor = new Color(0, 0, 150, 150);
        [NonSerialized]
        private Color _highlightColor = new Color(100, 0, 0, 100);

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

        [OnDeserialized]
        void OnDeserialized(StreamingContext c)
        {
            _vertexWorld = Matrix.Identity;
            _usualColor = new Color(0, 0, 150, 150);
            _highlightColor = new Color(100, 0, 0, 100);
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
        }

        public void Translate(Vector3 delta)
        {
            FirstVertex += delta;
            SecondVertex += delta;
            ThirdVertex += delta;
        }
        public void TranslateToPoint(Vector3 point)
        {
            FirstVertex = point;
            SecondVertex = point;
            ThirdVertex = point;
        }
        public void Rotate(Quaternion qRotate, Vector3 center)
        {
            FirstVertex = Vector3.Transform(FirstVertex - center, qRotate) + center;
            SecondVertex = Vector3.Transform(SecondVertex - center, qRotate) + center;
            ThirdVertex = Vector3.Transform(ThirdVertex - center, qRotate) + center;
        }
        public void Scale(Vector3 scale, Vector3 center)
        {
            FirstVertex += (FirstVertex - center) * scale;
            SecondVertex += (SecondVertex - center) * scale;
            ThirdVertex += (ThirdVertex - center) * scale;
        }

        public void Draw()
        {
            if (!IsSelected) return;

            _effect.World = Matrix.Identity;
            _effect.View = Engine.ActiveCamera.ViewMatrix;
            _effect.Projection = Engine.ActiveCamera.ProjectionMatrix;

            _effect.DiffuseColor = _highlightColor.ToVector3();
            _effect.EmissiveColor = _effect.DiffuseColor;

            _effect.CurrentTechnique.Passes[0].Apply();

            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexData, 0, 1);
        }

        public bool Contains(ControlEdge edge)
        {
            return (FirstVertex == edge.FirstVertex || SecondVertex == edge.FirstVertex || ThirdVertex == edge.FirstVertex) &&
                   (FirstVertex == edge.SecondVertex || SecondVertex == edge.SecondVertex || ThirdVertex == edge.SecondVertex);
        }

        public bool Contains(ControlEdge edge1, ControlEdge edge2, ControlEdge edge3)
        {
            return Contains(edge1) && Contains(edge2) && Contains(edge3);
        }
    }
}
