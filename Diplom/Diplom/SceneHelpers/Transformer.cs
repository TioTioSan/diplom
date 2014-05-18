using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diplom.SceneHelpers
{
    public static class Transformer
    {
        private static Vector3 _tDelta;
        private static Vector3 _lastIntersectionPosition;
        private static Vector3 _intersectPosition;

        public static void ResetDeltas()
        {
            _tDelta = Vector3.Zero;
            _lastIntersectionPosition = Vector3.Zero;
            _intersectPosition = Vector3.Zero;
        }

        public static void Translate()
        {
            Vector3 delta = Vector3.Zero;
            Ray curRay = Engine.CurrentMouseRay;

            Matrix transform = Matrix.Invert(Engine.ActiveControlAxis.RotationMatrix);
            curRay.Position = Vector3.Transform(curRay.Position, transform);
            curRay.Direction = Vector3.TransformNormal(curRay.Direction, transform);

            _lastIntersectionPosition = _intersectPosition;

            switch (Engine.ActiveControlAxis.ActiveAxis)
            {
                case Axis.XY:
                case Axis.X:
                    {
                        Plane plane = new Plane(Vector3.Forward, Vector3.Transform(Engine.ActiveControlAxis.Position, Matrix.Invert(Engine.ActiveControlAxis.RotationMatrix)).Z);

                        float? intersection = curRay.Intersects(plane);
                        if (intersection.HasValue)
                        {
                            _intersectPosition = (curRay.Position + (curRay.Direction * intersection.Value));
                            if (_lastIntersectionPosition != Vector3.Zero)
                            {
                                _tDelta = _intersectPosition - _lastIntersectionPosition;
                            }
                            delta = Engine.ActiveControlAxis.ActiveAxis == Axis.X
                                      ? new Vector3(_tDelta.X, 0, 0)
                                      : new Vector3(_tDelta.X, _tDelta.Y, 0);
                        }
                    }
                    break;
                case Axis.Z:
                case Axis.YZ:
                case Axis.Y:
                    {
                        Plane plane = new Plane(Vector3.Left, Vector3.Transform(Engine.ActiveControlAxis.Position, Matrix.Invert(Engine.ActiveControlAxis.RotationMatrix)).X);

                        float? intersection = curRay.Intersects(plane);
                        if (intersection.HasValue)
                        {
                            _intersectPosition = (curRay.Position + (curRay.Direction * intersection.Value));
                            if (_lastIntersectionPosition != Vector3.Zero)
                            {
                                _tDelta = _intersectPosition - _lastIntersectionPosition;
                            }
                            switch (Engine.ActiveControlAxis.ActiveAxis)
                            {
                                case Axis.Y:
                                    delta = new Vector3(0, _tDelta.Y, 0);
                                    break;
                                case Axis.Z:
                                    delta = new Vector3(0, 0, _tDelta.Z);
                                    break;
                                default:
                                    delta = new Vector3(0, _tDelta.Y, _tDelta.Z);
                                    break;
                            }
                        }
                    }
                    break;
                case Axis.ZX:
                    {
                        Plane plane = new Plane(Vector3.Down, Vector3.Transform(Engine.ActiveControlAxis.Position, Matrix.Invert(Engine.ActiveControlAxis.RotationMatrix)).Y);

                        float? intersection = curRay.Intersects(plane);
                        if (intersection.HasValue)
                        {
                            _intersectPosition = (curRay.Position + (curRay.Direction * intersection.Value));
                            if (_lastIntersectionPosition != Vector3.Zero)
                            {
                                _tDelta = _intersectPosition - _lastIntersectionPosition;
                            }
                        }
                        delta = new Vector3(_tDelta.X, 0, _tDelta.Z);
                    }
                    break;
            }

            Engine.ActiveControlAxis.Translate(delta);

            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.Translate(delta);
                    }
                    break;
                case SubObjectMode.Vertex:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.TranslateControlVertices(delta);
                    }
                    break;
                case SubObjectMode.Edge:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.TranslateControlEdges(delta);
                    }
                    break;
                case SubObjectMode.Triangle:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.TranslateControlTriangles(delta);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void Rotate()
        {
            float delta = Engine.CurrentMousePos.X - Engine.PreviousMousePos.X;
            delta *= 0.04f;
            Quaternion qRotate = new Quaternion();

            switch (Engine.ActiveControlAxis.ActiveAxis)
            {
                case Axis.X:
                    qRotate = Quaternion.CreateFromAxisAngle(Engine.ActiveControlAxis.X_AxisDirection, delta);
                    break;
                case Axis.Y:
                    qRotate = Quaternion.CreateFromAxisAngle(Engine.ActiveControlAxis.Y_AxisDirection, delta);
                    break;
                case Axis.Z:
                    qRotate = Quaternion.CreateFromAxisAngle(Engine.ActiveControlAxis.Z_AxisDirection, delta);
                    break;
                default:
                    break;
            }

            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.Rotate(qRotate);
                    }
                    break;
                case SubObjectMode.Vertex:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.RotateControlVertices(qRotate);
                    }
                    break;
                case SubObjectMode.Edge:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.RotateControlEdges(qRotate);
                    }
                    break;
                case SubObjectMode.Triangle:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.RotateControlTriangles(qRotate);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void Scale()
        {
            float delta = Engine.CurrentMousePos.X - Engine.PreviousMousePos.X;
            delta *= 0.04f;
            Vector3 scale = Vector3.Zero;

            switch (Engine.ActiveControlAxis.ActiveAxis)
            {
                case Axis.X:
                    scale.X += delta;
                    break;
                case Axis.Y:
                    scale.Y += delta;
                    break;
                case Axis.Z:
                    scale.Z += delta;
                    break;
                case Axis.XY:
                    scale.X += delta;
                    scale.Y += delta;
                    break;
                case Axis.ZX:
                    scale.Z += delta;
                    scale.X += delta;
                    break;
                case Axis.YZ:
                    scale.Y += delta;
                    scale.Z += delta;
                    break;
                default:
                    break;
            }

            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.Scale(scale);
                    }
                    break;
                case SubObjectMode.Vertex:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.ScaleControlVertices(scale);
                    }
                    break;
                case SubObjectMode.Edge:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.ScaleControlEdges(scale);
                    }
                    break;
                case SubObjectMode.Triangle:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.ScaleControlTriangles(scale);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void TranslateByValue(Vector3 value)
        {
            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.Translate(value - Engine.ActiveControlAxis.Position);
                    }
                    break;
                case SubObjectMode.Vertex:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.TranslateControlVertices(value - Engine.ActiveControlAxis.Position);
                    }
                    break;
                case SubObjectMode.Edge:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.TranslateControlEdges(value - Engine.ActiveControlAxis.Position);
                    }
                    break;
                case SubObjectMode.Triangle:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.TranslateControlTriangles(value - Engine.ActiveControlAxis.Position);
                    }
                    break;
            }

            Engine.ActiveControlAxis.Translate(value - Engine.ActiveControlAxis.Position);
        }

        public static void RotateByValue(Vector3 value)
        {
            Quaternion qRotate = new Quaternion();

            if (value.X != 0)
                qRotate = Quaternion.CreateFromAxisAngle(Engine.ActiveControlAxis.X_AxisDirection, MathHelper.ToRadians(value.X));
            if (value.Y != 0)
                qRotate = Quaternion.CreateFromAxisAngle(Engine.ActiveControlAxis.Y_AxisDirection, MathHelper.ToRadians(value.Y));
            if (value.Z != 0)
                qRotate = Quaternion.CreateFromAxisAngle(Engine.ActiveControlAxis.Z_AxisDirection, MathHelper.ToRadians(value.Z));

            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.Rotate(qRotate);
                    }
                    break;
                case SubObjectMode.Vertex:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.RotateControlVertices(qRotate);
                    }
                    break;
                case SubObjectMode.Edge:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.RotateControlEdges(qRotate);
                    }
                    break;
                case SubObjectMode.Triangle:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.RotateControlTriangles(qRotate);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void ScaleByValue(Vector3 value)
        {
            Vector3 scale = value;

            switch (Engine.ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.Scale(scale);
                    }
                    break;
                case SubObjectMode.Vertex:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.ScaleControlVertices(scale);
                    }
                    break;
                case SubObjectMode.Edge:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.ScaleControlEdges(scale);
                    }
                    break;
                case SubObjectMode.Triangle:
                    foreach (var item in Engine.EntitySelectionPool)
                    {
                        item.ScaleControlTriangles(scale);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
