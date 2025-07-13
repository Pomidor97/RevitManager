using backend.core.Entities;
using backend.core.Interfaces;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

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
        return await _context.Families.ToListAsync();
    }
}