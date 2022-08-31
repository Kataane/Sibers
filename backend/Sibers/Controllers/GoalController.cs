namespace Sibers.Controllers;

[Authorize(Roles = $"{Roles.Lead}, {Roles.Manager}")]
public class GoalController : BaseController<Service, Goal>
{
    public GoalController(Service service) : base(service) {}
    

    [HttpGet]
    public IActionResult OrderBy([FromQuery]GoalSort sort, [FromQuery]string? propertyNameForOrder)
    {
        var filterPipeline = new GoalFilterPipeline(sort);

        try
        {
            var result = Service.Get(
                sort.Page, sort.PageSize,
                filterPipeline.Filter, propertyNameForOrder);

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("goals/{goalId}")]
    public async Task<IActionResult> ChangeStatus(Guid goalId, GoalStatus status)
    {
        try
        {
            var goal = await Service.GetAsync<Goal>(goalId);

            if (goal is null) return NotFound(goalId);

            goal.Status = status;

            await Service.SaveAsync();

            return Ok(goal);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

}