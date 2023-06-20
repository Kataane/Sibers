namespace Sibers.Data;

public static class Program
{
    public static void AddSqlDbCore(this IServiceCollection services, string con)
    {
        services.AddDbContext<SibersContext>(options =>
            options.UseSqlServer(con));
    }
}