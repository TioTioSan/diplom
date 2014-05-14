using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace Diplom.SceneHelpers
{
    public static class Selector
    {
        public static void PickSceneEntity()
        {
            Ray ray = Engine.CurrentMouseRay;
            float closest = float.MaxValue;
            SceneEntity obj = null;

            foreach (var entity in Engine.SceneEntities)
            {
                float? intersection = entity.Select(ray);
                if (intersection.HasValue && intersection < closest)
                {
                    obj = entity;
                    closest = intersection.Value;
                }
            }

            bool isObjPicked = obj != null;
            bool isAlreadySelected = isObjPicked && Engine.EntitySelectionPool.Contains(obj);

            switch (Control.ModifierKeys)
            {
                case Keys.Control:
                    if (isAlreadySelected)
                    {
                        Engine.EntitySelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    else if (isObjPicked)
                    {
                        Engine.EntitySelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                case Keys.Alt:
                    if (isAlreadySelected)
                    {
                        Engine.EntitySelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                default:
                    if (isObjPicked)
                    {
                        Engine.EntitySelectionPool.Clear();
                        Engine.EntitySelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    else if (Engine.EntitySelectionPool.Count != 0)
                    {
                        Engine.EntitySelectionPool.Clear();
                        Engine.SelectionChanged();
                    }
                    break;
            }
        }

        public static void PickControlVertex()
        {
            Ray ray;
            float closest = float.MaxValue;
            ControlVertex obj = null;

            foreach (var entity in Engine.EntitySelectionPool)
            {
                foreach (var vertex in entity.ControlVertices)
                {
                    ray = Engine.CurrentMouseRay;
                    // transform ray into local-space of the boundingboxes.
                    ray.Direction = Vector3.TransformNormal(ray.Direction, Matrix.Invert(vertex.VertexWorld));
                    ray.Position = Vector3.Transform(ray.Position, Matrix.Invert(vertex.VertexWorld));

                    float? intersection = vertex.BoundingBox.Intersects(ray);
                    if (intersection.HasValue && intersection < closest)
                    {
                        obj = vertex;
                        closest = intersection.Value;
                    }
                }
            }

            bool isObjPicked = obj != null;
            bool isAlreadySelected = isObjPicked && Engine.VertexSelectionPool.Contains(obj);

            switch (Control.ModifierKeys)
            {
                case Keys.Control:
                    if (isAlreadySelected)
                    {
                        Engine.VertexSelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    else if (isObjPicked)
                    {
                        Engine.VertexSelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                case Keys.Alt:
                    if (isAlreadySelected)
                    {
                        Engine.VertexSelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                default:
                    if (isObjPicked)
                    {
                        Engine.VertexSelectionPool.Clear();
                        Engine.VertexSelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    else if (Engine.VertexSelectionPool.Count != 0)
                    {
                        Engine.VertexSelectionPool.Clear();
                        Engine.SelectionChanged();
                    }
                    break;
            }
        }

        public static void PickControlEdge()
        {
            ControlEdge obj = null;

            foreach (var entity in Engine.EntitySelectionPool)
            {
                foreach (var edge in entity.ControlEdges)
                {
                    if (edge.Select(Engine.CurrentMouseRay))
                        obj = edge;
                }
            }

            bool isObjPicked = obj != null;
            bool isAlreadySelected = isObjPicked && Engine.EdgeSelectionPool.Contains(obj);

            switch (Control.ModifierKeys)
            {
                case Keys.Control:
                    if (isAlreadySelected)
                    {
                        Engine.EdgeSelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    else if (isObjPicked)
                    {
                        Engine.EdgeSelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                case Keys.Alt:
                    if (isAlreadySelected)
                    {
                        Engine.EdgeSelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                default:
                    if (isObjPicked)
                    {
                        Engine.EdgeSelectionPool.Clear();
                        Engine.EdgeSelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    else if (Engine.EdgeSelectionPool.Count != 0)
                    {
                        Engine.EdgeSelectionPool.Clear();
                        Engine.SelectionChanged();
                    }
                    break;
            }
        }

        public static void PickControlTriangle()
        {
            float closest = float.MaxValue;
            ControlTriangle obj = null;

            foreach (var entity in Engine.EntitySelectionPool)
            {
                foreach (var tngl in entity.ControlTriangles)
                {
                    float? intersection = Utils.RayIntersectsTriangle(Engine.CurrentMouseRay, tngl.FirstVertex, tngl.SecondVertex, tngl.ThirdVertex);
                    if (intersection.HasValue && intersection < closest)
                    {
                        obj = tngl;
                        closest = intersection.Value;
                    }
                }
            }

            bool isObjPicked = obj != null;
            bool isAlreadySelected = isObjPicked && Engine.TriangleSelectionPool.Contains(obj);

            switch (Control.ModifierKeys)
            {
                case Keys.Control:
                    if (isAlreadySelected)
                    {
                        Engine.TriangleSelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    else if (isObjPicked)
                    {
                        Engine.TriangleSelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                case Keys.Alt:
                    if (isAlreadySelected)
                    {
                        Engine.TriangleSelectionPool.Remove(obj);
                        Engine.SelectionChanged();
                    }
                    break;
                default:
                    if (isObjPicked)
                    {
                        Engine.TriangleSelectionPool.Clear();
                        Engine.TriangleSelectionPool.Add(obj);
                        Engine.SelectionChanged();
                    }
                    else if (Engine.TriangleSelectionPool.Count != 0)
                    {
                        Engine.TriangleSelectionPool.Clear();
                        Engine.SelectionChanged();
                    }
                    break;
            }
        }

        public static void SelectEntityByRectangle(Rectangle rect)
        {
            bool selectionChanged = false;
            if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Alt)
            {
                Engine.EntitySelectionPool.Clear();
                selectionChanged = true;
            }

            BoundingFrustum bf = UnprojectRectangle(rect);
            foreach (var entity in Engine.SceneEntities)
            {
                bool isObjPicked = bf.Contains(entity.BoundingBox) != ContainmentType.Disjoint;
                bool isAlreadySelected = isObjPicked && Engine.EntitySelectionPool.Contains(entity);

                switch (Control.ModifierKeys)
                {
                    case Keys.Control:
                        if (isObjPicked && !isAlreadySelected)
                        {
                            Engine.EntitySelectionPool.Add(entity);
                            selectionChanged = true;
                        }
                        break;
                    case Keys.Alt:
                        if (isAlreadySelected)
                        {
                            Engine.EntitySelectionPool.Remove(entity);
                            selectionChanged = true;
                        }
                        break;
                    default:
                        if (isObjPicked)
                        {
                            Engine.EntitySelectionPool.Add(entity);
                        }
                        break;
                }
            }

            if (selectionChanged)
                Engine.SelectionChanged();
        }

        public static void SelectControlVertexByRectangle(Rectangle rect)
        {
            bool selectionChanged = false;
            if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Alt)
            {
                Engine.VertexSelectionPool.Clear();
                selectionChanged = true;
            }

            BoundingFrustum bf = UnprojectRectangle(rect);
            foreach (var entity in Engine.EntitySelectionPool)
            {
                foreach (var vertex in entity.ControlVertices)
                {
                    BoundingBox transformedBoundingBox = vertex.BoundingBox;
                    transformedBoundingBox.Max = Vector3.Transform(transformedBoundingBox.Max, vertex.VertexWorld);
                    transformedBoundingBox.Min = Vector3.Transform(transformedBoundingBox.Min, vertex.VertexWorld);

                    bool isObjPicked = bf.Contains(transformedBoundingBox) != ContainmentType.Disjoint;
                    bool isAlreadySelected = isObjPicked && Engine.VertexSelectionPool.Contains(vertex);

                    switch (Control.ModifierKeys)
                    {
                        case Keys.Control:
                            if (isObjPicked && !isAlreadySelected)
                            {
                                Engine.VertexSelectionPool.Add(vertex);
                                selectionChanged = true;
                            }
                            break;
                        case Keys.Alt:
                            if (isAlreadySelected)
                            {
                                Engine.VertexSelectionPool.Remove(vertex);
                                selectionChanged = true;
                            }
                            break;
                        default:
                            if (isObjPicked)
                            {
                                Engine.VertexSelectionPool.Add(vertex);
                            }
                            break;
                    }
                }
            }

            if (selectionChanged)
                Engine.SelectionChanged();
        }

        public static void SelectControlEdgeByRectangle(Rectangle rect)
        {
            bool selectionChanged = false;
            if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Alt)
            {
                Engine.EdgeSelectionPool.Clear();
                selectionChanged = true;
            }

            BoundingFrustum bf = UnprojectRectangle(rect);
            foreach (var entity in Engine.EntitySelectionPool)
            {
                foreach (var edge in entity.ControlEdges)
                {
                    bool isObjPicked = bf.Contains(edge.FirstVertex)!= ContainmentType.Disjoint || 
                                       bf.Contains(edge.Center)!= ContainmentType.Disjoint || 
                                       bf.Contains(edge.SecondVertex) != ContainmentType.Disjoint;
                    bool isAlreadySelected = isObjPicked && Engine.EdgeSelectionPool.Contains(edge);

                    switch (Control.ModifierKeys)
                    {
                        case Keys.Control:
                            if (isObjPicked && !isAlreadySelected)
                            {
                                Engine.EdgeSelectionPool.Add(edge);
                                selectionChanged = true;
                            }
                            break;
                        case Keys.Alt:
                            if (isAlreadySelected)
                            {
                                Engine.EdgeSelectionPool.Remove(edge);
                                selectionChanged = true;
                            }
                            break;
                        default:
                            if (isObjPicked)
                            {
                                Engine.EdgeSelectionPool.Add(edge);
                            }
                            break;
                    }
                }
            }

            if (selectionChanged)
                Engine.SelectionChanged();
        }

        public static void SelectControlTriangleByRectangle(Rectangle rect)
        {
            bool selectionChanged = false;
            if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Alt)
            {
                Engine.TriangleSelectionPool.Clear();
                selectionChanged = true;
            }

            BoundingFrustum bf = UnprojectRectangle(rect);
            foreach (var entity in Engine.EntitySelectionPool)
            {
                foreach (var trngl in entity.ControlTriangles)
                {
                    bool isObjPicked = bf.Contains(trngl.FirstVertex) != ContainmentType.Disjoint ||
                                       bf.Contains(trngl.SecondVertex) != ContainmentType.Disjoint ||
                                       bf.Contains(trngl.ThirdVertex) != ContainmentType.Disjoint ||
                                       bf.Contains(trngl.Center) != ContainmentType.Disjoint;
                    bool isAlreadySelected = isObjPicked && Engine.TriangleSelectionPool.Contains(trngl);

                    switch (Control.ModifierKeys)
                    {
                        case Keys.Control:
                            if (isObjPicked && !isAlreadySelected)
                            {
                                Engine.TriangleSelectionPool.Add(trngl);
                                selectionChanged = true;
                            }
                            break;
                        case Keys.Alt:
                            if (isAlreadySelected)
                            {
                                Engine.TriangleSelectionPool.Remove(trngl);
                                selectionChanged = true;
                            }
                            break;
                        default:
                            if (isObjPicked)
                            {
                                Engine.TriangleSelectionPool.Add(trngl);
                            }
                            break;
                    }
                }
            }

            if (selectionChanged)
                Engine.SelectionChanged();
        }

        private static BoundingFrustum UnprojectRectangle(Rectangle rect)
        {
            // Point in screen space of the center of the region selected
            Vector2 regionCenterScreen = new Vector2(rect.Center.X, rect.Center.Y);

            // Generate the projection matrix for the screen region
            Matrix regionProjMatrix = Engine.ActiveCamera.ProjectionMatrix;

            // Calculate the region dimensions in the projection matrix. M11 is inverse of width, M22 is inverse of height.
            regionProjMatrix.M11 /= ((float)rect.Width / (float)Engine.ActiveGraphicsDevice.Viewport.Width);
            regionProjMatrix.M22 /= ((float)rect.Height / (float)Engine.ActiveGraphicsDevice.Viewport.Height);

            // Calculate the region center in the projection matrix. M31 is horizonatal center.
            regionProjMatrix.M31 = (regionCenterScreen.X - (Engine.ActiveGraphicsDevice.Viewport.Width / 2f)) / ((float)rect.Width / 2f);

            // M32 is vertical center. Notice that the screen has low Y on top, projection has low Y on bottom.
            regionProjMatrix.M32 = -(regionCenterScreen.Y - (Engine.ActiveGraphicsDevice.Viewport.Height / 2f)) / ((float)rect.Height / 2f);

            return new BoundingFrustum(Engine.ActiveCamera.ViewMatrix * regionProjMatrix);
        }
    }
}
