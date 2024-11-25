namespace WorkItemService.DAL.Entities
{
    public class User
    {
        public string Username { get; set; }
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
