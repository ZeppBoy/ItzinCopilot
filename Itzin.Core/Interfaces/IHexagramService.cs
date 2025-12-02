using Itzin.Core.Entities;

namespace Itzin.Core.Interfaces;

public interface IHexagramService
{
    Task<Hexagram?> GetHexagramByBinaryAsync(string binary);
    Task<Hexagram?> GetHexagramByNumberAsync(int number);
    Task<List<Hexagram>> GetAllHexagramsAsync();
    string CalculateHexagramBinary(List<int> tossValues);
    List<int> GetChangingLines(List<int> tossValues);
    string CalculateRelatingHexagramBinary(string primaryBinary, List<int> changingLines);
}
