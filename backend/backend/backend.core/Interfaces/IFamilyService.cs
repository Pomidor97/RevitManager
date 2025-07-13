using backend.Contracts.DTOs;
using backend.core.Entities;

namespace backend.core.Interfaces;

public interface IFamilyService
{
    Task UploadAsync(FamilyUploadDto dto);
    Task<List<Family>> GetAllAsync();
    Task<Family?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateFamilyDto dto);
    Task DeleteAsync(Guid id);
}