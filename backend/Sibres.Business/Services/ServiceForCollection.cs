namespace Sibres.Business.Services;

public class ServiceForCollection : Service
{
    public ServiceForCollection(SibersContext context) : base(context)
    {
    }

    public async Task<ICollection<TProperty>?> GetCollection<TEntity, TProperty, TDiff>(Guid entityId, string collectionName)
        where TEntity : Entity, IEntityWithCollection<TProperty, TDiff>
        where TProperty : Entity
    {
        TDiff? diff = default;

        var value = await GetWithIncludeAsync<TEntity>(entityId, collectionName);
        return value?.Get(diff);
    }

    public virtual async Task<TEntity?> AddToCollection<TEntity, TProperty, TDiff>(Guid entityId, Guid propertyId, string collectionName)
        where TEntity : Entity, IEntityWithCollection<TProperty, TDiff>
        where TProperty : Entity
    {
        TDiff? diff = default;

        var entities = Context.Set<TEntity>();
        var properties = Context.Set<TProperty>();

        var entity = await entities
            .Include(collectionName)
            .FirstOrDefaultAsync(e => e.Id == entityId);

        var property = properties.FirstOrDefault(p => p.Id == propertyId);

        if (entity == null || property == null) return null;

        var collection = entity.Get(diff);

        if (collection == null) return null;
        if (collection.Contains(property)) collection.Remove(property);
        entity.Get(diff)?.Add(property);

        return entity;
    }

    public async Task<bool> RemoveFromCollection<TEntity, TProperty, TDiff>(Guid entityId, Guid propertyId, string collectionName)
        where TEntity : Entity, IEntityWithCollection<TProperty, TDiff>
        where TProperty : Entity
    {
        TDiff? diff = default;

        var entities = Context.Set<TEntity>();
        var properties = Context.Set<TProperty>();

        var entity = await entities
            .Include(collectionName)
            .FirstOrDefaultAsync(e => e.Id == entityId);

        var property = properties.FirstOrDefault(p => p.Id == propertyId);

        if (entity == null || property == null) return false;
        var collection = entity.Get(diff);
        if (collection == null) return false;

        return collection.Contains(property) && collection.Remove(property);
    }
}
