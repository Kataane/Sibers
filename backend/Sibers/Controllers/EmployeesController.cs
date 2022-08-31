using Sibers.Entities.Base;

namespace Sibers.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}, {Roles.Employee}")]
public class EmployeesController : BaseController<ServiceForCollection, Employee>
{
    private readonly UserManager<EmployeeUser> userManager;
    public EmployeesController(UserManager<EmployeeUser> userManager, ServiceForCollection service) : base(service)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Search(string fullName)
    {
        var employees = Service.Get<Employee>(0, filter: e => e.FullName.Contains(fullName));
        if (!employees.Data.Any()) return NotFound();
        return Ok(employees);
    }

    #region Goal

    [HttpGet]
    [Route("employees/{employeeId}/goals")]
    public async Task<ActionResult<Employee>> GetGoals(Guid employeeId)
    {
        try
        {
            if (!await HaveAccess(employeeId)) return BadRequest();

            var employee = await Service.GetWithIncludeAsync<Project>(employeeId, nameof(Employee.Goals));
            return Ok(employee?.Goals);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("employees/{employeeId}/goals/{goalId}")]
    public async Task<ActionResult<Employee>> AddGoal(Guid employeeId, Guid goalId)
    {
        try
        {
            var employee = await Service.AddToCollection<Employee, Goal, Second>(
                employeeId, goalId,
                nameof(Employee.Goals));
            await Service.SaveAsync();

            return Ok(employee);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("employees/{employeeId}/goals/{goalId}")]
    public async Task<ActionResult<Employee>> RemoveGoal(Guid employeeId, Guid goalId)
    {
        try
        {
            await Service.RemoveFromCollection<Employee, Goal, Second>(
                employeeId, employeeId, 
                nameof(Employee.Goals));
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
    public async Task<bool> HaveAccess(Guid employeeId)
    {
        var mail = User.Identity?.Name;
        var user = await userManager.FindByEmailAsync(mail);

        if (user.Role is Roles.Lead) return true;

        return user.Role is Roles.Employee or Roles.Manager && user.EmployeeId == employeeId;
    }

}