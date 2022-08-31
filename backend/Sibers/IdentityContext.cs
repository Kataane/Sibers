using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Sibers;

public class IdentityContext : IdentityDbContext<EmployeeUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }
}