using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.SceneHelpers;
using Diplom.Primitives;

namespace Diplom
{
    class ModelViewerControl : GraphicsDeviceControl
    {
        private SnapGrid _snapGrid;
        private ControlAxis _controlAxis;
        private Camera _camera;
        private SpriteBatch _spriteBatch;
        private Color _backGroundColor = new Color(255, 255, 255);

        private int _mouseLeftDownX, _mouseLeftDownY;
        private static Rectangle _selectionRect = new Rectangle(-1, -1, 0, 0);
        private static Texture2D _dottedLine;

        protected override void Initialize()
        {
            Engine.ContentLoader = new ContentLoader(Services);
            Engine.ActiveGraphicsDevice = GraphicsDevice;

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        public void Load()
        {
            _dottedLine = Engine.ContentLoader.GetLoadedTexture("DottedLine");

            _camera = new Camera(GraphicsDevice.Viewport.AspectRatio, new Vector3(6f, 6f, 6f), Vector3.Zero);
            _snapGrid = new SnapGrid(GraphicsDevice, 10);
            _controlAxis = new ControlAxis();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            const float scale = 0.13f;
            Cube cube = new Cube();
            for (int i = 0; i < cube.VertexData.Length; i++)
            {
                Utils.VerticesOfControlVertex[i] = new VertexPositionColor(cube.VertexData[i].Position * scale, Color.White);
            }

            Engine.ActiveControlAxis = _controlAxis;
            Engine.ActiveCamera = _camera;
            Engine.SpriteBatch = _spriteBatch;
        }

        protected override void Draw()
        {
            // Clear to the default control background color.
            GraphicsDevice.Clear(_backGroundColor);

            if (GraphicsDevice.Viewport.AspectRatio != Engine.ActiveCamera.AspectRatio)
                Engine.ActiveCamera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;

            _snapGrid.Draw(Engine.ActiveCamera);
            Engine.Draw(Engine.ActiveCamera);
            _controlAxis.Draw(Engine.ActiveCamera);

            if (_selectionRect.X != -1)
                DrawSelectionRect();
        }

        public void AddNewSceneEntity(PrimitiveBase prim)
        {
            if (Engine.StartSceneState == null)
            {
                Engine.StartAction(ActionType.EntityCount);
                Engine.SceneEntities.Add(new SceneEntity(prim));
                Engine.EndAction();
                Engine.LastAddedPrimitive = prim;
            }
        }

        public void MyLeftMouseDown(int x, int y)
        {
            _mouseLeftDownX = x;
            _mouseLeftDownY = y;

            _selectionRect.X = x;
            _selectionRect.Y = y;

            _controlAxis.MouseDown();
        }

        public void MyLeftMouseUp(int x, int y)
        {
            int deltaX = _mouseLeftDownX - x;
            int deltaY = _mouseLeftDownY - y;

            if (_selectionRect.X != -1 && !Engine.ActiveControlAxis.IsTransforming && deltaX != 0 && deltaY != 0)
            {
                Engine.StartAction(ActionType.Selection);
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
                        Selector.SelectControlTriangleByRectangle(_selectionRect);
                        break;
                }
                Engine.EndAction();
            }

            _selectionRect.X = -1;
            _selectionRect.Y = -1;
            _selectionRect.Width = 0;
            _selectionRect.Height = 0;

            if (Engine.ActiveControlAxis.IsTransforming)
                Transformer.ResetDeltas();

            Engine.ActiveControlAxis.MouseUp();

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
                    if (Control.ModifierKeys == Keys.Control)
                        Engine.ActiveCamera.Move(deltaX, deltaY);
                    else
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
                                Transformer.Rotate();
                                break;
                            case TransformationMode.Scale:
                                Transformer.Scale();
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

        public void MyKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Z)
            {
                Engine.UndoAction();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Y)
            {
                Engine.RedoAction();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Q)
            {
                if (Engine.ActiveControlAxis.IsEnabled)
                    Engine.ActiveCamera.LookAtSelection();
            }
        }


        private void MyMouseClick(int x, int y)
        {
            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    if (!Engine.IsInAttachMode)
                    {
                        Engine.StartAction(ActionType.Selection);
                        Selector.PickSceneEntity();
                        Engine.EndAction();
                    }
                    else
                        Selector.PickSceneEntity();
                    break;
                case SubObjectMode.Vertex:
                    Engine.StartAction(ActionType.Selection);
                    Selector.PickControlVertex();
                    Engine.EndAction();
                    break;
                case SubObjectMode.Edge:
                    Engine.StartAction(ActionType.Selection);
                    Selector.PickControlEdge();
                    Engine.EndAction();
                    break;
                case SubObjectMode.Triangle:
                    Engine.StartAction(ActionType.Selection);
                    Selector.PickControlTriangle();
                    Engine.EndAction();
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
