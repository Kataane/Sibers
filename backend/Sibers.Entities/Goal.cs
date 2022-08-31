using Sibers.Entities.Enums;

namespace Sibers.Entities;

public class Goal : Entity
{
    public string Name { get; set; }
    public GoalStatus? Status { get; set; } = GoalStatus.Untrack;
    public string? Description { get; set; }
    [Range(0, 10)] public int? Priority { get; set; } = 0;


    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
    public Guid? ProjectId { get; set; }


    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }
    public Guid? EmployeeId { get; set; }
}