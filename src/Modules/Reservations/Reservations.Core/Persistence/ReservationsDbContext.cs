using Microsoft.EntityFrameworkCore;
using Reservations.Core.Domain.Entities;

namespace Reservations.Core.Persistence;
internal class ReservationsDbContext : DbContext
{
    internal DbSet<Receptionist> Receptionists { get; set; }

    public ReservationsDbContext(DbContextOptions<ReservationsDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.ReservationsSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
