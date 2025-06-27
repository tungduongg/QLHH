
using Microsoft.AspNetCore.Mvc;
using hoho.Repositories;
using hoho.Data;
using hoho.Models.UGP1;

namespace hihihihohoho.Controllers.UGP1
{
    [ApiController]
    [Route("api/[controller]")]
    public class UGP1Controller : Controller
    {
        private readonly IUGP1Repository _repo;
        private readonly QLHHDatacontext _context;

        public UGP1Controller(IUGP1Repository repo, QLHHDatacontext context)
        {
            _repo = repo;
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllItemAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddUGP1([FromBody] UGP1Model uGP1Model)
        {
            try
            {
                var addnewUGP1 = await _repo.AddAsync(uGP1Model);
                return CreatedAtAction(nameof(AddUGP1), new { id = addnewUGP1 }, uGP1Model);

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUGP1(int id, [FromBody] UGP1Model uGP1Model)
        {
            if (id != uGP1Model.Id)
            {
                return BadRequest(new { message = "ID không khớp" });
            }

            await _repo.UpdateAsync(id, uGP1Model);
            return NoContent();
        }

        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }

        //[HttpGet("Phan_Trang")]

        //public IActionResult GetPage([FromQuery] GridifyQuery query)
        //{
        //    try {
        //        var queryable = _context.UGP1.AsQueryable();
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
