﻿using JobManager.Contracts;
using JobManagerImpl;
using MediatR;

namespace JobManager.RequestHandlers
{

    public class CreateJobRequestHandler(IJobManager Job) : IRequestHandler<CreateJobRequest, CreateJobResponse>
    {
        public Task<CreateJobResponse> Handle(CreateJobRequest request, CancellationToken cancellationToken)
        {
            var job = Job.CreateJob(request.JobName);
            //Check to start
            return Task.FromResult(new CreateJobResponse(job));
        }
    }

}
