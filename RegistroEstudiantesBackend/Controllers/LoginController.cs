using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantesBackend.Data;
using RegistroEstudiantesBackend.Models;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
            return BadRequest("Usuario o contraseña no pueden estar vacíos.");
        }

        var user = _context.estudiantes.SingleOrDefault(u => u.NumeroDocumento == loginRequest.Username);

        if (user == null)
        {
            return Unauthorized("Usuario no encontrado.");
        }

        if (user.Contraseña != loginRequest.Password)
        {
            return Unauthorized("Contraseña incorrecta.");
        }

       
        return Ok(new { Message = "Inicio de sesión exitoso", Cedula = user.NumeroDocumento });
    }


}
