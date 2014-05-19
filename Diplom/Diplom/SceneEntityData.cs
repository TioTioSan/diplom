using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplom.Primitives;
using Diplom.SceneHelpers;

namespace Diplom
{
    public struct SceneEntityData
    {
        public int Id;
        public Vector3 Position;
        public Vector3 Center;

        public Vector3[] VertexPositions;
        public VertexPositionNormalTexture[] VertexData;
        
        public int PrimitiveCount;

        public List<Vert> VertData;
        public List<Edge> EdgeData;
        public List<Trian> TrianData;

        public SceneEntityData(SceneEntity entity)
        {
            Id = entity.Id;
            Center = entity.Center;
            Position = entity.Position;
            VertData = entity.ControlVertices.Select(x => new Vert(x.Position)).ToList();
            EdgeData = entity.ControlEdges.Select(x => new Edge(x.FirstVertex, x.SecondVertex)).ToList();
            TrianData = entity.ControlTriangles.Select(x => new Trian(x.FirstVertex, x.SecondVertex, x.ThirdVertex)).ToList();
            PrimitiveCount = entity.PrimitiveCount;
            VertexData = entity.VertexData.ToArray();
            VertexPositions = entity.VertexPositions.ToArray();
        }

        public bool Different(SceneEntity entity)
        {
            if (entity.Center != Center) return true;
            if (entity.Position != Position) return true;
            if (entity.PrimitiveCount != PrimitiveCount) return true;

            if(entity.ControlVertices.Count != VertData.Count) return true;
            if (entity.ControlEdges.Count != EdgeData.Count) return true;
            if(entity.ControlTriangles.Count != TrianData.Count) return true;

            if(entity.VertexData.Length != VertexData.Length) return true;
            if(entity.VertexPositions.Length != VertexPositions.Length) return true;

            for (int i = 0; i < VertData.Count; i++)
                if (VertData[i].Position != entity.ControlVertices[i].Position) return true;
            for (int i = 0; i < EdgeData.Count; i++)
                if (EdgeData[i].FirstVertex != entity.ControlEdges[i].FirstVertex &&
                    EdgeData[i].SecondVertex != entity.ControlEdges[i].SecondVertex) return true;
            for (int i = 0; i < TrianData.Count; i++)
                if (TrianData[i].FirstVertex != entity.ControlTriangles[i].FirstVertex &&
                    TrianData[i].SecondVertex != entity.ControlTriangles[i].SecondVertex &&
                    TrianData[i].ThirdVertex != entity.ControlTriangles[i].ThirdVertex) return true;

            for (int i = 0; i < VertexData.Length; i++)
                if (VertexData[i].Position != entity.VertexData[i].Position) return true;
            for (int i = 0; i < VertexPositions.Length; i++)
                if (VertexPositions[i] != entity.VertexPositions[i]) return true;

            return false;
        }
    }
}
