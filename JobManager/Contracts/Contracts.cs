using JobManager.Domain;
using MediatR;

namespace JobManager.Contracts;

public record CreateJobRequest(string JobName) : IRequest<CreateJobResponse>, IThreadSafeJobRequest;
public record CreateJobResponse(Job? job, IList<IJobEvent> Events);

public record UpdateJobRequest(Guid jobId, string state, bool error) : IRequest<UpdateJobResponse>, IThreadSafeJobRequest;
public record UpdateJobResponse(Job? Job, IList<IJobEvent> Events);

public record GetJobDataRequest(string JobName) : IRequest<GetJobDataResponse>, IThreadSafeJobRequest;
public record GetJobDataResponse(string JobName, string State);