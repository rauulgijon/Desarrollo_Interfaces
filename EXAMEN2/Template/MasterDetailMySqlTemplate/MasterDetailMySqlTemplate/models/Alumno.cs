namespace MasterDetailMySqlTemplate.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Curso { get; set; }
        public string Correo { get; set; }
        public string Ciclo { get; set; }
        public int? IdGrupo { get; set; }
    }
}