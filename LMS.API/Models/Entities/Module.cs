namespace LMS.API.Models.Entities
{
    public class Module
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public IEnumerable<Activity>? Activities { get; set; }
    }
}
