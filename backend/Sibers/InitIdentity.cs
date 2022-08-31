namespace Sibers;

public static class InitIdentity
{
    private static readonly string[] RoleNames = Roles.GetRoles;

    private static readonly char[] NonAlphanumeric =
    {
        'A', 'b', 'C', 'd', 'E', 'f', 'G',
        'I', 'i', 'j', 'J',
        '0', '1', '2', '3', '4', '5', '6',
        '!', '#', '@'
    };

    public static async Task SeedIdentity(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();

        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        var projectContext = scope.ServiceProvider.GetRequiredService<SibersContext>();

        await identityContext.Database.EnsureDeletedAsync();
        await identityContext.Database.EnsureCreatedAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<EmployeeUser>>();

        foreach (var roleName in RoleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        await AddUsers(projectContext, userManager);

        await identityContext.SaveChangesAsync();
        await projectContext.SaveChangesAsync();
    }

    private static async Task AddUsers(SibersContext sibersContext, UserManager<EmployeeUser> userManager)
    {
        var employees = await sibersContext.Employee.ToListAsync();
        var random = new Random();

        foreach (var employee in employees)
        {
            var role = RoleNames[random.Next(0, RoleNames.Length)];
            var user = new EmployeeUser { Role = role, EmployeeId = employee.Id, Email = employee.Email, UserName = employee.Email };
             var password = Password(25);
             var result = await userManager.CreateAsync(user, password);

             employee.Password = password;

             if (!result.Succeeded) continue;

             await userManager.AddToRoleAsync(user, role);
        }
    }

    private static string Password(byte length)
    {
        var @string = string.Empty;
        var random = new Random();

        for (byte a = 0; a < length; a++) {
            @string += NonAlphanumeric[random.Next(0, NonAlphanumeric.Length - 1)];
        };

        return @string;
    }
}