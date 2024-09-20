namespace LMS.API.Models.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }

        public IEnumerable<Module> Modules { get; set; }
        public IEnumerable<ApplicationUser>? Users { get; set; }
    }
}
