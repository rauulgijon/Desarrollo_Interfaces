using System;
using System.Collections.Generic;
using System.Windows;
using ExampleMVCnoDatabase.Persistence;
using MiniHito.domain;

namespace MiniHito.persistence
{
    public class EmpresaPersistence
    {
        public static List<Empresa> LeerEmpresas()
        {
            List<Empresa> lista = new List<Empresa>();
            try
            {
                string sql = "SELECT * FROM AceptasReto.empresa;";
                List<Object> aux = DBBroker.obtenerAgente().leer(sql);

                foreach (List<Object> fila in aux)
                {
                    // Orden en tu modelo: ID_EMPRESA, RAZON_SOCIAL, DIRECCION, CIUDAD, TELEFONO, CORREO
                    Empresa e = new Empresa();
                    e.Id = Convert.ToInt32(fila[0]);
                    e.RazonSocial = fila[1].ToString();
                    e.Direccion = fila[2].ToString();
                    e.Ciudad = fila[3].ToString();
                    e.Telefono = fila[4].ToString();
                    e.Correo = fila[5].ToString();

                    lista.Add(e);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error leyendo empresas: " + ex.Message); }
            return lista;
        }

        public void InsertarEmpresa(Empresa e)
        {
            try
            {
                string sql = "INSERT INTO AceptasReto.empresa (RAZON_SOCIAL, DIRECCION, CIUDAD, TELEFONO, CORREO) VALUES ('" +
                             e.RazonSocial + "', '" +
                             e.Direccion + "', '" +
                             e.Ciudad + "', '" +
                             e.Telefono + "', '" +
                             e.Correo + "');";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error insertando empresa: " + ex.Message); }
        }

        public void ActualizarEmpresa(Empresa e)
        {
            try
            {
                string sql = "UPDATE AceptasReto.empresa SET " +
                             "RAZON_SOCIAL = '" + e.RazonSocial + "', " +
                             "DIRECCION = '" + e.Direccion + "', " +
                             "CIUDAD = '" + e.Ciudad + "', " +
                             "TELEFONO = '" + e.Telefono + "', " +
                             "CORREO = '" + e.Correo + "' " +
                             "WHERE ID_EMPRESA = " + e.Id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error actualizando empresa: " + ex.Message); }
        }

        public void EliminarEmpresa(int id)
        {
            try
            {
                string sql = "DELETE FROM AceptasReto.empresa WHERE ID_EMPRESA = " + id + ";";
                DBBroker.obtenerAgente().modificar(sql);
            }
            catch (Exception ex) { MessageBox.Show("Error eliminando empresa: " + ex.Message); }
        }
    }
}