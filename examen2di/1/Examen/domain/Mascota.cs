using Examen.persistence.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.domain
{
    internal class Mascota
    {
        private MascotaPersistence pm { get; set; }
        private int id;
        private int idcliente;
        private String nombre;
        private String especie;
        private String raza;
        private String fechanac;
        private List<Mascota> lsmascotas;
        public int Id { get; set; }

        public String Nombre { get; set; }

        public String Especie { get; set; }

        public String Raza { get; set; }

        public String Fechanac { get; set; }

        public int Idcliente { get; set; }

        public Mascota(int id, String nombre, String especie, String raza, String fechanac, int idcliente)
        {
            Id = id;
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
            Fechanac = fechanac;
            Idcliente = idcliente;
            pm = new MascotaPersistence();
        }

        public Mascota(String nombre, String especie, String raza, String fechanac, int idcliente)
        {
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
            Fechanac = fechanac;
            Idcliente = idcliente;
            pm = new MascotaPersistence();
        }
        public Mascota(String nombre, String especie, String raza, String fechanac)
        {
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
            Fechanac = fechanac;
            pm = new MascotaPersistence();
        }

        public Mascota()
        {
            pm = new MascotaPersistence();
        }

        public Mascota(int id)
        {
            Id = id;
            pm = new MascotaPersistence();
        }

        public List<Mascota> getLsMascotas()
        {
            lsmascotas = MascotaPersistence.leerMascotas();
            return lsmascotas;
        }

        public void insertar()
        {
            pm.insertarMascota(this);
        }

        public void actualizar()
        {
            pm.actualizarMascota(this);
        }

        public void eliminar()
        {
            pm.eliminarMascota(this.Id);
        }
    }
}
