namespace RegistroEstudiantesBackend.Models
{
    public class MateriasEstudiantes
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }

        public Estudiantes Estudiante { get; set; }
        public Materias Materia { get; set; }
    }
}
