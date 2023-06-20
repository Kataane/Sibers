using Sibers.Entities.Requests;

namespace Sibers.Entities.Contracts;

public interface IProjectFactory
{
    Result<Project> Create(CreateProjectRequest createBudgetRequest);
}