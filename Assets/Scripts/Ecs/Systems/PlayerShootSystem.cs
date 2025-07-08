using Ecs.Compentments;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ecs.Systems
{
    public partial class PlayerShootSystem: SystemBase{
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<Player>();
        }

        
        protected override void OnUpdate()
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }

            SpawnPlayerBulletConfig spawnPlayerBulletConfig = SystemAPI.GetSingleton<SpawnPlayerBulletConfig>();
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator);//命令的状态，作为临时的命令
            //实体命令缓冲区，作用是先将数据命令添加，防止前面实例化的实体对后面的实体有影响，所以是先加载所有的命令。让后在实例化示例，而不是一边实例化实体一遍加载组件
            foreach ( RefRO<LocalTransform> localTransform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>())
            {
               // Entity spawnBulletEntity = EntityManager.Instantiate(spawnPlayerBulletConfig.BulletEntity);
                Entity spawnBulletEntity = entityCommandBuffer.Instantiate(spawnPlayerBulletConfig.BulletEntity);//先加载实体命令
                float3 position = new float3(localTransform.ValueRO.Position.x, 10,
                    localTransform.ValueRO.Position.z);
                entityCommandBuffer.SetComponent(spawnBulletEntity,new LocalTransform()
                {
                    Position = position,
                    Rotation = Quaternion.identity,
                    Scale = 1
                });
            }
            entityCommandBuffer.Playback(EntityManager);
            
        }
    }
}