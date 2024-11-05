namespace JobManager.Domain.Events;

public record JobCreated(Job Job) : IJobEvent;

