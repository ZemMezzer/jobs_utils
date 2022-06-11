using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobsUtils.Demo
{
    [BurstCompile]
    public struct MovementJob : IJobParallelFor
    {
        public NativeArray<Vector3> VectorsArray;
        
        private readonly Vector3 direction;
        private readonly float speed;
        private readonly float timeStep;

        public MovementJob(NativeArray<Vector3> nativeArray, Vector3 dir, float s, float t)
        {
            direction = dir;
            speed = s;
            timeStep = t;
            VectorsArray = nativeArray;
        }
        
        public void Execute(int i)
        {
            var data = VectorsArray[i];
            data = data + (direction * speed * timeStep);
            VectorsArray[i] = data;
        }
    }
}
