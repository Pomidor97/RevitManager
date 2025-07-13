using backend.Contracts.DTOs;
using backend.core.Entities;
using backend.core.Interfaces;

namespace backend.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Category>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Category?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
        return _repo.AddAsync(category);
    }

    public async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category is not null)
        {
            category.Name = dto.Name;
            await _repo.UpdateAsync(category);
        }
    }

    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}
