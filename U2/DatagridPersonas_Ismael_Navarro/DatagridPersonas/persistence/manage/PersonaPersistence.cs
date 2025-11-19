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
        List<Persona> ListaPersonas { get; set; }

        public PersonaPersistence()
        {
            table = new DataTable();
            ListaPersonas = new List<Persona>();
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

        public List<Persona> leerPersonas()
        {
            Persona persona = null;

            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM mydb.persona;");
            foreach (List<Object> c in aux)
            {
                persona = new Persona(Convert.ToInt32(c[0]), c[1].ToString(), c[2].ToString(), Convert.ToInt32(c[3]));
                ListaPersonas.Add(persona);

            }

            return ListaPersonas;

        }

        public void insertarPersona(Persona persona)
        {
            DBBroker.obtenerAgente().modificar("INSERT INTO mydb.persona (idpersona, nombre, apellidos, edad)" +
                                                "VALUES ("+persona.Id+",'" +persona.Nombre +"', '" + persona.Apellido + "'," + persona.Edad + ");");
        }

        public void lastId(Persona p)
        {
            List<Object> lpeople;
            lpeople = DBBroker.obtenerAgente().leer("SELECT MAX(idpersona) FROM mydb.persona;");
            foreach (List<Object> c in lpeople)
            {
                p.Id = Convert.ToInt32(c[0]) + 1;
            }
        }
    }
}
