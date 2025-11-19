using DatagridPersonas.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatagridPersonas.domain
{
    internal class Persona
    {
        private int id;
        private String nombre;
        private String apellidos;
        private int edad;
        private static List<Persona> listaPersonas;
        public PersonaPersistence pm;

        public Persona(int id, string nombre, string apellidos, int edad)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.edad = edad;
        }


        public Persona(string nombre, string apellidos, int edad)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.edad = edad;
            pm = new PersonaPersistence();

        }

        public Persona()
        {
            pm = new PersonaPersistence();
        }

        public Persona(int id)
        {
            pm = new PersonaPersistence();
            this.id = id;
        }

        public String Nombre { get => nombre; set => nombre = value; }
        public String Apellido { get => apellidos; set => apellidos = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Id { get => id; set => id = value; }

        public static List<Persona> getPersonas()
        {
            // 1. Crear una instancia de PersonaPersistence
            PersonaPersistence persistencia = new PersonaPersistence();

            // 2. Usar esa instancia para llamar al método
            listaPersonas = persistencia.leerPersonas();

            return listaPersonas;
        }

        public List<Persona> getListaPersonas()
        {
            listaPersonas = pm.leerPersonas();
            return listaPersonas;
        }

        public void insertar()
        {
            pm.insertarPersona(this);
        }

        public void last()
        {
            pm.lastId(this);
        }
    }
}