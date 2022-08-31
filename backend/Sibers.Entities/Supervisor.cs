namespace Sibers.Entities;

public class Supervisor : Entity
{
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
    public Guid ProjectId { get; set; }


    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }
    public Guid EmployeeId { get; set; }
}