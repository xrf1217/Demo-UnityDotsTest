using Unity.Entities;
using UnityEngine;

namespace Ecs.Compentments
{
    public struct RotatingCube : IComponentData
    {
        
    }
    
    public class RotatingCubeAuthoring: MonoBehaviour
    {
        public class Baker:Baker<RotatingCubeAuthoring>
        {
            public override void Bake(RotatingCubeAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new RotatingCube());
            }
        }
    }
}