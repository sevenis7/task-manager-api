using Microsoft.AspNetCore.Mvc;
using TaskManager.Services.APIService;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TCreateModel, TDto> : ControllerBase
        where TCreateModel : class
        where TDto : class
    {
        private readonly IBaseApiService<TCreateModel, TDto> _apiService;

        protected BaseController(IBaseApiService<TCreateModel, TDto> apiService)
        {
            _apiService = apiService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _apiService.GetByIdAsync(id);

            return entity != null
                ? Ok(entity)
                : NotFound();
        }
    }
}
