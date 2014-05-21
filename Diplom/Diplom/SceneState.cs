using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diplom.SceneHelpers;

namespace Diplom
{
    public class SceneState
    {
        public ActionType ActionType;

        public List<SceneEntity> SceneEntities;
        public List<SceneEntity> EntitySelectionPool;
        public List<ControlVertex> VertexSelectionPool;
        public List<ControlEdge> EdgeSelectionPool;
        public List<ControlTriangle> TriangleSelectionPool;
        public List<SceneEntityData> SceneEntitiesData;
        public SubObjectMode ActiveSubObjectMode;

        public SceneState(ActionType actionType)
        {
            ActionType = actionType;
            switch (actionType)
            {
                case ActionType.SubObjMode:
                    ActiveSubObjectMode = Engine.ActiveSubObjectMode;
                    break;
                case ActionType.EntityCount:
                    SceneEntities = new List<SceneEntity>(Engine.SceneEntities);
                    break;
                case ActionType.Selection:
                    switch (Engine.ActiveSubObjectMode)
                    {
                        case SubObjectMode.None:
                            EntitySelectionPool = new List<SceneEntity>(Engine.EntitySelectionPool);
                            break;
                        case SubObjectMode.Vertex:
                            VertexSelectionPool = new List<ControlVertex>(Engine.VertexSelectionPool);
                            break;
                        case SubObjectMode.Edge:
                            EdgeSelectionPool = new List<ControlEdge>(Engine.EdgeSelectionPool);
                            break;
                        case SubObjectMode.Triangle:
                            TriangleSelectionPool = new List<ControlTriangle>(Engine.TriangleSelectionPool);
                            break;
                    }
                    break;
                case ActionType.VertexData:
                    SceneEntitiesData = new List<SceneEntityData>();
                    Engine.EntitySelectionPool.ForEach(x => SceneEntitiesData.Add(new SceneEntityData(x)));
                    break;
                case ActionType.AttachMode:
                    SceneEntities = new List<SceneEntity>(Engine.SceneEntities);
                    SceneEntitiesData = new List<SceneEntityData>();
                    Engine.EntitySelectionPool.ForEach(x => SceneEntitiesData.Add(new SceneEntityData(x)));
                    break;
            }
        }

        public bool IsChanged()
        {
            switch (ActionType)
            {
                case ActionType.SubObjMode:
                    if (ActiveSubObjectMode != Engine.ActiveSubObjectMode) return true;
                    break;
                case ActionType.EntityCount:
                    if (SceneEntities.Count != Engine.SceneEntities.Count) return true;
                    break;
                case ActionType.Selection:
                    switch (Engine.ActiveSubObjectMode)
                    {
                        case SubObjectMode.None:
                            if (EntitySelectionPool.Count != Engine.EntitySelectionPool.Count) return true;
                            foreach (var item in EntitySelectionPool)
                                if (!Engine.EntitySelectionPool.Any(x => x.Id == item.Id)) return true;
                            break;
                        case SubObjectMode.Vertex:
                            if (VertexSelectionPool.Count != Engine.VertexSelectionPool.Count) return true;
                            foreach (var item in VertexSelectionPool)
                                if (!Engine.VertexSelectionPool.Any(x => x.Position == item.Position)) return true;
                            break;
                        case SubObjectMode.Edge:
                            if (EdgeSelectionPool.Count != Engine.EdgeSelectionPool.Count) return true;
                            foreach (var item in EdgeSelectionPool)
                                if (!Engine.EdgeSelectionPool.Any(x => x.FirstVertex == item.FirstVertex && x.SecondVertex == item.SecondVertex)) return true;
                            break;
                        case SubObjectMode.Triangle:
                            if (TriangleSelectionPool.Count != Engine.TriangleSelectionPool.Count) return true;
                            foreach (var item in TriangleSelectionPool)
                                if (!Engine.TriangleSelectionPool.Any(x => x.FirstVertex == item.FirstVertex && x.SecondVertex == item.SecondVertex && x.ThirdVertex == item.ThirdVertex)) return true;
                            break;
                    }
                    break;
                case ActionType.VertexData:
                    foreach (var data in SceneEntitiesData)
                    {
                        SceneEntity entity = Engine.EntitySelectionPool.FirstOrDefault(x => x.Id == data.Id);
                        if (entity == null) return true;
                        if (data.Different(entity)) return true;
                    }
                    break;
                case ActionType.AttachMode:
                    if (SceneEntities.Count != Engine.SceneEntities.Count) return true;
                    foreach (var data in SceneEntitiesData)
                    {
                        SceneEntity entity = Engine.EntitySelectionPool.FirstOrDefault(x => x.Id == data.Id);
                        if (entity == null) return true;
                        if (data.Different(entity)) return true;
                    }
                    break;
            }

            return false;
        }

        public void Restore()
        {
            Engine.IsRestore = true;
            switch (ActionType)
            {
                case ActionType.SubObjMode:
                    Engine.MainForm.SubObjComboBoxSelectedIndex = (int)ActiveSubObjectMode;
                    break;
                case ActionType.EntityCount:
                    //if (SceneEntities.Count > Engine.SceneEntities.Count)
                    //    foreach (var entity in SceneEntities.Except(Engine.SceneEntities))
                    //        Engine.EntitySelectionPool.Add(entity);
                    Engine.SceneEntities = SceneEntities;
                    //Engine.SelectionChanged();
                    break;
                case ActionType.Selection:
                    switch (Engine.ActiveSubObjectMode)
                    {
                        case SubObjectMode.None:
                            Engine.EntitySelectionPool = EntitySelectionPool;
                            break;
                        case SubObjectMode.Vertex:
                            Engine.VertexSelectionPool = VertexSelectionPool;
                            break;
                        case SubObjectMode.Edge:
                            Engine.EdgeSelectionPool = EdgeSelectionPool;
                            break;
                        case SubObjectMode.Triangle:
                            Engine.TriangleSelectionPool = TriangleSelectionPool;
                            break;
                    }
                    Engine.SelectionChanged();
                    break;
                case ActionType.VertexData:
                    SceneEntitiesData.ForEach(x => Engine.EntitySelectionPool.FirstOrDefault(y => y.Id == x.Id).RestoreData(x));
                    Engine.SelectionChanged();
                    break;
                case ActionType.AttachMode:
                    Engine.SceneEntities = SceneEntities;
                    SceneEntitiesData.ForEach(x => Engine.EntitySelectionPool.FirstOrDefault(y => y.Id == x.Id).RestoreData(x));
                    Engine.SelectionChanged();
                    break;
            }
            Engine.IsRestore = false;
        }
    }
}
