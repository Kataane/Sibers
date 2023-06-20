using Ardalis.Specification;

namespace Sibers.Entities.Contracts;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
    public Task<Maybe<IEnumerable<object>>> ApplyODataAsync(ODataQueryOptions<T>? queryOptions,
        ODataValidationSettings oDataQuerySettings, CancellationToken cancellationToken);
}