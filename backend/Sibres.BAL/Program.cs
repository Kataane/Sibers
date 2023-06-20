namespace Sibres.Business;

public static class Program
{
    public static async Task SeedData(this IServiceProvider provider)
    {
        await provider.Seed();
    }
}