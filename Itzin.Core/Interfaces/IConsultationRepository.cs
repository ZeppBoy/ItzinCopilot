using Itzin.Core.Entities;

namespace Itzin.Core.Interfaces;

public interface IConsultationRepository
{
    Task<Consultation?> GetByIdAsync(int id);
    Task<List<Consultation>> GetByUserIdAsync(int userId, int skip = 0, int take = 50);
    Task<Consultation> CreateAsync(Consultation consultation);
    Task UpdateAsync(Consultation consultation);
    Task<int> GetCountByUserIdAsync(int userId);
}
