
using GreenHealth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHealth.Persistence.EntityConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d =>  d.Id);
            builder.Property(d => d.SpecializationId).IsRequired();
            builder.Property(d => d.Name).IsRequired().HasMaxLength(255);
            builder.Property(d => d.Phone).IsRequired();
        }
    }
}