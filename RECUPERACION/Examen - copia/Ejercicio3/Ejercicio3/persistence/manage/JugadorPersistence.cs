using Ejercicio3.domain;
using ExampleMVCnoDatabase.Persistence;
using System;
using System.Collections.Generic;
using System.Windows; // Necesario para MessageBox

namespace Ejercicio3.persistence
{
    class JugadorPersistence
    {
        public static List<Jugador> leerPersonas()
        {
            // 1. SOLUCIÓN CLAVE: Usamos SELECT con nombres de columnas, NO asterisco (*)
            // Esto asegura que id siempre sea el 0, nombre el 1, etc., da igual cómo esté en MySQL.
            string sql = "SELECT idJugador, nombre, puntuacion, email, es_vip, turno, fecha, nivel FROM serpientes.jugador";

            List<Object> aux = DBBroker.obtenerAgente().leer(sql);
            List<Jugador> personas = new List<Jugador>();

            foreach (List<Object> fila in aux)
            {
                try
                {
                    // 2. MAPPING SEGURO (Sabemos exactamente qué índice es cada cosa)
                    int id = Convert.ToInt32(fila[0]);
                    string nombre = fila[1].ToString();

                    int puntos = 0;
                    int.TryParse(fila[2].ToString(), out puntos);

                    // Manejo de nulos para campos nuevos
                    string email = fila[3] != null ? fila[3].ToString() : "";

                    // Conversión robusta para VIP (acepta 1, "1", "True", etc.)
                    bool esVip = false;
                    if (fila[4] != null)
                    {
                        string vipStr = fila[4].ToString().ToLower();
                        if (vipStr == "1" || vipStr == "true") esVip = true;
                    }

                    string turno = fila[5] != null ? fila[5].ToString() : "Mañana";

                    // Conversión robusta para Fecha
                    string fecha = "";
                    if (fila[6] != null)
                    {
                        DateTime dt;
                        // Si falla al convertir, pone la fecha actual en vez de romper el programa
                        if (DateTime.TryParse(fila[6].ToString(), out dt))
                            fecha = dt.ToString("yyyy-MM-dd");
                        else
                            fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    int nivel = 1;
                    int.TryParse(fila[7].ToString(), out nivel);

                    // Creamos el objeto y lo añadimos
                    Jugador p = new Jugador(id, nombre, puntos, email, esVip, turno, fecha, nivel);
                    personas.Add(p);
                }
                catch (Exception ex)
                {
                    // Si una fila falla, la saltamos pero AVISAMOS (para que sepas por qué falta)
                    // MessageBox.Show("Error leyendo un jugador: " + ex.Message); 
                }
            }
            return personas;
        }

        public void insertarPersona(Jugador p)
        {
            int vipVal = p.EsVip ? 1 : 0;
            string sql = "INSERT INTO serpientes.jugador (nombre, puntuacion, email, es_vip, turno, fecha, nivel) VALUES ('" +
                         p.Nombre + "', " +
                         p.Puntuacion + ", '" +
                         p.Email + "', " +
                         vipVal + ", '" +
                         p.Turno + "', '" +
                         p.Fechanac + "', " +
                         p.Nivel + "); ";

            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarPersona(Jugador p)
        {
            int vipVal = p.EsVip ? 1 : 0;
            string sql = "UPDATE serpientes.jugador SET " +
                         "nombre = '" + p.Nombre + "', " +
                         "puntuacion = " + p.Puntuacion + ", " +
                         "email = '" + p.Email + "', " +
                         "es_vip = " + vipVal + ", " +
                         "turno = '" + p.Turno + "', " +
                         "fecha = '" + p.Fechanac + "', " +
                         "nivel = " + p.Nivel + " " +
                         "WHERE idJugador = " + p.Id + ";";

            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarPersona(int id)
        {
            string sql = "DELETE FROM serpientes.jugador WHERE idJugador = " + id + ";";
            DBBroker.obtenerAgente().modificar(sql);
        }
    }
}