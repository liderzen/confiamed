namespace WorkItemService.Entities
{
    public class User
    {
        public string Username { get; set; }
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
