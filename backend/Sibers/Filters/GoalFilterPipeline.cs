namespace Sibers.Filters;

public class GoalFilterPipeline
{
    private readonly PriorityOrderType? priorityOrderType;
    private readonly int? priority;
    private Func<Goal, bool>? PriorityFilter
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


    private GoalStatus? GoalStatus { get; set; }
    private Func<Goal, bool>? StatusFilter
    {
        get
        {
            if (GoalStatus is null) return null;
            return goal => goal.Status == GoalStatus;
        }
    }


    public Func<Goal, bool>? Filter
    {
        get
        {
            var filter = PriorityFilter;

            if (StatusFilter is not null)
                filter = filter is null ? StatusFilter : project => StatusFilter(project) && filter(project);

            return filter;
        }
    }



    public GoalFilterPipeline(GoalSort sort)
    {
        priorityOrderType = sort.PriorityOrderType ?? PriorityOrderType.Equals;
        priority = sort.Priority;
        
    }
}