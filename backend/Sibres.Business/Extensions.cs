namespace Sibres.Business;

public static class Extensions
{
    public static IEnumerable<TSource>? OrderBy<TSource>(
        this IQueryable<TSource> query, string propertyName)
    {
        var entityType = typeof(TSource);

        var propertyInfo = entityType.GetProperty(propertyName);
        var arg = Expression.Parameter(entityType, "x");
        var property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, arg);

        var enumarableType = typeof(Queryable);

        var method = enumarableType
            .GetMethods()
            .Where(m => m.Name == "OrderBy" && 
                        m.IsGenericMethodDefinition).
            FirstOrDefault(m => m.GetParameters().Length == 2);

        if (method is null) throw new ArgumentException();

        var genericMethod = method.MakeGenericMethod(entityType, propertyInfo?.PropertyType);
        
        var queryable = (IOrderedQueryable<TSource>)genericMethod
            .Invoke(genericMethod, new object[] { query, selector });

        return queryable?.AsEnumerable();
    }

    internal static IQueryable<T> IncludeMultipleWithNoTracking<T>(this IQueryable<T> query, 
        params string[] includes) where T : class
    {
        return includes.Aggregate(query, (current, include) => current.Include(include).AsNoTracking());
    }
}