using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Module> Modules { get; set; } = default!;
        public DbSet<Activity> Activities { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Courses
            var course1Id = Guid.NewGuid();
            var course2Id = Guid.NewGuid();

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = course1Id, Name = "Mathematics 101", Description = "Intro to Math", Start = DateTime.UtcNow },
                new Course { Id = course2Id, Name = "Physics 101", Description = "Intro to Physics", Start = DateTime.UtcNow }
            );
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; } 

        }
    }
}
