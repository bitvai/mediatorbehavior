using JobManager.Contracts;
using JobManager.Domain;
using JobManager.Domain.Events;
using JobManagerImpl;
using MediatR;

namespace JobManager.RequestHandlers
{
    internal class UpdateJobRequestHandler(IJobManager Job) : IRequestHandler<UpdateJobRequest, UpdateJobResponse>
    {
        public Task<UpdateJobResponse> Handle(UpdateJobRequest request, CancellationToken cancellationToken)
        {
            var job = Job.UpdateState(request.jobId, request.state);
            if (request.error) throw new ArgumentException("Test");
            return Task.FromResult(new UpdateJobResponse(job, new List<IJobEvent>() { new JobUpdated(job) }));
        }
    }


}
