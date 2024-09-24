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

            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Teacher", NormalizedName = "TEACHER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Student", NormalizedName = "STUDENT" }
            );

            // Seed Courses
            var course1Id = Guid.NewGuid();
            var course2Id = Guid.NewGuid();

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = course1Id, Name = "Mathematics 101", Description = "Intro to Math", Start = DateTime.UtcNow },
                new Course { Id = course2Id, Name = "Physics 101", Description = "Intro to Physics", Start = DateTime.UtcNow }
            );

            // Seed Users
            var teacherId = Guid.NewGuid().ToString();
            var studentId1 = Guid.NewGuid().ToString();
            var studentId2 = Guid.NewGuid().ToString();

            var teacher = new UserForRegistrationDto
            {
                UserName = "Teacher",
                Email = "teacher@example.com",
                Password = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!"),
                Role = "Teacher",
                CourseID = course1Id.ToString(),
            };

            var student1 = new ApplicationUser
            {
                Id = studentId1,
                UserName = "student1@example.com",
                NormalizedUserName = "STUDENT1@EXAMPLE.COM",
                Email = "student1@example.com",
                NormalizedEmail = "STUDENT1@EXAMPLE.COM",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                CourseId = course1Id
            };

            var student2 = new ApplicationUser
            {
                Id = studentId2,
                UserName = "student2@example.com",
                NormalizedUserName = "STUDENT2@EXAMPLE.COM",
                Email = "student2@example.com",
                NormalizedEmail = "STUDENT2@EXAMPLE.COM",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                CourseId = course2Id
            };

            modelBuilder.Entity<ApplicationUser>().HasData( student1, student2);
        }
    }
}
