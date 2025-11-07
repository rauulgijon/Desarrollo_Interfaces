using DataGrid.personapersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.domain
{
    internal class Persona
    {
        private String nombre;
        private String apellido;
        private int edad;
        private List<Persona> personas;
        public Persona(String nombre, String apellido, int edad)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = edad;
        }

        public Persona()
        {
            this.personas = new List<Persona>();
        }
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public String Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        public int Id { get; internal set; }

        public List<Persona> getPersonas()
        {
            personas = PersonaPersistence.leerPersonas();
            return personas;

        }
}
}
