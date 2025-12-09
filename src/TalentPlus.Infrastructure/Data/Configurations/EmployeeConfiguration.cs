using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentPlus.Domain.Entities;
using TalentPlus.Domain.Enums;

namespace TalentPlus.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Document)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(e => e.Document).IsUnique();

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.BirthDate)
                .IsRequired();

            builder.Property(e => e.Address)
                .HasMaxLength(300);

            builder.Property(e => e.Phone)
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.Salary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.HireDate)
                .IsRequired();

            builder.Property(e => e.ProfessionalProfile)
                .HasMaxLength(1000);

            builder.Property(e => e.State)
                .HasConversion<int>()
                .IsRequired();
            
            builder.HasOne(e => e.Position)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.EducationalLevel)
                .WithMany(el => el.Employees)
                .HasForeignKey(e => e.EducationalLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => new { e.FirstName, e.LastName });
        }
    }
}