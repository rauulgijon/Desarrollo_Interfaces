using DatagridPersonas.domain;
using ExampleMVCnoDatabase.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatagridPersonas.persistence
{
    internal class PersonaPersistence
    {
        private DataTable table { get; set; }
        List<Alumnado> ListaPersonas { get; set; }

        public PersonaPersistence()
        {
            table = new DataTable();
            ListaPersonas = new List<Alumnado>();
        }
        // Simulación lectura de Base de datos

        //public static List<Persona> leerPersonas()
        //{
        //    List<Persona> ListaPersonas = new List<Persona>();
        //    ListaPersonas.Add(new Persona("Manuel", "Ruiz",19));
        //    ListaPersonas.Add(new Persona("Ismael", "Navarro", 20));
        //    ListaPersonas.Add(new Persona("Rubén", "Rueda", 21));
        //    ListaPersonas.Add(new Persona("Raúl", "Gijón", 19));
        //    ListaPersonas.Add(new Persona("Gabriel", "Hernández", 21));
        //    ListaPersonas.Add(new Persona("Asier", "Carretero", 21));
        //    ListaPersonas.Add(new Persona("Adrian", "Luque", 19));
        //    ListaPersonas.Add(new Persona("Manuel Alejandro", "García", 24));

        //    return ListaPersonas;
        //}

        public List<Alumnado> leerAlumnado()
        {
            Alumnado persona = null;

            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM aceptasreto.alumnado;");
            foreach (List<Object> c in aux)
            {
                persona = new Alumnado(c[1].ToString(), c[2].ToString(), Convert.ToInt32(c[3]));
                ListaPersonas.Add(persona);

            }

            return ListaPersonas;

        }

        public void insertarPersona(Alumnado alumno)
        {
            DBBroker.obtenerAgente().modificar("INSERT INTO aceptasreto.alumnado (idAlumnado, nombre, apellidos, curso)" +
                                                "VALUES (" + alumno.Id + ",'" + alumno.Nombre + "', '" + alumno.Apellido + "'," + alumno.Curso + ");");
        }

        public void lastId(Alumnado p)
        {
            List<Object> lpeople;
            lpeople = DBBroker.obtenerAgente().leer("SELECT MAX(idAlumnado) FROM aceptasreto.alumnado;");
            foreach (List<Object> c in lpeople)
            {
                p.Id = Convert.ToInt32(c[0]) + 1;
            }
        }
    }
}
