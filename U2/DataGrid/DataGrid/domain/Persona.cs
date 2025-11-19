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

        private List<Persona> alumnos;
        public Persona(String nombre, String apellido, int curso)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = curso;

        }

        public Persona()
        {
            this.alumnos = new List<Persona>();
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

  


        public List<Persona> getPersonas()
        {
            alumnos = PersonaPersistence.leerPersonas();
            return alumnos;

        }
}
}
