namespace Akvelon.Models
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public enum StatusOfProject
        {
            NotStarted = 0,
            Active = 1,
            Completed = 2
        }
        public StatusOfProject Status { get; set; }
        public int Priority { get; set; }
        public string Tasks { get; set; } = string.Empty;
        public string TasksIds { get; set; } = string.Empty;
    }
}
