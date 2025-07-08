using Unity.Entities;
using UnityEngine;

namespace Ecs.Compentments
{
     public struct PlayerMoveSpeed : IComponentData
     {
         public float MoveSpeed;
     }

     public class PlayerMoveAuthoring : MonoBehaviour
     {
         public float moveSpeed;
          private class PlayerMoveBaker : Baker<PlayerMoveAuthoring>
          {
              public override void Bake(PlayerMoveAuthoring authoring)
              {
                  Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                  AddComponent(entity,new PlayerMoveSpeed()
                  {
                      MoveSpeed = authoring.moveSpeed
                  });
                  
              }
          }
         
     }
   
}