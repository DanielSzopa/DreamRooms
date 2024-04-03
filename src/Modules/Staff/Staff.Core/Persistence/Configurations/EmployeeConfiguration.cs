using BuildingBlocks.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Staff.Core.Domain.Entities;
using Staff.Core.Domain.ValueObjects;

namespace Staff.Core.Persistence.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new EmployeeId(x));

        builder.Property(e => e.FirstName)
            .HasConversion(f => f.Value, f => new FirstName(f));

        builder.Property(e => e.LastName)
            .HasConversion(l => l.Value, l => new LastName(l));

        builder.Property(e => e.Email)
            .HasConversion(e => e.Value, e => new Email(e));

        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x.Value,x=> new PhoneNumber(x));

        builder.Property(x => x.Role)
            .HasConversion(x => x.Value, x => new Role(x));

        builder.Property(x => x.PasswordHash)
            .HasConversion(x => x.Value, x => new PasswordHash(x));
    }
}
