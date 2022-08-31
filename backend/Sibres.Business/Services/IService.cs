namespace Sibres.Business.Services;

public interface IService
{
    public Task AddAsync<TEntity>(TEntity entity) where TEntity : Entity;


    public Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : Entity;
    PaginatedList<TEntity> Get<TEntity>(
        int page,
        PageSize pageSize = PageSize.Ten,
        Func<TEntity, bool>? filter = null,
        string? orderByPropertyName = null) where TEntity : Entity;
    Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : Entity;


    public void Update<TEntity>(TEntity entity) where TEntity : Entity;


    public Task<bool> DeleteAsync<TEntity>(Guid id) where TEntity : Entity;
    public Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity;


    public Task SaveAsync();
}