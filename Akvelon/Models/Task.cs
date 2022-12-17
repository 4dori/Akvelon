namespace Akvelon.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string ProjectName { get; set; }
        public Guid ProjectId { get; set; }

    }
}
