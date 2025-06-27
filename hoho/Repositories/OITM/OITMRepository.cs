using AutoMapper;
using Microsoft.EntityFrameworkCore;
using hoho.Data;
using hoho.Models.OITM;
using hoho.DTOs.OITM;

namespace hoho.Repositories
{
    public class OITMRepository : IOITMRepository
    {
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public OITMRepository(QLHHDatacontext context, IMapper mapper)
        {
            _context = context;
            // Constructor logic if needed
            _mapper = mapper;
        }

        public async Task<int> AddItemAsync(OITMModel item)
        {
            var newItem = _mapper.Map<OITM>(item);
               _context.Items!.Add(newItem);
              await _context.SaveChangesAsync();
               return newItem.Id;
        }

        public async Task DeleteItemAsync(int id)
        {
            var deleteItem = _context.Items!.FirstOrDefault(x => x.Id == id);
              if (deleteItem != null)
              { 
                   _context.Items!.Remove(deleteItem);
                   await _context.SaveChangesAsync();
               }
        }

        public async Task<List<OITMDTO>> GetAllItemAsync()
        {
            var items = await _context.Items!.ToListAsync();
            return _mapper.Map<List<OITMDTO>>(items);
        }

        public async Task<OITMModel?> GetItemByIdAsync(int id)
        {
            var item = await _context.Items!
                .Include(x => x.WarehouseStocks) // Lấy cả danh sách kho hàng
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<OITMModel?>(item);
        }
        public async Task UpdateItemAsync(int id, OITMModel item)
        {
            if (id == item.Id)
          { 
               var UpdateItem = _mapper.Map<OITM>(item);
                _context.Items!.Update(UpdateItem);
                await _context.SaveChangesAsync();
            }
        }

        //public async Task<int> AddItemAsync(OITM item)
        //{
        //    var newItem = _mapper.Map<OITM>(item);
        //    _context.OITMs!.Add(newItem);
        //    await _context.SaveChangesAsync();
        //    return newItem.Id;
        //}

        //public async Task DeleteItemAsync(int id)
        //{
        //    var deleteItem = _context.OITMs!.FirstOrDefault(x => x.Id == id);
        //    if (deleteItem != null)
        //    { 
        //        _context.OITMs!.Remove(deleteItem);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public Task UpdateItemAsync(int id, OITM item)
        //{
        //    if (id == item.Id)
        //    { 
        //        var 
        //    }
        //}
    }
}
