using System.Threading.Channels;
using JobManager.Contracts;
using MediatR;

namespace MediatorDemo.Behaviors;
internal class ThreadSafeJobBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IThreadSafeJobRequest

{
    private static Channel<(RequestHandlerDelegate<TResponse> next, TaskCompletionSource<TResponse> response)> _channel =
        Channel.CreateUnbounded<(RequestHandlerDelegate<TResponse>, TaskCompletionSource<TResponse>)>(new UnboundedChannelOptions()
        {
            SingleReader = true,
            SingleWriter = false
        });

    public ThreadSafeJobBehavior()
    {
        Task.Run(async () =>
        {
            await foreach (var msg in _channel.Reader.ReadAllAsync())
            {
                try
                {
                    msg.response.TrySetResult(await msg.next());
                }
                catch (Exception ex)
                {
                    msg.response.TrySetException(ex);
                }
            }
        });
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<TResponse>();
        await _channel.Writer.WriteAsync((next, tcs));
        return await tcs.Task;
    }

}


