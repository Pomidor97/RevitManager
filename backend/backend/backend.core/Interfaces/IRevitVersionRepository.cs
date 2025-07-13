using backend.core.Entities;

namespace backend.core.Interfaces;

public interface IRevitVersionRepository
{
    Task<List<RevitVersion>> GetAllAsync();
    Task<RevitVersion?> GetByIdAsync(Guid id);
    Task AddAsync(RevitVersion version);
    Task UpdateAsync(RevitVersion version);
    Task DeleteAsync(Guid id);
}