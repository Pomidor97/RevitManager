using backend.core.Entities;
using backend.core.Interfaces;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly AppDbContext _context;

    public SectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Section>> GetAllAsync() => _context.Sections.ToListAsync();

    public Task<Section?> GetByIdAsync(Guid id) => _context.Sections.FindAsync(id).AsTask();

    public async Task AddAsync(Section section)
    {
        _context.Sections.Add(section);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Section section)
    {
        _context.Sections.Update(section);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var section = await _context.Sections.FindAsync(id);
        if (section is not null)
        {
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
        }
    }
}