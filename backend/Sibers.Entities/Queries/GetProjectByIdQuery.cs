namespace Sibers.Entities.Queries;

public record GetProjectByIdQuery(Guid ProjectId) : IQuery<Maybe<ProjectResponse>>;