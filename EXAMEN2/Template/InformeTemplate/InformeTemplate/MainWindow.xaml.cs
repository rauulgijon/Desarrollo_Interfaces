using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using InformeTemplate.persistence;

namespace InformeTemplate
{
    /// <summary>
    /// Clase principal
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarInforme();
        }

        /// <summary>Cargar the informe.</summary>
        private void CargarInforme()
        {
            try
            {
                // 1. Consulta mediante Broker
                string sql = "SELECT nombre, especie, raza FROM examen.mascota ORDERED BY nombre;";
                List<Object> listaDatos = DBBroker.obtenerAgente().leer(sql);

                // 2. Crear DataTable compatible con el DataSet1.xsd
                DataTable dt = new DataTable("Mascota");
                dt.Columns.Add("nombre");
                dt.Columns.Add("especie");
                dt.Columns.Add("raza");

                // 3. Volcar datos
                foreach (List<Object> fila in listaDatos)
                {
                    dt.Rows.Add(fila[0].ToString(), fila[1].ToString(), fila[2].ToString());
                }

                // 4. Vincular con el archivo .rpt
                CrystalReport1 reporte = new CrystalReport1();
                reporte.SetDataSource(dt);
                VisorReporte.ViewerCore.ReportSource = reporte;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar informe: " + ex.Message);
            }
        }
    }
}