using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskApiService _taskApiService;

        public TaskController(ITaskApiService taskApiService)
        {
            _taskApiService = taskApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeExpired = false,
            [FromQuery] bool onlyExpired = false,
            [FromQuery] int? categoryId = null,
            [FromQuery] int? statusId = null)
        {
            var tasks = await _taskApiService.GetTasksAsync(
                includeExpired,
                onlyExpired,
                categoryId,
                statusId);

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskApiService.GetByIdAsync(id);

            return task != null
                ? Ok(task)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskModel taskModel)
        {
            var task = await _taskApiService.AddAsync(taskModel);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateTaskModel taskModel)
        {
            var updatedTask = await _taskApiService.EditTaskAsync(id, taskModel);

            return Ok(updatedTask);
        }

        [HttpPatch("{id}/change-status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] int statusId)
        {
            var updatedTask = await _taskApiService.ChangeStatusAsync(id, statusId);

            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskApiService.DeleteAsync(id);

            return NoContent();
        }
    }
}
