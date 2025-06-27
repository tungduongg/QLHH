
using hoho.DTOs.OITW;
using hoho.Models.OITW;

namespace hoho.Repositories
{
    public interface IOITWRepository
    {
        Task<List<OITWDTO>> GetAllAsync(); // lay tat
        Task<int> AddAsync(OITWModel item);// them moi san pham
        Task UpdateAsync(int id, OITWModel item);// cap nhat san pham
        Task DeleteAsync(int id);// xoa san pham
        Task<OITWModel?> GetByIdAsync(int id);
    }
}
