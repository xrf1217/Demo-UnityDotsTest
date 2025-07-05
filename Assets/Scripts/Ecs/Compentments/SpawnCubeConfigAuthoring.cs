using UnityEngine;
using Unity.Entities;

namespace Ecs.Compentments
{
    public class SpawnCubeConfigAuthoring : MonoBehaviour
    {
        public GameObject cubePrefab;
        public int amountToSpawn;
        
        
        public class Baker:Baker<SpawnCubeConfigAuthoring>
        {
            public override void Bake(SpawnCubeConfigAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity,new SpawnCubeConfig()
                {
                    cubeEntity=GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                    amountToSpawn = authoring.amountToSpawn
                });
            }
        }
    }

    public struct SpawnCubeConfig : IComponentData
    {
        public Entity cubeEntity;
        public int amountToSpawn;
    }
}