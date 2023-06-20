namespace Sibers.Entities;

public record ApiErrorResponse(IReadOnlyCollection<Error> Errors);