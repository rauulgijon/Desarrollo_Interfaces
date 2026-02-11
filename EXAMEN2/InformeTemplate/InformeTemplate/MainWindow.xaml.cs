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
                string sql = "SELECT NOMBRE, APELLIDO, CURSO FROM alumno;";
                List<Object> listaDatos = DBBroker.obtenerAgente().leer(sql);

                // 2. Crear DataTable compatible con el DataSet1.xsd
                DataTable dt = new DataTable("Alumno");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("APELLIDO");
                dt.Columns.Add("CURSO");

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