namespace JobManager.Domain;
public record Job(Guid JobId, string State, string Name)
{
    public string State { get; internal set; } = State;
}