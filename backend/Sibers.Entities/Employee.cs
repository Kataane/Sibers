namespace Sibers.Entities;

public record Employee : Entity<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    
    public required Role Role { get; set; }

    public string FullName => $"{LastName} {FirstName} {Patronymic}";
}