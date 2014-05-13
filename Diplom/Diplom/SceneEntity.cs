using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diplom.Intefaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.Primitives;
using Diplom.SceneHelpers;

namespace Diplom
{
    public class SceneEntity
    {
        #region Fields & Properties
        const float LENGTH = 5f;

        private Model _model;
        private Matrix _world;
        private Effect _effect;
        private BasicEffect _basicEffect;
        private DynamicVertexBuffer _dynamicVB;
        private GraphicsDevice _graphicsDevice;

        private BasicEffect _selectionBoxEffect;
        private List<VertexPositionColor> _selectionBoxVertices = new List<VertexPositionColor>();

        public string Name { get; set; }

        private Vector3 _position = Vector3.Zero;
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private Vector3 _scale = Vector3.One;
        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        private Vector3 _forward = Vector3.Forward;
        public Vector3 Forward
        {
            get { return _forward; }
            set
            {
                _forward = value;
                _forward.Normalize();
            }
        }

        private Vector3 _up = Vector3.Up;
        public Vector3 Up
        {
            get { return _up; }
            set
            {
                _up = value;
                _up.Normalize();
            }
        }

        private BoundingBox _boundingBox;
        public BoundingBox BoundingBox
        {
            get { return _boundingBox; }
            //get { return new BoundingBox(Position - (Vector3.One * LENGTH) * Scale, Position + (Vector3.One * LENGTH) * Scale); }
        }

        private Vector3 _center;
        public Vector3 Center { get { return _center; } }

        private Vector3[] _vertexPositions;

        private VertexPositionNormalTexture[] _vertexData;
        private PrimitiveType _primitiveType;
        private int _primitiveCount;

        private List<ControlVertex> _controlVertices;
        public List<ControlVertex> ControlVertices { get { return _controlVertices; } }

        private List<ControlEdge> _controlEdges;
        public List<ControlEdge> ControlEdges { get { return _controlEdges; } }
        #endregion

        public SceneEntity(Model model, Effect effect, GraphicsDevice graphicsDevice)
        {
            _model = model;
            _effect = effect;
            _graphicsDevice = graphicsDevice;

            //Vector3[] vert = new Vector3[_model.Meshes[0].MeshParts[0].VertexBuffer.VertexCount/3];
            //_model.Meshes[0].MeshParts[0].VertexBuffer.GetData<Vector3>(vert);

            //_dynamicVB = new DynamicVertexBuffer(_graphicsDevice, typeof(Vector3), vert.Length, BufferUsage.WriteOnly);
            //_dynamicVB.SetData<Vector3>(vert);
        }
        //public SceneEntity(Primitive primitive, Effect effect, GraphicsDevice graphicsDevice)
        //{
        //    _effect = effect;
        //    _graphicsDevice = graphicsDevice;
        //    _vertexData = primitive.VertexData;
        //    _vertexPositions = primitive.VertexPositions;
        //    _primitiveType = primitive.PrimitiveType;
        //    _primitiveCount = primitive.PrimitiveCount;
        //}
        public SceneEntity(PrimitiveBase primitive, GraphicsDevice graphicsDevice)
        {
            _basicEffect = new BasicEffect(graphicsDevice);
            _basicEffect.EnableDefaultLighting();

            _selectionBoxEffect = new BasicEffect(graphicsDevice) { VertexColorEnabled = true };

            _graphicsDevice = graphicsDevice;
            _vertexData = primitive.VertexData;
            _vertexPositions = primitive.VertexPositions;
            _primitiveType = primitive.PrimitiveType;
            _primitiveCount = primitive.PrimitiveCount;

            _controlVertices = new List<ControlVertex>();
            foreach (Vector3 vert in _vertexPositions)
            {
                _controlVertices.Add(new ControlVertex(vert));
            }

            _controlEdges = new List<ControlEdge>(); primitive.EdgeVertexIndexes.ToList();
            foreach (Tuple<int, int> tuple in primitive.EdgeVertexIndexes)
            {
                _controlEdges.Add(new ControlEdge(_vertexPositions[tuple.Item1], _vertexPositions[tuple.Item2]));
            }

            RecalcCenter();
            RecalcBoundingBox();
        }

        public void Update()
        {
            _world = Matrix.CreateScale(_scale) * Matrix.CreateWorld(_position, _forward, _up);
        }

        public float? Select(Ray selectionRay)
        {
            return selectionRay.Intersects(_boundingBox);
        }

        public void Draw(Camera camera)
        {
            _graphicsDevice.DepthStencilState = DepthStencilState.Default;
            _graphicsDevice.RasterizerState = RasterizerState.CullNone;

            if (_effect == null)
            {
                _basicEffect.World = camera.WorldMatrix;
                _basicEffect.View = camera.ViewMatrix;
                _basicEffect.Projection = camera.ProjectionMatrix;

                _basicEffect.DiffuseColor = Color.White.ToVector3();

                _basicEffect.CurrentTechnique.Passes[0].Apply();
            }
            else
            {

            }

            _graphicsDevice.DrawUserPrimitives(_primitiveType, _vertexData, 0, _primitiveCount);

            if (Engine.EntitySelectionPool.Contains(this))
            {
                DrawSelectionBox();
                switch (Engine.ActiveSubObjectMode)
                {
                    case SubObjectMode.None:
                        break;
                    case SubObjectMode.Vertex:
                        DrawConrtolVertices();
                        break;
                    case SubObjectMode.Edge:
                        DrawEdges();
                        break;
                    case SubObjectMode.Triangle:
                        break;
                    default:
                        break;
                }
            }
        }

        public void Translate(Vector3 delta)
        {
            _position += delta;
            _center += delta;
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
                    contrVertex.Position += delta;
                }
            }

            RecalcBoundingBox();
        }

        private void DrawConrtolVertices()
        {
            _controlVertices.ForEach(x => x.Draw());
        }

        private void DrawEdges()
        {
            _controlEdges.ForEach(x => x.Draw());
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
    }
}
