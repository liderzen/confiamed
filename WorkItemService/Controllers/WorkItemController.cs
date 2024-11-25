using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorkItemService.BLL.Interfaces;
using WorkItemService.DAL.Entities;

namespace WorkItemService.Controllers
{
    [ApiController]
    [Route("api/workitems")]
    public class WorkItemController : ControllerBase
    {

        private readonly IWorkItemService _workItemService;

        public WorkItemController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        [HttpGet]
        public ActionResult<List<WorkItem>> GetAll()
        {
            return Ok(_workItemService.GetAllWorkItems());
        }

        [HttpGet("{id}")]
        public ActionResult<WorkItem> GetById(int id)
        {
            var workItem = _workItemService.GetWorkItemById(id);

            if (workItem == null)
            {
                return NotFound();
            }

            return Ok(workItem);
        }

        [HttpPost]
        public IActionResult Add(WorkItem workItem)
        {
            try
            {
                _workItemService.AddWorkItem(workItem);
                return CreatedAtAction(nameof(GetById), new { id = workItem.Id }, workItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, WorkItem workItem)
        {
            if (id != workItem.Id)
            {
                return BadRequest("El ID no coincide.");
            }

            try
            {
                _workItemService.UpdateWorkItem(workItem);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workItemService.DeleteWorkItem(id);
            return NoContent();
        }

        [HttpGet("pending/{userId}")]
        public ActionResult<List<WorkItem>> GetPendingByUserId(int userId)
        {
            return Ok(_workItemService.GetPendingWorkItemsByUserId(userId));
        }

    }
}
