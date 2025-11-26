using ExampleMVCnoDatabase.Persistence;
using Ejercicio3.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Ejercicio3.persistence
{
    class JugadorPersistence
    {
        
            private DataTable personaTable { get; set; }
            public JugadorPersistence()
            {
                personaTable = new DataTable();

            }
            public static List<Jugador> leerPersonas()
            {
                Jugador p = null;
                List<Object> aux = DBBroker.obtenerAgente().leer("Select * from serpiente.jugador;");
                List<Jugador> personas = new List<Jugador>();
                foreach (List<Object> fila in aux)
                {
                    DateTime fechaDB = Convert.ToDateTime(fila[3]);
                    string fechaFormateada = fechaDB.ToString("dd/MM/yyyy");
                    p = new Jugador(Convert.ToInt32(fila[0]), fila[1].ToString(), Convert.ToInt32(fila[2]), fechaFormateada, Convert.ToInt32(fila[4]));
                    personas.Add(p);
                    Console.WriteLine(p.ToString());
                }
                return personas;
            }

            public void insertarPersona(Jugador persona)
            {
                string sql = "INSERT INTO serpiente.jugador (nombre, puntuacion, fecha, nivel) VALUES ('" +
                             persona.Nombre + "', " +
                             persona.Puntuacion + ", '" +
                             persona.Fechanac + "', " +
                             persona.Nivel + "); ";
                int a = DBBroker.obtenerAgente().modificar(sql);
            }

            public void actualizarPersona(Jugador persona)
            {
                string sql = "UPDATE serpiente.jugador SET " +
                             "nombre = '" + persona.Nombre + "', " +
                             "puntuacion = '" + persona.Puntuacion + "', " +
                             "fecha = " + persona.Fechanac + ", " +
                             "nivel = '" + persona.Nivel + "' " +
                             "WHERE idJugador = " + persona.Id + ";";
                int a = DBBroker.obtenerAgente().modificar(sql);
            }

            public void eliminarPersona(int id)
            {
                string sql = "DELETE FROM serpiente.jugador WHERE idJugador = " + id + ";";
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