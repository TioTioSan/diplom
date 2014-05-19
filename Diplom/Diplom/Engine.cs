using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Diplom.SceneHelpers;

namespace Diplom
{
    public static class Engine
    {
        public static SceneState StartSceneState;
        private static List<SceneState> UndoStack = new List<SceneState>();
        private static List<SceneState> RedoStack = new List<SceneState>();

        public static int UndoCount = 50;

        public static GraphicsDevice ActiveGraphicsDevice { get; set; }
        public static Camera ActiveCamera { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static ContentLoader ContentLoader { get; set; }

        public static MainForm MainForm { get; set; }

        public static Vector2 CurrentMousePos { get; set; }
        public static Vector2 PreviousMousePos { get; set; }

        public static Ray CurrentMouseRay { get; set; }
        public static Ray PreviousMouseRay { get; set; }

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

                if (value != TransformationMode.Translate)
                    Engine.MainForm.ResetNumericUpDowns();
                else if (ActiveControlAxis.Position.X != float.MaxValue)
                    Engine.MainForm.SetNumericUpDowns(ActiveControlAxis.Position);
            }
        }

        public static bool IsRestore = false;

        private static SubObjectMode _activeSubObjectMode = SubObjectMode.None;
        public static SubObjectMode ActiveSubObjectMode
        {
            get { return _activeSubObjectMode; }
            set
            {
                if (_activeSubObjectMode == value) return;

                if (!IsRestore)
                    StartAction(ActionType.SubObjMode);

                _activeSubObjectMode = value;
                ActiveControlAxis.SubObjectModeChanged();

                if (!IsRestore)
                    EndAction();
            }
        }

        public static DrawMode ActiveDrawMode { get; set; }

        public static List<SceneEntity> SceneEntities = new List<SceneEntity>();

        public static List<SceneEntity> EntitySelectionPool = new List<SceneEntity>();
        public static List<ControlVertex> VertexSelectionPool = new List<ControlVertex>();
        public static List<ControlEdge> EdgeSelectionPool = new List<ControlEdge>();
        public static List<ControlTriangle> TriangleSelectionPool = new List<ControlTriangle>();


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
                        Engine.MainForm.IsEnabledSubObjCmbBox = true;
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = EntitySelectionPool[0].Center;

                        CleanSelectionPools(new List<SceneEntity>() { EntitySelectionPool[0] });
                    }
                    else if (EntitySelectionPool.Count > 1)
                    {
                        Engine.MainForm.IsEnabledSubObjCmbBox = true;
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(EntitySelectionPool);
                        CleanSelectionPools(EntitySelectionPool);
                    }
                    else
                    {
                        ActiveControlAxis.IsEnabled = false;
                        Engine.MainForm.IsEnabledSubObjCmbBox = false;

                        VertexSelectionPool.Clear();
                        EdgeSelectionPool.Clear();
                        TriangleSelectionPool.Clear();
                    }
                    break;
                case SubObjectMode.Vertex:
                    if (VertexSelectionPool.Count == 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = VertexSelectionPool[0].Position;
                    }
                    else if (VertexSelectionPool.Count > 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(VertexSelectionPool);
                    }
                    else
                    {
                        ActiveControlAxis.IsEnabled = false;
                    }
                    break;
                case SubObjectMode.Edge:
                    if (EdgeSelectionPool.Count == 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = EdgeSelectionPool[0].Center;
                    }
                    else if (EdgeSelectionPool.Count > 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(EdgeSelectionPool);
                    }
                    else
                    {
                        ActiveControlAxis.IsEnabled = false;
                    }
                    break;
                case SubObjectMode.Triangle:
                    if (TriangleSelectionPool.Count == 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = TriangleSelectionPool[0].Center;
                    }
                    else if (TriangleSelectionPool.Count > 1)
                    {
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(TriangleSelectionPool);
                    }
                    else
                    {
                        ActiveControlAxis.IsEnabled = false;
                    }
                    break;
            }
        }

        public static void ResetAxisPos()
        {
            switch (ActiveSubObjectMode)
            {
                case SubObjectMode.None:
                    if (EntitySelectionPool.Count > 0)
                        ActiveControlAxis.Position = Utils.GetCenter(EntitySelectionPool);
                    break;
                case SubObjectMode.Vertex:
                    if (VertexSelectionPool.Count > 0)
                        ActiveControlAxis.Position = Utils.GetCenter(VertexSelectionPool);
                    break;
                case SubObjectMode.Edge:
                    if (EdgeSelectionPool.Count > 0)
                        ActiveControlAxis.Position = Utils.GetCenter(EdgeSelectionPool);
                    break;
                case SubObjectMode.Triangle:
                    if (TriangleSelectionPool.Count > 0)
                        ActiveControlAxis.Position = Utils.GetCenter(TriangleSelectionPool);
                    break;
            }
        }

        public static void StartAction(ActionType actionType)
        {
            StartSceneState = new SceneState(actionType);
        }
        public static void EndAction()
        {
            if (!StartSceneState.IsChanged())
            {
                StartSceneState = null;
                return;
            }

            if (RedoStack.Count != 0)
                RedoStack.Clear();

            UndoStack.Add(StartSceneState);

            if (UndoStack.Count > UndoCount)
                UndoStack.RemoveAt(0);

            StartSceneState = null;
        }
        public static void UndoAction()
        {
            if (UndoStack.Count != 0)
            {
                RedoStack.Add(new SceneState(UndoStack.Last().ActionType));
                UndoStack.Last().Restore();
                UndoStack.Remove(UndoStack.Last());
            }
        }
        public static void RedoAction()
        {
            if (RedoStack.Count != 0)
            {
                UndoStack.Add(new SceneState(RedoStack.Last().ActionType));
                RedoStack.Last().Restore();
                RedoStack.Remove(RedoStack.Last());
            }
        }

        private static void CleanSelectionPools(List<SceneEntity> entities)
        {
            VertexSelectionPool.ForEach(x =>
            {
                if (!entities.Any(y => y.ControlVertices.Contains(x)))
                    VertexSelectionPool.Remove(x);
            });
            EdgeSelectionPool.ForEach(x =>
            {
                if (!entities.Any(y => y.ControlEdges.Contains(x)))
                    EdgeSelectionPool.Remove(x);
            });
            TriangleSelectionPool.ForEach(x =>
            {
                if (!entities.Any(y => y.ControlTriangles.Contains(x)))
                    TriangleSelectionPool.Remove(x);
            });
        }
    }
}
