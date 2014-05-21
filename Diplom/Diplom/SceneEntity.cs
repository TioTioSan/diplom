using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.Primitives;
using Diplom.SceneHelpers;
using System.Runtime.Serialization;

namespace Diplom
{
    [Serializable]
    public class SceneEntity
    {
        #region Fields & Properties
        private int _id;
        public int Id { get { return _id; } }

        [NonSerialized]
        private BasicEffect _basicEffect;

        [NonSerialized]
        private BasicEffect _selectionBoxEffect;
        [NonSerialized]
        private List<VertexPositionColor> _selectionBoxVertices = new List<VertexPositionColor>();

        private Vector3 _position = Vector3.Zero;
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        [NonSerialized]
        private BoundingBox _boundingBox;
        public BoundingBox BoundingBox
        {
            get { return _boundingBox; }
            //get { return new BoundingBox(Position - (Vector3.One * LENGTH) * Scale, Position + (Vector3.One * LENGTH) * Scale); }
        }

        private Vector3 _center;
        public Vector3 Center { get { return _center; } }

        private Vector3[] _vertexPositions;
        public Vector3[] VertexPositions { get { return _vertexPositions; } }

        private VertexPositionNormalTexture[] _vertexData;
        public VertexPositionNormalTexture[] VertexData { get { return _vertexData; } }

        private PrimitiveType _primitiveType;
        public PrimitiveType PrimitiveType { get { return _primitiveType; } }

        private int _primitiveCount;
        public int PrimitiveCount { get { return _primitiveCount; } }

        private List<ControlVertex> _controlVertices;
        public List<ControlVertex> ControlVertices { get { return _controlVertices; } }

        private List<ControlEdge> _controlEdges;
        public List<ControlEdge> ControlEdges { get { return _controlEdges; } }

        private List<ControlTriangle> _controlTriangles;
        public List<ControlTriangle> ControlTriangles { get { return _controlTriangles; } }
        #endregion

        public SceneEntity(PrimitiveBase primitive)
        {
            _id = Engine.EntityCount++;

            _basicEffect = new BasicEffect(Engine.ActiveGraphicsDevice);
            _basicEffect.EnableDefaultLighting();

            _selectionBoxEffect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };

            _vertexData = primitive.VertexData;
            _vertexPositions = primitive.VertexPositions;
            _primitiveType = primitive.PrimitiveType;
            _primitiveCount = primitive.PrimitiveCount;

            _controlVertices = new List<ControlVertex>();
            foreach (Vector3 vert in _vertexPositions)
            {
                _controlVertices.Add(new ControlVertex(vert));
            }

            _controlEdges = new List<ControlEdge>();
            foreach (Tuple<int, int> tuple in primitive.EdgeVertexIndexes)
            {
                _controlEdges.Add(new ControlEdge(_vertexPositions[tuple.Item1], _vertexPositions[tuple.Item2]));
            }

            _controlTriangles = new List<ControlTriangle>();
            foreach (Tuple<int, int, int> tuple in primitive.TriangleVertexIndexes)
            {
                _controlTriangles.Add(new ControlTriangle(_vertexPositions[tuple.Item1], _vertexPositions[tuple.Item2], _vertexPositions[tuple.Item3]));
            }

            RecalcCenter();
            RecalcBoundingBox();
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext c)
        {
            _basicEffect = new BasicEffect(Engine.ActiveGraphicsDevice);
            _basicEffect.EnableDefaultLighting();
            _selectionBoxEffect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
            _selectionBoxVertices = new List<VertexPositionColor>();
            RecalcBoundingBox();
        }

        public float? Select(Ray selectionRay)
        {
            return selectionRay.Intersects(_boundingBox);
        }

        public void Draw(Camera camera)
        {
            Engine.ActiveGraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Engine.ActiveGraphicsDevice.RasterizerState = RasterizerState.CullNone;

            _basicEffect.World = camera.WorldMatrix;
            _basicEffect.View = camera.ViewMatrix;
            _basicEffect.Projection = camera.ProjectionMatrix;

            _basicEffect.DiffuseColor = Color.White.ToVector3();

            _basicEffect.CurrentTechnique.Passes[0].Apply();

            if (_primitiveCount > 0)
                Engine.ActiveGraphicsDevice.DrawUserPrimitives(_primitiveType, _vertexData, 0, _primitiveCount);

            if (Engine.EntitySelectionPool.Contains(this))
            {
                switch (Engine.ActiveSubObjectMode)
                {
                    case SubObjectMode.Vertex:
                        DrawConrtolVertices();
                        break;
                    case SubObjectMode.Edge:
                        DrawControlEdges();
                        break;
                    case SubObjectMode.Triangle:
                        DrawControlTriangles();
                        break;
                }
                DrawSelectionBox();
            }

            if (Engine.ActiveDrawMode == DrawMode.WithEdges &&
                Engine.ActiveSubObjectMode != SubObjectMode.Edge)
                DrawControlEdges();
        }

