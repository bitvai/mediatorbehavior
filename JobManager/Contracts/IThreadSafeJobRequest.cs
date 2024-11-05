using MediatR;

namespace JobManager.Contracts;

public interface IThreadSafeJobRequest<out TRequest> : IRequest<TRequest>, IThreadSafeJobRequest;

public interface IThreadSafeJobRequest;
