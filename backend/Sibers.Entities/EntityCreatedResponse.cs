namespace Sibers.Entities;

public record EntityCreatedResponse(Guid Id) : BaseResponse<Guid>(Id);