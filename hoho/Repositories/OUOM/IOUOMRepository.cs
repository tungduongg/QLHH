using hoho.DTOs.OUOM;
using hoho.Models.OUOM;

namespace hoho.Repositories
{
    public interface IOUOMRepository
    {
        Task<List<OUOMDTO>> GetAllItemAsync(); // lay tat
        Task<int> AddAsync(OUOMModel uoUMModel);
        Task UpdateAsync(int id, OUOMModel uoUMModel);
    }
}
