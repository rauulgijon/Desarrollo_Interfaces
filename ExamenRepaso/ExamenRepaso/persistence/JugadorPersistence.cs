using ExamenRepaso.domain;
using ExampleMVCnoDatabase.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenRepaso.persistence
{
    internal class JugadorPersistence
    {
        private DataTable personaTable;
        public JugadorPersistence()
        {
            personaTable = new DataTable();
        }

        public static List<JugadorPersistence> leerPersonas()
        {
            Jugador j = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from serpiente.jugador");
            List<Jugador> jugadores = new List<Jugador>();
            foreach (List<Object> fila in aux)
            {
                DateTime fechaDB = Convert.ToDateTime(fila[3]);
                string fechaFromateada = fechaDB.ToString("dd/MM/yyyy");
                j = new Jugador(Convert.ToInt32(fila[0]), fila[1].ToString, Convert.ToInt32(fila[2]), fechaFromateada, Convert.ToInt32(fila[4]));
                jugadores.Add(j);
                Console.WriteLine(j.ToString());
            }
            return jugadores;
        }
    }
}
