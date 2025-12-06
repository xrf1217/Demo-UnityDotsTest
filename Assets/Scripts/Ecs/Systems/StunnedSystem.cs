using Ecs.Compentments;
using Unity.Entities;

namespace Ecs.Systems
{
    public partial class StunnedSystem : SystemBase
    {
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
            foreach ((RefRW<StunnedEffect> stunnedEffect,EnabledRefRW<StunnedEffect> stunnedEffectEnable,Entity entity) in SystemAPI.Query<RefRW<StunnedEffect>,EnabledRefRW<StunnedEffect>>().WithEntityAccess())
            {
              
                stunnedEffect.ValueRW.Duration = stunnedEffect.ValueRO.Duration - SystemAPI.Time.DeltaTime;
                if (stunnedEffect.ValueRO.Duration <= 0)
                {
                  // ecb.RemoveComponent<StunnedEffect>(entity);//直接通过ecb移除组件
                   stunnedEffectEnable.ValueRW = false;//通过禁用组件
                }
            }
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}