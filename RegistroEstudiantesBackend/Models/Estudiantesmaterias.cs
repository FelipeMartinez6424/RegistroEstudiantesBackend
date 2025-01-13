using System;

namespace RegistroEstudiantesBackend.Models
{
    public class Estudiantesmaterias
    {
        public int Id { get; set; }
        public int MateriaId { get; set; }
        public Materias Materia { get; set; }
        public int EstudianteId { get; set; }
        public Estudiantes Estudiante { get; set; }
    }

}
