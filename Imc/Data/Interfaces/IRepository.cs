using Imc.Models;

namespace Imc.Data.Interfaces;

public interface IRepository
{
    Task<List<ImcCalculator>?> GetItemListAsync();
    Task SetItemAsync(IList<ImcCalculator> value);
}

public interface ILocalStorageRepository : IRepository;