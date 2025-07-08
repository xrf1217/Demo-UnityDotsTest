using UnityEngine;
using Unity.Entities;

namespace Ecs.Compentments
{
    public class  SpawnPlayerBulletAuthoring: MonoBehaviour
    {
        public GameObject bulletprefab;
      
        public class Baker:Baker<SpawnPlayerBulletAuthoring>
        {
            public override void Bake(SpawnPlayerBulletAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity,new SpawnPlayerBulletConfig()
                {
                    BulletEntity=GetEntity(authoring.bulletprefab,TransformUsageFlags.Dynamic)  
                });
            }
        }
    }

    public struct SpawnPlayerBulletConfig : IComponentData
    {
        public Entity BulletEntity;
      
    }
}