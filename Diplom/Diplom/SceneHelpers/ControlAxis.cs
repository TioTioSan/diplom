using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace Diplom.SceneHelpers
{
    public class ControlAxis
    {
        private readonly BasicEffect _lineEffect;
        private readonly BasicEffect _meshEffect;
        private readonly SpriteFont _font;

        private Matrix _axisWorld = Matrix.Identity;

        private VertexPositionColor[] _lineVertices;
        private const float LINE_LENGTH = 3f;
        private const float LINE_OFFSET = 1f;
        private const float halfLineOffset = LINE_OFFSET / 2;

        private Quad[] _quads;
        private readonly BasicEffect _quadEffect;

        private Color[] _axisColors;
        private Color _highlightColor;

        private string[] _axisText;
        private Vector3 _axisTextOffset = new Vector3(0, 0.5f, 0);

        private Matrix[] _modelLocalSpace;

        private Vector3 _position = Vector3.Zero;
        private Matrix _rotationMatrix = Matrix.Identity;

        private Matrix _screenScaleMatrix;
        private float _screenScale;

        private bool _isTransforming = false;
        public bool IsTransforming { get { return _isTransforming; } }

        public Vector3 X_AxisDirection { get; private set; }
        public Vector3 Y_AxisDirection { get; private set; }
        public Vector3 Z_AxisDirection { get; private set; }

        #region BoundingBoxes
        private const float MULTI_AXIS_THICKNESS = 0.05f;
        private const float SINGLE_AXIS_THICKNESS = 0.2f;

        private static BoundingBox XAxisBox
        {
            get
            {
                return new BoundingBox(new Vector3(LINE_OFFSET, 0, 0),
                                       new Vector3(LINE_OFFSET + LINE_LENGTH, SINGLE_AXIS_THICKNESS, SINGLE_AXIS_THICKNESS));
            }
        }
        private static BoundingBox YAxisBox
        {
            get
            {
                return new BoundingBox(new Vector3(0, LINE_OFFSET, 0),
                                       new Vector3(SINGLE_AXIS_THICKNESS, LINE_OFFSET + LINE_LENGTH, SINGLE_AXIS_THICKNESS));
            }
        }
        private static BoundingBox ZAxisBox
        {
            get
            {
                return new BoundingBox(new Vector3(0, 0, LINE_OFFSET),
                                       new Vector3(SINGLE_AXIS_THICKNESS, SINGLE_AXIS_THICKNESS, LINE_OFFSET + LINE_LENGTH));
            }
        }
        private static BoundingBox ZXBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero,
                                       new Vector3(LINE_OFFSET, MULTI_AXIS_THICKNESS, LINE_OFFSET));
            }
        }
        private static BoundingBox XYBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero,
                                       new Vector3(LINE_OFFSET, LINE_OFFSET, MULTI_AXIS_THICKNESS));
            }
        }
        private static BoundingBox YZBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero,
                                       new Vector3(MULTI_AXIS_THICKNESS, LINE_OFFSET, LINE_OFFSET));
            }
        }
        #endregion

        #region BoundingSpheres
        private const float RADIUS = 1f;

        private BoundingSphere XSphere
        {
            get
            {
                return new BoundingSphere(Vector3.Transform(_lineVertices[1].Position, _axisWorld),
                                          RADIUS * _screenScale);
            }
        }
        private BoundingSphere YSphere
        {
            get
            {
                return new BoundingSphere(Vector3.Transform(_lineVertices[7].Position, _axisWorld),
                                          RADIUS * _screenScale);
            }
        }
        private BoundingSphere ZSphere
        {
            get
            {
                return new BoundingSphere(Vector3.Transform(_lineVertices[13].Position, _axisWorld),
                                          RADIUS * _screenScale);
            }
        }
        #endregion

        private IndexedVertexModel _axisGeomerty = HelpersGeometry.ControlAxisTranslateGeomerty;
        private TransformationMode _activeMode = TransformationMode.Translate;
        private Axis _activeAxis = Axis.None;

        public TransformationMode ActiveMode
        {
            get { return _activeMode; }
            set
            {
                _activeMode = value;
                switch (_activeMode)
                {
                    case TransformationMode.Translate:
                        _axisGeomerty = HelpersGeometry.ControlAxisTranslateGeomerty;
                        break;
                    case TransformationMode.Rotate:
                        _axisGeomerty = HelpersGeometry.ControlAxisRotateGeometry;
                        break;
                    case TransformationMode.Scale:
                        _axisGeomerty = HelpersGeometry.ControlAxisScaleGeomerty;
                        break;
                }
            }
        }
        public Axis ActiveAxis
        {
            get { return _activeAxis; }
            set
            {
                if (_activeAxis == value) return;
                _activeAxis = value;

                ApplyColor(Axis.X, _axisColors[0]);
                ApplyColor(Axis.Y, _axisColors[1]);
                ApplyColor(Axis.Z, _axisColors[2]);
                if (_activeAxis != Axis.None)
                    ApplyColor(_activeAxis, _highlightColor);
            }
        }

        public Matrix RotationMatrix { get { return _rotationMatrix; } }
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_position.X == float.MaxValue) return;

                if (Engine.ActiveTransformMode == TransformationMode.Translate)
                    Engine.MainForm.SetNumericUpDowns(_position);
            }
        }

        private bool _isEnabled = false;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                Engine.MainForm.IsEnabledBntLookAtSelection = value;
                if (!_isEnabled)
                    Position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            }
        }


        public ControlAxis()
        {
            _axisColors = new Color[3];
            _axisColors[0] = Color.Red;
            _axisColors[1] = Color.Green;
            _axisColors[2] = Color.Blue;
            _highlightColor = Color.Gold;

            // text projected in 3D
            _axisText = new string[3];
            _axisText[0] = "X";
            _axisText[1] = "Y";
            _axisText[2] = "Z";

            _font = Engine.ContentLoader.GetLoadedFont("DiplomFont");

            X_AxisDirection = Vector3.Right;
            Y_AxisDirection = Vector3.Up;
            Z_AxisDirection = Vector3.Forward;

            #region Line init
            _lineEffect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true, AmbientLightColor = Vector3.One, EmissiveColor = Vector3.One };

            var vertexList = new List<VertexPositionColor>(18);

            // helper to apply colors
            Color xColor = _axisColors[0];
            Color yColor = _axisColors[1];
            Color zColor = _axisColors[2];

            // -- X Axis -- // index 0 - 5
            vertexList.Add(new VertexPositionColor(new Vector3(halfLineOffset, 0, 0), xColor));
            vertexList.Add(new VertexPositionColor(new Vector3(LINE_LENGTH, 0, 0), xColor));

            vertexList.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, 0, 0), xColor));
            vertexList.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, LINE_OFFSET, 0), xColor));

            vertexList.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, 0, 0), xColor));
            vertexList.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, 0, LINE_OFFSET), xColor));

            // -- Y Axis -- // index 6 - 11
            vertexList.Add(new VertexPositionColor(new Vector3(0, halfLineOffset, 0), yColor));
            vertexList.Add(new VertexPositionColor(new Vector3(0, LINE_LENGTH, 0), yColor));

            vertexList.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET, 0), yColor));
            vertexList.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, LINE_OFFSET, 0), yColor));

            vertexList.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET, 0), yColor));
            vertexList.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET, LINE_OFFSET), yColor));

            // -- Z Axis -- // index 12 - 17
            vertexList.Add(new VertexPositionColor(new Vector3(0, 0, halfLineOffset), zColor));
            vertexList.Add(new VertexPositionColor(new Vector3(0, 0, LINE_LENGTH), zColor));

            vertexList.Add(new VertexPositionColor(new Vector3(0, 0, LINE_OFFSET), zColor));
            vertexList.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, 0, LINE_OFFSET), zColor));

            vertexList.Add(new VertexPositionColor(new Vector3(0, 0, LINE_OFFSET), zColor));
            vertexList.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET, LINE_OFFSET), zColor));

            // -- Convert to array -- //
            _lineVertices = vertexList.ToArray();
            #endregion

            #region Helpers geomerty init
            _meshEffect = new BasicEffect(Engine.ActiveGraphicsDevice);

            _modelLocalSpace = new Matrix[3];
            _modelLocalSpace[0] = Matrix.CreateWorld(new Vector3(LINE_LENGTH, 0, 0), Vector3.Left, Vector3.Up);
            _modelLocalSpace[1] = Matrix.CreateWorld(new Vector3(0, LINE_LENGTH, 0), Vector3.Down, Vector3.Left);
            _modelLocalSpace[2] = Matrix.CreateWorld(new Vector3(0, 0, LINE_LENGTH), Vector3.Forward, Vector3.Up);

            #endregion

            #region Quads init
            _quads = new Quad[3];
            _quads[0] = new Quad(new Vector3(halfLineOffset, halfLineOffset, 0), Vector3.Backward, Vector3.Up, LINE_OFFSET,
                                 LINE_OFFSET); //XY
            _quads[1] = new Quad(new Vector3(halfLineOffset, 0, halfLineOffset), Vector3.Up, Vector3.Right, LINE_OFFSET,
                                 LINE_OFFSET); //ZX
            _quads[2] = new Quad(new Vector3(0, halfLineOffset, halfLineOffset), Vector3.Right, Vector3.Up, LINE_OFFSET,
                                 LINE_OFFSET); //YZ 

            _quadEffect = new BasicEffect(Engine.ActiveGraphicsDevice) { DiffuseColor = _highlightColor.ToVector3(), Alpha = 0.5f };
            _quadEffect.EnableDefaultLighting();
            #endregion
        }

        #region Mouse events
        public void MouseDown()
        {
            if (!IsEnabled) return;

            if (ActiveAxis != Axis.None)
            {
                Engine.StartAction(ActionType.VertexData);
                _isTransforming = true;
            }
        }

        public void MouseUp()
        {
            if (!IsEnabled) return;

            if (_isTransforming)
            {
                Engine.EndAction();
                _isTransforming = false;
            }
        }

        public void MyMouseDrag(Vector2 pos, Vector2 delta, MouseButtons btn)
        {
            //if (!IsEnabled) return;
        }

        public void MyMouseMove(Vector2 mousePosition)
        {
            if (!IsEnabled || _isTransforming)
                return;
            SelectAxis(mousePosition);
        }
        #endregion

        public void Draw(Camera camera)
        {
            if (!IsEnabled) return;

            Vector3 vLength = camera.Position - _position;
            const float scaleFactor = 25;

            _screenScale = vLength.Length() / scaleFactor;
            _screenScaleMatrix = Matrix.CreateScale(new Vector3(_screenScale));

            _axisWorld = _screenScaleMatrix * Matrix.CreateWorld(_position, Vector3.Forward, Vector3.Up);

            Engine.ActiveGraphicsDevice.DepthStencilState = DepthStencilState.None;

            #region Draw lines
            _lineEffect.World = _axisWorld;
            _lineEffect.View = camera.ViewMatrix;
            _lineEffect.Projection = camera.ProjectionMatrix;

            _lineEffect.CurrentTechnique.Passes[0].Apply();
            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _lineVertices, 0, _lineVertices.Length / 2);
            #endregion

            #region Draw quad
            if (ActiveAxis == Axis.XY || ActiveAxis == Axis.YZ ||
                ActiveAxis == Axis.XYZ || ActiveAxis == Axis.ZX)
            {
                Engine.ActiveGraphicsDevice.BlendState = BlendState.AlphaBlend;
                Engine.ActiveGraphicsDevice.RasterizerState = RasterizerState.CullNone;

                _quadEffect.World = _axisWorld;
                _quadEffect.View = camera.ViewMatrix;
                _quadEffect.Projection = camera.ProjectionMatrix;

                _quadEffect.CurrentTechnique.Passes[0].Apply();

                switch (ActiveAxis)
                {
                    case Axis.XY:
                        Engine.ActiveGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                    _quads[0].Vertices, 0, 4,
                                                    _quads[0].Indexes, 0, 2);
                        break;
                    case Axis.ZX:
                        Engine.ActiveGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                    _quads[1].Vertices, 0, 4,
                                                    _quads[1].Indexes, 0, 2);
                        break;
                    case Axis.YZ:
                        Engine.ActiveGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                    _quads[2].Vertices, 0, 4,
                                                    _quads[2].Indexes, 0, 2);
                        break;
                    case Axis.XYZ:
                        for (int i = 0; i < _quads.Length; i++)
                            Engine.ActiveGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                                _quads[i].Vertices, 0, 4,
                                                                _quads[i].Indexes, 0, 2);
                        break;
                }

                Engine.ActiveGraphicsDevice.BlendState = BlendState.Opaque;
                Engine.ActiveGraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            }
            #endregion

            #region Draw helpers geometry
            for (int i = 0; i < 3; i++) //(order: x, y, z)
            {
                Vector3 color = _axisColors[i].ToVector3();

                _meshEffect.World = _modelLocalSpace[i] * _axisWorld;
                _meshEffect.View = camera.ViewMatrix;
                _meshEffect.Projection = camera.ProjectionMatrix;

                _meshEffect.DiffuseColor = color;
                _meshEffect.EmissiveColor = color;

                _meshEffect.CurrentTechnique.Passes[0].Apply();

                Engine.ActiveGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    _axisGeomerty.Vertices, 0, _axisGeomerty.Vertices.Length,
                    _axisGeomerty.Indices, 0, _axisGeomerty.Indices.Length / 3);
            }
            #endregion

            Engine.ActiveGraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Draw2D();
        }

        public void SubObjectModeChanged()
        {
            Vector3? newPos = null;
            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    if (Engine.EntitySelectionPool.Count != 0)
                        newPos = Utils.GetCenter(Engine.EntitySelectionPool);
                    break;
                case SubObjectMode.Vertex:
                    if (Engine.VertexSelectionPool.Count != 0)
                        newPos = Utils.GetCenter(Engine.VertexSelectionPool);
                    break;
                case SubObjectMode.Edge:
                    if (Engine.EdgeSelectionPool.Count != 0)
                        newPos = Utils.GetCenter(Engine.EdgeSelectionPool);
                    break;
                case SubObjectMode.Triangle:
                    if (Engine.TriangleSelectionPool.Count != 0)
                        newPos = Utils.GetCenter(Engine.TriangleSelectionPool);
                    break;
            }
            if (newPos.HasValue)
            {
                Position = newPos.Value;
                IsEnabled = true;
            }
            else
                IsEnabled = false;
        }

        public void Translate(Vector3 delta)
        {
            Position += delta;
        }

        private void SelectAxis(Vector2 mousePosition)
        {
            float? intersection;
            float closestintersection = float.MaxValue;
            Ray ray = Engine.CurrentMouseRay;

            if (ActiveMode == TransformationMode.Rotate || ActiveMode == TransformationMode.Scale)
            {
                #region BoundingSpheres
                intersection = XSphere.Intersects(ray);
                if (intersection.HasValue)
                    if (intersection.Value < closestintersection)
                    {
                        ActiveAxis = Axis.X;
                        closestintersection = intersection.Value;
                    }
                intersection = YSphere.Intersects(ray);
                if (intersection.HasValue)
                    if (intersection.Value < closestintersection)
                    {
                        ActiveAxis = Axis.Y;
                        closestintersection = intersection.Value;
                    }
                intersection = ZSphere.Intersects(ray);
                if (intersection.HasValue)
                    if (intersection.Value < closestintersection)
                    {
                        ActiveAxis = Axis.Z;
                        closestintersection = intersection.Value;
                    }
                #endregion
            }

            if (ActiveMode == TransformationMode.Translate || ActiveMode == TransformationMode.Scale)
            {
                // transform ray into local-space of the boundingboxes.
                ray.Direction = Vector3.TransformNormal(ray.Direction, Matrix.Invert(_axisWorld));
                ray.Position = Vector3.Transform(ray.Position, Matrix.Invert(_axisWorld));
            }

            #region X,Y,Z Boxes
            intersection = XAxisBox.Intersects(ray);
            if (intersection.HasValue)
                if (intersection.Value < closestintersection)
                {
                    ActiveAxis = Axis.X;
                    closestintersection = intersection.Value;
                }
            intersection = YAxisBox.Intersects(ray);
            if (intersection.HasValue)
            {
                if (intersection.Value < closestintersection)
                {
                    ActiveAxis = Axis.Y;
                    closestintersection = intersection.Value;
                }
            }
            intersection = ZAxisBox.Intersects(ray);
            if (intersection.HasValue)
            {
                if (intersection.Value < closestintersection)
                {
                    ActiveAxis = Axis.Z;
                    closestintersection = intersection.Value;
                }
            }
            #endregion

            if (ActiveMode == TransformationMode.Translate || ActiveMode == TransformationMode.Scale)
            {
                // if no axis was hit (x,y,z) set value to lowest possible to select the 'farthest' intersection for the XY,XZ,YZ boxes. 
                // This is done so you may still select multi-axis if you're looking at the gizmo from behind!
                if (closestintersection >= float.MaxValue)
                    closestintersection = float.MinValue;

                #region BoundingBoxes
                intersection = XYBox.Intersects(ray);
                if (intersection.HasValue)
                    if (intersection.Value > closestintersection)
                    {
                        ActiveAxis = Axis.XY;
                        closestintersection = intersection.Value;
                    }
                intersection = ZXBox.Intersects(ray);
                if (intersection.HasValue)
                    if (intersection.Value > closestintersection)
                    {
                        ActiveAxis = Axis.ZX;
                        closestintersection = intersection.Value;
                    }
                intersection = YZBox.Intersects(ray);
                if (intersection.HasValue)
                    if (intersection.Value > closestintersection)
                    {
                        ActiveAxis = Axis.YZ;
                        closestintersection = intersection.Value;
                    }
                #endregion
            }
            if (closestintersection >= float.MaxValue || closestintersection <= float.MinValue)
                ActiveAxis = Axis.None;
        }

        private void Draw2D()
        {
            Engine.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // -- Draw Axis identifiers ("X,Y,Z") -- // 
            for (int i = 0; i < 3; i++)
            {
                Vector3 screenPos =
                  Engine.ActiveGraphicsDevice.Viewport.Project(_modelLocalSpace[i].Translation + _modelLocalSpace[i].Backward + _axisTextOffset,
                                             Engine.ActiveCamera.ProjectionMatrix, Engine.ActiveCamera.ViewMatrix, _axisWorld);

                if (screenPos.Z < 0f || screenPos.Z > 1.0f)
                    continue;

                Color color = Color.Black;
                switch (i)
                {
                    case 0:
                        if (ActiveAxis == Axis.X || ActiveAxis == Axis.XY || ActiveAxis == Axis.ZX)
                            color = _highlightColor;
                        break;
                    case 1:
                        if (ActiveAxis == Axis.Y || ActiveAxis == Axis.XY || ActiveAxis == Axis.YZ)
                            color = _highlightColor;
                        break;
                    case 2:
                        if (ActiveAxis == Axis.Z || ActiveAxis == Axis.YZ || ActiveAxis == Axis.ZX)
                            color = _highlightColor;
                        break;
                }

                Engine.SpriteBatch.DrawString(_font, _axisText[i], new Vector2(screenPos.X, screenPos.Y), color);
            }
            Engine.SpriteBatch.End();
        }

        #region Helpers
        private void ApplyColor(Axis axis, Color color)
        {
            switch (ActiveMode)
            {
                case TransformationMode.Scale:
                case TransformationMode.Translate:
                    switch (axis)
                    {
                        case Axis.X:
                            ApplyLineColor(0, 6, color);
                            break;
                        case Axis.Y:
                            ApplyLineColor(6, 6, color);
                            break;
                        case Axis.Z:
                            ApplyLineColor(12, 6, color);
                            break;
                        case Axis.XY:
                            ApplyLineColor(0, 4, color);
                            ApplyLineColor(6, 4, color);
                            break;
                        case Axis.YZ:
                            ApplyLineColor(6, 2, color);
                            ApplyLineColor(12, 2, color);
                            ApplyLineColor(10, 2, color);
                            ApplyLineColor(16, 2, color);
                            break;
                        case Axis.ZX:
                            ApplyLineColor(0, 2, color);
                            ApplyLineColor(4, 2, color);
                            ApplyLineColor(12, 4, color);
                            break;
                    }
                    break;
                case TransformationMode.Rotate:
                    switch (axis)
                    {
                        case Axis.X:
                            ApplyLineColor(0, 6, color);
                            break;
                        case Axis.Y:
                            ApplyLineColor(6, 6, color);
                            break;
                        case Axis.Z:
                            ApplyLineColor(12, 6, color);
                            break;
                    }
                    break;
                //case TransformationMode.UniformScale:
                //    ApplyLineColor(0, _lineVertices.Length,
                //                   ActiveAxis == Axis.None ? _axisColors[0] : _highlightColor);
                //    break;
            }
        }

        private void ApplyLineColor(int startindex, int count, Color color)
        {
            for (int i = startindex; i < (startindex + count); i++)
            {
                _lineVertices[i].Color = color;
            }
        }
        #endregion

        #region Private quad struct
        private struct Quad
        {
            public Vector3 Origin;
            public Vector3 UpperLeft;
            public Vector3 LowerLeft;
            public Vector3 UpperRight;
            public Vector3 LowerRight;
            public Vector3 Normal;
            public Vector3 Up;
            public Vector3 Left;

            public VertexPositionNormalTexture[] Vertices;
            public short[] Indexes;

            public Quad(Vector3 origin, Vector3 normal, Vector3 up, float width, float height)
            {
                Vertices = new VertexPositionNormalTexture[4];
                Indexes = new short[6];
                Origin = origin;
                Normal = normal;
                Up = up;

                // Calculate the quad corners
                Left = Vector3.Cross(normal, Up);
                Vector3 uppercenter = (Up * height / 2) + origin;
                UpperLeft = uppercenter + (Left * width / 2);
                UpperRight = uppercenter - (Left * width / 2);
                LowerLeft = UpperLeft - (Up * height);
                LowerRight = UpperRight - (Up * height);

                FillVertices();
            }

            private void FillVertices()
            {
                // Fill in texture coordinates to display full texture
                // on quad
                Vector2 textureUpperLeft = new Vector2(0.0f, 0.0f);
                Vector2 textureUpperRight = new Vector2(1.0f, 0.0f);
                Vector2 textureLowerLeft = new Vector2(0.0f, 1.0f);
                Vector2 textureLowerRight = new Vector2(1.0f, 1.0f);

                // Provide a normal for each vertex
                for (int i = 0; i < Vertices.Length; i++)
                {
                    Vertices[i].Normal = Normal;
                }

                // Set the position and texture coordinate for each
                // vertex
                Vertices[0].Position = LowerLeft;
                Vertices[0].TextureCoordinate = textureLowerLeft;
                Vertices[1].Position = UpperLeft;
                Vertices[1].TextureCoordinate = textureUpperLeft;
                Vertices[2].Position = LowerRight;
                Vertices[2].TextureCoordinate = textureLowerRight;
                Vertices[3].Position = UpperRight;
                Vertices[3].TextureCoordinate = textureUpperRight;

                // Set the index buffer for each vertex, using
                // clockwise winding
                Indexes[0] = 0;
                Indexes[1] = 1;
                Indexes[2] = 2;
                Indexes[3] = 2;
                Indexes[4] = 1;
                Indexes[5] = 3;
            }
        }
        #endregion
    }
}
