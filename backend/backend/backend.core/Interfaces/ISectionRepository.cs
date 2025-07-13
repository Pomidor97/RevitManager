using backend.core.Entities;

namespace backend.core.Interfaces;

public interface ISectionRepository
{
    Task<List<Section>> GetAllAsync();
    Task<Section?> GetByIdAsync(Guid id);
    Task AddAsync(Section section);
    Task UpdateAsync(Section section);
    Task DeleteAsync(Guid id);
}