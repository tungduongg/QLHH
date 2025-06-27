using hoho.Data;
using hoho.Repositories;
using Microsoft.AspNetCore.Mvc;
using hoho.Models;
using hoho.Data;
using hoho.Repositories;
using hoho.Models.OUOM;
//using Gridify;
namespace hoho.Controllers.OUOM
{
    [ApiController]
    [Route("api/[controller]")]
    public class OUOMController : Controller
    {
        private readonly IOUOMRepository _repo;
        private readonly QLHHDatacontext _context;

        public OUOMController(IOUOMRepository repo, QLHHDatacontext context)
        {
            // Constructor logic if needed
            _repo = repo;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddOUOM([FromBody] OUOMModel uOMModel)
        {
            try
            {
                var addnewitem = await _repo.AddAsync(uOMModel);
                return CreatedAtAction(nameof(AddOUOM), new { id = addnewitem }, uOMModel);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Thêm thất bại", error = ex.Message });

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOUOM(int id, [FromBody] OUOMModel uOMModel)
        {
            try
            {
                if (id != uOMModel.Id)
                {
                    return BadRequest(new { message = "ID không khớp" });
                }
                await _repo.UpdateAsync(id, uOMModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Cập nhật thất bại", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllItemAsync();
            return Ok(items);
        }

    }    

}
