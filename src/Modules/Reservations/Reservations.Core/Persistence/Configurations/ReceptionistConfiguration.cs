using BuildingBlocks.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Core.Domain.Entities;
using Reservations.Core.Domain.ValueObjects;

namespace Reservations.Core.Persistence.Configurations;
internal class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
{
    public void Configure(EntityTypeBuilder<Receptionist> builder)
    {
        builder.HasKey(r => r.EmployeeId);

        builder.Property(r => r.EmployeeId)
            .HasConversion(x => x.Value, x => new EmployeeId(x));

        builder.Property(r => r.FullName)
            .HasConversion(x => x.Value, x => new FullName(x));

        builder.Property(r => r.Email)
            .HasConversion(x => x.Value, x => new Email(x));
    }
}
