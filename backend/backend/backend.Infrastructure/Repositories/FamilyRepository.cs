using backend.core.Entities;
using backend.core.Interfaces;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.infrastructure.Repositories;

public class FamilyRepository : IFamilyRepository
{
    private readonly AppDbContext _context;

    public FamilyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Family family)
    {
        _context.Families.Add(family);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Family>> GetAllAsync()
    {
        return await _context.Families
            .Include(f => f.Attachments)
            .ToListAsync();
    }

    public async Task<Family?> GetByIdAsync(Guid id)
    {
        return await _context.Families
            .Include(f => f.Attachments)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task UpdateAsync(Family family)
    {
        _context.Families.Update(family);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var family = await _context.Families
            .Include(f => f.Attachments)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (family is not null)
        {
            _context.Families.Remove(family);
            await _context.SaveChangesAsync();
        }
    }
}