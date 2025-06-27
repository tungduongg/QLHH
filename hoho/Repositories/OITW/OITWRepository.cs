using AutoMapper;
using hoho.Data;
using hoho.DTOs.OITW;
using hoho.Models.OITW;
using Microsoft.EntityFrameworkCore;


namespace hoho.Repositories
{
    public class OITWRepository : IOITWRepository
    {
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public OITWRepository(QLHHDatacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OITWDTO>> GetAllAsync()
        {
            //var entities = await _context.OITWs
            //    .Include(x => x.Item) // optional: nếu bạn muốn include OITM
            //    .ToListAsync();
            //return _mapper.Map<List<OITWModel>>(entities);

            var items = await _context.ItemWarehouses!.ToListAsync();
            return _mapper.Map<List<OITWDTO>>(items);
        }

        public async Task<OITWModel?> GetByIdAsync(int id)
        {
            var entity = await _context.ItemWarehouses
                .Include(x => x.Item) // optional
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<OITWModel?>(entity);
        }

        public async Task<int> AddAsync(OITWModel item)
        {
            var entity = _mapper.Map<OITW>(item);
            _context.ItemWarehouses.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(int id, OITWModel item)
        {
            if (id != item.Id) return;

            var entity = _mapper.Map<OITW>(item);
            _context.ItemWarehouses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ItemWarehouses.FindAsync(id);
            if (entity != null)
            {
                _context.ItemWarehouses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
