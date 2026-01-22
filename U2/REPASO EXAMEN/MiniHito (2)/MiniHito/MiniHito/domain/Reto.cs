using MiniHito.persistence;
using System;

namespace MiniHito.domain
{
    public class Reto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        private RetoPersistence pm;

        public Reto()
        {
            pm = new RetoPersistence();
        }

        public Reto(string descripcion, bool activo)
        {
            Descripcion = descripcion;
            Activo = activo;
            pm = new RetoPersistence();
        }

        public void insertar() => pm.InsertarReto(this);
        public void actualizar() => pm.ActualizarReto(this);
        public void eliminar() => pm.EliminarReto(this.Id);

        public override string ToString()
        {
            return Descripcion;
        }
    }
}