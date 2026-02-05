using MiniHito.persistence;
using MiniHito.persistence.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniHito.domain
{
    internal class TalenLab
    {

        public int Id { get; set; }
        public string Titulo { get; set; }         
        public string Descripcion { get; set; }

        private TalentLabPersistence pm;

        public TalenLab()
        {
            pm = new TalentLabPersistence();
        }

        public TalenLab(string titulo, string descripcion)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            pm = new TalentLabPersistence();
        }

        public void insertar() => pm.InsertarTalentLab(this);
        public void actualizar() => pm.ActualizarTalentLab(this);
        public void eliminar() => pm.EliminarTalentLab(this.Id);

        public override string ToString()
        {
            return Titulo; // Para que se vea bonito en listas si hace falta
        }
    }
}
