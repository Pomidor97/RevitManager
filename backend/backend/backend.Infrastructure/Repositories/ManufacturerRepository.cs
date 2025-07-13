using backend.core.Entities;
using backend.core.Interfaces;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly AppDbContext _context;

    public ManufacturerRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Manufacturer>> GetAllAsync() => _context.Manufacturers.ToListAsync();
    public Task<Manufacturer?> GetByIdAsync(Guid id) => _context.Manufacturers.FindAsync(id).AsTask();

    public async Task AddAsync(Manufacturer manufacturer)
    {
        _context.Manufacturers.Add(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Manufacturer manufacturer)
    {
        _context.Manufacturers.Update(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Manufacturers.FindAsync(id);
        if (entity is not null)
        {
            _context.Manufacturers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}