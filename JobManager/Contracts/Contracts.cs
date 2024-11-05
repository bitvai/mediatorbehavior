using JobManagerImpl;
using MediatR;

namespace JobManager.Contracts;

public record CreateJobRequest(string JobName) : IRequest<CreateJobResponse>, IThreadSafeJobRequest;
public record CreateJobResponse(Job? job);

public record UpdateJobRequest(Guid jobId, string state, bool error) : IRequest<UpdateJobResponse>, IThreadSafeJobRequest;
public record UpdateJobResponse(Job? Job);
