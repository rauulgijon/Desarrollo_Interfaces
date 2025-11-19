using MiniHito.persistence;
using System;

namespace MiniHito.domain
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        private GrupoPersistence pm; // Gestor de persistencia

        public Grupo() { pm = new GrupoPersistence(); }

        public Grupo(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            pm = new GrupoPersistence();
        }

        public Grupo(string nombre)
        {
            Nombre = nombre;
            pm = new GrupoPersistence();
        }

        // Métodos que llaman a la persistencia
        public void Insertar() => pm.InsertarGrupo(this);
        public void Actualizar() => pm.ActualizarGrupo(this);
        public void Eliminar() => pm.EliminarGrupo(this.Id);

        // Esto es CRUCIAL para que en el ListBox se vea el nombre y no "MiniHito.domain.Grupo"
        public override string ToString()
        {
            return Nombre;
        }
    }
}