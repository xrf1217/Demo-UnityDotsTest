using Ecs.Compentments;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Ecs.Systems
{
    public partial struct PlayerBulletSystem: ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Player>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRW<LocalTransform> localTransform,RefRO<PlayerBullet> playerBullet)  in SystemAPI.Query<RefRW<LocalTransform>,RefRO<PlayerBullet>>().WithAll<PlayerBullet>())
            {
                localTransform.ValueRW =
                    localTransform.ValueRO.Translate(new float3(SystemAPI.Time.DeltaTime * playerBullet.ValueRO.Speed,
                        0, 0));
            }
        }
    }
}