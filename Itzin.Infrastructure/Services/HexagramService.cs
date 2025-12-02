using Itzin.Core.Entities;
using Itzin.Core.Interfaces;

namespace Itzin.Infrastructure.Services;

public class HexagramService : IHexagramService
{
    private readonly IHexagramRepository _hexagramRepository;

    public HexagramService(IHexagramRepository hexagramRepository)
    {
        _hexagramRepository = hexagramRepository;
    }

    public async Task<Hexagram?> GetHexagramByBinaryAsync(string binary)
    {
        return await _hexagramRepository.GetByBinaryAsync(binary);
    }

    public async Task<Hexagram?> GetHexagramByNumberAsync(int number)
    {
        return await _hexagramRepository.GetByNumberAsync(number);
    }

    public async Task<List<Hexagram>> GetAllHexagramsAsync()
    {
        return await _hexagramRepository.GetAllAsync();
    }

    public string CalculateHexagramBinary(List<int> tossValues)
    {
        if (tossValues.Count != 6)
            throw new ArgumentException("Must have exactly 6 toss values");

        var binary = new char[6];
        
        // Build hexagram from bottom to top (traditional I Ching order)
        for (int i = 0; i < 6; i++)
        {
            var value = tossValues[i];
            
            // Convert toss value to binary:
            // 3 (old yang) -> 1 (yang line, but changing)
            // 2 (young yin) -> 0 (yin line)
            // 1 (young yang) -> 1 (yang line)
            // 0 (old yin) -> 0 (yin line, but changing)
            binary[i] = (value == 1 || value == 3) ? '1' : '0';
        }
        
        return new string(binary);
    }

    public List<int> GetChangingLines(List<int> tossValues)
    {
        var changingLines = new List<int>();
        
        for (int i = 0; i < tossValues.Count; i++)
        {
            // Lines with value 0 or 3 are changing lines
            if (tossValues[i] == 0 || tossValues[i] == 3)
            {
                changingLines.Add(i + 1); // Line numbers are 1-indexed
            }
        }
        
        return changingLines;
    }

    public string CalculateRelatingHexagramBinary(string primaryBinary, List<int> changingLines)
    {
        if (changingLines.Count == 0)
            return primaryBinary; // No changing lines, no relating hexagram

        var binary = primaryBinary.ToCharArray();
        
        foreach (var lineNumber in changingLines)
        {
            var index = lineNumber - 1; // Convert to 0-indexed
            
            // Flip the line: 0 -> 1, 1 -> 0
            binary[index] = binary[index] == '1' ? '0' : '1';
        }
        
        return new string(binary);
    }
}
