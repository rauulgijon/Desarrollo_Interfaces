using MiniHito.persistence;
using System;

namespace MiniHito.domain
{
    public class Reto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }         // NUEVO (Examen)
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }  // NUEVO (Examen)
        public bool Activo { get; set; }

        private RetoPersistence pm;

        public Reto()
        {
            pm = new RetoPersistence();
            FechaInicio = DateTime.Now; // Fecha por defecto hoy
        }

        public Reto(string nombre, string descripcion, DateTime fecha, bool activo)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            FechaInicio = fecha;
            Activo = activo;
            pm = new RetoPersistence();
        }

        public void insertar() => pm.InsertarReto(this);
        public void actualizar() => pm.ActualizarReto(this);
        public void eliminar() => pm.EliminarReto(this.Id);

        public override string ToString()
        {
            return Nombre; // Para que se vea bonito en listas si hace falta
        }
    }
}