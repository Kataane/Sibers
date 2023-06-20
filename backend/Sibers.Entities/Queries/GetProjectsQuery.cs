namespace Sibers.Entities.Queries;

public record GetProjectsQuery(ODataQueryOptions<Project> ODataQueryOptions, ODataValidationSettings ODataQuerySettings) : IQuery<IEnumerable<ProjectResponse>>;