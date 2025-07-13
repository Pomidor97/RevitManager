using backend.core.Entities;

namespace backend.core.Interfaces;

public interface IFamilyRepository
{
    Task AddAsync(Family family);
    Task<List<Family>> GetAllAsync();
}