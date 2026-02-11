using InformesBBDD.form;
using Minihito2.domain;
using Minihito2.persistence.manage;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformesBBDD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable tabla1;
        UsuarioPersistence usuarioPersistence;

        public MainWindow()
        {
            InitializeComponent();

            tabla1 = new DataTable("DataTable1");
            usuarioPersistence = new UsuarioPersistence();

            tabla1.Columns.Add("idUsuario");
            tabla1.Columns.Add("username");
            tabla1.Columns.Add("password");
            tabla1.Columns.Add("roles");
            tabla1.Columns.Add("activo");

            List<Usuario> lista = usuarioPersistence.obtenerTodosUsuarios();

            foreach(var usuario in lista)
            {
                DataRow row = tabla1.NewRow();
                row["idUsuario"] = usuario.idUsuario;
                row["username"] = usuario.username;
                row["password"] = usuario.password;
                row["roles"] = usuario.roles;
                row["activo"] = usuario.activo ? 1 : 0;
                tabla1.Rows.Add(row);
            }

            CrystalReport1 reporte = new CrystalReport1();

            reporte.Database.Tables["DataTable1"].SetDataSource(tabla1);

            visor.ViewerCore.ReportSource = reporte;

        }

        private void CRV_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
