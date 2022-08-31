using Sibers.Entities.Base;

namespace Sibers.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<TService, TEntity> : ControllerBase where TService : IService where TEntity : Entity
{
    public TService Service { get; set; }

    public BaseController(TService service)
    {
        Service = service;
    }


    [Authorize(Roles = $"{Roles.Lead}")]
    [HttpGet]
    [Route("{id}")]
    public virtual async Task<ActionResult<TEntity>> Get(Guid id)
    {
        var project = await Service.GetAsync<TEntity>(id);
        return project == null ? NotFound() : Ok(project);
    }


    [Authorize(Roles = $"{Roles.Lead}")]
    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Post(TEntity entity)
    {
        try
        {
            await Service.AddAsync(entity);
            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(entity);
        }

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }


    [Authorize(Roles = $"{Roles.Lead}")]
    [HttpPut]
    [Route("{id}")]
    public virtual async Task<ActionResult<TEntity>> Put(TEntity entity, string id)
    {
        try
        {
            Service.Update(entity);
            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(entity);
        }

        return Ok(entity);
    }


    [Authorize(Roles = $"{Roles.Lead}")]
    [HttpDelete]
    [Route("{id}")]
    public virtual async Task<ActionResult<bool>> Delete(Guid id)
    {
        try
        {
            var delete = await Service.DeleteAsync<TEntity>(id);
            if (!delete) return NotFound();
            await Service.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }

        return NoContent();
    }

}