using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniHito.persistence.manage
{
    internal class TalentLabPersistence
    {
        public static List<TalenLab> LeerTalentLab()
        {
            List<TalenLab> lista = new List<TalenLab>();
            try
            {
                string sql = "SELECT * FROM aceptasreto.talent_lab;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    TalenLab r = new TalenLab();
                    r.Id = Convert.ToInt32(fila[0]);
                    r.Titulo = fila[1].ToString();      
                    r.Descripcion = fila[2].ToString();

                    lista.Add(r);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error leyendo retos: " + ex.Message); }
            return lista;
        }

        public void InsertarTalentLab(TalenLab r)
        {
            try
            {

                // Consulta actualizada con NOMBRE y FECHA_INICIO
                string sql = $"INSERT INTO aceptasreto.talent_lab (TITULO, DESCRIPCION) " +
                             $"VALUES ('{r.Titulo}', '{r.Descripcion}');";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando talent lab: " + ex.Message); }
        }

        public void ActualizarTalentLab(TalenLab r)
        {
            try
            {

                string sql = $"UPDATE aceptasreto.talent_lab SET " +
                             $"NOMBRE = '{r.Titulo}', " +
                             $"DESCRIPCION = '{r.Descripcion}', " +
                             $"WHERE ID_TALENT_LAB = {r.Id};";

                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando talent lab: " + ex.Message); }
        }

        public void EliminarTalentLab(int id)
        {
            try
            {
                string sql = "DELETE FROM aceptasreto.talent_lab WHERE ID_TALENT_LAB = " + id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando talent lab: " + ex.Message); }
        }
    }
}
