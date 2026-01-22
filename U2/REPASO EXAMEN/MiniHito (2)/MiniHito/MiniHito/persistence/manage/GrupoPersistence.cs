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
<<<<<<< Updated upstream
                string sql = "SELECT * FROM aceptasreto.grupo;";
=======
                string sql = "SELECT * FROM AceptasReto.grupo;";
>>>>>>> Stashed changes
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    if (fila.Count >= 2)
                    {
<<<<<<< Updated upstream
                        // 0: ID_GRUPO, 1: DESCRIPCION (O NOMBRE)
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                string sql = "INSERT INTO aceptasreto.grupo (DESCRIPCION) VALUES ('" + g.Nombre + "');";
=======
                string sql = "INSERT INTO AceptasReto.grupo (nombre) VALUES ('" + g.Nombre + "');";
>>>>>>> Stashed changes
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando grupo: " + ex.Message); }
        }

        public void ActualizarGrupo(Grupo g)
        {
            try
            {
<<<<<<< Updated upstream
                string sql = "UPDATE aceptasreto.grupo SET DESCRIPCION = '" + g.Nombre + "' WHERE ID_GRUPO = " + g.Id + ";";
=======
                // La tabla 'grupo' SÍ tiene 'idgrupo' (clave primaria)
                string sql = "UPDATE AceptasReto.grupo SET nombre = '" + g.Nombre + "' WHERE idgrupo = " + g.Id + ";";
>>>>>>> Stashed changes
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando grupo: " + ex.Message); }
        }

        public void EliminarGrupo(int id)
        {
            try
            {
<<<<<<< Updated upstream
                // Primero: Quitamos a los alumnos de este grupo (poner a NULL o 0)
                string sqlLiberar = "UPDATE aceptasreto.alumno SET ID_GRUPO = 0 WHERE ID_GRUPO = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberar);

                // Segundo: Borramos el grupo
                string sqlBorrar = "DELETE FROM aceptasreto.grupo WHERE ID_GRUPO = " + id + ";";
=======
                // CORREGIDO: La tabla 'alumnado' tiene columna 'grupo'
                string sqlLiberar = "UPDATE AceptasReto.alumnado SET grupo = 0 WHERE grupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberar);

                // La tabla 'grupo' tiene columna 'idgrupo'
                string sqlBorrar = "DELETE FROM AceptasReto.grupo WHERE idgrupo = " + id + ";";
>>>>>>> Stashed changes
                DBBroker.obtenerAgente().modificar(sqlBorrar);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando grupo: " + ex.Message); }
        }

        public static Grupo ObtenerGrupoPorNombre(string nombre)
        {
            try
            {
<<<<<<< Updated upstream
                // Buscar por DESCRIPCION
                string sql = "SELECT * FROM aceptasreto.grupo WHERE DESCRIPCION = '" + nombre + "' ORDER BY ID_GRUPO DESC LIMIT 1;";
=======
                string sql = "SELECT * FROM AceptasReto.grupo WHERE nombre = '" + nombre + "' ORDER BY idgrupo DESC LIMIT 1;";
>>>>>>> Stashed changes
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