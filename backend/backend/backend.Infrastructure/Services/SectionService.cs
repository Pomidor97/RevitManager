using backend.Contracts.DTOs;
using backend.core.Entities;
using backend.core.Interfaces;

namespace backend.Infrastructure.Services;

public class SectionService : ISectionService
{
    private readonly ISectionRepository _repo;

    public SectionService(ISectionRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Section>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Section?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(CreateSectionDto dto)
    {
        var section = new Section
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
        return _repo.AddAsync(section);
    }

    public async Task UpdateAsync(Guid id, UpdateSectionDto dto)
    {
        var section = await _repo.GetByIdAsync(id);
        if (section is not null)
        {
            section.Name = dto.Name;
            await _repo.UpdateAsync(section);
        }
    }

    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}