using AutoMapper;
using hoho.Data;
using Microsoft.EntityFrameworkCore;
using hoho.Models.UGP1;
using hoho.DTOs.UGP1;
using hoho.Models.OUOM;

namespace hoho.Repositories
{
    public class UGP1Repository : IUGP1Repository
    {
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public UGP1Repository(QLHHDatacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(UGP1Model ugp1Model)
        {
            if (ugp1Model.BaseQty <= 0 || ugp1Model.AltQty <= 0)
            {
                throw new ArgumentException("AltQty và BaseQty phải lớn hơn 0");
            }

            var entity = _mapper.Map<UGP1>(ugp1Model);
            _context.UnitConversions.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.UnitConversions.Find(id);
            if (entity != null)
            {
                _context.UnitConversions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UGP1DTO>> GetAllItemAsync()
        {
            var entities = await _context.UnitConversions.ToListAsync();
            return _mapper.Map<List<UGP1DTO>>(entities);
        }

        public async Task UpdateAsync(int id, UGP1Model Ugp1Model)
        {
            if (id != Ugp1Model.Id)
                return;
            //var entity = _mapper.Map<UOUMModel, UOUM>(uoUMModel);
            var entity = _mapper.Map<UGP1>(Ugp1Model); // viết tắt của dòng trên
            _context.UnitConversions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
