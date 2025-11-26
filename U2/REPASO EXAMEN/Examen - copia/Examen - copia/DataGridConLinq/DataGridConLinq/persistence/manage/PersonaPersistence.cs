using DataGridConLinq;
using ExampleMVCnoDatabase.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datagrid.persistence
{
    class PersonaPersistence
    {
        private DataTable personaTable { get; set;}
        public PersonaPersistence()
        {
            personaTable = new DataTable();
            
        }
        public static List<Persona> leerPersonas()
        {
            Persona p = null;
            List<Object> aux =DBBroker.obtenerAgente().leer("Select * from personas.persona2;");
            List<Persona> personas = new List<Persona>();
            foreach (List<Object> fila in aux)
            {
                DateTime fechaDB = Convert.ToDateTime(fila[4]);
                string fechaFormateada = fechaDB.ToString("dd/MM/yyyy");
                p = new Persona(Convert.ToInt32(fila[0]), fila[1].ToString(), fila[2].ToString(), Convert.ToInt32(fila[3]), fechaFormateada);
                personas.Add(p);
                Console.WriteLine(p.ToString());
            }
            return personas;
        }

        public void insertarPersona(Persona persona)
        {
            string sql = "INSERT INTO personas.persona2 (nombre, apellidos, edad, fechanac) VALUES ('" + 
                         persona.Nombre + "', '" + 
                         persona.Apellidos + "', " + 
                         persona.Edad + ", '" +
                         persona.Fechanac +"');";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarPersona(Persona persona)
        {
            string sql = "UPDATE personas.persona2 SET " +
                         "nombre = '" + persona.Nombre + "', " +
                         "apellidos = '" + persona.Apellidos + "', " +
                         "edad = " + persona.Edad + ", " +
                         "fechanac = '" + persona.Fechanac + "' " +
                         "WHERE idpersona = " + persona.Id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarPersona(int id)
        {
            string sql = "DELETE FROM personas.persona2 WHERE idpersona = " + id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        /*
        //simulates reading from a database
        public static List<Persona> leerPersonas()
        {
            List<Persona> personas = new List<Persona>();
            personas.Add(new Persona("Luis", "Rodríguez", 40));
            personas.Add(new Persona("Pepe", "Sanchez", 60));
            personas.Add(new Persona("Jose", "Mondongo", 10));
            personas.Add(new Persona("Gabriel", "Hernandez", 86));
            personas.Add(new Persona("Asier", "Carretero", 32));
            personas.Add(new Persona("Cristobal", "Colon", 344));
            return personas;
        }*/
    }
}
