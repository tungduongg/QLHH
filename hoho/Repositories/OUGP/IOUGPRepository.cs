
using hoho.Data;
using hoho.DTO;
using hoho.DTOs.OUGP;


namespace hoho.Repositories
    
{
    public interface IOUGPRepository
    {
        Task<List<OUGPDTO>> GetAllItemAsync(); // lay tat
        Task<int?> AddAsync(OUGPCreateDto dto);
        Task<bool> UpdateAsync(int id, OUGPUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
