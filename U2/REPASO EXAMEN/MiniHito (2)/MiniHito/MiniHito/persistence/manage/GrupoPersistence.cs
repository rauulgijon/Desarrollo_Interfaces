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
<<<<<<< HEAD
                string sql = "SELECT * FROM AceptasReto.grupo;";
=======
                string sql = "SELECT * FROM aceptasreto.grupo;";
>>>>>>> c74427af33eaaa8c2592c24ba51e07f593c2c5b8
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    if (fila.Count >= 2)
                    {
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
<<<<<<< HEAD
                string sql = "INSERT INTO AceptasReto.grupo (nombre) VALUES ('" + g.Nombre + "');";
=======
                string sql = "INSERT INTO aceptasreto.grupo (nombre) VALUES ('" + g.Nombre + "');";
>>>>>>> c74427af33eaaa8c2592c24ba51e07f593c2c5b8
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando grupo: " + ex.Message); }
        }

        public void ActualizarGrupo(Grupo g)
        {
            try
            {
                // La tabla 'grupo' SÍ tiene 'idgrupo' (clave primaria)
<<<<<<< HEAD
                string sql = "UPDATE AceptasReto.grupo SET nombre = '" + g.Nombre + "' WHERE idgrupo = " + g.Id + ";";
=======
                string sql = "UPDATE aceptasreto.grupo SET nombre = '" + g.Nombre + "' WHERE idgrupo = " + g.Id + ";";
>>>>>>> c74427af33eaaa8c2592c24ba51e07f593c2c5b8
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando grupo: " + ex.Message); }
        }

        public void EliminarGrupo(int id)
        {
            try
            {
                // CORREGIDO: La tabla 'alumnado' tiene columna 'grupo'
<<<<<<< HEAD
                string sqlLiberar = "UPDATE AceptasReto.alumnado SET grupo = 0 WHERE grupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberar);

                // La tabla 'grupo' tiene columna 'idgrupo'
                string sqlBorrar = "DELETE FROM AceptasReto.grupo WHERE idgrupo = " + id + ";";
=======
                string sqlLiberar = "UPDATE aceptasreto.alumnado SET grupo = 0 WHERE grupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberar);

                // La tabla 'grupo' tiene columna 'idgrupo'
                string sqlBorrar = "DELETE FROM aceptasreto.grupo WHERE idgrupo = " + id + ";";
>>>>>>> c74427af33eaaa8c2592c24ba51e07f593c2c5b8
                DBBroker.obtenerAgente().modificar(sqlBorrar);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando grupo: " + ex.Message); }
        }

        public static Grupo ObtenerGrupoPorNombre(string nombre)
        {
            try
            {
<<<<<<< HEAD
                string sql = "SELECT * FROM AceptasReto.grupo WHERE nombre = '" + nombre + "' ORDER BY idgrupo DESC LIMIT 1;";
=======
                string sql = "SELECT * FROM aceptasreto.grupo WHERE nombre = '" + nombre + "' ORDER BY idgrupo DESC LIMIT 1;";
>>>>>>> c74427af33eaaa8c2592c24ba51e07f593c2c5b8
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