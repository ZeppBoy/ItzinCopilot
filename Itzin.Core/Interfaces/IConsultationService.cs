using Itzin.Core.Entities;

namespace Itzin.Core.Interfaces;

public interface IConsultationService
{
    Task<Consultation> CreateConsultationAsync(int userId, string question, string language);
    Task<Consultation?> GetConsultationByIdAsync(int consultationId, int userId);
    Task<List<Consultation>> GetUserConsultationsAsync(int userId, int skip = 0, int take = 50);
    Task UpdateConsultationNotesAsync(int consultationId, int userId, string notes);
}
