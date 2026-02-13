using Examen.domain;
using ExampleMVCnoDatabase.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.persistence.manage
{
    internal class MascotaPersistence
    {
        private DataTable mascotaTable { get; set; }
        public MascotaPersistence()
        {
            mascotaTable = new DataTable();

        }
        public static List<Mascota> leerMascotas()
        {
            Mascota p = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM examen.mascota;");
            List<Mascota> mascotas = new List<Mascota>();
            foreach (List<Object> fila in aux)
            {
                DateTime fechaDB = Convert.ToDateTime(fila[4]);
                string fechaFormateada = fechaDB.ToString("dd/MM/yyyy");
                p = new Mascota(Convert.ToInt32(fila[0]), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fechaFormateada, Convert.ToInt32(fila[5]));
                mascotas.Add(p);
                Console.WriteLine(p.ToString());
            }
            return mascotas;
        }

        public void insertarMascota(Mascota mascota)
        {
            string sql = "INSERT INTO examen.mascota (nombre, especie, raza, fechanacimiento, idcliente) VALUES ('" +
                         mascota.Nombre + "', '" +
                         mascota.Especie + "', '" +
                         mascota.Raza + "', '" +
                         mascota.Fechanac + "', " +
                         mascota.Idcliente + "); ";

            int filasAfectadas = DBBroker.obtenerAgente().modificar(sql);

            // Si filasAfectadas es 0, significa que hubo un error o no se insertó nada
            if (filasAfectadas == 0)
            {
                throw new Exception("No se ha podido insertar el jugador en la base de datos.");
            }
        }

        public void actualizarMascota(Mascota mascota)
        {
            string sql = "UPDATE examen.mascota SET " +
                         "nombre = '" + mascota.Nombre + "', " +
                         "especie = '" + mascota.Especie + "', " +
                         "raza = '" + mascota.Raza + "', " +
                         "fechanacimiento = '" + mascota.Fechanac + "', " +
                         "idcliente = '" + mascota.Idcliente + "' " +
                         "WHERE idmascota = " + mascota.Id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarMascota(int id)
        {
            string sql = "DELETE FROM examen.mascota WHERE idmascota = " + id + ";";
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
