using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace MiniHito.persistence
{
    class AlumnoPersistence
    {
        // SELECT: Estaba bien, pero aseguramos la lectura correcta
        public static List<Alumno> leerPersonas()
        {
            List<Alumno> personas = new List<Alumno>();
            try
            {
                // Usamos la tabla correcta: 'alumno'
                List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM aceptasreto.alumno;");

                foreach (List<Object> fila in aux)
                {
                    int id = Convert.ToInt32(fila[0]);
                    string nombre = fila[1].ToString();
                    string apellidos = fila[2].ToString();

                    int especialidad = 0;
                    int.TryParse(fila[3].ToString(), out especialidad);

                    int grupo = 0;
                    if (fila.Count >= 7 && fila[6] != null && fila[6].ToString() != "")
                    {
                        int.TryParse(fila[6].ToString(), out grupo);
                    }

                    Alumno a = new Alumno(id, nombre, apellidos, especialidad, grupo);
                    personas.Add(a);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error leyendo alumnos: " + ex.Message);
            }
            return personas;
        }

        public void insertarPersona(Alumno alumno)
        {
            try
            {
                // SQL ajustado a tus columnas reales: NOMBRE, APELLIDO, CURSO, ID_GRUPO
                // Nota: He puesto valores por defecto para CORREO y CICLO para que no falle el INSERT si son Not Null
                string sql = "INSERT INTO aceptasreto.alumno (NOMBRE, APELLIDO, CURSO, CORREO, CICLO, ID_GRUPO) VALUES ('" +
                             alumno.Nombre + "', '" +
                             alumno.Apellidos + "', '" +
                             alumno.Especialidad + "', " +
                             "'correo@default.com', 'DAW', " + // Valores relleno obligatorios
                             alumno.Grupo + ");";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando alumno: " + ex.Message); }
        }

        public void actualizarPersona(Alumno alumno)
        {
            try
            {
                // CORREGIDO: Tabla 'alumno', Clave 'ID_ALUMNO', Columnas correctas
                string sql = "UPDATE aceptasreto.alumno SET " +
                             "NOMBRE = '" + alumno.Nombre + "', " +
                             "APELLIDO = '" + alumno.Apellidos + "', " +
                             "CURSO = '" + alumno.Especialidad + "', " +
                             "ID_GRUPO = " + alumno.Grupo + " " +
                             "WHERE ID_ALUMNO = " + alumno.Id + ";";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando alumno: " + ex.Message); }
        }

        public void eliminarPersona(int id)
        {
            try
            {
                // CORREGIDO: Estaba 'alumnado' (ERROR) -> Ahora es 'alumno' (BIEN)
                // CORREGIDO: Clave 'ID_ALUMNO'
                string sql = "DELETE FROM aceptasreto.alumno WHERE ID_ALUMNO = " + id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando alumno: " + ex.Message); }
        }
    }
}