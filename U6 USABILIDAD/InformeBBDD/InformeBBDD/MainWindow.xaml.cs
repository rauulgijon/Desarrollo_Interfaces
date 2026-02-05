using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformeBBDD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataTable tabla1;
        private static readonly Random r = new Random();
        private static readonly object Synclock = new object();

        public MainWindow()
        {
            InitializeComponent();
            //Crear un datatable con el mismo nombre lde la tabla del informe (.rpt)
            tabla1 = new DataTable("DataTable1");
            tabla1.Columns.Add("Name");
            tabla1.Columns.Add("Age");
            tabla1.Columns.Add("Address");
            tabla1.Columns.Add("Phone");
            // añadimos 100 filas a nuestro datatable de ejemplo
            for (int i = 0; i <= 100; i++)
            {
                //Crear una columna de datos de la tabla creada
                DataRow row = tabla1.NewRow();
                row["Name"] = "Raúl";
                row["Age"] = RandomNumber(0, 100);
                row["Address"] = "Mi casa";
                row["Phone"] = 692323187;
                tabla1.Rows.Add(row);
            }

            //Instanciamos crystalreport
            CrystalReport1 report = new CrystalReport1();
            //Incluir el daasosource al crystal report
            report.SetDataSource(tabla1);
            visor.ViewerCore.ReportSource = report;

        }

        public static int RandomNumber(int min, int max)
        {
            lock (Synclock)
            {
                return r.Next(min, max);
            }
        }

    }
}
