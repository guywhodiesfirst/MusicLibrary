﻿namespace Business.Interfaces
{
    public interface IService<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task AddAsync(TDto model);
        Task DeleteAsync(int modelId);
        Task UpdateAsync(TDto model);
    }
}
