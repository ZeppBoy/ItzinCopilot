using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Itzin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itzin.Infrastructure.Repositories;

public class HexagramRuDescriptionRepository : IHexagramRuDescriptionRepository
{
    private readonly ItzinDbContext _context;

    public HexagramRuDescriptionRepository(ItzinDbContext context)
    {
        _context = context;
    }

    public async Task<HexagramRuDescription?> GetByIdAsync(int id)
    {
        return await _context.HexagramRuDescriptions
            .Include(h => h.Hexagram)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<HexagramRuDescription?> GetByHexagramIdAsync(int hexagramId)
    {
        return await _context.HexagramRuDescriptions
            .Include(h => h.Hexagram)
            .FirstOrDefaultAsync(h => h.HexagramId == hexagramId);
    }

    public async Task<List<HexagramRuDescription>> GetAllAsync()
    {
        return await _context.HexagramRuDescriptions
            .Include(h => h.Hexagram)
            .OrderBy(h => h.HexagramId)
            .ToListAsync();
    }

    public async Task<HexagramRuDescription> CreateAsync(HexagramRuDescription description)
    {
        _context.HexagramRuDescriptions.Add(description);
        await _context.SaveChangesAsync();
        return description;
    }

    public async Task<HexagramRuDescription> UpdateAsync(HexagramRuDescription description)
    {
        _context.HexagramRuDescriptions.Update(description);
        await _context.SaveChangesAsync();
        return description;
    }

    public async Task DeleteAsync(int id)
    {
        var description = await _context.HexagramRuDescriptions.FindAsync(id);
        if (description != null)
        {
            _context.HexagramRuDescriptions.Remove(description);
            await _context.SaveChangesAsync();
        }
    }
}

