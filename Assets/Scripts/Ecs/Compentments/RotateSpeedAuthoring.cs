using System;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ecs.Compentments
{
    public struct RotateSpeed : IComponentData
    {
        public float Value;
    }
    public class RotateSpeedAuthoring: MonoBehaviour
    {
        public int value;
        
        private class RotateSpeedBaker : Baker<RotateSpeedAuthoring>
        {
            public override void Bake(RotateSpeedAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);//拿到所有带有变换的组实体
                AddComponent(entity,new RotateSpeed//给实体添加组件
                {
                    Value =Random.Range(1,10)
                });
            }
        }
    }
}