using Sibers.Entities.Requests;

namespace Sibres.Business.Factories;

public class ProjectFactory : IProjectFactory
{
    private readonly IMapper mapper;

    public ProjectFactory(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public Result<Project> Create(CreateProjectRequest createProjectRequest)
    {
        return mapper.Map<Project>(createProjectRequest);
    }
}