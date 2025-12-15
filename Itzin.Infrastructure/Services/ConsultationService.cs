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

    public async Task<Consultation> CreateConsultationAsync(int userId, string question, string language, bool isAdvanced = false)
    {
        // Perform coin tosses (6 times, 3 coins each time)
        var tossValues = _coinTossService.TossCoins(6);
        return await CreateConsultationWithTossesAsync(userId, question, tossValues, language, isAdvanced);
    }

    public async Task<Consultation> CreateConsultationWithTossesAsync(int userId, string question, List<int> tossResults, string language, bool isAdvanced = false)
    {
        // Calculate primary hexagram
        var primaryBinary = _hexagramService.CalculateHexagramBinary(tossResults);
        var primaryHexagram = await _hexagramService.GetHexagramByBinaryAsync(primaryBinary);
        
        if (primaryHexagram == null)
            throw new InvalidOperationException($"Hexagram not found for binary: {primaryBinary}");

        // Get changing lines
        var changingLines = _hexagramService.GetChangingLines(tossResults);
        
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
            TossResults = string.Join(",", tossResults),
            PrimaryHexagramId = primaryHexagram.Id,
            RelatingHexagramId = relatingHexagram?.Id,
            ChangingLines = changingLines.Count > 0 ? string.Join(",", changingLines) : null,
            ConsultationDate = DateTime.UtcNow,
            IsAdvanced = isAdvanced
        };

        // If advanced consultation, calculate additional hexagrams
        if (isAdvanced)
        {
            await CalculateAdvancedHexagrams(consultation, primaryBinary, changingLines);
        }

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

    public async Task DeleteConsultationAsync(int consultationId, int userId)
    {
        var consultation = await _consultationRepository.GetByIdAsync(consultationId);
        
        if (consultation == null || consultation.UserId != userId)
            throw new UnauthorizedAccessException("Consultation not found or access denied");

        await _consultationRepository.DeleteAsync(consultationId);
    }

    #region Advanced Consultation Methods

    /// <summary>
    /// Calculates additional hexagrams for advanced consultation
    /// </summary>
    private async Task CalculateAdvancedHexagrams(Consultation consultation, string primaryBinary, List<int> changingLines)
    {
        // Calculate Anti-Hexagram (opposite of primary hexagram)
        consultation.AntiHexagramId = await CalculateAntiHexagram(primaryBinary);

        // Calculate Changing Hexagram (if there are changing lines)
        if (changingLines.Count > 0)
        {
            consultation.ChangingHexagramId = await CalculateChangingHexagram(primaryBinary, changingLines);

            // Calculate Additional Changing Hexagrams (progressive changes)
            var additionalHexagrams = await CalculateAdditionalChangingHexagrams(primaryBinary, changingLines);
            if (additionalHexagrams.Count > 0)
            {
                consultation.AdditionalChangingHexagrams = string.Join(",", additionalHexagrams);
            }
        }
    }

    /// <summary>
    /// Calculates the Anti-Hexagram (opposite/inverse of primary hexagram)
    /// All lines are flipped: Yang becomes Yin, Yin becomes Yang
    /// </summary>
    private async Task<int?> CalculateAntiHexagram(string primaryBinary)
    {
        // Flip all bits: 0 -> 1, 1 -> 0
        // Example: "111000" -> "000111"
        
        var antiHexagramBinary = FlipBinaryString(primaryBinary);
        var antiHexagram = await _hexagramService.GetHexagramByBinaryAsync(antiHexagramBinary);
        return antiHexagram?.Id;
    }

    /// <summary>
    /// Calculates the Changing Hexagram based on changing lines pattern
    /// Changing lines become Yin (0), non-changing lines become Yang (1)
    /// This creates a hexagram that highlights which lines are in flux
    /// </summary>
    private async Task<int?> CalculateChangingHexagram(string primaryBinary, List<int> changingLines)
    {
        // Create a new binary where:
        // - Changing lines (positions in changingLines list) → '0' (Yin)
        // - Non-changing lines → '1' (Yang)
        
        var charArray = new char[primaryBinary.Length];

        for (int i = 0; i < primaryBinary.Length; i++)
        {
            int linePosition = i + 1; // Lines are 1-indexed

            if (changingLines.Contains(linePosition))
            {
                // Changing line → Yin (0)
                charArray[i] = '0';
            }
            else
            {
                // Non-changing line → Yang (1)
                charArray[i] = '1';
            }
        }

        //for (int i = primaryBinary.Length; i <= 0 ; i--)
        //{
        //    int linePosition = i - 1; // Lines are 1-indexed

        //    if (changingLines.Contains(linePosition))
        //    {
        //        // Changing line → Yin (0)
        //        charArray[i] = '0';
        //    }
        //    else
        //    {
        //        // Non-changing line → Yang (1)
        //        charArray[i] = '1';
        //    }
        //}

        charArray = charArray.Reverse().ToArray();

        var changingHexagramBinary = new string(charArray);
        var changingHexagram = await _hexagramService.GetHexagramByBinaryAsync(changingHexagramBinary);
        return changingHexagram?.Id;
    }

    /// <summary>
    /// Calculates a list of additional hexagrams representing progressive changes
    /// Each hexagram shows the result of applying individual changing lines one at a time
    /// </summary>
    private async Task<List<int>> CalculateAdditionalChangingHexagrams(string primaryBinary, List<int> changingLines)
    {
        // Calculate hexagram for each individual changing line
        // This shows the progression of changes
        var additionalHexagrams = new List<int>();

        foreach (var linePosition in changingLines)
        {
            var hexagramBinary = ApplySingleLineChange(primaryBinary, linePosition);
            var hexagram = await _hexagramService.GetHexagramByBinaryAsync(hexagramBinary);
            if (hexagram != null && hexagram.Id != 0)
            {
                additionalHexagrams.Add(hexagram.Id);
            }
        }

        return additionalHexagrams;
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Flips all bits in a binary string (0 -> 1, 1 -> 0)
    /// </summary>
    private string FlipBinaryString(string binary)
    {
        return new string(binary.Select(c => c == '0' ? '1' : '0').ToArray());
    }

    /// <summary>
    /// Applies a single line change to a binary string at the specified position
    /// </summary>
    private string ApplySingleLineChange(string binary, int linePosition)
    {
        // Line positions are 1-indexed from bottom
        // Binary string is 0-indexed from left
        binary = new string(binary.Reverse().ToArray());

        var charArray = binary.ToCharArray();
        var index = linePosition - 1;
        
        if (index >= 0 && index < charArray.Length)
        {
            charArray[index] = charArray[index] == '0' ? '1' : '0';
        }

        charArray = charArray.Reverse().ToArray();
        return new string(charArray);
    }

    #endregion
}
