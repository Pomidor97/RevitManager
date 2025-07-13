using backend.Contracts.DTOs;
using backend.core.Entities;

namespace backend.core.Interfaces;

public interface IManufacturerService
{
    Task<List<Manufacturer>> GetAllAsync();
    Task<Manufacturer?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateManufacturerDto dto);
    Task UpdateAsync(Guid id, UpdateManufacturerDto dto);
    Task DeleteAsync(Guid id);
}