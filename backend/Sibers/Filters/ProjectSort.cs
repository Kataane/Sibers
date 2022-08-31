namespace Sibers.Filters;

public class ProjectSort
{
    public int Page { get; set; } = 0;
    public PageSize PageSize { get; set; } = PageSize.Ten;


    public PriorityOrderType? PriorityOrderType { get; set; }
    [Range(0, 10)]
    public int? Priority { get; set; }


    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

public class GoalSort
{
    public int Page { get; set; } = 0;
    public PageSize PageSize { get; set; } = PageSize.Ten;


    public GoalStatus? Status { get; set; }


    [Range(0, 10)]
    public int? Priority { get; set; }
    public PriorityOrderType? PriorityOrderType { get; set; }
}