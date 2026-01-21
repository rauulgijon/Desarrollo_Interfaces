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
                string sql = "SELECT * FROM aceptasreto.grupo;";
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
                string sql = "INSERT INTO AceptasReto.grupo (nombre) VALUES ('" + g.Nombre + "');";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando grupo: " + ex.Message); }
        }

        public void ActualizarGrupo(Grupo g)
        {
            try
            {
                // La tabla 'grupo' SÍ tiene 'idgrupo' (clave primaria)
                string sql = "UPDATE aceptasreto.grupo SET nombre = '" + g.Nombre + "' WHERE idgrupo = " + g.Id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando grupo: " + ex.Message); }
        }

        public void EliminarGrupo(int id)
        {
            try
            {
                // CORREGIDO: La tabla 'alumnado' tiene columna 'grupo'

                // La tabla 'grupo' tiene columna 'idgrupo'
                string sqlLiberar = "UPDATE aceptasreto.alumnado SET grupo = 0 WHERE grupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlLiberar);

                // La tabla 'grupo' tiene columna 'idgrupo'
                string sqlBorrar = "DELETE FROM aceptasreto.grupo WHERE idgrupo = " + id + ";";
                DBBroker.obtenerAgente().modificar(sqlBorrar);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando grupo: " + ex.Message); }
        }

        public static Grupo ObtenerGrupoPorNombre(string nombre)
        {
            try
            {
                string sql = "SELECT * FROM aceptasreto.grupo WHERE nombre = '" + nombre + "' ORDER BY idgrupo DESC LIMIT 1;";
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