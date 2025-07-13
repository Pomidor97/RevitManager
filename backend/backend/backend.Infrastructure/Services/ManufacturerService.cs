using backend.Contracts.DTOs;
using backend.core.Entities;
using backend.core.Interfaces;

namespace backend.Infrastructure.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _repo;

    public ManufacturerService(IManufacturerRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Manufacturer>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Manufacturer?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(CreateManufacturerDto dto)
    {
        var entity = new Manufacturer
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
        return _repo.AddAsync(entity);
    }

    public async Task UpdateAsync(Guid id, UpdateManufacturerDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is not null)
        {
            entity.Name = dto.Name;
            await _repo.UpdateAsync(entity);
        }
    }

    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}