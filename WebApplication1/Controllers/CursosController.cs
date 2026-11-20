using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly WebApplication1Context _context;
    private readonly IMapper _mapper;   
    public CursosController(WebApplication1Context context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Curso
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoDto>>> GetCurso()
    {
        var modelos = await _context.Curso.ToListAsync();

        var  dtos = _mapper.Map<List<Curso>, List<CursoDto>>(modelos);

        return dtos;
    }

    // GET: api/Curso/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CursoDto>> GetCurso(int id)
    {
        var curso = await _context.Curso.FindAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<Curso, CursoDto>(curso);

        return dto;
    }

    // PUT: api/Curso/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurso(int? id, CursoDto cursodto)
    {

        if (id != cursodto.Id)
        {
            return BadRequest();
        }

        var cursoDB = await _context.Curso.FindAsync(id);

        if (cursoDB == null)
        {
            return NotFound();
        }

        _mapper.Map<CursoDto, Curso>(cursodto, cursoDB);

        _context.Entry(cursoDB).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CursoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Curso
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(CursoDto cursoDto)
    {
        var curso = _mapper.Map<CursoDto, Curso>(cursoDto);

        _context.Curso.Add(curso);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
    }

    // DELETE: api/Curso/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int? id)
    {
        var curso = await _context.Curso.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _context.Curso.Remove(curso);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CursoExists(int? id)
    {
        return _context.Curso.Any(e => e.Id == id);
    }
}
