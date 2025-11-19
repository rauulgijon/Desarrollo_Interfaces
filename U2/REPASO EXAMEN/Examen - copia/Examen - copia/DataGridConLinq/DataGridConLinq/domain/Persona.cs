using Datagrid.persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridConLinq
{
    class Persona
    {
        private PersonaPersistence pm { get; set; }
        private int id;
        private String nombre;
        private String apellidos;
        private int edad;
        private String fechanac;
        private List<Persona> lspersonas;
        public int Id { get;set; }
        
        public String Nombre { get; set; }
        
        public String Apellidos { get; set; }
        
        public int Edad { get;set; }
        
        public String Fechanac { get; set; }
        
        public Persona(int id, String nombre, String apellidos, int edad, String fechanac)
        {
            Id = id;
            Nombre = nombre;
            Edad = edad;
            Apellidos = apellidos;
            Fechanac = fechanac;
            pm = new PersonaPersistence();
        }

        public Persona(String nombre, String apellidos, int edad, String fechanac)
        {
            Nombre = nombre;
            Edad = edad;
            Apellidos = apellidos;
            Fechanac = fechanac;
            pm = new PersonaPersistence();
        }

        public Persona() 
        { 
            pm = new PersonaPersistence();
        }

        public Persona(int id)
        {
            Id = id;
            pm = new PersonaPersistence();
        }
        
        public List<Persona> getLspersonas()
        {
            lspersonas= PersonaPersistence.leerPersonas();
            return lspersonas;
        }

        public void insertar()
        {
            pm.insertarPersona(this);
        }

        public void actualizar()
        {
            pm.actualizarPersona(this);
        }

        public void eliminar()
        {
            pm.eliminarPersona(this.Id);
        }
    }
}
