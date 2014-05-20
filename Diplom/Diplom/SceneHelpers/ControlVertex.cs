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
    public class ControlVertex
    {
        [NonSerialized]
        private BasicEffect _effect;

        [NonSerialized]
        private Matrix _vertexWorld = Matrix.Identity;
        public Matrix VertexWorld { get { return _vertexWorld; } }

        [NonSerialized]
        private BoundingBox _boundingBox;
        public BoundingBox BoundingBox { get { return _boundingBox; } }

        [NonSerialized]
        private Color _usualColor = Color.Blue;
        [NonSerialized]
        private Color _highlightColor = Color.Red;

        public Vector3 Position { get; set; }

        public bool IsSelected { get { return Engine.VertexSelectionPool.Contains(this); } }

        private ControlVertex()
        {
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
            Position = new Vector3(0f);

            _boundingBox = Utils.CalculateBoundingBox(Utils.VerticesOfControlVertex);
        }

        public ControlVertex(Vector3 position)
            : this()
        {
            Position = position;
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext c)
        {
            _vertexWorld = Matrix.Identity;
            _usualColor = Color.Blue;
            _highlightColor = Color.Red;
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
            _boundingBox = Utils.CalculateBoundingBox(Utils.VerticesOfControlVertex);
        }

        public void Translate(Vector3 delta)
        {
            Position += delta;
        }
        public void TranslateToPoint(Vector3 point)
        {
            Position = point;
        }
        public void Rotate(Quaternion qRotate, Vector3 center)
        {
            if (center == Position) return;
            Position = Vector3.Transform(Position - center, qRotate) + center;
        }
        public void Scale(Vector3 scale, Vector3 center)
        {
            if (center == Position) return;
            Position += (Position - center) * scale;
        }

        public void Draw()
        {
            _vertexWorld = Matrix.CreateScale(new Vector3((Engine.ActiveCamera.Position - Position).Length() / 25)) * 
                           Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up);

            Engine.ActiveGraphicsDevice.DepthStencilState = DepthStencilState.Default;

            _effect.World = _vertexWorld;
            _effect.View = Engine.ActiveCamera.ViewMatrix;
            _effect.Projection = Engine.ActiveCamera.ProjectionMatrix;

            _effect.DiffuseColor = IsSelected ? _highlightColor.ToVector3() : _usualColor.ToVector3();
            _effect.EmissiveColor = _effect.DiffuseColor;

            _effect.CurrentTechnique.Passes[0].Apply();

            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Utils.VerticesOfControlVertex, 0, 12);
        }
    }
}
