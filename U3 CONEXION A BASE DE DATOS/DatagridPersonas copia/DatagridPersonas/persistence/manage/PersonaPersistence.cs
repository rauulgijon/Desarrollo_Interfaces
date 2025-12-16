using DatagridPersonas.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatagridPersonas.persistence
{
    internal class PersonaPersistence
    {

        private string path;
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

        public void leerPersonas()


        {
            try
            {
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    throw new Exception("Hay un error en el file path");
                }

                string jsonContent = File.ReadAllText(path);

                var rootObject = jsonConvert.DeserializeObject<Persona>(jsonContent);
                if (rootObject != null || rootObject.Persona == null)
                {
                    throw new Exception("El archivo json no tiene un formato valido o esta vacio");
                }

                ListaPersonas = rootObject.Persona.OrderBy(Persona => Persona.Id).toList();
            } catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }

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
