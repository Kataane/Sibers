namespace Sibers.Data;

public static class DatabaseInitializer
{
    public static Faker<Employee> RuleForEmployee = new Faker<Employee>()
        .RuleFor(p => p.Email, f => f.Internet.Email())
        .RuleFor(p => p.Name, f => f.Name.FirstName())
        .RuleFor(p => p.Patronymic, f => f.Name.LastName())
        .RuleFor(p => p.Surname, f => f.Name.LastName());

    public static Faker<Client> RuleForClient = new Faker<Client>()
        .RuleFor(c => c.Name, f => f.Company.CompanyName());
    public static Faker<Implementer> RuleForImplementer = new Faker<Implementer>()
        .RuleFor(c => c.Name, f => f.Company.CompanyName());

    public static Faker<Project> RuleForProject = new Faker<Project>()
        .RuleFor(c => c.Name, f => f.Company.CompanyName())
        .RuleFor(c => c.StartTime, f => f.Date.Past())
        .RuleFor(c => c.EndTime, f => f.Date.Future())
        .RuleFor(c => c.Priority, f => f.Random.Int(0, 10));

    public static Faker<Goal> RuleForGoal = new Faker<Goal>()
        .RuleFor(c => c.Name, f => f.Lorem.Slug(4))
        .RuleFor(g => g.Priority, f => f.Random.Int(0, 10))
        .RuleFor(g => g.Status, f => (GoalStatus)f.Random.Int(1, 3));

    public static async Task Seed(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<SibersContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        await context.Employee.AddRangeAsync(RuleForEmployee.Generate(500));
        await context.Client.AddRangeAsync(RuleForClient.Generate(100));
        await context.Implementer.AddRangeAsync(RuleForImplementer.Generate(100));
        await context.Project.AddRangeAsync(RuleForProject.Generate(100));
        await context.Goal.AddRangeAsync(RuleForGoal.Generate(100));

        await context.SaveChangesAsync();
    }
}