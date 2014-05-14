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
    public class ControlVertex
    {
        private BasicEffect _effect;
        private Matrix _vertexWorld = Matrix.Identity;
        private BoundingBox _boundingBox;
        private Color _usualColor = Color.Blue;
        private Color _highlightColor = Color.Red;

        public Vector3 Position { get; set; }
        public bool IsSelected { get { return Engine.VertexSelectionPool.Contains(this); } }
        public BoundingBox BoundingBox { get { return _boundingBox; } }
        public Matrix VertexWorld { get { return _vertexWorld; } }

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

        public void Translate(Vector3 delta)
        {
            Position += delta;
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
