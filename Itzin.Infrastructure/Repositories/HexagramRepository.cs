using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Itzin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itzin.Infrastructure.Repositories;

public class HexagramRepository : IHexagramRepository
{
    private readonly ItzinDbContext _context;

    public HexagramRepository(ItzinDbContext context)
    {
        _context = context;
    }

    public async Task<Hexagram?> GetByIdAsync(int id)
    {
        return await _context.Hexagrams.FindAsync(id);
    }

    public async Task<Hexagram?> GetByNumberAsync(int number)
    {
        return await _context.Hexagrams.FirstOrDefaultAsync(h => h.Number == number);
    }

    public async Task<Hexagram?> GetByBinaryAsync(string binary)
    {
        return await _context.Hexagrams.FirstOrDefaultAsync(h => h.Binary == binary);
    }

    public async Task<List<Hexagram>> GetAllAsync()
    {
        return await _context.Hexagrams.OrderBy(h => h.Number).ToListAsync();
    }

    public async Task<Hexagram> CreateAsync(Hexagram hexagram)
    {
        _context.Hexagrams.Add(hexagram);
        await _context.SaveChangesAsync();
        return hexagram;
    }
}
