namespace Sibers.Data.Context;

public class SibersContext : DbContext
{
    public DbSet<Project> Project { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Implementer> Implementer { get; set; }
    public DbSet<Supervisor> Supervisor { get; set; }
    public DbSet<Goal> Goal { get; set; }

    public SibersContext(DbContextOptions<SibersContext> options) : base(options)
    {
    }
}