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
        var familyId = Guid.NewGuid();
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
        var storagePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "storage", "families");

        Directory.CreateDirectory(storagePath);

        var fullPath = Path.Combine(storagePath, fileName);
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var family = new Family
        {
            Id = familyId,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            SectionId = dto.SectionId,
            VersionId = dto.VersionId,
            ManufacturerId = dto.ManufacturerId,
            Description = dto.Description,
            FilePath = $"storage/families/{fileName}",
            UploadDate = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow,
            Attachments = new List<FamilyFile>()
        };

        if (dto.Attachments is not null)
        {
            foreach (var attach in dto.Attachments)
            {
                var attachFileName = $"{Guid.NewGuid()}{Path.GetExtension(attach.FileName)}";
                var attachPath = Path.Combine(storagePath, attachFileName);

                using var stream = new FileStream(attachPath, FileMode.Create);
                await attach.CopyToAsync(stream);

                family.Attachments.Add(new FamilyFile
                {
                    Id = Guid.NewGuid(),
                    FileName = attach.FileName,
                    FilePath = $"storage/families/{attachFileName}",
                    ContentType = attach.ContentType ?? "application/octet-stream"
                });
            }
        }

        await _repo.AddAsync(family);
    }

    public async Task<List<FamilyDto>> GetAllAsync()
    {
        var families = await _repo.GetAllAsync();
        return families.Select(MapToDto).ToList();
    }

    public async Task<FamilyDto?> GetByIdAsync(Guid id)
    {
        var family = await _repo.GetByIdAsync(id);
        return family is null ? null : MapToDto(family);
    }

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

        var storagePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "storage", "families");

        if (dto.DeleteAttachmentIds is not null && family.Attachments is not null)
        {
            var toDelete = family.Attachments
                .Where(f => dto.DeleteAttachmentIds.Contains(f.Id))
                .ToList();

            foreach (var attachment in toDelete)
            {
                var path = Path.Combine(_env.WebRootPath ?? "wwwroot", attachment.FilePath);
                if (File.Exists(path)) File.Delete(path);
                family.Attachments.Remove(attachment);
            }
        }

        if (dto.NewAttachments is not null)
        {
            foreach (var file in dto.NewAttachments)
            {
                var attachFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var attachPath = Path.Combine(storagePath, attachFileName);

                await using var stream = new FileStream(attachPath, FileMode.Create);
                await file.CopyToAsync(stream);

                family.Attachments.Add(new FamilyFile
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    FilePath = $"storage/families/{attachFileName}",
                    ContentType = file.ContentType ?? "application/octet-stream"
                });
            }
        }

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

        if (family.Attachments is not null)
        {
            foreach (var attachment in family.Attachments)
            {
                var path = Path.Combine(_env.WebRootPath ?? "wwwroot", attachment.FilePath);
                if (File.Exists(path)) File.Delete(path);
            }
        }

        await _repo.DeleteAsync(id);
    }

    private FamilyDto MapToDto(Family family)
    {
        return new FamilyDto
        {
            Id = family.Id,
            Name = family.Name,
            Description = family.Description,
            FilePath = family.FilePath,
            UploadDate = family.UploadDate,
            LastUpdate = family.LastUpdate,
            CategoryId = family.CategoryId,
            SectionId = family.SectionId,
            VersionId = family.VersionId,
            ManufacturerId = family.ManufacturerId,
            Attachments = family.Attachments?.Select(a => new AttachmentDto
            {
                Id = a.Id,
                FileName = a.FileName,
                FilePath = a.FilePath,
                ContentType = a.ContentType
            }).ToList() ?? new()
        };
    }
}
