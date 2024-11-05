﻿namespace JobManagerImpl
{
    public class JobManager : IJobManager
    {
        readonly List<Job> _jobs = new List<Job>();

        public Job? CreateJob(string jobName)
        {
            if (_jobs.Any(job => String.Equals(job.Name, jobName)))
            {
                return null;
            }
            var job = new Job(Guid.NewGuid(), "Idle", jobName);
            _jobs.Add(job);
            return job;
        }

        public Job? UpdateState(Guid jobId, string state)
        {
            var job = _jobs.FirstOrDefault(job => job.JobId == jobId);
            if (job == null) return null;
            job.State = state;
            return job;
        }
    }

    public record Job(Guid JobId, string State, string Name)
    {
        public string State { get; internal set; } = State;
    }

    public interface IJobManager
    {
        Job? CreateJob(string jobName);
        Job? UpdateState(Guid jobId, string state);
    }
}