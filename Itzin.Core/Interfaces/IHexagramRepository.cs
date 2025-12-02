using Itzin.Core.Entities;

namespace Itzin.Core.Interfaces;

public interface IHexagramRepository
{
    Task<Hexagram?> GetByIdAsync(int id);
    Task<Hexagram?> GetByNumberAsync(int number);
    Task<Hexagram?> GetByBinaryAsync(string binary);
    Task<List<Hexagram>> GetAllAsync();
    Task<Hexagram> CreateAsync(Hexagram hexagram);
}
