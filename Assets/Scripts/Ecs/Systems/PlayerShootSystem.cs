using System;
using Ecs.Compentments;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ecs.Systems
{
    public partial class PlayerShootSystem: SystemBase
    {
        public event EventHandler OnShoot;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<Player>();
        }
        
        protected override void OnUpdate()
        {
           

            if (Input.GetKey(KeyCode.K))
            {
                foreach (Entity entity in SystemAPI.QueryBuilder()
                             .WithAll<Player>()
                             .WithDisabled<StunnedEffect>()
                             .Build()
                             .ToEntityArray(Unity.Collections.Allocator.Temp))
                {
                    // 设置数据（注意这里不能用 RefRW，只能通过 EntityManager 操作）
                    var stun = EntityManager.GetComponentData<StunnedEffect>(entity);
                    stun.Duration = 5f;
                    EntityManager.SetComponentData(entity, stun);

                    // 启用组件
                    EntityManager.SetComponentEnabled<StunnedEffect>(entity, true);
                }
            }
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }
            SpawnPlayerBulletConfig spawnPlayerBulletConfig = SystemAPI.GetSingleton<SpawnPlayerBulletConfig>();
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator);//命令的状态，作为临时的命令
            //实体命令缓冲区，作用是先将数据命令添加，防止前面实例化的实体对后面的实体有影响，所以是先加载所有的命令。让后在实例化示例，而不是一边实例化实体一遍加载组件
            foreach ((RefRO<LocalTransform> localTransform,Entity entity) in SystemAPI.Query<RefRO<LocalTransform>>()
                         .WithAll<Player>()
                         .WithDisabled<StunnedEffect>()
                         .WithEntityAccess())
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
               OnShoot?.Invoke(entity,EventArgs.Empty);
            }
            entityCommandBuffer.Playback(EntityManager);
            
        }
    }
}