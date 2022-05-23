using System;
using System.Collections.Generic;
using Unity.Jobs;

namespace JobsUtils.Reference
{
    public class JobHandler<T> : JobHandler
    {
        private static readonly Stack<JobHandler<T>> pool = new Stack<JobHandler<T>>();

        private Action<T> onCompleteEvent;
        
        private T value;

        private JobHandler()
        {
            
        }
        
        public static JobHandler<T> Get(in JobHandle jobHandle, in T inputValue, Action<T> onComplete)
        {
            var job = pool.Count > 0 ? pool.Pop() : new JobHandler<T>();
            job.Initialize(jobHandle, onComplete, inputValue);
            return job;
        }

        private void Initialize(JobHandle jobHandle, Action<T> onComplete, in T inputValue)
        {
            Handle = jobHandle;
            onCompleteEvent = onComplete;
            value = inputValue;
            
            Jobs.Add(this);
        }

        public override void Complete()
        {
            base.Complete();
            onCompleteEvent?.Invoke(value);
            pool.Push(this);
        }
    }

    public abstract class JobHandler
    {
        public JobHandle Handle { get; protected set; }
        
        public virtual void Complete()
        {
            Handle.Complete();
        }
    }
}
