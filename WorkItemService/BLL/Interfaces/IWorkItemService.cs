using WorkItemService.DAL.Entities;

namespace WorkItemService.BLL.Interfaces
{
    public interface IWorkItemService
    {
        List<WorkItem> GetAllWorkItems();
        WorkItem GetWorkItemById(int id);
        void AddWorkItem(WorkItem workItem);
        void UpdateWorkItem(WorkItem workItem);
        void DeleteWorkItem(int id);
        List<WorkItem> GetPendingWorkItemsByUserId(int userId);
    }
}
