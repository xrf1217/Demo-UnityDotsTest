using Ecs.Compentments;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ecs.Systems
{
    public  partial class HandlePlayerSystem: SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<Player>();
        }

        protected override void OnUpdate()
        {
            
            foreach ((RefRW<LocalTransform> localTransform, RefRO<PlayerMoveSpeed> playerMoveSpeed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<PlayerMoveSpeed>>().WithAll<Player>())
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    localTransform.ValueRW =
                        localTransform.ValueRO.Translate(new float3(SystemAPI.Time.DeltaTime *
                                                                    playerMoveSpeed.ValueRO.MoveSpeed,0,0));
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    localTransform.ValueRW =
                        localTransform.ValueRO.Translate(new float3(-SystemAPI.Time.DeltaTime *
                                                                    playerMoveSpeed.ValueRO.MoveSpeed,0,0));
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    localTransform.ValueRW =
                        localTransform.ValueRO.Translate(new float3(0,0,SystemAPI.Time.DeltaTime *
                                                                    playerMoveSpeed.ValueRO.MoveSpeed));
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    localTransform.ValueRW =
                        localTransform.ValueRO.Translate(new float3(0,0,-SystemAPI.Time.DeltaTime *
                                                                    playerMoveSpeed.ValueRO.MoveSpeed));
                }
            }
        }
    }
}