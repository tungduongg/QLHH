using Microsoft.AspNetCore.Mvc;


using AutoMapper;

using hoho.Repositories;
using hoho.Data;
using hoho.DTO;
using hoho.DTOs.OUGP;
namespace hoho.Controllers.OUGP
{
    [ApiController]
    [Route("api/[controller]")]
    public class OUGPController : Controller
    {
        private readonly IOUGPRepository _repo;
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public OUGPController(IOUGPRepository repo, QLHHDatacontext context, IMapper mapper)
        {
            _repo = repo;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllItemAsync();
            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> AddOUGP([FromBody] OUGPCreateDto dto)
        {
            try
            {
                var newId = await _repo.AddAsync(dto); // dùng DTO, không dùng OUGPModel
                return CreatedAtAction(nameof(AddOUGP), new { id = newId }, new { id = newId });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateOUGP(int id, [FromBody] OUGPUpdateDto dto)
        {


            var success = await _repo.UpdateAsync(id, dto);

            if (!success)
                return NotFound(new { message = $"Không tìm thấy OUGP có ID = {id}" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();

        }

        //[HttpGet("PhanTrang")]
        //public IActionResult phatrang([FromQuery] GridifyQuery query)
        //{
        //    try
        //    {

        //        var queryable = _context.OUGP.AsQueryable().Include(x => x.UGP1).AsQueryable();
        //        var results = queryable.Gridify(query);

        //        return Ok(new
        //        {
        //            data = results.Data,
        //            totalCount = results.Count
        //        });
        //    }
        //    catch (GridifyFilteringException e)
        //    {
        //        return BadRequest(new { message = e.Message });
        //    }
        //}
        ////[HttpGet("PhanTrang")
        //    [HttpGet("PhanTrang")]
        //public IActionResult GetPage([FromQuery] GridifyQuery query)
        //{
        //    try
        //    {
        //        var queryable = _context.OUPG.AsQueryable();
        //        //return queryable.Gridify(query, out int totalCount)
        //        //    .ToList()
        //        //    .ToPagedResult(totalCount);

        //        //cái nay có phân trang
        //        var result = queryable.Gridify(query);

        //        return Ok(new
        //        {
        //            data = result.Data,
        //            totalCount = result.Count // Fix: Use 'Count' instead of 'TotalItems'  
        //        });

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { message = e.Message });
        //    }

        //}

    }
}
