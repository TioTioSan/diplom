﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diplom.Intefaces;
using Diplom.Primitives;

namespace Diplom.SceneHelpers
{
    public class ControlEdge
    {
        private BasicEffect _effect;
        private Matrix _vertexWorld = Matrix.Identity;
        private Color _usualColor = Color.Blue;
        private Color _highlightColor = Color.Red;

        private VertexPositionColor[] _vertexData;

        public Vector3 FirstVertex 
        {
            get { return _vertexData[0].Position; }
            set { _vertexData[0].Position = value; }
        }
        public Vector3 SecondVertex
        {
            get { return _vertexData[1].Position; }
            set { _vertexData[1].Position = value; }
        }

        public Vector3 Center { get { return (FirstVertex + SecondVertex) / 2; } }
        public float Length { get { return Vector3.Distance(FirstVertex, SecondVertex); } }
        public bool IsSelected { get { return Engine.EdgeSelectionPool.Contains(this); } }
        public Matrix VertexWorld { get { return _vertexWorld; } }

        private ControlEdge()
        {
            _effect = new BasicEffect(Engine.ActiveGraphicsDevice) { VertexColorEnabled = true };
            _vertexData = new VertexPositionColor[2];

            FirstVertex = new Vector3(0f);
            SecondVertex = new Vector3(0f);
        }

        public ControlEdge(Vector3 firstVertex, Vector3 secondVertex)
            : this()
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;

            _vertexData[0] = new VertexPositionColor(FirstVertex, Color.White);
            _vertexData[1] = new VertexPositionColor(SecondVertex, Color.White);
        }

        public void Translate(Vector3 delta)
        {
            FirstVertex += delta;
            SecondVertex += delta;
        }

        public void Draw()
        {
            _effect.World = Matrix.Identity;
            _effect.View = Engine.ActiveCamera.ViewMatrix;
            _effect.Projection = Engine.ActiveCamera.ProjectionMatrix;

            _effect.DiffuseColor = IsSelected ? _highlightColor.ToVector3() : _usualColor.ToVector3();
            _effect.EmissiveColor = _effect.DiffuseColor;

            _effect.CurrentTechnique.Passes[0].Apply();

            Engine.ActiveGraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _vertexData, 0, 1);
        }

        public bool Select(Ray ray)
        {
            Vector3 crossVector = Vector3.Cross(FirstVertex - SecondVertex, ray.Position - SecondVertex);
            Plane plane = new Plane(FirstVertex, SecondVertex, crossVector + SecondVertex);
            float? dist = ray.Intersects(plane);

            if (!dist.HasValue)
                return false;

            Vector3 intersectPoint = ray.Position + ray.Direction * dist.Value;

            if (Vector3.Distance(intersectPoint, FirstVertex) > Length)
                return false;
            if (Vector3.Distance(intersectPoint, SecondVertex) > Length)
                return false;

            float intersectDist = Vector3.Cross(SecondVertex - FirstVertex, intersectPoint - FirstVertex).Length() / Vector3.Distance(SecondVertex, FirstVertex);

            return intersectDist < 0.1f;
        }
    }
}
