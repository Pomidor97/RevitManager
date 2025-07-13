using backend.Contracts.DTOs;

namespace backend.core.Interfaces;

public interface IFamilyService
{
    Task UploadAsync(FamilyUploadDto dto);
    Task<List<FamilyDto>> GetAllAsync();
    Task<FamilyDto?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateFamilyDto dto);
    Task DeleteAsync(Guid id);
}