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
    internal class ConsultaPersistence
    {
        private DataTable consultaTable { get; set; }
        public ConsultaPersistence()
        {
            consultaTable = new DataTable();

        }
        public static List<Consulta> leerConsultas()
        {
            Consulta p = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM examen.consulta;");
            List<Consulta> consultas = new List<Consulta>();
            foreach (List<Object> fila in aux)
            {
                DateTime fechaDB = Convert.ToDateTime(fila[1]);
                string fechaFormateada = fechaDB.ToString("dd/MM/yyyy");
                p = new Consulta(Convert.ToInt32(fila[0]), fechaFormateada, fila[2].ToString(), Convert.ToInt32(fila[3]), Convert.ToInt32(fila[3]));
                consultas.Add(p);
                Console.WriteLine(p.ToString());
            }
            return consultas;
        }

        public void insertarConsulta(Consulta mascota)
        {
            string sql = "INSERT INTO examen.consulta (fechaconsulta, diagnostico, idmascota, idveterinario) VALUES ('" +
                         mascota.Fechaconsulta + "', '" +
                         mascota.Diagnostico + "', " +
                         mascota.Idmascota + ", " +
                         mascota.Idveterinario + "); ";

            int filasAfectadas = DBBroker.obtenerAgente().modificar(sql);

            // Si filasAfectadas es 0, significa que hubo un error o no se insertó nada
            if (filasAfectadas == 0)
            {
                throw new Exception("No se ha podido insertar la consulta en la base de datos.");
            }
        }

        public void actualizarConsulta(Consulta consulta)
        {
            string sql = "UPDATE examen.consulta SET " +
                         "fechaconsulta = '" + consulta.Fechaconsulta + "', " +
                         "diagnostico = '" + consulta.Diagnostico + "', " +
                         "idmascota = " + consulta.Idmascota + ", " +
                         "idveterinario = " + consulta.Idveterinario + " " +
                         "WHERE idconsulta = " + consulta.Id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarConsulta(int id)
        {
            string sql = "DELETE FROM examen.consulta WHERE idconsulta = " + id + ";";
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
