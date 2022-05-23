using System;
using Unity.Jobs;

namespace JobsUtils.Value
{
    public readonly struct JobHandler
    {
        public JobHandle Handle { get; }
        private readonly Action<JobHandle> onCompleteEvent;

        public JobHandler(in JobHandle jobHandle, Action<JobHandle> onComplete)
        {
            Handle = jobHandle;
            onCompleteEvent = onComplete;
            
            Jobs.Add(this);
        }
        
        public void Complete()
        {
            onCompleteEvent?.Invoke(Handle);
        }
    }
}
