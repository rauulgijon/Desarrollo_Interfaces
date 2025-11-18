using DatagridPersonas.domain;
using ExampleMVCnoDatabase.Persistence;
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

namespace DatagridPersonas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Alumnado> lstPersonas;
        private Alumnado persona;

        // Tengo que crear otra lista para poder hacer las filtraciones y despuesd volver a mostrar todo
        private List<Alumnado> lstPersonasOriginal;




        public MainWindow()
        {

            InitializeComponent();
            lstPersonas = new List<Alumnado>();

            persona = new Alumnado();
            lstPersonas = persona.getListaAlumnado();
            dvgPersonas.ItemsSource = lstPersonas;
            start();

            //InitializeComponent();

            //persona = new Persona();
            //lstPersonas = Persona.getPersonas();
            //dgvPersonas.ItemsSource = lstPersonas;
            //btnModificar.IsEnabled = false;

            //lstPersonasOriginal = new List<Persona>(lstPersonas);
            //dgvPersonas.ItemsSource = lstPersonas;
        }


        public void start()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCurso.Text = "";
            btnModificar.IsEnabled = false;
        }
        private void dvgPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Alumnado p = dvgPersonas.SelectedItem as Alumnado;
            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                txtApellido.Text = p.Apellido;
                txtCurso.Text = p.Curso.ToString();
                btnModificar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            lstPersonas.Remove((Alumnado)dvgPersonas.SelectedItem);
            dvgPersonas.Items.Refresh();
            start();
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            Alumnado persona = new Alumnado(txtNombre.Text, txtApellido.Text, int.Parse(txtCurso.Text));
            persona.last();

            persona.insertar();

            lstPersonas.Add(persona);
            dvgPersonas.Items.Refresh();
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Alumnado p = (Alumnado)dvgPersonas.SelectedItem;
            if (p == null)
            {
                btnModificar.IsEnabled = false;
            }
            else
            {
                btnModificar.IsEnabled = true;
                p.Nombre = txtNombre.Text;
                p.Apellido = txtApellido.Text;
                p.Curso = int.Parse(txtCurso.Text);
                dvgPersonas.Items.Refresh();
                start();
            }
            /*
            Asi esta hecho un poco a pelo

            String nombre = txtNombre.Text;
            String apellido = txtApellido.Text;
            int edad = int.Parse(txtEdad.Text);

            persona = new Persona(nombre, apellido, edad);
            lstPersonas.Remove((Persona)dgvPersonas.SelectedItem);

            lstPersonas.Add(persona);
            dgvPersonas.Items.Refresh();
            start();
            */
        }

        /*
        private void btnFiltrarEdad_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtEdadFiltro.Text, out int edad))
            {
                string condicion = (cmbCondicionEdad.SelectedItem as ComboBoxItem).Content.ToString();

                switch (condicion)
                {
                    case "Mayores":
                        lstPersonas = lstPersonasOriginal.Where(p => p.Edad > edad).ToList();
                        break;
                    case "Menores":
                        lstPersonas = lstPersonasOriginal.Where(p => p.Edad < edad).ToList();
                        break;
                    case "Iguales":
                        lstPersonas = lstPersonasOriginal.Where(p => p.Edad == edad).ToList();
                        break;
                }

                dgvPersonas.ItemsSource = lstPersonas;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string texto = txtBuscar.Text.ToLower();

            lstPersonas = lstPersonasOriginal
                .Where(p => p.Nombre.ToLower().Contains(texto) || p.Apellido.ToLower().Contains(texto))
                .ToList();

            dgvPersonas.ItemsSource = lstPersonas;
        }

        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            string campo = (cmbOrdenar.SelectedItem as ComboBoxItem).Content.ToString();

            switch (campo)
            {
                case "Nombre":
                    lstPersonas = lstPersonas.OrderBy(p => p.Nombre).ToList();
                    break;
                case "Apellidos":
                    lstPersonas = lstPersonas.OrderBy(p => p.Apellido).ToList();
                    break;
                case "Edad":
                    lstPersonas = lstPersonas.OrderBy(p => p.Edad).ToList();
                    break;
            }

            dgvPersonas.ItemsSource = lstPersonas;
        }
        private void btnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            lstPersonas = new List<Persona>(lstPersonasOriginal);
            dgvPersonas.ItemsSource = lstPersonas;
        }

        */
    }
}