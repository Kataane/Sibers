namespace Sibers.Entities;

public record Company : Entity<Guid>
{
    public required string Name { get; set; }
}