using backend.Contracts.DTOs;
using backend.core.Entities;

namespace backend.core.Interfaces;

public interface IRevitVersionService
{
    Task<List<RevitVersion>> GetAllAsync();
    Task<RevitVersion?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateRevitVersionDto dto);
    Task UpdateAsync(Guid id, UpdateRevitVersionDto dto);
    Task DeleteAsync(Guid id);
}