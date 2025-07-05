using Ecs.Compentments;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

namespace Ecs.Systems
{
    
    
    public partial class SpawnCubeSystem: SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<SpawnCubeConfig>();
        }

        protected override void OnUpdate()
        {
            this.Enabled = false;
            SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();
            for (int i = 0; i < spawnCubeConfig.amountToSpawn; i++)
            {
                Entity entity = EntityManager.Instantiate(spawnCubeConfig.cubeEntity);
                SystemAPI.SetComponent(entity,new LocalTransform()
                {
                    Position = new float3(Random.Range(-20,20),5,Random.Range(-20,20)),
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                
            }
        }
    }
}