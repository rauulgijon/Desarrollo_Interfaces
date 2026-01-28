using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient; // Asegúrate de tener esta referencia

namespace ExampleMVCnoDatabase.Persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;

        // --- OJO AQUÍ: CAMBIA LA CONTRASEÑA (pwd) SI LA TUYA NO ES 'toor' ---
        private const String cadenaConexion = "server=localhost;database=serpiente;uid=root;pwd=toor";

        private DBBroker()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        public static DBBroker obtenerAgente()
        {
            if (_instancia == null)
            {
                _instancia = new DBBroker();
            }
            return _instancia;
        }

        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();

            // Usamos un bloque try-catch para conectar
            try
            {
                conectar();
                MySqlCommand com = new MySqlCommand(sql, conexion);
                MySqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    List<Object> fila = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fila.Add(reader[i].ToString());
                    }
                    resultado.Add(fila);
                }
                reader.Close(); // Importante cerrar el reader
            }
            catch (Exception ex)
            {
                // Esto te avisará si falla la conexión al leer
                System.Windows.MessageBox.Show("Error SQL (Leer): " + ex.Message);
            }
            finally
            {
                desconectar();
            }

            return resultado;
        }

        public int modificar(String sql)
        {
            int filas = 0;
            try
            {
                conectar();
                MySqlCommand com = new MySqlCommand(sql, conexion);
                filas = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Esto te avisará si falla la conexión al guardar/borrar
                System.Windows.MessageBox.Show("Error SQL (Modificar): " + ex.Message);
            }
            finally
            {
                desconectar();
            }
            return filas;
        }

        private void conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }

        private void desconectar()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}