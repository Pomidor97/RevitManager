using backend.core.Entities;
using backend.core.Interfaces;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class RevitVersionRepository : IRevitVersionRepository
{
    private readonly AppDbContext _context;

    public RevitVersionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<RevitVersion>> GetAllAsync() => _context.RevitVersions.ToListAsync();
    public Task<RevitVersion?> GetByIdAsync(Guid id) => _context.RevitVersions.FindAsync(id).AsTask();

    public async Task AddAsync(RevitVersion version)
    {
        _context.RevitVersions.Add(version);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RevitVersion version)
    {
        _context.RevitVersions.Update(version);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var version = await _context.RevitVersions.FindAsync(id);
        if (version is not null)
        {
            _context.RevitVersions.Remove(version);
            await _context.SaveChangesAsync();
        }
    }
}