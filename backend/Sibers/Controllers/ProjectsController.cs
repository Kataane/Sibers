namespace Sibers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ISender sender;
    private readonly IMapper mapper;

    public ProjectsController(ISender sender, IMapper mapper)
    {
        this.sender = sender;
        this.mapper = mapper;
    }

    [HttpGet]
    [EnableQuery]
    [ProducesResponseType(typeof(IEnumerable<ProjectResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        ODataQueryOptions<Project> queryOptions, 
        CancellationToken cancellationToken) => 
        await sender
            .Send(new GetProjectsQuery(queryOptions, ProjectODataValidationSettings.Default), cancellationToken)
            .Map(Ok);

    /// <summary>
    /// Gets the project for the specified identifier.
    /// </summary>
    /// <param name="projectId">The project identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The budget with the specified identifier, if it exists.</returns>
    [HttpGet("{projectId:guid}")]
    [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProjectById(
        Guid projectId, CancellationToken cancellationToken) => 
        await Maybe<GetProjectByIdQuery>
        .From(new GetProjectByIdQuery(projectId))
        .Bind(projectByIdQuery => sender.Send(projectByIdQuery, cancellationToken))
        .Match(Ok, NotFound);

    /// <summary>
    /// Creates the project based on the specified request.
    /// </summary>
    /// <param name="createProjectRequest">The create project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The empty response if the operation was successful, otherwise an error response.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateProjectRequest createProjectRequest,
        CancellationToken cancellationToken) =>
        await Result
            .Create(createProjectRequest, Entities.Strings.ApiErrors_UnProcessableRequest)
            .Map(request => mapper.Map<CreateProjectCommand>(request))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(CreatedAtAction, BadRequest);

    /// <summary>
    /// Updates the specified project based on the specified request.
    /// </summary>
    /// <param name="updateProjectRequest">The update budget request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The empty response if the operation was successful, otherwise an error response.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateBudget(
        [FromBody] UpdateProjectRequest updateProjectRequest,
        CancellationToken cancellationToken) =>
        await Result.Create(updateProjectRequest, Entities.Strings.ApiErrors_UnProcessableRequest)
            .Map(request => mapper.Map<UpdateProjectCommand>(request))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, BadRequest);
    
    /// <summary>
    /// Deletes the specified project with the specified identifier.
    /// </summary>
    /// <param name="budgetId">The project identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The empty response if the operation was successful, otherwise an error response.</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBudget(Guid budgetId, CancellationToken cancellationToken) =>
        await Result.Success(new DeleteBudgetCommand(budgetId))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(NoContent, NotFound);
    
    /// <summary>
    /// Creates an <see cref="OkObjectResult"/> that produces a <see cref="StatusCodes.Status200OK"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
    protected new IActionResult Ok(object value) => base.Ok(value);
    
    protected new IActionResult NoContent() => base.NoContent();

    protected IActionResult CreatedAtAction<TId>(BaseResponse<TId> value) => 
        base.CreatedAtAction(nameof(GetProjectById), new {id = value.Id}, value);
    
    /// <summary>
    /// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
    /// </summary>
    /// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
    protected new IActionResult NotFound() => base.NotFound();
    
    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    /// response based on the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));
}