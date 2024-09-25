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

            modelBuilder.Entity<Module>().HasData(
                new Module { Id = Guid.NewGuid(), Name = "Functions", Description = "Intro to Functions", Start = DateTime.UtcNow, End = DateTime.UtcNow.AddMonths(1), CourseId = course1Id},
                new Module { Id = Guid.NewGuid(), Name = "Polynomials", Description = "Intro to Polynomials", Start = DateTime.UtcNow.AddMonths(1), End = DateTime.UtcNow.AddMonths(2), CourseId = course1Id },
                new Module { Id = Guid.NewGuid(), Name = "Vektors", Description = "Intro to Vektors", Start = DateTime.UtcNow, End = DateTime.UtcNow.AddMonths(1), CourseId = course2Id },
                new Module { Id = Guid.NewGuid(), Name = "Kimenatics", Description = "Intro to Kinematics", Start = DateTime.UtcNow.AddMonths(1), End = DateTime.UtcNow.AddMonths(2), CourseId = course2Id }

             );


        }
    }
}
