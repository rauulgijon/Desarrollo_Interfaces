using MiniHito.persistence;

namespace MiniHito.domain
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Especialidad { get; set; } // 1=DAM, 2=DAW...
        public int Grupo { get; set; }        // ID del grupo

        private AlumnoPersistence pm;

        public Alumno()
        {
            pm = new AlumnoPersistence();
        }

        // Constructor completo para cuando leemos de la BBDD
        public Alumno(int id, string nombre, string apellidos, int especialidad, int grupo)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            Especialidad = especialidad;
            Grupo = grupo;
            pm = new AlumnoPersistence();
        }

        // Constructor para crear uno nuevo (sin ID)
        public Alumno(string nombre, string apellidos, int especialidad)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Especialidad = especialidad;
            Grupo = 0; // Sin grupo por defecto
            pm = new AlumnoPersistence();
        }

        public void insertar() => pm.insertarPersona(this);
        public void actualizar() => pm.actualizarPersona(this);
        public void eliminar() => pm.eliminarPersona(this.Id);
    }
}