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
    public class ControlEdge
    {
        [NonSerialized]
        private BasicEffect _effect;
        [NonSerialized]
        private Matrix _vertexWorld = Matrix.Identity;
        [NonSerialized]
        private Color _usualColor = Color.Blue;
        [NonSerialized]
        private Color _highlightColor = Color.Red;
        [NonSerialized]
        private Color _wireModeColor = new Color(10, 10, 10);

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

        public Vector3 Center { get { return (FirstVertex + SecondVertex) / 2; } }
        public float Length { get { return Vector3.Distance(FirstVertex, SecondVertex); } }
        public bool IsSelected { get { return Engine.EdgeSelectionPool.Contains(this); } }
        public Matrix VertexWorld { get { return _vertexWorld; } }

        private ControlEdge()
        {
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
            _vertexData = new VertexPositionColor[2];

            FirstVertex = new Vector3(0f);
            SecondVertex = new Vector3(0f);
        }

        public ControlEdge(Vector3 firstVertex, Vector3 secondVertex)
            : this()
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;

            _vertexData[0] = new VertexPositionColor(FirstVertex, Color.White);
            _vertexData[1] = new VertexPositionColor(SecondVertex, Color.White);
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext c)
        {
            _vertexWorld = Matrix.Identity;
            _usualColor = Color.Blue;
            _highlightColor = Color.Red;
            _wireModeColor = new Color(10, 10, 10);
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
        }

        public void Translate(Vector3 delta)
        {
            FirstVertex += delta;
            SecondVertex += delta;
        }
        public void TranslateToPoint(Vector3 point)
        {
            FirstVertex = point;
            SecondVertex = point;
        }
        public void Rotate(Quaternion qRotate, Vector3 center)
        {
            FirstVertex = Vector3.Transform(FirstVertex - center, qRotate) + center;
            SecondVertex = Vector3.Transform(SecondVertex - center, qRotate) + center;
        }
        public void Scale(Vector3 scale, Vector3 center)
        {
            FirstVertex += (FirstVertex - center) * scale;
            SecondVertex += (SecondVertex - center) * scale;
        }

        public void Draw()
        {
            Vector3 drawCol = IsSelected ? _highlightColor.ToVector3() : _usualColor.ToVector3();
            if (Engine.ActiveSubObjectMode != SubObjectMode.Edge)
                drawCol = _wireModeColor.ToVector3();

            _effect.World = Matrix.Identity;
            _effect.View = Engine.ActiveCamera.ViewMatrix;
            _effect.Projection = Engine.ActiveCamera.ProjectionMatrix;

            _effect.DiffuseColor = drawCol;
            _effect.EmissiveColor = _effect.DiffuseColor;

            _effect.CurrentTechnique.Passes[0].Apply();

            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _vertexData, 0, 1);
        }

        public bool Select(Ray ray)
        {
            Vector3 crossVector = Vector3.Cross(FirstVertex - SecondVertex, ray.Position - SecondVertex);
            Microsoft.Xna.Framework.Plane plane = new Microsoft.Xna.Framework.Plane(FirstVertex, SecondVertex, crossVector + SecondVertex);
            float? dist = ray.Intersects(plane);

            if (!dist.HasValue)
                return false;

            Vector3 intersectPoint = ray.Position + ray.Direction * dist.Value;

            if (Vector3.Distance(intersectPoint, FirstVertex) > Length)
                return false;
            if (Vector3.Distance(intersectPoint, SecondVertex) > Length)
                return false;

            float intersectDist = Vector3.Cross(SecondVertex - FirstVertex, intersectPoint - FirstVertex).Length() / Vector3.Distance(SecondVertex, FirstVertex);

            return intersectDist < 0.1f;
        }

        public bool Contains(ControlVertex vert1, ControlVertex vert2)
        {
            return (FirstVertex == vert1.Position && SecondVertex == vert2.Position) ||
                   (SecondVertex == vert2.Position && FirstVertex == vert1.Position);
        }
    }
}
