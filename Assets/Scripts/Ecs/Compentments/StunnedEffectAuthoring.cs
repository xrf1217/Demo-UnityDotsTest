using Unity.Entities;
using UnityEngine;

namespace Ecs.Compentments
{
    public class StunnedEffectAuthoring: MonoBehaviour
    {
        public float duration;
         private class StunnedEffectBaker : Baker<StunnedEffectAuthoring>
        {
            public override void Bake(StunnedEffectAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new StunnedEffect()
                {
                    Duration = authoring.duration
                });
                SetComponentEnabled<StunnedEffect>(entity,false);
            }
        }
    }
    
    public struct StunnedEffect : IComponentData,IEnableableComponent
    {
        public float Duration;
    }
}