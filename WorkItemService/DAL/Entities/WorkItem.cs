namespace WorkItemService.DAL.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string Relevance { get; set; }
        public int AssignedUserId { get; set; } // ID del usuario asignado
        public bool IsCompleted { get; set; }
    }
}
