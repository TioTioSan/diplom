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
        public static string AssociatedFile = "";
        public static bool IsChangesUnsaved = false;

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

                        CleanSelectionPools();
                    }
                    else if (EntitySelectionPool.Count > 1)
                    {
                        Engine.MainForm.IsEnabledSubObjCmbBox = true;
                        ActiveControlAxis.IsEnabled = true;
                        ActiveControlAxis.Position = Utils.GetCenter(EntitySelectionPool);

                        CleanSelectionPools();
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

        public static void Reset()
        {
            ActiveTransformMode = TransformationMode.Translate;
            ActiveSubObjectMode = SubObjectMode.None;

            SceneEntities = new List<SceneEntity>();

            EntitySelectionPool = new List<SceneEntity>();
            VertexSelectionPool = new List<ControlVertex>();
            EdgeSelectionPool = new List<ControlEdge>();
            TriangleSelectionPool = new List<ControlTriangle>();

            UndoStack = new List<SceneState>();
            RedoStack = new List<SceneState>();

            SelectionChanged();
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

            if (RedoStack.Count != 0) RedoStack.Clear();
            UndoStack.Add(StartSceneState);
            if (UndoStack.Count > UndoCount) UndoStack.RemoveAt(0);

            if (StartSceneState.ActionType == ActionType.VertexData)
                IsChangesUnsaved = true;

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

        private static void CleanSelectionPools()
        {
            VertexSelectionPool.RemoveAll(x => !EntitySelectionPool.Any(y => y.ControlVertices.Any(z => z.Position == x.Position)));
            EdgeSelectionPool.RemoveAll(x => !EntitySelectionPool.Any(y => y.ControlEdges.Any(z => z.FirstVertex == x.FirstVertex && z.SecondVertex == x.SecondVertex)));
            TriangleSelectionPool.RemoveAll(x => !EntitySelectionPool.Any(y => y.ControlTriangles.Any(z => z.FirstVertex == x.FirstVertex && z.SecondVertex == x.SecondVertex && z.ThirdVertex == x.ThirdVertex)));
        }
    }
}
