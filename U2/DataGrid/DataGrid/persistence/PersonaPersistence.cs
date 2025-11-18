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
        
        public static List<Alumnado> leerAlumnado()
        {
            List<Alumnado> personas = new List<Alumnado>();
            personas.Add(new Alumnado("Manuel", "Ruiz", 19));
            personas.Add(new Alumnado("Ismael", "Navarro", 20));
            personas.Add(new Alumnado("Ruben", "Rueda", 21));
            personas.Add(new Alumnado("Raul", "Gijon", 19));
            personas.Add(new Alumnado("Gabriel", "Hernandez", 21));
            personas.Add(new Alumnado("Asier", "Carretero", 21));
            personas.Add(new Alumnado("Adrian", "Luque", 19));
            personas.Add(new Alumnado("Manuel Alejandro", "Garcia", 24));
            return personas;
        }
    }
}
