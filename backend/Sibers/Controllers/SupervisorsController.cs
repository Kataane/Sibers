namespace Sibers.Controllers;

public class SupervisorsController : BaseController<IService, Supervisor>
{
    public SupervisorsController(IService service) : base(service) {}

    public override async Task<ActionResult<Supervisor>> Post(Supervisor entity)
    {
        try
        {
            var employee = await Service.GetAsync<Employee>(entity.EmployeeId);
            var project = await Service.GetAsync<Project>(entity.ProjectId);

            entity.Employee = employee;
            entity.Project = project;
        
            await Service.SaveAsync();

            return Ok(entity);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}