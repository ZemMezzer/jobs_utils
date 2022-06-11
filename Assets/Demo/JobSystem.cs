using System;
using JobsUtils.Reference;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobsUtils.Demo
{
    public class JobSystem : MonoBehaviour
    {
        [SerializeField] private Transform[] transforms;
        [SerializeField] private float patternChangeTime;

        private float actualTime;
        private bool patternFlag;

        private bool canContinue = true;

        private NativeArray<Vector3> positionsArray;
        
        private void Update()
        {
            if(!canContinue)
                return;
            
            positionsArray = new NativeArray<Vector3>(transforms.Length, Allocator.TempJob);
            
            for (int i = 0; i < transforms.Length; i++)
            {
                positionsArray[i] = transforms[i].position;
            }
            
            MovementJob movementJob = new MovementJob(positionsArray, patternFlag ? Vector3.up : Vector3.down, 10, Time.deltaTime);

            JobHandle handle = movementJob.Schedule(transforms.Length, 6);
            
            JobHandler<NativeArray<Vector3>> jobHandler = JobHandler<NativeArray<Vector3>>.Get(handle, positionsArray,
                array =>
                {
                    for (int i = 0; i < transforms.Length; i++)
                    {
                        transforms[i].position = array[i];
                    }

                    canContinue = true;
                    array.Dispose();
                });

            canContinue = false;
        }

        private void FixedUpdate()
        {
            if (actualTime <= 0)
            {
                patternFlag = !patternFlag;
                actualTime = patternChangeTime;
            }

            actualTime -= Time.fixedDeltaTime;
        }
    }
}
