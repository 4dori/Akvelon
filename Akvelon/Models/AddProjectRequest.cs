namespace Akvelon.Models
{
    public class AddProjectRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public Project.StatusOfProject Status { get; set; }

        public int Priority { get; set; }
    }
}
