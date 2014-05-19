using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.SceneHelpers;
using System.Reflection;

namespace Diplom
{
    public static class Utils
    {
        public static VertexPositionColor[] VerticesOfControlVertex = new VertexPositionColor[36];

        public static Ray ConvertMouseToRay(int x, int y)
        {
            return ConvertMouseToRay(new Vector2((float)x, (float)y));
        }

        public static Ray ConvertMouseToRay(Vector2 mousePosition)
        {
            Vector3 nearPoint = new Vector3(mousePosition, 0);
            Vector3 farPoint = new Vector3(mousePosition, 1);

            nearPoint = Engine.ActiveGraphicsDevice.Viewport.Unproject(nearPoint, Engine.ActiveCamera.ProjectionMatrix, Engine.ActiveCamera.ViewMatrix, Matrix.Identity);
            farPoint = Engine.ActiveGraphicsDevice.Viewport.Unproject(farPoint, Engine.ActiveCamera.ProjectionMatrix, Engine.ActiveCamera.ViewMatrix, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public static BoundingBox CalculateBoundingBox(IEnumerable<VertexPositionColor> vertexData)
        {
            return CalculateBoundingBox(vertexData.Select(x => x.Position));
        }

        public static BoundingBox CalculateBoundingBox(IEnumerable<Vector3> vertexPositions)
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (Vector3 vertPos in vertexPositions)
            {
                if (vertPos.X > max.X) max.X = vertPos.X;
                if (vertPos.Y > max.Y) max.Y = vertPos.Y;
                if (vertPos.Z > max.Z) max.Z = vertPos.Z;
                if (vertPos.X < min.X) min.X = vertPos.X;
                if (vertPos.Y < min.Y) min.Y = vertPos.Y;
                if (vertPos.Z < min.Z) min.Z = vertPos.Z;
            }
            return new BoundingBox(min, max);
        }

        public static Vector3 GetCenter(IEnumerable<SceneEntity> entities)
        {
            return entities.Average(x => x.Center);
        }

        public static Vector3 GetCenter(IEnumerable<ControlVertex> controlVertices)
        {
            return controlVertices.Average(x => x.Position);
        }

        public static Vector3 GetCenter(IEnumerable<ControlEdge> controlEdges)
        {
            return controlEdges.Average(x => x.Center);
        }

        public static Vector3 GetCenter(IEnumerable<ControlTriangle> contolTriangles)
        {
            return contolTriangles.Average(x => x.Center);
        }

        public static Vector3 GetCenter(IEnumerable<Vector3> vertices)
        {
            return vertices.Average(x => x);
        }

        public static float? RayIntersectsTriangle(Ray ray, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            // Compute vectors along two edges of the triangle.
            Vector3 edge1, edge2;

            Vector3.Subtract(ref vertex2, ref vertex1, out edge1);
            Vector3.Subtract(ref vertex3, ref vertex1, out edge2);

            // Compute the determinant.
            Vector3 directionCrossEdge2;
            Vector3.Cross(ref ray.Direction, ref edge2, out directionCrossEdge2);

            float determinant;
            Vector3.Dot(ref edge1, ref directionCrossEdge2, out determinant);

            // If the ray is parallel to the triangle plane, there is no collision.
            if (determinant > -float.Epsilon && determinant < float.Epsilon)
            {
                return null;
            }

            float inverseDeterminant = 1.0f / determinant;

            // Calculate the U parameter of the intersection point.
            Vector3 distanceVector;
            Vector3.Subtract(ref ray.Position, ref vertex1, out distanceVector);

            float triangleU;
            Vector3.Dot(ref distanceVector, ref directionCrossEdge2, out triangleU);
            triangleU *= inverseDeterminant;

            // Make sure it is inside the triangle.
            if (triangleU < 0 || triangleU > 1)
            {
                return null;
            }

            // Calculate the V parameter of the intersection point.
            Vector3 distanceCrossEdge1;
            Vector3.Cross(ref distanceVector, ref edge1, out distanceCrossEdge1);

            float triangleV;
            Vector3.Dot(ref ray.Direction, ref distanceCrossEdge1, out triangleV);
            triangleV *= inverseDeterminant;

            // Make sure it is inside the triangle.
            if (triangleV < 0 || triangleU + triangleV > 1)
            {
                return null;
            }

            // Compute the distance along the ray to the triangle.
            float rayDistance;
            Vector3.Dot(ref edge2, ref distanceCrossEdge1, out rayDistance);
            rayDistance *= inverseDeterminant;

            // Is the triangle behind the ray origin?
            if (rayDistance < 0)
            {
                return null;
            }

            return rayDistance;
        }

        public static void SyncVertexLists(ref List<ControlVertex> contrVert, List<Vert> vertData)
        {
            if (vertData.Count > contrVert.Count)
            {
                for (int i = 0; i < vertData.Count; i++)
                {
                    if (i == contrVert.Count || contrVert[i].Position != vertData[i].Position)
                    {
                        contrVert.Insert(i, new ControlVertex(vertData[i].Position));
                        if (Engine.ActiveSubObjectMode == SubObjectMode.Vertex)
                            Engine.VertexSelectionPool.Add(contrVert[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < contrVert.Count; i++)
                {
                    if (i == vertData.Count || contrVert[i].Position != vertData[i].Position)
                    {
                        if (Engine.ActiveSubObjectMode == SubObjectMode.Vertex)
                            Engine.VertexSelectionPool.Remove(contrVert[i]);
                        contrVert.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public static void SyncEdgeLists(ref List<ControlEdge> contrEdge, List<Edge> edgeData)
        {
            if (edgeData.Count > contrEdge.Count)
            {
                for (int i = 0; i < edgeData.Count; i++)
                {
                    if (i == contrEdge.Count || !(contrEdge[i].FirstVertex == edgeData[i].FirstVertex && contrEdge[i].SecondVertex == edgeData[i].SecondVertex))
                    {
                        contrEdge.Insert(i, new ControlEdge(edgeData[i].FirstVertex, edgeData[i].SecondVertex));
                        if (Engine.ActiveSubObjectMode == SubObjectMode.Edge)
                            Engine.EdgeSelectionPool.Add(contrEdge[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < contrEdge.Count; i++)
                {
                    if (i == edgeData.Count || !(contrEdge[i].FirstVertex == edgeData[i].FirstVertex && contrEdge[i].SecondVertex == edgeData[i].SecondVertex))
                    {
                        if (Engine.ActiveSubObjectMode == SubObjectMode.Edge)
                            Engine.EdgeSelectionPool.Remove(contrEdge[i]);
                        contrEdge.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public static void SyncTriangleLists(ref List<ControlTriangle> contrTrian, List<Trian> trianData)
        {
            if (trianData.Count > contrTrian.Count)
            {
                for (int i = 0; i < trianData.Count; i += 3)
                {
                    if (i == contrTrian.Count || !(contrTrian[i].FirstVertex == trianData[i].FirstVertex && contrTrian[i].SecondVertex == trianData[i].SecondVertex && contrTrian[i].ThirdVertex == trianData[i].ThirdVertex))
                    {
                        contrTrian.Insert(i, new ControlTriangle(trianData[i].FirstVertex, trianData[i].SecondVertex, trianData[i].ThirdVertex));
                        if (Engine.ActiveSubObjectMode == SubObjectMode.Triangle)
                            Engine.TriangleSelectionPool.Add(contrTrian[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < contrTrian.Count; i += 3)
                {
                    if (i == trianData.Count || !(contrTrian[i].FirstVertex == trianData[i].FirstVertex && contrTrian[i].SecondVertex == trianData[i].SecondVertex && contrTrian[i].ThirdVertex == trianData[i].ThirdVertex))
                    {
                        if (Engine.ActiveSubObjectMode == SubObjectMode.Triangle)
                            Engine.TriangleSelectionPool.Remove(contrTrian[i]);
                        contrTrian.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    public struct Vert
    {
        public Vector3 Position;
        public Vert(Vector3 pos)
        {
            Position = pos;
        }
    }
    public struct Edge
    {
        public Vector3 FirstVertex;
        public Vector3 SecondVertex;
        public Edge(Vector3 fv, Vector3 sv)
        {
            FirstVertex = fv;
            SecondVertex = sv;
        }
    }
    public struct Trian
    {
        public Vector3 FirstVertex;
        public Vector3 SecondVertex;
        public Vector3 ThirdVertex;
        public Trian(Vector3 fv, Vector3 sv, Vector3 tv)
        {
            FirstVertex = fv;
            SecondVertex = sv;
            ThirdVertex = tv;
        }
    }
}
