using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

namespace informes1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable tabla1;
        private static readonly Random r=new Random();
        private static readonly object Synclock=new object();
        public MainWindow()
        {
            InitializeComponent();
            //Crear un data table con el mismo nombre de la tabla del informe
            tabla1=new DataTable("DataTable1");
            //Crear las columnas con los mismos nombres de campos del datatable de crystal report
            tabla1.Columns.Add("Name");
            tabla1.Columns.Add("age");
            tabla1.Columns.Add("address");
            tabla1.Columns.Add("phone");
            // Añadimos 100 filas a nuestro datatable
            for(int i = 1; i <= 100; i++)
            {
                Random rnd = new Random();
                //Crear una columnad de datos de la tabla creada
                DataRow row = tabla1.NewRow();
                row["Name"] = "Rosa";
                row["Age"] = RandomNumber(10,100);
                row["Address"] = "Dirección";
                row["Phone"] = "666666666";
                tabla1.Rows.Add(row);
            }
            //Crear una instancia de crystal report
            CrystalReport1 report = new CrystalReport1();
            //Incluir el Datasource al crystal report
            report.Database.Tables["Datatable1"].SetDataSource(tabla1);
            //Asignar el informe para crystal report viewer
            visor.ViewerCore.ReportSource = report;

        }
        public static int RandomNumber(int min,int max)
        {
            lock (Synclock)
            {
                return r.Next(min, max);
            }
        }

        private void CrystalReportsViewer_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
