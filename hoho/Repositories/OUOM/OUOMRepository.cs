using AutoMapper;
using hoho.Data;
using Microsoft.EntityFrameworkCore;
using hoho.Models.OUOM;
using hoho.DTOs.OUOM;


namespace hoho.Repositories
{
    public class OUOMRepository : IOUOMRepository
    {
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public OUOMRepository(QLHHDatacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(OUOMModel oUOMModel)
        {
           //var entity = _mapper.Map<UOUMModel, UOUM>(uoUMModel);
            var entity = _mapper.Map<OUOM>(oUOMModel); // viết tắt của dòng trên
            _context.UnitsOfMeasure.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;

        }

        public async Task<List<OUOMDTO>> GetAllItemAsync()
        {
            var entity = await _context!.UnitsOfMeasure.ToListAsync();
            return _mapper.Map<List<OUOMDTO>>(entity);
        }

        public async Task UpdateAsync(int id ,OUOMModel oUOMModel)
        {
            if (id != oUOMModel.Id) 
                return;
            //var entity = _mapper.Map<UOUMModel, UOUM>(uoUMModel);
            var entity = _mapper.Map<OUOM>(oUOMModel); // viết tắt của dòng trên
            _context.UnitsOfMeasure.Update(entity);
            await _context.SaveChangesAsync(); 
        }

        
    }
}
