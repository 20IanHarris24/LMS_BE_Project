using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        //public DbSet<ApplicationUser> Users { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Module> Modules { get; set; } = default!;
        public DbSet<Activity> Activitys { get; set; }

    }
}
