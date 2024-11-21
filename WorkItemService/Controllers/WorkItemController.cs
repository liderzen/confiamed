using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorkItemService.Entities;

namespace WorkItemService.Controllers
{
    [ApiController]
    [Route("api/workitems")]
    public class WorkItemController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<WorkItemController> _logger;

        private readonly List<WorkItem> _workItems;

        public WorkItemController(IHttpClientFactory httpClientFactory, ILogger<WorkItemController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("UserService");
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetWorkItems()
        {
            /*var items = new List<object>
            {
                new { Title = "Error login", DueDate = "2024-11-22", Priority = "High" },
                new { Title = "Error dashboard", DueDate = "2024-11-28", Priority = "Low" }
            };*/

            return Ok(_workItems);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersFromUserService()
        {
            var response = await _httpClient.GetAsync("api/users");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int) response.StatusCode, "Error al consultar el servicio: UserService");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var users = JsonSerializer.Deserialize<List<User>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Ok(users);
        }

        [HttpPost("distribute")]
        public async Task<IActionResult> DistributeWorkItems()
        {
            var response = await GetUsersFromUserService() as OkObjectResult;
            var users = response?.Value as List<User>;

            if (users == null || !users.Any())
            {
                return BadRequest("No existen usuarios disponibles.");
            }

            DistributeWorkItems(users, _workItems);
            return Ok(new { Message = "Ítems distribuidos correctamente.", Users = users });
        }

        private void DistributeWorkItems(List<User> users, List<WorkItem> workItems)
        {
            var now = DateTime.Now;

            // Ítems urgentes
            var urgentItems = workItems
                .Where(item => !item.IsCompleted && (item.DueDate - now).TotalDays < 3)
                .OrderBy(item => item.DueDate)
                .ToList();

            var relevantItems = workItems
                .Where(item => !item.IsCompleted && (item.DueDate - now).TotalDays >= 3 && item.Relevance == "Alta")
                .OrderBy(item => item.DueDate)
                .ToList();

            // Urgentes
            foreach (var item in urgentItems)
            {
                var user = GetUserWithLeastWorkItems(users);
                if (user != null)
                {
                    item.AssignedUser = user.Username;
                    user.WorkItems.Add(item);
                }
            }

            // Relevantes
            foreach (var item in relevantItems)
            {
                var user = GetUserWithLeastWorkItems(users);
                if (user != null)
                {
                    item.AssignedUser = user.Username;
                    user.WorkItems.Add(item);
                }
            }

            // Pendientes
            foreach (var user in users)
            {
                user.WorkItems = user.WorkItems
                    .Where(workItem => !workItem.IsCompleted)
                    .OrderBy(workItem => workItem.DueDate)
                    .ToList();
            }
        }

        private User GetUserWithLeastWorkItems(List<User> users)
        {
            return users
                .Where(user => user.WorkItems.Count(workItem => !workItem.IsCompleted) <= 3)
                .OrderBy(user => user.WorkItems.Count(workItem => !workItem.IsCompleted))
                .FirstOrDefault();
        }

    }
}
