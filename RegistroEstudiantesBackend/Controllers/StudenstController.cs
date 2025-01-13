using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesBackend.Data;
using RegistroEstudiantesBackend.Models;

[Route("api/[controller]")]
[ApiController]
public class StudenstController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudenstController(ApplicationDbContext context)
    {
        _context = context;
    }

   
    [HttpGet]
    public async Task<IActionResult> GetStudent()
    {
        var estudiantes = await _context.estudiantes.ToListAsync();
        return Ok(estudiantes);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var estudiante = await _context.estudiantes.FindAsync(id);

        if (estudiante == null)
        {
            return NotFound();
        }

        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEstudent([FromBody] Estudiantes estudiante)
    {
        if (!ModelState.IsValid)
        {
           
            return BadRequest(ModelState);
        }

        try
        {
            _context.estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = estudiante.Id }, estudiante);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new { Message = "Hubo un error al guardar el estudiante.", Error = ex.Message });
        }
    }





}


