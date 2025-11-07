using DataGrid.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.personapersistence
{
    internal class PersonaPersistence
    {
        private DataTable table { get; set; } // Revisar a posetriori

        public PersonaPersistence()
        {
            table = new DataTable();
        }

        // Simulacion lectura de base de datos
        
        public static List<Persona> leerPersonas()
        {
            List<Persona> personas = new List<Persona>();
            personas.Add(new Persona("Manuel", "Ruiz", 19));
            personas.Add(new Persona("Ismael", "Navarro", 20));
            personas.Add(new Persona("Ruben", "Rueda", 21));
            personas.Add(new Persona("Raul", "Gijon", 19));
            personas.Add(new Persona("Gabriel", "Hernandez", 21));
            personas.Add(new Persona("Asier", "Carretero", 21));
            personas.Add(new Persona("Adrian", "Luque", 19));
            personas.Add(new Persona("Manuel Alejandro", "Garcia", 24));
            return personas;
        }
    }
}
