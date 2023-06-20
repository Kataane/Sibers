namespace Sibres.Business.Handlers;

/// <summary>
/// Represents the <see cref="GetProjectByIdQuery"/> handler.
/// </summary>
public sealed class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Maybe<ProjectResponse>>
{
    private readonly IServiceProvider serviceProvider;
    private readonly IMapper mapper;

    public GetProjectByIdQueryHandler(IServiceProvider serviceProvider, IMapper mapper)
    {
        this.serviceProvider = serviceProvider;
        this.mapper = mapper;
    }

    public async ValueTask<Maybe<ProjectResponse>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var projectRepository = scope.ServiceProvider.GetRequiredService<IRepository<Project>>();
        
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        return project is null ? Maybe<ProjectResponse>.None : mapper.Map<ProjectResponse>(project);
    }
}