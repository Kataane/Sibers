namespace Sibres.Business.Handlers;

public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand, Result>
{
    private readonly IMapper mapper;
    private readonly IServiceProvider serviceProvider;

    public UpdateProjectCommandHandler(IMapper mapper, IServiceProvider serviceProvider)
    {
        this.mapper = mapper;
        this.serviceProvider = serviceProvider;
    }
    
    public async ValueTask<Result> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var projectRepository = scope.ServiceProvider.GetRequiredService<IRepository<Project>>();

        var project = mapper.Map<Project>(command);
        await projectRepository.UpdateAsync(project, cancellationToken);

        return Result.Success();
    }
}