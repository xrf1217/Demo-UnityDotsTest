using Ecs.Compentments;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Ecs.Systems
{
    public partial struct RotatingCubeSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotateSpeed>();
        }
        
        [BurstCompile]
        public void OnUpdate( ref SystemState state)
        {
            state.Enabled = false;
            return;
            // foreach (var rotateSpeed in SystemAPI.Query<RefRW<LocalTransform>,RefRO<RotateSpeed>>().WithNone<Player>())
            // {
            //     
            //     rotateSpeed.Item1.ValueRW =
            //         rotateSpeed.Item1.ValueRO.RotateY(rotateSpeed.Item2.ValueRO.Value * SystemAPI.Time.DeltaTime);
            // }

            RotatingCubeJob rotatingCubeJob = new RotatingCubeJob() { deltaTime = SystemAPI.Time.DeltaTime };
            rotatingCubeJob.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    [WithNone(typeof(Player))]
    public partial struct RotatingCubeJob: IJobEntity
    {
        public float deltaTime;

        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)//将自动执行该查询，并且对查询到的每个实体运行一下代码
        {
            float test = 1;
            for (int i = 0; i < 10000; i++)
            {
                test *= i + 3;
                test *= i / 3f;
            }

            localTransform= localTransform.RotateY(rotateSpeed.Value*deltaTime);
        }
        

    }
}