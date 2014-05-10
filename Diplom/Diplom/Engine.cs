using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Diplom.Intefaces;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom
{
    public static class Engine
    {
        public static GraphicsDevice ActiveGraphicsDevice { get; set; }
        public static Camera ActiveCamera { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static ControlAxis ActiveControlAxis { get; set; }

        private static TransformationMode _activeTransformMode;
        public static TransformationMode ActiveTransformMode
        {
            get { return _activeTransformMode; }
            set
            {
                if (_activeTransformMode != value)
                    _activeTransformMode = value;
                ActiveControlAxis.ActiveMode = value;
            }
        }

        private static SubObjectMode _activeSubObjectMode = SubObjectMode.None;
        public static SubObjectMode ActiveSubObjectMode
        {
            get { return _activeSubObjectMode; }
            set
            {
                if (_activeSubObjectMode == value) return;
                _activeSubObjectMode = value;
                ActiveControlAxis.SubObjectModeChanged();
            }
        }

        public static Ray CurrentMouseRay { get; set; }
        public static Ray PreviousMouseRay { get; set; }

        public static VertexPositionColor[] VerticesOfControlVertex = new VertexPositionColor[36];

        public static ContentLoader ContentLoader { get; set; }

        public static List<SceneEntity> SceneEntities = new List<SceneEntity>();
        public static List<SceneEntity> EntitySelectionPool = new List<SceneEntity>();
        public static List<ControlVertex> VertexSelectionPool = new List<ControlVertex>();


        public static void Draw(Camera camera)
        {
            foreach (SceneEntity entity in SceneEntities)
                entity.Draw(camera);
        }

        public static void SelectionChanged()
        {
            switch (ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    if (EntitySelectionPool.Count == 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = EntitySelectionPool[0].Center;
                        ActiveControlAxis.SetDirections(EntitySelectionPool[0].Up, EntitySelectionPool[0].Forward);
                    }
                    else if (EntitySelectionPool.Count > 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(EntitySelectionPool);
                        ActiveControlAxis.SetDirections(Vector3.Up, Vector3.Forward);
                    }
                    else
                    {
                        ActiveControlAxis.IsEnabled = false;
                    }
                    break;
                case SubObjectMode.Vertex:
                    if (VertexSelectionPool.Count == 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = VertexSelectionPool[0].Position;
                        ActiveControlAxis.SetDirections(Vector3.Up, Vector3.Forward);
                    }
                    else if (VertexSelectionPool.Count > 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(VertexSelectionPool);
                        ActiveControlAxis.SetDirections(Vector3.Up, Vector3.Forward);
                    }
                    else
                    {
                        ActiveControlAxis.IsEnabled = false;
                    }
                    break;
            }
        }
    }
}
