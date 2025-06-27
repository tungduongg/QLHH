using hoho.DTOs.OITM;
using hoho.Models.OITM;

namespace hoho.Repositories
{
    public interface IOITMRepository
    {
        Task<List<OITMDTO>> GetAllItemAsync(); // lay tat
        Task<int> AddItemAsync(OITMModel item);// them moi san pham
        Task UpdateItemAsync(int id , OITMModel item);// cap nhat san pham
        Task DeleteItemAsync(int id);// xoa san pham
        Task<OITMModel?> GetItemByIdAsync(int id);

    }
}
