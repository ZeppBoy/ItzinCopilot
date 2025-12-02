using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Itzin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itzin.Infrastructure.Repositories;

public class ConsultationRepository : IConsultationRepository
{
    private readonly ItzinDbContext _context;

    public ConsultationRepository(ItzinDbContext context)
    {
        _context = context;
    }

    public async Task<Consultation?> GetByIdAsync(int id)
    {
        return await _context.Consultations
            .Include(c => c.PrimaryHexagram)
            .Include(c => c.RelatingHexagram)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Consultation>> GetByUserIdAsync(int userId, int skip = 0, int take = 50)
    {
        return await _context.Consultations
            .Include(c => c.PrimaryHexagram)
            .Include(c => c.RelatingHexagram)
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.ConsultationDate)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<Consultation> CreateAsync(Consultation consultation)
    {
        consultation.CreatedAt = DateTime.UtcNow;
        consultation.UpdatedAt = DateTime.UtcNow;
        _context.Consultations.Add(consultation);
        await _context.SaveChangesAsync();
        return consultation;
    }

    public async Task UpdateAsync(Consultation consultation)
    {
        consultation.UpdatedAt = DateTime.UtcNow;
        _context.Consultations.Update(consultation);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCountByUserIdAsync(int userId)
    {
        return await _context.Consultations.CountAsync(c => c.UserId == userId);
    }
}
