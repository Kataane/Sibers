namespace Sibers.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectResponse>().ReverseMap();
        CreateMap<Project, CreateProjectRequest>().ReverseMap();
        CreateMap<Project, UpdateProjectCommand>().ReverseMap();
        
        CreateMap<CreateProjectCommand, CreateProjectRequest>().ReverseMap();
        CreateMap<UpdateProjectCommand, CreateProjectRequest>().ReverseMap();
    }
}