// See https://aka.ms/new-console-template for more information

using JobManager.Contracts;
using JobManagerImpl;
using MediatorDemo.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");


var services = new ServiceCollection();
services.AddLogging(builder => builder.AddConsole());
services.AddSingleton<IJobManager, JobManagerImpl.JobManager>();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(JobManagerImpl.JobManager).Assembly);
    //cfg.AddOpenBehavior(typeof(ThreadSafeBehavior<,>)); //vagy ez 1
});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ThreadSafeJobBehavior<,>)); //vagy ez 2
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

var app = services.BuildServiceProvider();

IMediator mediator = app.GetRequiredService<IMediator>();
var result = await mediator.Send(new CreateJobRequest("PJ1"));
Console.WriteLine($"Job id:{result.job.JobId}");

var jobId = result.job.JobId;
var updatedJob = await mediator.Send(new UpdateJobRequest(jobId, "Run", false));
Console.WriteLine($"Job state: {updatedJob.Job.State}");

await mediator.Send(new UpdateJobRequest(jobId, "Run", true));

