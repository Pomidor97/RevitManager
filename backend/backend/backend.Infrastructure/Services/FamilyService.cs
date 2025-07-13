using backend.core.Entities;
using backend.core.Interfaces;
using backend.Contracts.DTOs;
using Microsoft.AspNetCore.Hosting;

namespace backend.infrastructure.Services;

public class FamilyService : IFamilyService
{
    private readonly IFamilyRepository _repo;
    private readonly IWebHostEnvironment _env;

    public FamilyService(IFamilyRepository repo, IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }

    public async Task UploadAsync(FamilyUploadDto dto)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
        var storagePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "storage");

        if (!Directory.Exists(storagePath))
            Directory.CreateDirectory(storagePath);

        var fullPath = Path.Combine(storagePath, fileName);
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var family = new Family
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            SectionId = dto.SectionId,
            VersionId = dto.VersionId,
            ManufacturerId = dto.ManufacturerId,
            Description = dto.Description,
            FilePath = $"storage/{fileName}",
            UploadDate = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow
        };

        await _repo.AddAsync(family);
    }

    public Task<List<Family>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Family?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public async Task UpdateAsync(Guid id, UpdateFamilyDto dto)
    {
        var family = await _repo.GetByIdAsync(id);
        if (family is null) return;

        family.Name = dto.Name;
        family.CategoryId = dto.CategoryId;
        family.SectionId = dto.SectionId;
        family.VersionId = dto.VersionId;
        family.ManufacturerId = dto.ManufacturerId;
        family.Description = dto.Description;
        family.LastUpdate = DateTime.UtcNow;

        await _repo.UpdateAsync(family);
    }

    public async Task DeleteAsync(Guid id)
    {
        var family = await _repo.GetByIdAsync(id);
        if (family is null) return;

        var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", family.FilePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        await _repo.DeleteAsync(id);
    }
}
