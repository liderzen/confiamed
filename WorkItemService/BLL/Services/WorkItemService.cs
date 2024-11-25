using WorkItemService.BLL.Interfaces;
using WorkItemService.DAL.Entities;
using WorkItemService.DAL.Repositories;

namespace WorkItemService.BLL.Services
{
    public class WorkItemService: IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemService(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public List<WorkItem> GetAllWorkItems()
        {
            return _workItemRepository.GetAllWorkItems();
        }

        public WorkItem GetWorkItemById(int id)
        {
            return _workItemRepository.GetWorkItemById(id);
        }

        public void AddWorkItem(WorkItem workItem)
        {
            // Validaciones antes de agregar
            if (string.IsNullOrEmpty(workItem.Title))
            {
                throw new ArgumentException("El título no puede estar vacío.");
            }

            _workItemRepository.AddWorkItem(workItem);
        }

        public void UpdateWorkItem(WorkItem workItem)
        {
            // Validaciones antes de actualizar
            if (workItem.Id <= 0)
            {
                throw new ArgumentException("El ID del ítem de trabajo debe ser válido.");
            }

            _workItemRepository.UpdateWorkItem(workItem);
        }

        public void DeleteWorkItem(int id)
        {
            _workItemRepository.DeleteWorkItem(id);
        }

        public List<WorkItem> GetPendingWorkItemsByUserId(int userId)
        {
            return _workItemRepository.GetPendingWorkItemsByUserId(userId);
        }
    }
}
