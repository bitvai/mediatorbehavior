namespace JobManager.Domain.Events;

public record JobUpdated(Job Job) : IJobEvent;