        #region Translate
        public void Translate(Vector3 delta)
        {
            _position += delta;
            _center += delta;
            for (int i = 0; i < _controlTriangles.Count; i++)
            {
                _controlTriangles[i].Translate(delta);
            }
            for (int i = 0; i < _controlEdges.Count; i++)
            {
                _controlEdges[i].Translate(delta);
            }
            for (int i = 0; i < _controlVertices.Count; i++)
            {
                _controlVertices[i].Translate(delta);
            }
            for (int i = 0; i < _vertexPositions.Length; i++)
            {
                _vertexPositions[i] += delta;
            }
            for (int i = 0; i < _vertexData.Length; i++)
            {
                _vertexData[i].Position += delta;
            }
            _boundingBox.Min += delta;
            _boundingBox.Max += delta;

            RecreateSelectionBox();
        }

        public void TranslateControlVertices(Vector3 delta)
        {
            foreach (var contrVertex in _controlVertices)
            {
                if (contrVertex.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrVertex.Position)
                            _vertexPositions[i] += delta;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrVertex.Position)
                            _vertexData[i].Position += delta;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].FirstVertex == contrVertex.Position)
                        {
                            _controlEdges[i].FirstVertex += delta;
                            continue;
                        }
                        if (_controlEdges[i].SecondVertex == contrVertex.Position)
                            _controlEdges[i].SecondVertex += delta;
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].FirstVertex == contrVertex.Position)
                        {
                            _controlTriangles[i].FirstVertex += delta;
                            continue;
                        }
                        if (_controlTriangles[i].SecondVertex == contrVertex.Position)
                        {
                            _controlTriangles[i].SecondVertex += delta;
                            continue;
                        }
                        if (_controlTriangles[i].ThirdVertex == contrVertex.Position)
                            _controlTriangles[i].ThirdVertex += delta;
                    }

                    contrVertex.Translate(delta);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void TranslateControlEdges(Vector3 delta)
        {
            foreach (var contrEdge in _controlEdges)
            {
                if (contrEdge.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrEdge.FirstVertex || _vertexPositions[i] == contrEdge.SecondVertex)
                            _vertexPositions[i] += delta;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrEdge.FirstVertex || _vertexData[i].Position == contrEdge.SecondVertex)
                            _vertexData[i].Position += delta;
                    }

                    for (int i = 0; i < _controlVertices.Count; i++)
                    {
                        if (_controlVertices[i].Position == contrEdge.FirstVertex || _controlVertices[i].Position == contrEdge.SecondVertex)
                            _controlVertices[i].Translate(delta);
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].FirstVertex == contrEdge.FirstVertex || _controlTriangles[i].FirstVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].FirstVertex += delta;
                        if (_controlTriangles[i].SecondVertex == contrEdge.FirstVertex || _controlTriangles[i].SecondVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].SecondVertex += delta;
                        if (_controlTriangles[i].ThirdVertex == contrEdge.FirstVertex || _controlTriangles[i].ThirdVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].ThirdVertex += delta;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].IsSelected)
                            continue;

                        if (_controlEdges[i].FirstVertex == contrEdge.FirstVertex || _controlEdges[i].FirstVertex == contrEdge.SecondVertex)
                            _controlEdges[i].FirstVertex += delta;
                        if (_controlEdges[i].SecondVertex == contrEdge.FirstVertex || _controlEdges[i].SecondVertex == contrEdge.SecondVertex)
                            _controlEdges[i].SecondVertex += delta;
                    }

                    contrEdge.Translate(delta);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void TranslateControlTriangles(Vector3 delta)
        {
            foreach (var contrTrn in _controlTriangles)
            {
                if (contrTrn.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrTrn.FirstVertex ||
                            _vertexPositions[i] == contrTrn.SecondVertex ||
                            _vertexPositions[i] == contrTrn.ThirdVertex)
                            _vertexPositions[i] += delta;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrTrn.FirstVertex ||
                            _vertexData[i].Position == contrTrn.SecondVertex ||
                            _vertexData[i].Position == contrTrn.ThirdVertex)
                            _vertexData[i].Position += delta;
                    }

                    for (int i = 0; i < _controlVertices.Count; i++)
                    {
                        if (_controlVertices[i].Position == contrTrn.FirstVertex ||
                            _controlVertices[i].Position == contrTrn.SecondVertex ||
                            _controlVertices[i].Position == contrTrn.ThirdVertex)
                            _controlVertices[i].Translate(delta);
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].FirstVertex == contrTrn.FirstVertex ||
                            _controlEdges[i].FirstVertex == contrTrn.SecondVertex ||
                            _controlEdges[i].FirstVertex == contrTrn.ThirdVertex)
                            _controlEdges[i].FirstVertex += delta;
                        if (_controlEdges[i].SecondVertex == contrTrn.FirstVertex ||
                            _controlEdges[i].SecondVertex == contrTrn.SecondVertex ||
                            _controlEdges[i].SecondVertex == contrTrn.ThirdVertex)
                            _controlEdges[i].SecondVertex += delta;
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].IsSelected)
                            continue;

                        if (_controlTriangles[i].FirstVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].FirstVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].FirstVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].FirstVertex += delta;
                        if (_controlTriangles[i].SecondVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].SecondVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].SecondVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].SecondVertex += delta;
                        if (_controlTriangles[i].ThirdVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].ThirdVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].ThirdVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].ThirdVertex += delta;
                    }

                    contrTrn.Translate(delta);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }
        #endregion

        #region Rotate
        public void Rotate(Quaternion qRotate)
        {
            for (int i = 0; i < _controlTriangles.Count; i++)
            {
                _controlTriangles[i].Rotate(qRotate, _center);
            }
            for (int i = 0; i < _controlEdges.Count; i++)
            {
                _controlEdges[i].Rotate(qRotate, _center);
            }
            for (int i = 0; i < _controlVertices.Count; i++)
            {
                _controlVertices[i].Rotate(qRotate, _center);
            }
            for (int i = 0; i < _vertexPositions.Length; i++)
            {
                _vertexPositions[i] = Vector3.Transform(_vertexPositions[i] - _center, qRotate) + _center;
            }
            for (int i = 0; i < _vertexData.Length; i++)
            {
                _vertexData[i].Position = Vector3.Transform(_vertexData[i].Position - _center, qRotate) + _center;
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void RotateControlVertices(Quaternion qRotate)
        {
            foreach (var contrVertex in _controlVertices)
            {
                if (contrVertex.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrVertex.Position)
                            _vertexPositions[i] = Vector3.Transform(_vertexPositions[i] - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrVertex.Position)
                            _vertexData[i].Position = Vector3.Transform(_vertexData[i].Position - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].FirstVertex == contrVertex.Position)
                        {
                            _controlEdges[i].FirstVertex = Vector3.Transform(_controlEdges[i].FirstVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                            continue;
                        }
                        if (_controlEdges[i].SecondVertex == contrVertex.Position)
                            _controlEdges[i].SecondVertex = Vector3.Transform(_controlEdges[i].SecondVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].FirstVertex == contrVertex.Position)
                        {
                            _controlTriangles[i].FirstVertex = Vector3.Transform(_controlTriangles[i].FirstVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                            continue;
                        }
                        if (_controlTriangles[i].SecondVertex == contrVertex.Position)
                        {
                            _controlTriangles[i].SecondVertex = Vector3.Transform(_controlTriangles[i].SecondVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                            continue;
                        }
                        if (_controlTriangles[i].ThirdVertex == contrVertex.Position)
                            _controlTriangles[i].ThirdVertex = Vector3.Transform(_controlTriangles[i].ThirdVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }

                    contrVertex.Rotate(qRotate, Engine.ActiveControlAxis.Position);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void RotateControlEdges(Quaternion qRotate)
        {
            foreach (var contrEdge in _controlEdges)
            {
                if (contrEdge.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrEdge.FirstVertex || _vertexPositions[i] == contrEdge.SecondVertex)
                            _vertexPositions[i] = Vector3.Transform(_vertexPositions[i] - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrEdge.FirstVertex || _vertexData[i].Position == contrEdge.SecondVertex)
                            _vertexData[i].Position = Vector3.Transform(_vertexData[i].Position - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }

                    for (int i = 0; i < _controlVertices.Count; i++)
                    {
                        if (_controlVertices[i].Position == contrEdge.FirstVertex || _controlVertices[i].Position == contrEdge.SecondVertex)
                            _controlVertices[i].Position = Vector3.Transform(_controlVertices[i].Position - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;

                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].FirstVertex == contrEdge.FirstVertex || _controlTriangles[i].FirstVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].FirstVertex = Vector3.Transform(_controlTriangles[i].FirstVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                        if (_controlTriangles[i].SecondVertex == contrEdge.FirstVertex || _controlTriangles[i].SecondVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].SecondVertex = Vector3.Transform(_controlTriangles[i].SecondVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                        if (_controlTriangles[i].ThirdVertex == contrEdge.FirstVertex || _controlTriangles[i].ThirdVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].ThirdVertex = Vector3.Transform(_controlTriangles[i].ThirdVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].IsSelected)
                            continue;

                        if (_controlEdges[i].FirstVertex == contrEdge.FirstVertex || _controlEdges[i].FirstVertex == contrEdge.SecondVertex)
                            _controlEdges[i].FirstVertex = Vector3.Transform(_controlEdges[i].FirstVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                        if (_controlEdges[i].SecondVertex == contrEdge.FirstVertex || _controlEdges[i].SecondVertex == contrEdge.SecondVertex)
                            _controlEdges[i].SecondVertex = Vector3.Transform(_controlEdges[i].SecondVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }

                    contrEdge.Rotate(qRotate, Engine.ActiveControlAxis.Position);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void RotateControlTriangles(Quaternion qRotate)
        {
            foreach (var contrTrn in _controlTriangles)
            {
                if (contrTrn.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrTrn.FirstVertex ||
                            _vertexPositions[i] == contrTrn.SecondVertex ||
                            _vertexPositions[i] == contrTrn.ThirdVertex)
                            _vertexPositions[i] = Vector3.Transform(_vertexPositions[i] - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrTrn.FirstVertex ||
                            _vertexData[i].Position == contrTrn.SecondVertex ||
                            _vertexData[i].Position == contrTrn.ThirdVertex)
                            _vertexData[i].Position = Vector3.Transform(_vertexData[i].Position - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }

                    for (int i = 0; i < _controlVertices.Count; i++)
                    {
                        if (_controlVertices[i].Position == contrTrn.FirstVertex ||
                            _controlVertices[i].Position == contrTrn.SecondVertex ||
                            _controlVertices[i].Position == contrTrn.ThirdVertex)
                            _controlVertices[i].Rotate(qRotate, Engine.ActiveControlAxis.Position);
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].FirstVertex == contrTrn.FirstVertex ||
                            _controlEdges[i].FirstVertex == contrTrn.SecondVertex ||
                            _controlEdges[i].FirstVertex == contrTrn.ThirdVertex)
                            _controlEdges[i].FirstVertex = Vector3.Transform(_controlEdges[i].FirstVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                        if (_controlEdges[i].SecondVertex == contrTrn.FirstVertex ||
                            _controlEdges[i].SecondVertex == contrTrn.SecondVertex ||
                            _controlEdges[i].SecondVertex == contrTrn.ThirdVertex)
                            _controlEdges[i].SecondVertex = Vector3.Transform(_controlEdges[i].SecondVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].IsSelected)
                            continue;

                        if (_controlTriangles[i].FirstVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].FirstVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].FirstVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].FirstVertex = Vector3.Transform(_controlTriangles[i].FirstVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                        if (_controlTriangles[i].SecondVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].SecondVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].SecondVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].SecondVertex = Vector3.Transform(_controlTriangles[i].SecondVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                        if (_controlTriangles[i].ThirdVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].ThirdVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].ThirdVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].ThirdVertex = Vector3.Transform(_controlTriangles[i].ThirdVertex - Engine.ActiveControlAxis.Position, qRotate) + Engine.ActiveControlAxis.Position;
                    }

                    contrTrn.Rotate(qRotate, Engine.ActiveControlAxis.Position);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }
        #endregion

        #region Scale
        public void Scale(Vector3 scale)
        {
            if (scale.Length() == 0) return;
            for (int i = 0; i < _controlTriangles.Count; i++)
            {
                _controlTriangles[i].Scale(scale, _center);
            }
            for (int i = 0; i < _controlEdges.Count; i++)
            {
                _controlEdges[i].Scale(scale, _center);
            }
            for (int i = 0; i < _controlVertices.Count; i++)
            {
                _controlVertices[i].Scale(scale, _center);
            }
            for (int i = 0; i < _vertexPositions.Length; i++)
            {
                _vertexPositions[i] += (_vertexPositions[i] - _center) * scale;
            }
            for (int i = 0; i < _vertexData.Length; i++)
            {
                _vertexData[i].Position += (_vertexData[i].Position - _center) * scale;
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void ScaleControlVertices(Vector3 scale)
        {
            foreach (var contrVertex in _controlVertices)
            {
                if (contrVertex.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrVertex.Position)
                            _vertexPositions[i] += (_vertexPositions[i] - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrVertex.Position)
                            _vertexData[i].Position += (_vertexData[i].Position - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].FirstVertex == contrVertex.Position)
                        {
                            _controlEdges[i].FirstVertex += (_controlEdges[i].FirstVertex - Engine.ActiveControlAxis.Position) * scale;
                            continue;
                        }
                        if (_controlEdges[i].SecondVertex == contrVertex.Position)
                            _controlEdges[i].SecondVertex += (_controlEdges[i].SecondVertex - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].FirstVertex == contrVertex.Position)
                        {
                            _controlTriangles[i].FirstVertex += (_controlTriangles[i].FirstVertex - Engine.ActiveControlAxis.Position) * scale;
                            continue;
                        }
                        if (_controlTriangles[i].SecondVertex == contrVertex.Position)
                        {
                            _controlTriangles[i].SecondVertex += (_controlTriangles[i].SecondVertex - Engine.ActiveControlAxis.Position) * scale;
                            continue;
                        }
                        if (_controlTriangles[i].ThirdVertex == contrVertex.Position)
                            _controlTriangles[i].ThirdVertex += (_controlTriangles[i].ThirdVertex - Engine.ActiveControlAxis.Position) * scale;
                    }

                    contrVertex.Scale(scale, Engine.ActiveControlAxis.Position);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void ScaleControlEdges(Vector3 scale)
        {
            foreach (var contrEdge in _controlEdges)
            {
                if (contrEdge.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrEdge.FirstVertex || _vertexPositions[i] == contrEdge.SecondVertex)
                            _vertexPositions[i] += (_vertexPositions[i] - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrEdge.FirstVertex || _vertexData[i].Position == contrEdge.SecondVertex)
                            _vertexData[i].Position += (_vertexData[i].Position - Engine.ActiveControlAxis.Position) * scale;
                    }

                    for (int i = 0; i < _controlVertices.Count; i++)
                    {
                        if (_controlVertices[i].Position == contrEdge.FirstVertex || _controlVertices[i].Position == contrEdge.SecondVertex)
                            _controlVertices[i].Position += (_controlVertices[i].Position - Engine.ActiveControlAxis.Position) * scale;

                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].FirstVertex == contrEdge.FirstVertex || _controlTriangles[i].FirstVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].FirstVertex += (_controlTriangles[i].FirstVertex - Engine.ActiveControlAxis.Position) * scale;
                        if (_controlTriangles[i].SecondVertex == contrEdge.FirstVertex || _controlTriangles[i].SecondVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].SecondVertex += (_controlTriangles[i].SecondVertex - Engine.ActiveControlAxis.Position) * scale;
                        if (_controlTriangles[i].ThirdVertex == contrEdge.FirstVertex || _controlTriangles[i].ThirdVertex == contrEdge.SecondVertex)
                            _controlTriangles[i].ThirdVertex += (_controlTriangles[i].ThirdVertex - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].IsSelected)
                            continue;

                        if (_controlEdges[i].FirstVertex == contrEdge.FirstVertex || _controlEdges[i].FirstVertex == contrEdge.SecondVertex)
                            _controlEdges[i].FirstVertex += (_controlEdges[i].FirstVertex - Engine.ActiveControlAxis.Position) * scale;
                        if (_controlEdges[i].SecondVertex == contrEdge.FirstVertex || _controlEdges[i].SecondVertex == contrEdge.SecondVertex)
                            _controlEdges[i].SecondVertex += (_controlEdges[i].SecondVertex - Engine.ActiveControlAxis.Position) * scale;
                    }

                    contrEdge.Scale(scale, Engine.ActiveControlAxis.Position);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void ScaleControlTriangles(Vector3 scale)
        {
            foreach (var contrTrn in _controlTriangles)
            {
                if (contrTrn.IsSelected)
                {
                    for (int i = 0; i < _vertexPositions.Length; i++)
                    {
                        if (_vertexPositions[i] == contrTrn.FirstVertex ||
                            _vertexPositions[i] == contrTrn.SecondVertex ||
                            _vertexPositions[i] == contrTrn.ThirdVertex)
                            _vertexPositions[i] += (_vertexPositions[i] - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _vertexData.Length; i++)
                    {
                        if (_vertexData[i].Position == contrTrn.FirstVertex ||
                            _vertexData[i].Position == contrTrn.SecondVertex ||
                            _vertexData[i].Position == contrTrn.ThirdVertex)
                            _vertexData[i].Position += (_vertexData[i].Position - Engine.ActiveControlAxis.Position) * scale;
                    }

                    for (int i = 0; i < _controlVertices.Count; i++)
                    {
                        if (_controlVertices[i].Position == contrTrn.FirstVertex ||
                            _controlVertices[i].Position == contrTrn.SecondVertex ||
                            _controlVertices[i].Position == contrTrn.ThirdVertex)
                            _controlVertices[i].Position += (_controlVertices[i].Position - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _controlEdges.Count; i++)
                    {
                        if (_controlEdges[i].FirstVertex == contrTrn.FirstVertex ||
                            _controlEdges[i].FirstVertex == contrTrn.SecondVertex ||
                            _controlEdges[i].FirstVertex == contrTrn.ThirdVertex)
                            _controlEdges[i].FirstVertex += (_controlEdges[i].FirstVertex - Engine.ActiveControlAxis.Position) * scale;
                        if (_controlEdges[i].SecondVertex == contrTrn.FirstVertex ||
                            _controlEdges[i].SecondVertex == contrTrn.SecondVertex ||
                            _controlEdges[i].SecondVertex == contrTrn.ThirdVertex)
                            _controlEdges[i].SecondVertex += (_controlEdges[i].SecondVertex - Engine.ActiveControlAxis.Position) * scale;
                    }
                    for (int i = 0; i < _controlTriangles.Count; i++)
                    {
                        if (_controlTriangles[i].IsSelected)
                            continue;

                        if (_controlTriangles[i].FirstVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].FirstVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].FirstVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].FirstVertex += (_controlTriangles[i].FirstVertex - Engine.ActiveControlAxis.Position) * scale;
                        if (_controlTriangles[i].SecondVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].SecondVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].SecondVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].SecondVertex += (_controlTriangles[i].SecondVertex - Engine.ActiveControlAxis.Position) * scale;
                        if (_controlTriangles[i].ThirdVertex == contrTrn.FirstVertex ||
                            _controlTriangles[i].ThirdVertex == contrTrn.SecondVertex ||
                            _controlTriangles[i].ThirdVertex == contrTrn.ThirdVertex)
                            _controlTriangles[i].ThirdVertex += (_controlTriangles[i].ThirdVertex - Engine.ActiveControlAxis.Position) * scale;
                    }

                    contrTrn.Scale(scale, Engine.ActiveControlAxis.Position);
                }
            }

            RecalcBoundingBox();
            RecalcNormals();
        }
        #endregion

        #region Deteling
        public void DeleteVertex()
        {
            List<Vector3> vPos = _vertexPositions.ToList();

            List<ControlTriangle> delTris = new List<ControlTriangle>();
            List<ControlVertex> delVerts = _controlVertices.Where(x => x.IsSelected).ToList();

            delVerts.ForEach(v =>
            {
                _controlTriangles.ForEach(ct =>
                {
                    if (ct.FirstVertex == v.Position || ct.SecondVertex == v.Position || ct.ThirdVertex == v.Position)
                        if (!delTris.Contains(ct))
                            delTris.Add(ct);
                });

                _controlTriangles.RemoveAll(ct =>
                    ct.FirstVertex == v.Position || ct.SecondVertex == v.Position || ct.ThirdVertex == v.Position);
                _controlEdges.RemoveAll(e => e.FirstVertex == v.Position || e.SecondVertex == v.Position);
                _controlVertices.Remove(v);
                vPos.RemoveAll(vp => vp == v.Position);

                Engine.TriangleSelectionPool.RemoveAll(ct =>
                    ct.FirstVertex == v.Position || ct.SecondVertex == v.Position || ct.ThirdVertex == v.Position);
                Engine.EdgeSelectionPool.RemoveAll(e => e.FirstVertex == v.Position || e.SecondVertex == v.Position);
                Engine.VertexSelectionPool.Remove(v);
            });

            _primitiveCount -= delTris.Count;
            _vertexPositions = vPos.ToArray();

            _vertexData = RemoveVertexData(delTris);

            RecalcBoundingBox();
        }

        public void DeleteEdge()
        {
            List<ControlTriangle> delTris = new List<ControlTriangle>();
            List<ControlEdge> delEdges = _controlEdges.Where(x => x.IsSelected).ToList();

            delEdges.ForEach(e =>
            {
                _controlTriangles.ForEach(ct =>
                {
                    if (ct.Contains(e) && !delTris.Contains(ct))
                        delTris.Add(ct);
                });

                _controlTriangles.RemoveAll(ct => ct.Contains(e));
                _controlEdges.Remove(e);

                Engine.TriangleSelectionPool.RemoveAll(ct => ct.Contains(e));
                Engine.EdgeSelectionPool.Remove(e);
            });

            _primitiveCount -= delTris.Count;

            _vertexData = RemoveVertexData(delTris);

            RecalcBoundingBox();
        }

        public void DeleteTriangle()
        {
            List<ControlTriangle> delTris = _controlTriangles.Where(x => x.IsSelected).ToList();

            delTris.ForEach(t =>
            {
                _controlTriangles.Remove(t);
                Engine.TriangleSelectionPool.Remove(t);
            });

            _primitiveCount -= delTris.Count;

            _vertexData = RemoveVertexData(delTris);

            RecalcBoundingBox();
        }
        #endregion

        public void RestoreData(SceneEntityData data)
        {
            _center = data.Center;
            _position = data.Position;

            if (data.VertData.Count == _controlVertices.Count)
                for (int i = 0; i < data.VertData.Count; i++)
                    _controlVertices[i].Position = data.VertData[i].Position;
            else
                Utils.SyncVertexLists(ref _controlVertices, data.VertData);

            if (data.EdgeData.Count == _controlEdges.Count)
                for (int i = 0; i < data.EdgeData.Count; i++)
                {
                    _controlEdges[i].FirstVertex = data.EdgeData[i].FirstVertex;
                    _controlEdges[i].SecondVertex = data.EdgeData[i].SecondVertex;
                }
            else
                Utils.SyncEdgeLists(ref _controlEdges, data.EdgeData);

            if (data.TrianData.Count == _controlTriangles.Count)
                for (int i = 0; i < data.TrianData.Count; i++)
                {
                    _controlTriangles[i].FirstVertex = data.TrianData[i].FirstVertex;
                    _controlTriangles[i].SecondVertex = data.TrianData[i].SecondVertex;
                    _controlTriangles[i].ThirdVertex = data.TrianData[i].ThirdVertex;
                }
            else
                Utils.SyncTriangleLists(ref _controlTriangles, data.TrianData);

            _primitiveCount = data.PrimitiveCount;
            _vertexData = data.VertexData;
            _vertexPositions = data.VertexPositions;

            RecalcBoundingBox();
            RecalcNormals();
        }

        public void Attach(SceneEntity entity)
        {
            _primitiveCount += entity.PrimitiveCount;

            var vertData = _vertexData.ToList();
            vertData.AddRange(entity.VertexData);
            _vertexData = vertData.ToArray();

            var posData = _vertexPositions.ToList();
            posData.AddRange(entity.VertexPositions);
            _vertexPositions = posData.ToArray();

            ControlVertices.AddRange(entity.ControlVertices);
            ControlEdges.AddRange(entity.ControlEdges);
            ControlTriangles.AddRange(entity.ControlTriangles);

            RecalcBoundingBox();
            RecalcCenter();
            Engine.ActiveControlAxis.Position = _center;
        }

        private void DrawConrtolVertices()
        {
            _controlVertices.ForEach(x => x.Draw());
        }

        private void DrawControlEdges()
        {
            _controlEdges.ForEach(x => x.Draw());
        }

        private void DrawControlTriangles()
        {
            _controlTriangles.ForEach(x => x.Draw());
        }

        private void DrawSelectionBox()
        {
            Engine.ActiveGraphicsDevice.DepthStencilState = DepthStencilState.None;

            _selectionBoxEffect.View = Engine.ActiveCamera.ViewMatrix;
            _selectionBoxEffect.Projection = Engine.ActiveCamera.ProjectionMatrix;
            _selectionBoxEffect.World = Matrix.Identity;

            _selectionBoxEffect.CurrentTechnique.Passes[0].Apply();
            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _selectionBoxVertices.ToArray(), 0,
                                         _selectionBoxVertices.Count / 2);

            Engine.ActiveGraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        private void RecalcBoundingBox()
        {
            _boundingBox = Utils.CalculateBoundingBox(_vertexPositions);

            RecreateSelectionBox();
        }

        private void RecreateSelectionBox()
        {
            Color lineColor = Color.White;
            const float lineLength = 5f;

            _selectionBoxVertices.Clear();

            Vector3[] boundingBoxCorners = _boundingBox.GetCorners();
            for (int i = 0; i < boundingBoxCorners.Length; i++)
            {
                boundingBoxCorners[i] += Vector3.Normalize(boundingBoxCorners[i] - Center) * 0.05f;
            }

            #region Create Corners
            // --- Corner 0 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[0], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[0] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[3].Y - boundingBoxCorners[0].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[0], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[0] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[4].Z - boundingBoxCorners[0].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[0], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[0] +
                                                              new Vector3(
                                                                (boundingBoxCorners[1].X - boundingBoxCorners[0].X) / lineLength,
                                                                0, 0), lineColor));


            // --- Corner 1 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[1], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[1] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[2].Y - boundingBoxCorners[1].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[1], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[1] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[5].Z - boundingBoxCorners[1].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[1], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[1] +
                                                              new Vector3(
                                                                (boundingBoxCorners[0].X - boundingBoxCorners[1].X) / lineLength,
                                                                0, 0), lineColor));


            // --- Corner 2 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[2], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[2] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[1].Y - boundingBoxCorners[2].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[2], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[2] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[6].Z - boundingBoxCorners[2].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[2], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[2] +
                                                              new Vector3(
                                                                (boundingBoxCorners[3].X - boundingBoxCorners[2].X) / lineLength,
                                                                0, 0), lineColor));


            // --- Corner 3 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[3], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[3] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[0].Y - boundingBoxCorners[3].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[3], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[3] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[7].Z - boundingBoxCorners[3].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[3], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[3] +
                                                              new Vector3(
                                                                (boundingBoxCorners[2].X - boundingBoxCorners[3].X) / lineLength,
                                                                0, 0), lineColor));


            // --- Corner 4 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[4], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[4] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[7].Y - boundingBoxCorners[4].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[4], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[4] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[0].Z - boundingBoxCorners[4].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[4], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[4] +
                                                              new Vector3(
                                                                (boundingBoxCorners[5].X - boundingBoxCorners[4].X) / lineLength,
                                                                0, 0), lineColor));


            // --- Corner 5 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[5], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[5] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[6].Y - boundingBoxCorners[5].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[5], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[5] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[1].Z - boundingBoxCorners[5].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[5], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[5] +
                                                              new Vector3(
                                                                (boundingBoxCorners[4].X - boundingBoxCorners[5].X) / lineLength,
                                                                0, 0), lineColor));

            // --- Corner 6 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[6], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[6] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[5].Y - boundingBoxCorners[6].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[6], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[6] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[2].Z - boundingBoxCorners[6].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[6], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[6] +
                                                              new Vector3(
                                                                (boundingBoxCorners[7].X - boundingBoxCorners[6].X) / lineLength,
                                                                0, 0), lineColor));


            // --- Corner 7 --- // 
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[7], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[7] +
                                                              new Vector3(0,
                                                                          (boundingBoxCorners[4].Y - boundingBoxCorners[7].Y) /
                                                                          lineLength, 0), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[7], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[7] +
                                                              new Vector3(0, 0,
                                                                          (boundingBoxCorners[3].Z - boundingBoxCorners[7].Z) /
                                                                          lineLength), lineColor));

            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[7], lineColor));
            _selectionBoxVertices.Add(new VertexPositionColor(boundingBoxCorners[7] +
                                                              new Vector3(
                                                                (boundingBoxCorners[6].X - boundingBoxCorners[7].X) / lineLength,
                                                                0, 0), lineColor));
            #endregion
        }

        private void RecalcCenter()
        {
            _center = Vector3.Zero;
            for (int i = 0; i < _vertexPositions.Length; i++)
            {
                _center += _vertexPositions[i] / _vertexPositions.Length;
            }
        }

        private void RecalcNormals()
        {
            for (int i = 0; i < _vertexData.Length; i += 3)
            {
                Vector3 normal = Vector3.Cross(_vertexData[i + 1].Position - _vertexData[i].Position, _vertexData[i + 2].Position - _vertexData[i].Position);
                normal.Normalize();
                _vertexData[i].Normal = normal;
                _vertexData[i + 1].Normal = normal;
                _vertexData[i + 2].Normal = normal;
            }
        }

        private VertexPositionNormalTexture[] RemoveVertexData(List<ControlTriangle> deletedTris)
        {
            List<VertexPositionNormalTexture> vData = _vertexData.ToList();
            List<int> inds = new List<int>();

            deletedTris.ForEach(dt =>
            {
                for (int i = 0; i < vData.Count; i += 3)
                {
                    if (vData[i].Position == dt.FirstVertex &&
                        vData[i + 1].Position == dt.SecondVertex &&
                        vData[i + 2].Position == dt.ThirdVertex)
                        inds.AddRange(new[] { i, i + 1, i + 2 });
                }
            });
            inds = inds.OrderByDescending(i => i).ToList();
            inds.ForEach(i => vData.RemoveAt(i));

            return vData.ToArray();
        }
    }
}
