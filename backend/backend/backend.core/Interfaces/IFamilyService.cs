using backend.Contracts.DTOs;

namespace backend.core.Interfaces;

public interface IFamilyService
{
    Task UploadAsync(FamilyUploadDto dto);
}