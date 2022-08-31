namespace Sibers.Filters;

public class ProjectFilterPipeline
{
    private readonly EmployeeUser user;
    private bool OnlyOwnProject => user.Role is Roles.Employee or Roles.Manager;

    private readonly Func<Project, bool> ownProjectFilter;


    private static readonly Func<Project, DateTime, bool> FilterStartDate = (project, startTime) =>
        project.StartTime >= startTime;

    private static readonly Func<Project, DateTime, bool> FilterEndDate = (project, endTime) =>
        project.EndTime <= endTime;

    private readonly PriorityOrderType? priorityOrderType;
    private readonly int? priority;
    private Func<Project, bool>? PriorityFilter
    {
        get
        {
            if (priorityOrderType is null || priority is null) return null;

            return priorityOrderType switch
            {
                PriorityOrderType.Equals => project => project.Priority == priority.Value,
                PriorityOrderType.Greater => project => project.Priority > priority.Value,
                PriorityOrderType.Less => project => project.Priority < priority.Value,
                PriorityOrderType.GreaterEquals => project => project.Priority >= priority.Value,
                PriorityOrderType.LessEquals => project => project.Priority <= priority.Value,
                null => null,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }


    private readonly DateTime? startTime;
    private readonly DateTime? endTime;
    private Func<Project, bool>? DateFilter
    {
        get
        {
            if (startTime is null || endTime is null) return null;

            return endTime is null
                ? project => FilterStartDate(project, startTime.Value)
                : project =>
                    FilterStartDate(project, startTime.Value) && FilterEndDate(project, endTime.Value);
        }
    }


    public Func<Project, bool>? Filter
    {
        get
        {
            var filter = PriorityFilter;

            if (DateFilter is not null)
                filter = filter is null ? DateFilter : project => DateFilter(project) && filter(project);
            if(OnlyOwnProject)
                filter = filter is null ? ownProjectFilter : project => ownProjectFilter(project) && filter(project);

            return filter;
        }
    }


    public ProjectFilterPipeline(ProjectSort sort, EmployeeUser user)
    {
        this.user = user;

        priorityOrderType = sort.PriorityOrderType ?? PriorityOrderType.Equals;

        ownProjectFilter = project =>
        {
            var id = this.user.EmployeeId;
            return project.Employees is not null && project.Employees.Any(e => e.Id == id);
        };
        priority = sort.Priority;

        startTime = sort.StartTime;
        endTime = sort.EndTime;
    }
}