namespace Sibers.Entities.Base;

public record Entity<TId>
{
    public required TId Id { get; set; }
}