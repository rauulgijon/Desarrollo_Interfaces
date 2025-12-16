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
        private DataTable personaTable { get; set; }

        public AlumnoPersistence()
        {
            personaTable = new DataTable();
        }

        public static List<Alumno> leerPersonas()
        {
            List<Alumno> personas = new List<Alumno>();
            try
            {
                List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM aceptasreto.alumnado;");

                foreach (List<Object> fila in aux)
                {
                    // Mapeo de columnas según tu tabla: 
                    // 0:id, 1:nombre, 2:apellidos, 3:especialidad, 4:grupo
                    int id = Convert.ToInt32(fila[0]);
                    string nombre = fila[1].ToString();
                    string apellidos = fila[2].ToString();
                    int especialidad = Convert.ToInt32(fila[3]);
                    int grupo = 0;

                    if (fila.Count >= 5 && fila[4] != null && fila[4].ToString() != "")
                    {
                        int.TryParse(fila[4].ToString(), out grupo);
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
                // CORREGIDO: Usamos 'grupo' (tu columna real) en vez de 'idgrupo'
                string sql = "INSERT INTO aceptasreto.alumnado (nombre, apellidos, especialidad, grupo) VALUES ('" +
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
                // CORREGIDO: Usamos 'grupo' en vez de 'idgrupo'
                string sql = "UPDATE aceptasreto.alumnado SET " +
                             "nombre = '" + alumno.Nombre + "', " +
                             "apellidos = '" + alumno.Apellidos + "', " +
                             "especialidad = " + alumno.Especialidad + ", " +
                             "grupo = " + alumno.Grupo + " " +
                             "WHERE idAlumnado = " + alumno.Id + ";";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando alumno: " + ex.Message); }
        }

        public void eliminarPersona(int id)
        {
            try
            {
                string sql = "DELETE FROM aceptasreto.alumnado WHERE idAlumnado = " + id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando alumno: " + ex.Message); }
        }
    }
}