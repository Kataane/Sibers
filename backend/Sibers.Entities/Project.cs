namespace Sibers.Entities;

public class Project : Entity, IEntityWithTwoCollection<Employee, Goal>
{
    public string Name { get; set; }


    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    
    [ForeignKey("ClientId")]
    public Client? Client { get; set; }
    public Guid? ClientId { get; set; }


    [ForeignKey("ImplementerId")]
    public Implementer? Implementer { get; set; }
    public Guid? ImplementerId { get; set; }


    [Range(0, 10, ErrorMessage = "Value must be between 0 and 10")] public int? Priority { get; set; } = 0;


    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Goal>? Goals { get; set; } = new List<Goal>();


    public ICollection<Employee>? Get(First diff) => Employees;
    public ICollection<Goal>? Get(Second? diff) => Goals;
}