
using GreenHealth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHealth.Persistence.EntityConfigurations
{
    public class PatientStatusConfiguration : IEntityTypeConfiguration<PatientStatus>
    {
        public void Configure(EntityTypeBuilder<PatientStatus> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);
        }
    }
}