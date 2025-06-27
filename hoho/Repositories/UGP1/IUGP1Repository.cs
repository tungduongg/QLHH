
using hoho.DTOs.UGP1;
using hoho.Models.UGP1;

namespace hoho.Repositories
{
    public interface IUGP1Repository
    {
        Task<List<UGP1DTO>> GetAllItemAsync(); // lay tat
        Task<int> AddAsync(UGP1Model ugp1Model);
        Task UpdateAsync(int id, UGP1Model ugp1Model);
        Task DeleteAsync(int id);
      
    }
}
