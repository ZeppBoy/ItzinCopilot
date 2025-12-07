using Itzin.Core.Entities;

namespace Itzin.Core.Interfaces;

public interface IHexagramRuDescriptionRepository
{
    Task<HexagramRuDescription?> GetByIdAsync(int id);
    Task<HexagramRuDescription?> GetByHexagramIdAsync(int hexagramId);
    Task<List<HexagramRuDescription>> GetAllAsync();
    Task<HexagramRuDescription> CreateAsync(HexagramRuDescription description);
    Task<HexagramRuDescription> UpdateAsync(HexagramRuDescription description);
    Task DeleteAsync(int id);
}

