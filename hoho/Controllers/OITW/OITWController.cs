using AutoMapper;
using Gridify;
using hoho.Data;
using hoho.DTOs.OITW;
using hoho.Models.OITW;
using hoho.Repositories;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class OITWController : ControllerBase
{
    private readonly IOITWRepository _repo;
    private readonly QLHHDatacontext _context;
    private readonly IMapper _mapper;

    public OITWController(IOITWRepository repo, QLHHDatacontext context, IMapper mapper)
    {
        _repo = repo;
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _repo.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OITWModel model)
    {
        var id = await _repo.AddAsync(model);
        return CreatedAtAction(nameof(GetById), new { id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OITWModel model)
    {
        if (id != model.Id) return BadRequest("ID không khớp");
        await _repo.UpdateAsync(id, model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] GridifyQuery query)
    {
        try
        {

            var queryable = _context.ItemWarehouses.AsQueryable();

            var result = queryable.Gridify(query); // Gridify làm filter, sort, phân trang  
            var dto = _mapper.Map<List<OITWDTO>>(result.Data);

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
