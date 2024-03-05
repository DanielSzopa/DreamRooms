using Microsoft.EntityFrameworkCore;
using Staff.Core.Domain.Entities;

namespace Staff.Core.Persistence;

internal class StaffDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("staff");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
