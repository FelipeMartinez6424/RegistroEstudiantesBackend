using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesBackend.Data;
using RegistroEstudiantesBackend.Models;


[Route("api/[controller]")]
[ApiController]
public class MateriasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MateriasController(ApplicationDbContext context)
    {
        _context = context;
    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Materias>>> GetMaterias()
    {
        return await _context.Materias.ToListAsync();
    }
    [HttpGet("HasRegisteredCourses/{cedula}")]
    public IActionResult HasRegisteredCourses(string cedula)
    {
        var estudiante = _context.estudiantes
            .Include(e => e.EstudiantesMaterias)
            .FirstOrDefault(e => e.NumeroDocumento == cedula);

        if (estudiante == null)
        {
            return NotFound("Estudiante no encontrado.");
        }

        bool hasCourses = estudiante.EstudiantesMaterias.Any();
        return Ok(hasCourses);
    }

    [HttpPost("RegisterCourses")]
    public async Task<IActionResult> RegistrarMaterias([FromBody] RegistrarMateriasRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Cedula) || request.Materias == null || !request.Materias.Any())
        {
            return BadRequest(new { Message = "Datos inválidos para registrar materias." });
        }

        var estudiante = await _context.estudiantes.FirstOrDefaultAsync(e => e.NumeroDocumento == request.Cedula);
        if (estudiante == null)
        {
            return NotFound(new { Message = "Estudiante no encontrado." });
        }

        foreach (var materiaId in request.Materias)
        {
            var materia = await _context.Materias.FindAsync(materiaId);
            if (materia == null)
            {
                return NotFound(new { Message = $"Materia con ID {materiaId} no encontrada." });
            }

            var estudianteMateria = new Estudiantesmaterias
            {
                EstudianteId = estudiante.Id,
                MateriaId = materia.Id
            };
            _context.Estudiantesmaterias.Add(estudianteMateria);
        }

        await _context.SaveChangesAsync();

        
        return Ok(new { Message = "Materias registradas exitosamente." });
    }
    [HttpGet("CoursesofStudents/{cedula}")]
    public IActionResult GetMateriasPorUsuario(string cedula)
    {
       
        var estudiante = _context.estudiantes
            .Include(e => e.EstudiantesMaterias)
            .ThenInclude(em => em.Materia)
            .ThenInclude(m => m.Profesor)
            .FirstOrDefault(e => e.NumeroDocumento == cedula);

        if (estudiante == null)
        {
            return NotFound("Estudiante no encontrado.");
        }

        
        var materias = estudiante.EstudiantesMaterias.Select(em => new
        {
            MateriaId = em.Materia.Id,
            MateriaNombre = em.Materia.nombre,
            EstudianteNombre = estudiante.Nombre, 
            EstudianteApellido = estudiante.Apellido, 
            ProfesorNombre = em.Materia.Profesor.nombre
        });

        return Ok(materias);
    }


    [HttpGet("StudentforCourses/{materiaId}")]
    public IActionResult GetStudentsForCourse(int materiaId)
    {
       
        var materia = _context.Materias
            .Include(m => m.Estudiantesmaterias)
            .ThenInclude(em => em.Estudiante)
            .FirstOrDefault(m => m.Id == materiaId);

        if (materia == null)
        {
            return NotFound("Materia no encontrada.");
        }

        
        var estudiantes = materia.Estudiantesmaterias.Select(em => new
        {
            EstudianteNombre = em.Estudiante.Nombre,
            EstudianteApellido = em.Estudiante.Apellido
        });

        return Ok(estudiantes);
    }




}


