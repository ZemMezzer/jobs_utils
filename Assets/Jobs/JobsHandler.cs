using System;
using System.Collections.Generic;
using JobsUtils.Value;
using UnityEngine;

namespace JobsUtils
{
    public class JobsHandler : MonoBehaviour
    {
        private readonly List<JobHandler> valueJobsHandlers = new List<JobHandler>();
        private readonly List<Reference.JobHandler> referenceJobsHandlers = new List<Reference.JobHandler>();

        public void AddJobInQueue(JobHandler jobHandler)
        {
            valueJobsHandlers.Add(jobHandler);
        }

        public void AddJobInQueue(Reference.JobHandler jobHandler)
        {
            referenceJobsHandlers.Add(jobHandler);
        }

        private void Update()
        {
            IterateOverValueJobs();
            IterateOverReferenceJobs();
        }

        private void IterateOverValueJobs()
        {
            for (int i = valueJobsHandlers.Count - 1; i >= 0; i--)
            {
                var job = valueJobsHandlers[i];
                try
                {
                    if (job.Handle.IsCompleted)
                    {
                        job.Complete();
                        valueJobsHandlers.RemoveAt(i);
                    }
                }
                catch (Exception e)
                {
                    valueJobsHandlers.RemoveAt(i);
                    Debug.LogError(e);
                }
            }
        }

        private void IterateOverReferenceJobs()
        {
            for (int i = referenceJobsHandlers.Count - 1; i >= 0; i--)
            {
                var job = referenceJobsHandlers[i];
                try
                {
                    if (job.Handle.IsCompleted)
                    {
                        job.Complete();
                        referenceJobsHandlers.RemoveAt(i);
                    }
                }
                catch (Exception e)
                {
                    referenceJobsHandlers.RemoveAt(i);
                    Debug.LogError(e);
                }
            }
        }
    }
}
