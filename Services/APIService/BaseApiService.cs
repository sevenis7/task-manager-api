using TaskManager.Services.Mappers;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager.Services.APIService
{
    public abstract class BaseApiService<TEntity, TCreateModel, TDto> : IBaseApiService<TCreateModel, TDto> 
        where TEntity : class
        where TCreateModel : class
        where TDto : class
    {
        protected readonly IBaseService<TEntity, TCreateModel> _service;
        protected readonly IEntityMapper<TEntity, TDto> _mapper;

        protected BaseApiService(IBaseService<TEntity, TCreateModel> service, IEntityMapper<TEntity, TDto> mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var existedEntity = await _service.GetByIdASync(id);

            if (existedEntity == null)
                return null;

            return _mapper.MapToDto(existedEntity);
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var col = await _service.GetAllAsync();

            return _mapper.MapCollectionToDto(col);
        }

        public virtual async Task<TDto> AddAsync(TCreateModel model)
        {
            try
            {
                var entity = await _service.AddAsync(model);

                return _mapper.MapToDto(entity);
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
