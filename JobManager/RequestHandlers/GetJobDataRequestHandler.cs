using JobManager.Contracts;
using JobManagerImpl;
using MediatR;

namespace JobManager.RequestHandlers;

internal class GetJobDataRequestHandler(IJobManager Job) : IRequestHandler<GetJobDataRequest, GetJobDataResponse>
{
    public Task<GetJobDataResponse> Handle(GetJobDataRequest request, CancellationToken cancellationToken)
    {
        var job = Job.GetJobByName(request.JobName);
        if (job == null) { return Task.FromResult(new GetJobDataResponse(String.Empty, String.Empty)); }

        return Task.FromResult(new GetJobDataResponse(job.Name, job.State));
    }
}

