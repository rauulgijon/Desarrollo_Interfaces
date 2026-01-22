using System;
using System.Collections.Generic;
using System.Windows;
using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;

namespace MiniHito.persistence
{
    public class RetoPersistence
    {
        public static List<Reto> LeerRetos()
        {
            List<Reto> lista = new List<Reto>();
            try
            {
                string sql = "SELECT * FROM aceptasreto.reto;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    // 0:ID_RETO, 1:DESCRIPCION, 2:ACTIVO
                    Reto r = new Reto();
                    r.Id = Convert.ToInt32(fila[0]);
                    r.Descripcion = fila[1].ToString();
                    // Convertimos TinyInt (0/1) a Boolean si tu clase lo usa, o int si prefieres
                    r.Activo = Convert.ToInt32(fila[2]) == 1;

                    lista.Add(r);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error leyendo retos: " + ex.Message); }
            return lista;
        }

        public void InsertarReto(Reto r)
        {
            try
            {
                int activoVal = r.Activo ? 1 : 0;
                string sql = "INSERT INTO aceptasreto.reto (DESCRIPCION, ACTIVO) VALUES ('" +
                             r.Descripcion + "', " +
                             activoVal + ");";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando reto: " + ex.Message); }
        }

        public void ActualizarReto(Reto r)
        {
            try
            {
                int activoVal = r.Activo ? 1 : 0;
                string sql = "UPDATE aceptasreto.reto SET " +
                             "DESCRIPCION = '" + r.Descripcion + "', " +
                             "ACTIVO = " + activoVal + " " +
                             "WHERE ID_RETO = " + r.Id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando reto: " + ex.Message); }
        }

        public void EliminarReto(int id)
        {
            try
            {
                string sql = "DELETE FROM aceptasreto.reto WHERE ID_RETO = " + id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando reto: " + ex.Message); }
        }
    }
}