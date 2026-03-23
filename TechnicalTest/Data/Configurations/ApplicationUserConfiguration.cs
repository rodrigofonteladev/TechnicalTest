using TechnicalTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnicalTest.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasIndex(col => col.UserName)
                .IsUnique();

            builder
                .HasIndex(col => col.DocumentNumber)
                .IsUnique();

            builder
                .HasIndex(col => col.Email)
                .IsUnique();
        }
    }
}
