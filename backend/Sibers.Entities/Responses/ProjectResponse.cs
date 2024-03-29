﻿namespace Sibers.Entities.Responses;

public record ProjectResponse : BaseResponse<Guid>
{
    public required string Name { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    public required Company Client { get; set; }
    public required Company Provider  { get; set; }
    
    public required Employee Supervisor { get; set; }
    
    [Range(0, 10, ErrorMessage = "Value must be between 0 and 10")] 
    public int Priority { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
}

public record BaseResponse<TId>
{
    protected BaseResponse(TId id)
    {
        Id = id;
    }

    protected BaseResponse()
    {
        Id = default;
    }

    public TId? Id { get; set; }
};