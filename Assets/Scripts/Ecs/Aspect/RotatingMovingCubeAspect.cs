using Ecs.Compentments;
using Unity.Entities;
using Unity.Transforms;

namespace Ecs.Aspect
{
    public readonly partial  struct RotatingMovingCubeAspect: IAspect
    {
        public readonly RefRW<LocalTransform> localTransform;
        public readonly RefRO<RotateSpeed> rotateSpeed;
        public readonly RefRO<Movement> movement;
        public readonly RefRO<RotatingCube> RotatingCube;
        
        
        public void MoveAndRotate(float time)
        {
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.Value * time);
            localTransform.ValueRW =
                localTransform.ValueRO.Translate(movement.ValueRO.movementVector * time);
        }
    }
    
  
}