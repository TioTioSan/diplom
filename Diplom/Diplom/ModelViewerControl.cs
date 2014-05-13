using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.SceneHelpers;
using Diplom.Primitives;

namespace Diplom
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, and displays
    /// a spinning 3D model. The main form class is responsible for loading
    /// the model: this control just displays it.
    /// </summary>
    class ModelViewerControl : GraphicsDeviceControl
    {
        private SnapGrid _snapGrid;
        private ControlAxis _controlAxis;
        private Camera _camera;
        private SpriteBatch _spriteBatch;

        private int _mouseLeftDownX, _mouseLeftDownY;
        private static Rectangle _selectionRect = new Rectangle(-1, -1, 0, 0);
        private static Texture2D _dottedLine;

        protected override void Initialize()
        {
            _camera = new Camera(GraphicsDevice.Viewport.AspectRatio, new Vector3(50f, 50f, 50f), Vector3.Zero);
            _snapGrid = new SnapGrid(GraphicsDevice, 8);
            _controlAxis = new ControlAxis(GraphicsDevice, Engine.ActiveCamera);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            const float scale = 0.13f;
            Cube cube = new Cube();
            for (int i = 0; i < cube.VertexData.Length; i++)
            {
                Engine.VerticesOfControlVertex[i] = new VertexPositionColor(cube.VertexData[i].Position * scale, Color.White);
            }

            Engine.ActiveControlAxis = _controlAxis;
            Engine.ActiveCamera = _camera;
            Engine.ActiveGraphicsDevice = GraphicsDevice;
            Engine.SpriteBatch = _spriteBatch;
            Engine.ContentLoader = new ContentLoader(Services);

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        public void Load()
        {
            _dottedLine = Engine.ContentLoader.GetLoadedTexture("DottedLine");
        }

        protected override void Draw()
        {
            // Clear to the default control background color.
            GraphicsDevice.Clear(new Color(BackColor.R, BackColor.G, BackColor.B));

            if (GraphicsDevice.Viewport.AspectRatio != Engine.ActiveCamera.AspectRatio)
                Engine.ActiveCamera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;

            _snapGrid.Draw(Engine.ActiveCamera);
            Engine.Draw(Engine.ActiveCamera);
            _controlAxis.Draw(Engine.ActiveCamera);

            if (_selectionRect.X != -1)
                DrawSelectionRect();
        }

        public void MyLeftMouseDown(int x, int y)
        {
            _mouseLeftDownX = x;
            _mouseLeftDownY = y;

            _selectionRect.X = x;
            _selectionRect.Y = y;

            _controlAxis.MouseDown(new Vector2((float)x, (float)y));
        }

        public void MyLeftMouseUp(int x, int y)
        {
            int deltaX = _mouseLeftDownX - x;
            int deltaY = _mouseLeftDownY - y;

            if (_selectionRect.X != -1 && !Engine.ActiveControlAxis.IsTransforming && deltaX != 0 && deltaY != 0)
            {
                switch (Engine.ActiveSubObjectMode)
                {
                    case SubObjectMode.None:
                        Selector.SelectEntityByRectangle(_selectionRect);
                        break;
                    case SubObjectMode.Vertex:
                        Selector.SelectControlVertexByRectangle(_selectionRect);
                        break;
                    case SubObjectMode.Edge:
                        Selector.SelectControlEdgeByRectangle(_selectionRect);
                        break;
                    case SubObjectMode.Triangle:
                        break;
                    default:
                        break;
                }
            }

            _selectionRect.X = -1;
            _selectionRect.Y = -1;
            _selectionRect.Width = 0;
            _selectionRect.Height = 0;

            if (Engine.ActiveControlAxis.IsTransforming)
                Transformer.ResetDeltas();

            Engine.ActiveControlAxis.MouseUp(new Vector2((float)x, (float)y));

            if (deltaX == 0 && deltaY == 0)
                MyMouseClick(_mouseLeftDownX, _mouseLeftDownY);
        }

        public void MyMouseMove(int mouseX, int mouseY)
        {
            Engine.ActiveControlAxis.MyMouseMove(new Vector2((float)mouseX, (float)mouseY));
        }

        public void MyMouseDrag(int posX, int posY, int deltaX, int deltaY, MouseButtons btn)
        {
            switch (btn)
            {
                case MouseButtons.Middle:
                    Engine.ActiveCamera.RotateAroundTarget(deltaX, deltaY);
                    break;

                case MouseButtons.Left:
                    if (Engine.ActiveControlAxis.IsTransforming)
                    {
                        switch (Engine.ActiveTransformMode)
                        {
                            case TransformationMode.Translate:
                                Transformer.Translate();
                                break;
                            case TransformationMode.Rotate:
                                break;
                            case TransformationMode.Scale:
                                break;
                        }
                    }
                    else
                    {
                        _selectionRect.Width = posX - _mouseLeftDownX;
                        _selectionRect.Height = posY - _mouseLeftDownY;
                    }
                    break;
            }

            Engine.ActiveControlAxis.MyMouseDrag(new Vector2((float)posX, (float)posY), new Vector2((float)deltaX, (float)deltaY), btn);
        }

        public void MyMouseWheel(int delta)
        {
            Engine.ActiveCamera.MoveToTarget(delta);
        }

        private void MyMouseClick(int x, int y)
        {
            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    Selector.PickSceneEntity();
                    break;
                case SubObjectMode.Vertex:
                    Selector.PickControlVertex();
                    break;
                case SubObjectMode.Edge:
                    Selector.PickControlEdge();
                    break;
                case SubObjectMode.Triangle:
                    break;
                default:
                    break;
            }
        }

        private void DrawSelectionRect()
        {
            Engine.SpriteBatch.Begin();

            DrawHorizontalLine(_selectionRect.Y);
            DrawVerticalLine(_selectionRect.X);
            DrawHorizontalLine(_selectionRect.Y + _selectionRect.Height);
            DrawVerticalLine(_selectionRect.X + _selectionRect.Width);


            Engine.SpriteBatch.End();
        }

        private void DrawHorizontalLine(int y)
        {
            if (_selectionRect.Width > 0)
            {
                //Draw the line starting at the starting location and moving to the right
                for (int aCounter = -1; aCounter <= _selectionRect.Width; aCounter += 2)
                {
                    if (_selectionRect.Width - aCounter >= 0)
                    {
                        Engine.SpriteBatch.Draw(_dottedLine, new Rectangle(_selectionRect.X + aCounter, y, 2, 1),
                            new Rectangle(0, 0, 2, 1), Color.White, MathHelper.ToRadians(0), new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
            }
            else if (_selectionRect.Width < 0)
            {
                //Draw the line starting at the starting location and moving to the left
                for (int aCounter = 0; aCounter >= _selectionRect.Width; aCounter -= 2)
                {
                    if (_selectionRect.Width - aCounter <= 0)
                    {
                        Engine.SpriteBatch.Draw(_dottedLine, new Rectangle(_selectionRect.X + aCounter, y + 1, 2, 1),
                            new Rectangle(0, 0, 2, 1), Color.White, MathHelper.ToRadians(180), new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
            }
        }
        private void DrawVerticalLine(int x)
        {
            if (_selectionRect.Height > 0)
            {
                //Draw the line starting at the starting location and moving to the right
                for (int aCounter = 0; aCounter <= _selectionRect.Height; aCounter += 2)
                {
                    if (_selectionRect.Height - aCounter >= 0)
                    {
                        Engine.SpriteBatch.Draw(_dottedLine, new Rectangle(x, _selectionRect.Y + aCounter, 2, 1),
                            new Rectangle(0, 0, 2, 1), Color.White, MathHelper.ToRadians(90), new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
            }
            else if (_selectionRect.Height < 0)
            {
                //Draw the line starting at the starting location and moving to the left
                for (int aCounter = 1; aCounter >= _selectionRect.Height; aCounter -= 2)
                {
                    if (_selectionRect.Height - aCounter <= 0)
                    {
                        Engine.SpriteBatch.Draw(_dottedLine, new Rectangle(x - 1, _selectionRect.Y + aCounter, 2, 1),
                            new Rectangle(0, 0, 2, 1), Color.White, MathHelper.ToRadians(270), new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}
