using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Контроллер управления задачами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskApiService _taskApiService;

        public TaskController(ITaskApiService taskApiService)
        {
            _taskApiService = taskApiService;
        }

        /// <summary>
        /// Метод получения задач
        /// </summary>
        /// <param name="includeExpired">Параметр, включающий истекшие задачи по сроку истечения. По умолчанию false.</param>
        /// <param name="onlyExpired">Параметр, включающий ТОЛЬКО истекшие задачи по сроку истечения. По умолчанию false</param>
        /// <param name="categoryId">ID параметр выбранной категории. Без указания будут получены задачи всех категорий.</param>
        /// <param name="statusId">ID параметр выбранного статуса. Без указания будут получени задачи со всеми статусами</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeExpired = false,
            [FromQuery] bool onlyExpired = false,
            [FromQuery] int? categoryId = null,
            [FromQuery] int? statusId = null)
        {

            var userId = GetUserId();

            if (userId is null)
                return Unauthorized();

            var tasks = await _taskApiService.GetTasksAsync(
                userId.Value,
                includeExpired,
                onlyExpired,
                categoryId,
                statusId);

            return Ok(tasks);
        }

        /// <summary>
        /// Метод получения задачи
        /// </summary>
        /// <param name="id">ID задачи</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();

            if (userId is null)
                return Unauthorized();

            var task = await _taskApiService.GetByIdAsync(id, userId.Value);

            return task != null
                ? Ok(task)
                : NotFound();
        }

        /// <summary>
        /// Метод создания задачи
        /// </summary>
        /// <param name="taskModel">Модель задачи CreateTaskModel, принимаемая из тела запроса</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskModel taskModel)
        {
            var userId = GetUserId();

            if (userId is null)
                return Unauthorized();

            var task = await _taskApiService.AddAsync(taskModel, userId.Value);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        /// <summary>
        /// Метод изменения задачи
        /// </summary>
        /// <param name="id">ID изменяемой задачи</param>
        /// <param name="taskModel">Модель задачи UpdateTaskModel, принимаемая из тела запроса</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateTaskModel taskModel)
        {
            var userId = GetUserId();

            if (userId is null)
                return Unauthorized();

            var updatedTask = await _taskApiService.EditTaskAsync(id, taskModel, userId.Value);

            return Ok(updatedTask);
        }

        /// <summary>
        /// Метод изменения статуса задачи
        /// </summary>
        /// <param name="id">ID задачи в которой меняется статус</param>
        /// <param name="statusId">ID статуса присваиваемый задаче</param>
        /// <returns></returns>
        [HttpPatch("{id}/change-status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] int statusId)
        {
            var userId = GetUserId();

            if (userId is null)
                return Unauthorized();

            var updatedTask = await _taskApiService.ChangeStatusAsync(id, statusId, userId.Value);

            return Ok(updatedTask);
        }


        /// <summary>
        /// Метод удаления задачи
        /// </summary>
        /// <param name="id">ID удаляемой задачи</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            if (userId is null)
                return Unauthorized();

            await _taskApiService.DeleteAsync(id, userId.Value);

            return NoContent();
        }

        private int? GetUserId()
        {
            var stringUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (stringUserId == null) 
                return null;

            return Convert.ToInt32(stringUserId);
        }
    }
}
