using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diplom.SceneHelpers;

namespace Diplom
{
    public class SceneState
    {
        public List<SceneEntity> SceneEntities = new List<SceneEntity>();

        public List<SceneEntity> EntitySelectionPool = new List<SceneEntity>();
        public List<ControlVertex> VertexSelectionPool = new List<ControlVertex>();
        public List<ControlEdge> EdgeSelectionPool = new List<ControlEdge>();
        public List<ControlTriangle> TriangleSelectionPool = new List<ControlTriangle>();

        public List<SceneEntityData> SceneEntitiesData = new List<SceneEntityData>();

        public SubObjectMode ActiveSubObjectMode;

        public SceneState()
        {
            ActiveSubObjectMode = Engine.ActiveSubObjectMode;

            SceneEntities = new List<SceneEntity>(Engine.SceneEntities);

            EntitySelectionPool = new List<SceneEntity>(Engine.EntitySelectionPool);
            VertexSelectionPool = new List<ControlVertex>(Engine.VertexSelectionPool);
            EdgeSelectionPool = new List<ControlEdge>(Engine.EdgeSelectionPool);
            TriangleSelectionPool = new List<ControlTriangle>(Engine.TriangleSelectionPool);

            Engine.EntitySelectionPool.ForEach(x => SceneEntitiesData.Add(new SceneEntityData(x)));
        }

        public bool IsChanged()
        {
            if (ActiveSubObjectMode != Engine.ActiveSubObjectMode) return true;

            if (SceneEntities.Count != Engine.SceneEntities.Count) return true;

            if (EntitySelectionPool.Count != Engine.EntitySelectionPool.Count) return true;
            if (VertexSelectionPool.Count != Engine.VertexSelectionPool.Count) return true;
            if (EdgeSelectionPool.Count != Engine.EdgeSelectionPool.Count) return true;
            if (TriangleSelectionPool.Count != Engine.TriangleSelectionPool.Count) return true;

            foreach (var item in EntitySelectionPool)
            {
                if (!Engine.EntitySelectionPool.Any(x => x.Id == item.Id)) return true;
            }
            foreach (var item in VertexSelectionPool)
            {
                if (!Engine.VertexSelectionPool.Any(x => x.Position == item.Position)) return true;
            }
            foreach (var item in EdgeSelectionPool)
            {
                if (!Engine.EdgeSelectionPool.Any(x => x.FirstVertex == item.FirstVertex && x.SecondVertex == item.SecondVertex)) return true;
            }
            foreach (var item in TriangleSelectionPool)
            {
                if (!Engine.TriangleSelectionPool.Any(x => x.FirstVertex == item.FirstVertex && x.SecondVertex == item.SecondVertex && x.ThirdVertex == item.ThirdVertex)) return true;
            }

            foreach (var data in SceneEntitiesData)
            {
                SceneEntity entity = Engine.EntitySelectionPool.FirstOrDefault(x=>x.Id == data.Id);
                if (entity == null) return true;
                if (data.Different(entity)) return true;
            }

            return false;
        }

        public void Restore()
        {
            Engine.IsRestore = true;

            Engine.MainForm.SubObjComboBoxSelectedIndex = (int)ActiveSubObjectMode;

            Engine.SceneEntities = SceneEntities;

            Engine.EntitySelectionPool = EntitySelectionPool;
            Engine.VertexSelectionPool = VertexSelectionPool;
            Engine.EdgeSelectionPool = EdgeSelectionPool;
            Engine.TriangleSelectionPool = TriangleSelectionPool;

            SceneEntitiesData.ForEach(x=>
            {
                Engine.EntitySelectionPool.FirstOrDefault(y => y.Id == x.Id).RestoreData(x);
            });

            Engine.SelectionChanged();

            Engine.IsRestore = false;
        }
    }
}
