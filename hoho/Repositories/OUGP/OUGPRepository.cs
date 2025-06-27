using AutoMapper;
using hoho.DTO;
using hoho.Data;
using Microsoft.EntityFrameworkCore;
using hoho.DTOs.OUGP;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hoho.Repositories
{
    public class OUGPRepository : IOUGPRepository
    {
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public OUGPRepository(QLHHDatacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int?> AddAsync(OUGPCreateDto dto)
        {

            // 1. Check trùng mã
            var exists = await _context.UnitOfMeasureGroups
                .AnyAsync(x => x.Code == dto.Code);
            if (exists)
                return null;
            // 2. Map OUGP
            var entity = _mapper.Map<OUGP>(dto);
            // 3. Tách UGP1 và bỏ để tránh vòng lặp
            var ugp1List = _mapper.Map<List<UGP1>>(dto.UGP1);
            entity.UnitConversions = null;
            // 4. Add OUGP trước để có ID
            _context.UnitOfMeasureGroups.Add(entity);
            await _context.SaveChangesAsync(); // sinh entity.Id
            // 5. Gán FatherId cho từng UGP1
            foreach (var item in ugp1List)
            {
                item.FatherId = entity.Id;
            }

            // 6. Lưu UGP1
            _context.UnitConversions.AddRange(ugp1List);
            await _context.SaveChangesAsync();

            return entity.Id; // Trả về Id để controller xử lý


        }

        public async Task<bool> UpdateAsync(int id, OUGPUpdateDto dto)
        {
            var entity = await _context.UnitOfMeasureGroups.Include(x => x.UnitConversions)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return false;

            var checkDuplicate = await _context.UnitOfMeasureGroups
                .FirstOrDefaultAsync(x => x.Code == dto.Code && x.Id != id );
            if (checkDuplicate is not null)
            {
                return false;
            }
            // Update OUGP properties
            entity.Code = dto.Code;
            entity.Name = dto.Name;
            entity.BaseUomId = dto.BaseUomId;

            // Get IDs of items that should remain (from DTO)
            var dtoIds = dto.UGP1.Select(x => x.Id).ToList();

            // Remove items that are no longer in the DTO
            entity.UnitConversions = entity.UnitConversions
                .Where(x => dtoIds.Contains(x.Id))
                .ToList();

            // Add or update items from DTO
            foreach (var item in dto.UGP1)
            {
                var existed = entity.UnitConversions.FirstOrDefault(x => x.Id == item.Id);
                

                if (existed is null)
                {
                    var newItem = new UGP1
                    {
                        AlternateUoMId = item.AlternateUoMId,
                        AltQty = item.AltQty,
                        BaseQty = item.BaseQty,
                    };
                    entity.UnitConversions.Add(newItem);
                }
                else
                {
                    existed.AlternateUoMId = item.AlternateUoMId;
                    existed.AltQty = item.AltQty;
                    existed.BaseQty = item.BaseQty;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.UnitOfMeasureGroups.Find(id);
            if (entity != null)
            {
                _context.UnitOfMeasureGroups.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<OUGPDTO>> GetAllItemAsync()
        {
            var entities = await _context.UnitOfMeasureGroups
                         .Include(x => x.UnitConversions)
                         .ToListAsync();
            var result = _mapper.Map<List<OUGPDTO>>(entities);
            return result; // ✅ Fixed: Removed incorrect usage of 'Ok'
        }

        
    }
}
