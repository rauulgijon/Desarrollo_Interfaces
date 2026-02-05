using MiniHito.persistence;

namespace MiniHito.domain
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Mapea a 'DESCRIPCION' en la BD

        private GrupoPersistence pm;

        public Grupo()
        {
            pm = new GrupoPersistence();
        }

        public Grupo(string nombre)
        {
            Nombre = nombre;
            pm = new GrupoPersistence();
        }

        public Grupo(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            pm = new GrupoPersistence();
        }

        public void Insertar() => pm.InsertarGrupo(this);
        public void Actualizar() => pm.ActualizarGrupo(this);
        public void Eliminar() => pm.EliminarGrupo(this.Id);

        public override string ToString()
        {
            return Nombre; // Importante para que el ComboBox muestre el nombre y no "MiniHito.domain.Grupo"
        }
    }
}