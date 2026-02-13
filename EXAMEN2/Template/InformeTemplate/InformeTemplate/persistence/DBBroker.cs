using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InformeTemplate.persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;
        // ⚠️ Ajusta la base de datos y contraseña según tu examen
        private const String cadenaConexion = "server=localhost;database=examen;uid=root;pwd=toor;";

        private DBBroker()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        public static DBBroker obtenerAgente()
        {
            if (_instancia == null) { _instancia = new DBBroker(); }
            return _instancia;
        }

        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();
            try
            {
                conectar();
                MySqlCommand com = new MySqlCommand(sql, conexion);
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    List<Object> fila = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++) { fila.Add(reader[i]); }
                    resultado.Add(fila);
                }
                reader.Close();
            }
            catch (Exception ex) { System.Windows.MessageBox.Show("Error SQL: " + ex.Message); }
            finally { desconectar(); }
            return resultado;
        }

        private void conectar() { if (conexion.State == System.Data.ConnectionState.Closed) conexion.Open(); }
        private void desconectar() { if (conexion.State == System.Data.ConnectionState.Open) conexion.Close(); }
    }
}