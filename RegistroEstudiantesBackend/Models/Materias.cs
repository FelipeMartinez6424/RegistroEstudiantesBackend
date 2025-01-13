namespace RegistroEstudiantesBackend.Models
{
    using System.Collections.Generic;

    public class Materias
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public int Creditos { get; set; } = 3;

       
        public int ProfesorId{ get; set; }
        public Profesores Profesor { get; set; }


        public List<Estudiantesmaterias> Estudiantesmaterias { get; set; }
    }

}
