using System;
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
                int2 rowAndCol = new int2(i / 100, i % 100);
                float3 position = new float3(10f * rowAndCol.x, 10, 10f * rowAndCol.y)-new float3(50,0,50);
                Entity entity = EntityManager.Instantiate(spawnCubeConfig.cubeEntity);
                SystemAPI.SetComponent(entity,new LocalTransform()
                {
                    Position =position,
                    Rotation = quaternion.identity,
                    Scale = 5f
                });
            }
        }
    }
}