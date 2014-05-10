using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.SceneHelpers;

namespace Diplom
{
    public static class Utils
    {
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

        public static Vector3 GetCenter(IEnumerable<Vector3> vertices)
        {
            return vertices.Average(x => x);
        }
    }
}
