using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Start { get; set; }
        //[Required]
        //public DateTime End { get; set; }

        public IEnumerable<Module> Modules { get; set; }
        public IEnumerable<ApplicationUser>? Users { get; set; }
    }
}
