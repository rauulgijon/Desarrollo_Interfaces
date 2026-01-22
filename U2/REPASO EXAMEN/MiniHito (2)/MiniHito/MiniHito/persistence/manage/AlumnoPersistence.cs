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
<<<<<<< Updated upstream
        // SELECT: Estaba bien, pero aseguramos la lectura correcta
=======
        private DataTable personaTable { get; set; }

        public AlumnoPersistence()
        {
            personaTable = new DataTable();
        }

>>>>>>> Stashed changes
        public static List<Alumno> leerPersonas()
        {
            List<Alumno> personas = new List<Alumno>();
            try
            {
<<<<<<< Updated upstream
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
=======
                List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM AceptasReto.alumnado;");

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
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                // SQL ajustado a tus columnas reales: NOMBRE, APELLIDO, CURSO, ID_GRUPO
                // Nota: He puesto valores por defecto para CORREO y CICLO para que no falle el INSERT si son Not Null
                string sql = "INSERT INTO aceptasreto.alumno (NOMBRE, APELLIDO, CURSO, CORREO, CICLO, ID_GRUPO) VALUES ('" +
                             alumno.Nombre + "', '" +
                             alumno.Apellidos + "', '" +
                             alumno.Especialidad + "', " +
                             "'correo@default.com', 'DAW', " + // Valores relleno obligatorios
=======
                // CORREGIDO: Usamos 'grupo' (tu columna real) en vez de 'idgrupo'
                string sql = "INSERT INTO AceptasReto.alumnado (nombre, apellidos, especialidad, grupo) VALUES ('" +
                             alumno.Nombre + "', '" +
                             alumno.Apellidos + "', " +
                             alumno.Especialidad + ", " +
>>>>>>> Stashed changes
                             alumno.Grupo + ");";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando alumno: " + ex.Message); }
        }

        public void actualizarPersona(Alumno alumno)
        {
            try
            {
<<<<<<< Updated upstream
                // CORREGIDO: Tabla 'alumno', Clave 'ID_ALUMNO', Columnas correctas
                string sql = "UPDATE aceptasreto.alumno SET " +
                             "NOMBRE = '" + alumno.Nombre + "', " +
                             "APELLIDO = '" + alumno.Apellidos + "', " +
                             "CURSO = '" + alumno.Especialidad + "', " +
                             "ID_GRUPO = " + alumno.Grupo + " " +
                             "WHERE ID_ALUMNO = " + alumno.Id + ";";
=======
                // CORREGIDO: Usamos 'grupo' en vez de 'idgrupo'
                string sql = "UPDATE AceptasReto.alumnado SET " +
                             "nombre = '" + alumno.Nombre + "', " +
                             "apellidos = '" + alumno.Apellidos + "', " +
                             "especialidad = " + alumno.Especialidad + ", " +
                             "grupo = " + alumno.Grupo + " " +
                             "WHERE idAlumnado = " + alumno.Id + ";";
>>>>>>> Stashed changes

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando alumno: " + ex.Message); }
        }

        public void eliminarPersona(int id)
        {
            try
            {
<<<<<<< Updated upstream
                // CORREGIDO: Estaba 'alumnado' (ERROR) -> Ahora es 'alumno' (BIEN)
                // CORREGIDO: Clave 'ID_ALUMNO'
                string sql = "DELETE FROM aceptasreto.alumno WHERE ID_ALUMNO = " + id + ";";
=======
                string sql = "DELETE FROM AceptasReto.alumnado WHERE idAlumnado = " + id + ";";
>>>>>>> Stashed changes
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando alumno: " + ex.Message); }
        }
    }
}