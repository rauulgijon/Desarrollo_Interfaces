using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace MiniHito.persistence
{
    // Clase de persistencia para gestionar las operaciones CRUD de Alumno en la base de datos
    // Actúa como intermediario entre la capa de dominio y el DBBroker
    class AlumnoPersistence
    {
        // Tabla de datos para almacenar información de personas (no se está utilizando actualmente)
        private DataTable personaTable { get; set;}
        
        // Constructor que inicializa la tabla de personas
        public AlumnoPersistence()
        {
            personaTable = new DataTable();
            
        }

        // Lee todos los alumnos de la base de datos y los convierte en objetos Alumno
        public static List<Alumno> leerPersonas()
        {
            Alumno a = null;
            // Asegúrate de que el SELECT trae todo
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from AceptasReto.alumnado;");
            List<Alumno> personas = new List<Alumno>();

            foreach (List<Object> fila in aux)
            {
                // Lectura segura del Grupo (índice 4, asumiendo orden: id, nombre, apellidos, esp, grupo)
                int grupo = 0;
                if (fila.Count > 4 && fila[4] != null && fila[4].ToString() != "")
                {
                    int.TryParse(fila[4].ToString(), out grupo);
                }

                a = new Alumno(Convert.ToInt32(fila[0]), fila[1].ToString(), fila[2].ToString(), Convert.ToInt32(fila[3]), grupo);
                personas.Add(a);
            }
            return personas;
        }

        // Inserta un nuevo alumno en la base de datos
        public void insertarPersona(Alumno alumno)
        {
            // Construye la consulta SQL INSERT con los datos del alumno
            string sql = "INSERT INTO AceptasReto.alumnado (nombre, apellidos, especialidad) VALUES ('" + 
                         alumno.Nombre + "', '" + 
                         alumno.Apellidos + "', " + 
                         alumno.Especialidad + ");";
            // Ejecuta la consulta de modificación
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        // Actualiza los datos de un alumno existente en la base de datos
        public void actualizarPersona(Alumno alumno)
        {
            // AÑADIDO: idGrupo = ...
            string sql = "UPDATE AceptasReto.alumnado SET " +
                            "nombre = '" + alumno.Nombre + "', " +
                            "apellidos = '" + alumno.Apellidos + "', " +
                            "especialidad = " + alumno.Especialidad + ", " +
                            "idGrupo = " + alumno.Grupo + " " +
                            "WHERE idAlumnado = " + alumno.Id + ";";

            DBBroker.obtenerAgente().modificar(sql);
        }

        // Elimina un alumno de la base de datos usando su ID
        public void eliminarPersona(int id)
        {
            // Construye la consulta SQL DELETE usando el ID del alumno
            string sql = "DELETE FROM AceptasReto.alumnado WHERE idAlumnado   = " + id + ";";
            // Ejecuta la consulta de modificación
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

    }
}
