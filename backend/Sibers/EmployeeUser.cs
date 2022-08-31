namespace Sibers;

public class EmployeeUser : IdentityUser
{
    public Guid EmployeeId { get; set; }

    public string Role { get; set; }
}