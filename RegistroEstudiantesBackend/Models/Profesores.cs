using System.Collections.Generic;

namespace RegistroEstudiantesBackend.Models
{
    public class Profesores
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public ICollection<Materias> Materias { get; set; }
        
    }
}
