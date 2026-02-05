using System;
using System.Collections.Generic;
using System.Windows;
using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;

namespace MiniHito.persistence
{
    public class GrupoPersistence
    {
        public static List<Grupo> LeerGrupos()
        {
            List<Grupo> grupos = new List<Grupo>();
            try
            {
                string sql = "SELECT * FROM AceptasReto.grupo;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    if (fila.Count >= 2)
                    {
                        // En tu DB es 'DESCRIPCION', pero en C# lo llamamos 'Nombre'
                        Grupo g = new Grupo(Convert.ToInt32(fila[0]), fila[1].ToString());
                        grupos.Add(g);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error leyendo grupos: " + ex.Message); }
            return grupos;
        }

        public void InsertarGrupo(Grupo g)
        {
            try
            {
                string sql = "INSERT INTO AceptasReto.grupo (DESCRIPCION) VALUES ('" + g.Nombre + "');";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando grupo: " + ex.Message); }
        }

        public void ActualizarGrupo(Grupo g)
        {
            try
            {
                string sql = "UPDATE AceptasReto.grupo SET DESCRIPCION = '" + g.Nombre + "' WHERE ID_GRUPO = " + g.Id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando grupo: " + ex.Message); }
        }

        public void EliminarGrupo(int id)
        {
            try
            {
                // Liberar alumnos (ID_GRUPO = 0 o NULL)
                string sqlLiberar = "UPDATE AceptasReto.alumno SET ID_GRUPO = 0 WHERE ID_GRUPO = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberar);

                string sqlBorrar = "DELETE FROM AceptasReto.grupo WHERE ID_GRUPO = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlBorrar);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando grupo: " + ex.Message); }
        }

        public static Grupo ObtenerGrupoPorNombre(string nombre)
        {
            try
            {
                string sql = "SELECT * FROM AceptasReto.grupo WHERE DESCRIPCION = '" + nombre + "' ORDER BY ID_GRUPO DESC LIMIT 1;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);
                if (aux.Count > 0)
                {
                    List<Object> fila = aux[0] as List<Object>;
                    return new Grupo(Convert.ToInt32(fila[0]), fila[1].ToString());
                }
            }
            catch { }
            return null;
        }
    }
}