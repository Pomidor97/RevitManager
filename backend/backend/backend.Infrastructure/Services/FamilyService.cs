using backend.Contracts.DTOs;
using backend.core.Entities;
using backend.core.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace backend.Infrastructure.Services;

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
        var stroagePath = Path.Combine(_env.ContentRootPath ?? "wwwroot", "stroage");
        Directory.CreateDirectory($"{stroagePath}");
        
        var fullPath = Path.Combine($"{stroagePath}", fileName);
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
}