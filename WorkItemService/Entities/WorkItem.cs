namespace WorkItemService.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Relevance { get; set; }
        public string AssignedUser { get; set; }
        public bool IsCompleted { get; set; }
    }
}
