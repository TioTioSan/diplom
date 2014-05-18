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
        public struct V
        {
            public Vector3 Position;
            public V(Vector3 pos)
            {
                Position = pos;
            }
        }
        public struct E
        {
            public Vector3 FirstVertex;
            public Vector3 SecondVertex;
            public E(Vector3 fv, Vector3 sv)
            {
                FirstVertex = fv;
                SecondVertex = sv;
            }
        }
        public struct T
        {
            public Vector3 FirstVertex;
            public Vector3 SecondVertex;
            public Vector3 ThirdVertex;
            public T(Vector3 fv, Vector3 sv, Vector3 tv)
            {
                FirstVertex = fv;
                SecondVertex = sv;
                ThirdVertex = tv;
            }
        }

        public int Id;
        public Vector3 Position;
        public Vector3 Center;

        public Vector3[] VertexPositions;
        public VertexPositionNormalTexture[] VertexData;
        
        public int PrimitiveCount;

        //public List<ControlVertex> ControlVertices;
        //public List<ControlEdge> ControlEdges;
        //public List<ControlTriangle> ControlTriangles;

        public List<V> ControlVertices;
        public List<E> ControlEdges;
        public List<T> ControlTriangles;

        public SceneEntityData(SceneEntity entity)
        {
            Id = entity.Id;
            Center = entity.Center;
            Position = entity.Position;
            ControlVertices = entity.ControlVertices.Select(x => new V(x.Position)).ToList();
            ControlEdges = entity.ControlEdges.Select(x => new E(x.FirstVertex, x.SecondVertex)).ToList();
            ControlTriangles = entity.ControlTriangles.Select(x => new T(x.FirstVertex, x.SecondVertex, x.ThirdVertex)).ToList();
            //ControlVertices = entity.ControlVertices.Select(x=>new ControlVertex(x.Position)).ToList();
            //ControlEdges = entity.ControlEdges.Select(x=>new ControlEdge(x.FirstVertex, x.SecondVertex)).ToList();
            //ControlTriangles = entity.ControlTriangles.Select(x => new ControlTriangle(x.FirstVertex, x.SecondVertex, x.ThirdVertex)).ToList();
            PrimitiveCount = entity.PrimitiveCount;
            VertexData = entity.VertexData;
            VertexPositions = entity.VertexPositions;
        }

        public bool Different(SceneEntity entity)
        {
            if (entity.Center != Center) return true;
            if (entity.Position != Position) return true;
            if (entity.PrimitiveCount != PrimitiveCount) return true;

            if(entity.ControlVertices.Count != ControlVertices.Count) return true;
            if (entity.ControlEdges.Count != ControlEdges.Count) return true;
            if(entity.ControlTriangles.Count != ControlTriangles.Count) return true;

            if(entity.VertexData.Length != VertexData.Length) return true;
            if(entity.VertexPositions.Length != VertexPositions.Length) return true;

            for (int i = 0; i < ControlVertices.Count; i++)
                if (ControlVertices[i].Position != entity.ControlVertices[i].Position) return true;
            for (int i = 0; i < ControlEdges.Count; i++)
                if (ControlEdges[i].FirstVertex != entity.ControlEdges[i].FirstVertex &&
                    ControlEdges[i].SecondVertex != entity.ControlEdges[i].SecondVertex) return true;
            for (int i = 0; i < ControlTriangles.Count; i++)
                if (ControlTriangles[i].FirstVertex != entity.ControlTriangles[i].FirstVertex &&
                    ControlTriangles[i].SecondVertex != entity.ControlTriangles[i].SecondVertex &&
                    ControlTriangles[i].ThirdVertex != entity.ControlTriangles[i].ThirdVertex) return true;

            for (int i = 0; i < VertexData.Length; i++)
                if (VertexData[i].Position != entity.VertexData[i].Position) return true;
            for (int i = 0; i < VertexPositions.Length; i++)
                if (VertexPositions[i] != entity.VertexPositions[i]) return true;

            return false;
        }
    }
}
