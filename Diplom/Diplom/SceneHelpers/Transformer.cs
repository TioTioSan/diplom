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

        public static void MoveVertexFor(int vertexIndex, Vector3 direction, ref VertexPositionNormalTexture[] vertexData)
        {
            MoveVertexFor(vertexData[vertexIndex].Position, direction, ref vertexData);
        }
        public static void MoveVertexFor(Vector3 vertexPos, Vector3 direction, ref VertexPositionNormalTexture[] vertexData)
        {
            for (int i = 0; i < vertexData.Length; i++)
            {
                if (vertexData[i].Position == vertexPos)
                    vertexData[i].Position += direction;
            }
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
    }
}
