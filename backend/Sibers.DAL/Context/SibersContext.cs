namespace Sibers.Data.Context;

public class SibersContext : DbContext
{
    public DbSet<Company> Company { get; set; } = null!;
    public DbSet<Project> Project { get; set; } = null!;
    public DbSet<Employee> Employee { get; set; } = null!;
    public DbSet<Goal> Goal { get; set; } = null!;

    public SibersContext(DbContextOptions<SibersContext> options) : base(options) {}
}