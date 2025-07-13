using backend.Contracts.DTOs;
using backend.core.Entities;
using backend.core.Interfaces;

namespace backend.Infrastructure.Services;

public class RevitVersionService : IRevitVersionService
{
    private readonly IRevitVersionRepository _repo;

    public RevitVersionService(IRevitVersionRepository repo)
    {
        _repo = repo;
    }

    public Task<List<RevitVersion>> GetAllAsync() => _repo.GetAllAsync();

    public Task<RevitVersion?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(CreateRevitVersionDto dto)
    {
        var entity = new RevitVersion
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
        return _repo.AddAsync(entity);
    }

    public async Task UpdateAsync(Guid id, UpdateRevitVersionDto dto)
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