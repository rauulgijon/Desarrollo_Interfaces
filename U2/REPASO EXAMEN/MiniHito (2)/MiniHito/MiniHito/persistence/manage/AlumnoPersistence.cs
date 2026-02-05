using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MiniHito.persistence
{
    class AlumnoPersistence
    {
        public static List<Alumno> leerPersonas()
        {
            List<Alumno> personas = new List<Alumno>();
            try
            {
                // SQL ajustado a tu modelo
                string sql = "SELECT ID_ALUMNO, NOMBRE, APELLIDO, CURSO, ID_GRUPO FROM AceptasReto.alumno;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    int id = Convert.ToInt32(fila[0]);
                    string nombre = fila[1].ToString();
                    string apellidos = fila[2].ToString();

                    int especialidad = 0;
                    if (fila[3] != null && fila[3].ToString() != "")
                        int.TryParse(fila[3].ToString(), out especialidad);

                    int grupo = 0;
                    if (fila.Count >= 5 && fila[4] != null && fila[4].ToString() != "")
                        int.TryParse(fila[4].ToString(), out grupo);

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
                string sql = "INSERT INTO AceptasReto.alumno (NOMBRE, APELLIDO, CURSO, ID_GRUPO) VALUES ('" +
                             alumno.Nombre + "', '" +
                             alumno.Apellidos + "', " +
                             alumno.Especialidad + ", " +
                             alumno.Grupo + ");";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando alumno: " + ex.Message); }
        }

        public void actualizarPersona(Alumno alumno)
        {
            try
            {
                string sql = "UPDATE AceptasReto.alumno SET " +
                             "NOMBRE = '" + alumno.Nombre + "', " +
                             "APELLIDO = '" + alumno.Apellidos + "', " +
                             "CURSO = " + alumno.Especialidad + ", " +
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
                string sql = "DELETE FROM AceptasReto.alumno WHERE ID_ALUMNO = " + id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando alumno: " + ex.Message); }
        }
    }
}