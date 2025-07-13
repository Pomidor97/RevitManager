using backend.Contracts.DTOs;
using backend.core.Entities;

namespace backend.core.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateCategoryDto dto);
    Task UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task DeleteAsync(Guid id);
}
