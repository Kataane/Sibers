using Sibers.Entities;
using Sibers.Entities.Base;

namespace Sibers.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}, {Roles.Employee}")]
public class ProjectsController : BaseController<ServiceForCollection, Project>
{
    private readonly UserManager<EmployeeUser> userManager;
    public ProjectsController(UserManager<EmployeeUser> userManager, ServiceForCollection project) : base(project)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> OrderBy([FromQuery]ProjectSort sort, [FromQuery]string? propertyNameForOrder)
    {
        var mail = User.Identity?.Name;
        var user = await userManager.FindByEmailAsync(mail);

        try
        {
            var filterPipeline = new ProjectFilterPipeline(sort, user);
            var filter = filterPipeline.Filter;

            var result = Service.GetWithInclude(
                nameof(Project.Employees),
                sort.Page, sort.PageSize,
                filter, propertyNameForOrder);

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


    #region Employee

    [HttpGet]
    [Route("{projectId}/employees")]
    [Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
    public async Task<ActionResult<Project>> GetEmployees(Guid projectId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var collection = await Service.GetCollection<Project, Employee, First>(projectId, nameof(Project.Employees));
            return Ok(collection);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("{projectId}/employees/{employeeId}")]
    [Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
    public async Task<ActionResult<Project>> AddEmployee(Guid projectId, Guid employeeId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.AddToCollection<Project, Employee, First>(
                projectId, employeeId, 
                nameof(Project.Employees));
            await Service.SaveAsync();

            return Ok(project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("{projectId}/employees/{employeeId}")]
    [Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
    public async Task<ActionResult<Project>> RemoveEmployee(Guid projectId, Guid employeeId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            await Service.RemoveFromCollection<Project, Employee, First>(projectId, employeeId, nameof(Project.Employees));
            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }

        return NoContent();
    }

    #endregion


    #region Goal

    [HttpGet]
    [Route("{projectId}/goals")]
    [Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
    public async Task<ActionResult<Project>> GetGoal(Guid projectId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var goals = await Service.GetCollection<Project, Goal, Second>(projectId, nameof(Project.Goals));

            return Ok(goals);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("{projectId}/goals/{goalId}")]
    [Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
    public async Task<ActionResult<Project>> AddGoal(Guid projectId, Guid goalId)
    {
        try
        {            
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.AddToCollection<Project, Goal, Second>(
                projectId, goalId, 
                nameof(Project.Goals));
            await Service.SaveAsync();

            return Ok(project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("{projectId}/goals/{goalId}")]
    [Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
    public async Task<ActionResult<Project>> RemoveGoal(Guid projectId, Guid goalId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            await Service.RemoveFromCollection<Project, Goal, Second>(projectId, goalId, nameof(Project.Goals));
            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }

        return NoContent();
    }

    #endregion

    #region Client

    [HttpGet]
    [Route("{projectId}/clients")]
    public async Task<ActionResult<Client>> GetClient(Guid projectId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.GetWithIncludeAsync<Project>(projectId, nameof(Project.Client));
            return Ok(project?.Client);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


    [HttpPut]
    [Route("{projectId}/clients/{clientId}")]
    public async Task<ActionResult> AddClient(Guid projectId, Guid clientId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var client = await Service.GetWithIncludeAsync<Client>(clientId);
            var project = await Service.GetWithIncludeAsync<Project>(projectId);

            if (project is null) return NotFound(project);
            if (client is null) return NotFound(client);

            project.Client = client;
            await Service.SaveAsync();

            return Ok(project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("{projectId}/clients")]
    public async Task<ActionResult<Project>> RemoveClient(Guid projectId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.GetWithIncludeAsync<Project>(projectId);

            if (project is null) return NotFound(project);

            project.Client = null;

            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }

        return NoContent();
    }

    #endregion


    #region Implementer

    [HttpGet]
    [Route("{projectId}/implementers/")]
    public async Task<ActionResult<Implementer>> GetImplementer(Guid projectId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.GetWithIncludeAsync<Project>(projectId, nameof(Project.Implementer));
            return Ok(project?.Implementer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


    [HttpPut]
    [Route("{projectId}/implementers/{implementerId}")]
    public async Task<ActionResult<Project>> AddImplementer(Guid projectId, Guid implementerId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.GetWithIncludeAsync<Project>(projectId);
            var implementer = await Service.GetWithIncludeAsync<Implementer>(implementerId);

            if (project is null) return NotFound(project);
            if (implementer is null) return NotFound(implementer);

            project.Implementer = implementer;

            await Service.SaveAsync();

            return Ok(project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("{projectId}/implementers/")]
    public async Task<ActionResult<Project>> RemoveImplementers(Guid projectId)
    {
        try
        {
            if (!await HaveAccess(projectId)) return BadRequest();

            var project = await Service.GetWithIncludeAsync<Project>(projectId);

            if (project is null) return NotFound(project);

            project.Implementer = null;

            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }

        return NoContent();
    }

    #endregion

    [NonAction]
    public async Task<bool> HaveAccess(Guid projectId)
    {
        var mail = User.Identity?.Name;
        var user = await userManager.FindByEmailAsync(mail);

        var collection = await Service.GetCollection<Project, Employee, First>(projectId, nameof(Project.Employees));

        if (collection is null) return false;

        var ownProject = collection.Any(e => e.Id == user.EmployeeId);
        return user.Role is not Roles.Manager || ownProject;
    }

}