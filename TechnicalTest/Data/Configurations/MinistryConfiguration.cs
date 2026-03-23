using TechnicalTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnicalTest.Data.Configurations
{
    public class MinistryConfiguration : IEntityTypeConfiguration<Ministry>
    {
        public void Configure(EntityTypeBuilder<Ministry> builder)
        {
            builder
                .HasIndex(col => col.NormalizedName)
                .IsUnique();
        }
    }
}
