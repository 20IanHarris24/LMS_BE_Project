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
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Courses
            var course1Id = Guid.Parse("6f01e571-41f0-4789-8059-422ae07d736e");
            var course2Id = Guid.Parse("a767cdee-e833-427a-9349-3ee71cca8a39");

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
        public DbSet<ActivityType> ActivityType { get; set; } 

        
    }
}
