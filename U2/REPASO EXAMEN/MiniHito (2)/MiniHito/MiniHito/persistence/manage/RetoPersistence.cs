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
                    // Mapeo actualizado: 
                    // 0:ID_RETO, 1:NOMBRE, 2:DESCRIPCION, 3:FECHA_INICIO, 4:ACTIVO
                    Reto r = new Reto();
                    r.Id = Convert.ToInt32(fila[0]);
                    r.Nombre = fila[1].ToString();      // CAMPO NUEVO
                    r.Descripcion = fila[2].ToString();

                    // Manejo seguro de fechas
                    if (fila[3] != null && DateTime.TryParse(fila[3].ToString(), out DateTime fecha))
                    {
                        r.FechaInicio = fecha;          // CAMPO NUEVO
                    }

                    // Convertir TinyInt (0/1) a Bool
                    r.Activo = Convert.ToInt32(fila[4]) == 1;

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
                // Formato de fecha para MySQL: YYYY-MM-DD
                string fechaSql = r.FechaInicio.ToString("yyyy-MM-dd");

                // Consulta actualizada con NOMBRE y FECHA_INICIO
                string sql = $"INSERT INTO aceptasreto.reto (NOMBRE, DESCRIPCION, FECHA_INICIO, ACTIVO) " +
                             $"VALUES ('{r.Nombre}', '{r.Descripcion}', '{fechaSql}', {activoVal});";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando reto: " + ex.Message); }
        }

        public void ActualizarReto(Reto r)
        {
            try
            {
                int activoVal = r.Activo ? 1 : 0;
                string fechaSql = r.FechaInicio.ToString("yyyy-MM-dd");

                // Consulta actualizada
                string sql = $"UPDATE aceptasreto.reto SET " +
                             $"NOMBRE = '{r.Nombre}', " +
                             $"DESCRIPCION = '{r.Descripcion}', " +
                             $"FECHA_INICIO = '{fechaSql}', " +
                             $"ACTIVO = {activoVal} " +
                             $"WHERE ID_RETO = {r.Id};";

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