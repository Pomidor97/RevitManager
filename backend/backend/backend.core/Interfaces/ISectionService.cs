using backend.Contracts.DTOs;
using backend.core.Entities;


namespace backend.core.Interfaces;

public interface ISectionService
{
    Task<List<Section>> GetAllAsync();
    Task<Section?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateSectionDto dto);
    Task UpdateAsync(Guid id, UpdateSectionDto dto);
    Task DeleteAsync(Guid id);
}