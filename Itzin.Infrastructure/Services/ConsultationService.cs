using Itzin.Core.Entities;
using Itzin.Core.Interfaces;

namespace Itzin.Infrastructure.Services;

public class ConsultationService : IConsultationService
{
    private readonly IConsultationRepository _consultationRepository;
    private readonly ICoinTossService _coinTossService;
    private readonly IHexagramService _hexagramService;

    public ConsultationService(
        IConsultationRepository consultationRepository,
        ICoinTossService coinTossService,
        IHexagramService hexagramService)
    {
        _consultationRepository = consultationRepository;
        _coinTossService = coinTossService;
        _hexagramService = hexagramService;
    }

    public async Task<Consultation> CreateConsultationAsync(int userId, string question, string language)
    {
        // Perform coin tosses (6 times, 3 coins each time)
        var tossValues = _coinTossService.TossCoins(6);
        
        // Calculate primary hexagram
        var primaryBinary = _hexagramService.CalculateHexagramBinary(tossValues);
        var primaryHexagram = await _hexagramService.GetHexagramByBinaryAsync(primaryBinary);
        
        if (primaryHexagram == null)
            throw new InvalidOperationException($"Hexagram not found for binary: {primaryBinary}");

        // Get changing lines
        var changingLines = _hexagramService.GetChangingLines(tossValues);
        
        // Calculate relating hexagram if there are changing lines
        Hexagram? relatingHexagram = null;
        if (changingLines.Count > 0)
        {
            var relatingBinary = _hexagramService.CalculateRelatingHexagramBinary(primaryBinary, changingLines);
            relatingHexagram = await _hexagramService.GetHexagramByBinaryAsync(relatingBinary);
        }

        // Create consultation record
        var consultation = new Consultation
        {
            UserId = userId,
            Question = question,
            QuestionLanguage = language,
            TossResults = string.Join(",", tossValues),
            PrimaryHexagramId = primaryHexagram.Id,
            RelatingHexagramId = relatingHexagram?.Id,
            ChangingLines = changingLines.Count > 0 ? string.Join(",", changingLines) : null,
            ConsultationDate = DateTime.UtcNow
        };

        return await _consultationRepository.CreateAsync(consultation);
    }

    public async Task<Consultation?> GetConsultationByIdAsync(int consultationId, int userId)
    {
        var consultation = await _consultationRepository.GetByIdAsync(consultationId);
        
        // Verify the consultation belongs to the user
        if (consultation?.UserId != userId)
            return null;

        return consultation;
    }

    public async Task<List<Consultation>> GetUserConsultationsAsync(int userId, int skip = 0, int take = 50)
    {
        return await _consultationRepository.GetByUserIdAsync(userId, skip, take);
    }

    public async Task UpdateConsultationNotesAsync(int consultationId, int userId, string notes)
    {
        var consultation = await _consultationRepository.GetByIdAsync(consultationId);
        
        if (consultation == null || consultation.UserId != userId)
            throw new UnauthorizedAccessException("Consultation not found or access denied");

        consultation.Notes = notes;
        await _consultationRepository.UpdateAsync(consultation);
    }
}
