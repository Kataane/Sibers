namespace Sibres.Business.Services;

public class Service : IService
{
    protected readonly SibersContext Context;


    public Service(SibersContext context) => Context = context;


    public virtual async Task AddAsync<TEntity>(TEntity entity) where TEntity : Entity
    {
        var set = Context.Set<TEntity>();
        await set.AddAsync(entity);
    }


    #region Get

    public async Task<TEntity?> GetWithIncludeAsync<TEntity>(Guid id, string collectionName)
        where TEntity : Entity
    {
        var entities = Context.Set<TEntity>();
        return await entities
            .Include(collectionName)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity?> GetWithIncludeAsync<TEntity>(Guid id, params string[] collectionsName)
        where TEntity : Entity
    {
        var entities = Context.Set<TEntity>();

        return await entities.IncludeMultipleWithNoTracking(collectionsName).FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : Entity
    {
        var set = Context.Set<TEntity>();
        return await set.FindAsync(id);
    }

    public virtual PaginatedList<TEntity> Get<TEntity>(
        int page,
        PageSize pageSize = PageSize.Ten,
        Func<TEntity, bool>? filter = null,
        string? orderByPropertyName = null) where TEntity : Entity
    {
        var set = Context.Set<TEntity>();

        var result = set.AsEnumerable();

        if (filter != null) result = set.Where(filter);
        if (orderByPropertyName != null) result = result.AsQueryable().OrderBy(orderByPropertyName);

        return PaginatedList<TEntity>.Create(result, page, pageSize);
    }

    public virtual PaginatedList<TEntity> GetWithInclude<TEntity>(
        string collectionName,
        int page,
        PageSize pageSize = PageSize.Ten,
        Func<TEntity, bool>? filter = null,
        string? orderByPropertyName = null) where TEntity : Entity
    {
        var set = Context.Set<TEntity>().Include(collectionName);

        var result = set.AsEnumerable();

        if (filter != null) result = set.Where(filter);
        if (orderByPropertyName != null) result = result.AsQueryable().OrderBy(orderByPropertyName);

        return PaginatedList<TEntity>.Create(result, page, pageSize);
    }


    public virtual async Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter) 
        where TEntity : Entity
    {
        var set = Context.Set<TEntity>();
        return await set.FirstOrDefaultAsync(filter);
    }


    #endregion


    public virtual void Update<TEntity>(TEntity entity) where TEntity : Entity
    {
        var set = Context.Set<TEntity>();

        set.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }


    #region Delete

    public virtual async Task<bool> DeleteAsync<TEntity>(Guid id) where TEntity : Entity
    {
        var set = Context.Set<TEntity>();

        var entity = await set.FindAsync(id);
        if (entity is null) return false;

        return await Delete(set, entity);
    }

    protected async Task<bool> Delete<TEntity>(DbSet<TEntity> set, TEntity entity) where TEntity : Entity
    {
        if (Context.Entry(entity).State == EntityState.Detached)
            set.Attach(entity);

        set.Remove(entity);

        await Context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity
    {
        var set = Context.Set<TEntity>();

        return await Delete(set, entity);
    }

    #endregion


    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}