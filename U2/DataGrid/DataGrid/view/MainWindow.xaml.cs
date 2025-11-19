using DataGrid.domain;
using System;
using System.Collections.Generic;
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

namespace DataGrid.view
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Persona> lstPersona;
        private Persona alumnado;

        public MainWindow()
        {
            InitializeComponent();
            lstPersona = new List<Persona>();
            alumnado = new Persona();
            lstPersona = alumnado.getPersonas();
            dvgPersonas.ItemsSource = lstPersona;
        }

        public void start()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCurso.Text = "";
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            lstPersona.Remove((Persona)dvgPersonas.SelectedItem);
            dvgPersonas.Items.Clear();
            start();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if(btnModificar.IsEnabled == false)
            {
                lstPersona.Remove((Persona)dvgPersonas.SelectedItem);
                dvgPersonas.Items.Refresh();
                lstPersona.Add(new Persona(txtNombre.Text, txtApellido.Text, int.Parse(txtCurso.Text)));
                dvgPersonas.Items.Refresh();
                txtApellido.Clear();
                
            }
            Persona persona = new Persona(txtNombre.Text, txtApellido.Text, int.Parse(txtCurso.Text));
            lstPersona.Add(persona);
            dvgPersonas.Items.Refresh();
            start();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Persona persona = (Persona)dvgPersonas.SelectedItem;
            persona.Nombre = txtNombre.Text;
            persona.Apellido = txtApellido.Text;
            persona.Edad = int.Parse(txtCurso.Text);

            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnAgregar.Content = "Actualizar datos";

            dvgPersonas.Items.Refresh();
            start();
        }

        private void dvgPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}