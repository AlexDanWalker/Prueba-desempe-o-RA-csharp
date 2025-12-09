using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Infrastructure.Data
{
    // Heredamos de IdentityDbContext con IdentityUser y IdentityRole
    public class TalentPlusDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public TalentPlusDbContext(DbContextOptions<TalentPlusDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<EducationalLevel> EducationalLevels { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primero se llama al base para configurar Identity
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones propias
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalentPlusDbContext).Assembly);

            // Converter para DateOnly
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue),
                dt => DateOnly.FromDateTime(dt));

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(DateOnly))
                    {
                        property.SetValueConverter(dateOnlyConverter);
                    }
                }
            }

            // Seed data con int como PK
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Human Resources" },
                new Department { Id = 2, Name = "Technology" },
                new Department { Id = 3, Name = "Finance" }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Auxiliary" },
                new Position { Id = 2, Name = "Analyst" },
                new Position { Id = 3, Name = "Manager" }
            );

            modelBuilder.Entity<EducationalLevel>().HasData(
                new EducationalLevel { Id = 1, LevelName = "High School" },
                new EducationalLevel { Id = 2, LevelName = "Bachelor" },
                new EducationalLevel { Id = 3, LevelName = "Master" }
            );
        }
    }
}
