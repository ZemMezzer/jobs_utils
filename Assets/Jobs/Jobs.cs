using JobsUtils.Reference;
using UnityEngine;

namespace JobsUtils
{
    public static class Jobs
    {
        private static readonly JobsHandler jobsHandler;

        static Jobs()
        {
            GameObject componentHandler = new GameObject();
            jobsHandler = componentHandler.AddComponent<JobsHandler>();
            componentHandler.hideFlags = HideFlags.HideInInspector;
            
            Object.DontDestroyOnLoad(componentHandler);
        }
        
        public static void Add(Value.JobHandler jobHandler)
        {
            jobsHandler.AddJobInQueue(jobHandler);
        }
        
        public static void Add(JobHandler job)
        {
            jobsHandler.AddJobInQueue(job);
        }
    }
}
