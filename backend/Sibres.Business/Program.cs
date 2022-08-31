namespace Sibres.Business;

public static class Program
{
    public static void AddSqlDb(this IServiceCollection services, string conn)
    {
        services.AddSqlDbCore(conn);

        services.AddScoped<ServiceForCollection>();
        services.AddScoped<IService, Service>();
    }

    public static async Task SeedData(this IServiceProvider provider)
    {
        await provider.Seed();
    }
}