
using GreenHealth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHealth.Persistence.EntityConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.CityId).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(255);
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.Token).IsRequired();
            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient);
            
        }
    }
}