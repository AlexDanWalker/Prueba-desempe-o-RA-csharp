using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Infrastructure.Data.Configurations
{
    public class EducationalLevelConfiguration : IEntityTypeConfiguration<EducationalLevel>
    {
        public void Configure(EntityTypeBuilder<EducationalLevel> builder)
        {
            builder.ToTable("EducationalLevels");

            builder.HasKey(el => el.Id);
            builder.Property(el => el.Id).ValueGeneratedOnAdd();

            builder.Property(el => el.LevelName)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(el => el.LevelName).IsUnique();
            
            builder.HasMany(el => el.Employees)
                .WithOne(e => e.EducationalLevel)
                .HasForeignKey(e => e.EducationalLevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}