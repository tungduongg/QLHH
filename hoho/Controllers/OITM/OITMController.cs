using Microsoft.AspNetCore.Mvc;

using hoho.Data;


using AutoMapper;
using hoho.Models.OITM;
using hoho.DTOs.OITM;
using Gridify;
using hoho.Repositories;
namespace hihihihohoho.Controllers.OITM
{
    [ApiController]
    [Route("api/[controller]")]
    public class OITMController : Controller
    {
        private readonly IOITMRepository _repo;
        private readonly QLHHDatacontext _context;
        private readonly IMapper _mapper;

        public OITMController(IOITMRepository repo, QLHHDatacontext context, IMapper mapper)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // PUT: api/Ite
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] OITMModel model)
        {
            try
            {
                var newItemId = await _repo.AddItemAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = newItemId }, model);
            }
            catch (Exception ex)
            {
                    return BadRequest(new { message = "Thêm sản phẩm thất bại", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OITMModel model)
        {
            if (id != model.Id)
                return BadRequest("ID không khớp");

            await _repo.UpdateItemAsync(id, model);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteItemAsync(id);
            return NoContent();
        }
        [HttpGet("paged")]
        public IActionResult GetPaged([FromQuery] GridifyQuery query)
        {
            try { 
                
                 var queryable = _context.Items.AsQueryable();

                var result = queryable.Gridify(query); // Gridify làm filter, sort, phân trang  
                var dto = _mapper.Map<List<OITMDTO>>(result.Data);

                return Ok(new
                {
                    data = dto,
                    totalCount = result.Count // Fix: Use 'Count' instead of 'TotalItems'  
            });
            
            }
            catch (GridifyFilteringException ex)
            {
                return BadRequest(new { message = "Lỗi cú pháp filter Gridify: " + ex.Message });
            }

        }
     }
}

