using backend.core.Entities;

namespace backend.core.Interfaces;

public interface IManufacturerRepository
{
    Task<List<Manufacturer>> GetAllAsync();
    Task<Manufacturer?> GetByIdAsync(Guid id);
    Task AddAsync(Manufacturer manufacturer);
    Task UpdateAsync(Manufacturer manufacturer);
    Task DeleteAsync(Guid id);
}