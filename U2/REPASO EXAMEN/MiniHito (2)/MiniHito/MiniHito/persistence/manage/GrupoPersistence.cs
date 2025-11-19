using System;
using System.Collections.Generic;
using System.Windows; // Necesario para MessageBox
using ExampleMVCnoDatabase.Persistence; // Necesario para usar DBBroker
using MiniHito.domain;

namespace MiniHito.persistence
{
    public class GrupoPersistence
    {
        // MÉTODO PARA LEER TODOS LOS GRUPOS DE LA BASE DE DATOS
        public static List<Grupo> LeerGrupos()
        {
            List<Grupo> grupos = new List<Grupo>();
            try
            {
                // IMPORTANTE: Tabla en singular 'grupo'
                string sql = "SELECT * FROM AceptasReto.grupo;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    // Asumiendo que la columna 0 es idGrupo y la 1 es nombre
                    if (fila.Count >= 2)
                    {
                        Grupo g = new Grupo(Convert.ToInt32(fila[0]), fila[1].ToString());
                        grupos.Add(g);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL LEER GRUPOS: " + ex.Message);
            }
            return grupos;
        }

        // MÉTODO PARA INSERTAR UN NUEVO GRUPO
        public void InsertarGrupo(Grupo g)
        {
            try
            {
                // IMPORTANTE: Tabla en singular 'grupo'
                string sql = "INSERT INTO AceptasReto.grupo (nombre) VALUES ('" + g.Nombre + "');";

                // Ejecutamos la consulta
                int filasAfectadas = DBBroker.obtenerAgente().modificar(sql);

                // Comprobación visual opcional
                // if (filasAfectadas > 0) MessageBox.Show("Guardado en BD");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL INSERTAR GRUPO: " + ex.Message);
            }
        }

        // MÉTODO PARA ACTUALIZAR EL NOMBRE DE UN GRUPO EXISTENTE
        public void ActualizarGrupo(Grupo g)
        {
            try
            {
                // IMPORTANTE: Tabla en singular 'grupo'
                string sql = "UPDATE AceptasReto.grupo SET nombre = '" + g.Nombre + "' WHERE idGrupo = " + g.Id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL ACTUALIZAR GRUPO: " + ex.Message);
            }
        }

        // MÉTODO PARA ELIMINAR UN GRUPO
        public void EliminarGrupo(int id)
        {
            try
            {
                // PASO 1: Liberar a los alumnos (ponerles idGrupo = 0) antes de borrar el grupo
                // Esto evita errores de claves foráneas y cumple el requisito del enunciado
                string sqlLiberarAlumnos = "UPDATE AceptasReto.alumnado SET idGrupo = 0 WHERE idGrupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberarAlumnos);

                // PASO 2: Borrar el grupo de la tabla 'grupo'
                string sqlBorrarGrupo = "DELETE FROM AceptasReto.grupo WHERE idGrupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlBorrarGrupo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL ELIMINAR GRUPO: " + ex.Message);
            }
        }
    }
}