namespace RegistroEstudiantesBackend.Models
{
    public class RegistrarMateriasRequest
    {
        public string Cedula { get; set; }
        public List<int> Materias { get; set; }
    }
}
