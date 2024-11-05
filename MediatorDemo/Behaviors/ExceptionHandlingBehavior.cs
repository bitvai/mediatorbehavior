using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorDemo.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse>(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception occurred while handling {typeof(TRequest).Name}");
            throw;
        }
    }
}

