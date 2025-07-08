
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ecs.Compentments
{

    public struct PlayerBullet: IComponentData
    {
        public float Speed;
        public float3 Direction;
    }
    
    public class PlayerBulletAuthoring: MonoBehaviour
    {
        public float speed;
        public float3 direction;

        private class PlayerBulletBaker : Baker<PlayerBulletAuthoring>
        {
            public override void Bake(PlayerBulletAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new PlayerBullet
                {
                    Speed = authoring.speed,
                    Direction = authoring.direction,
                });
            }
        }
        
    }
}