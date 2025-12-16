using DatagridPersonas.domain;
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
        private List<Persona> lstPersonas;
        private Persona persona;

        // Tengo que crear otra lista para poder hacer las filtraciones y despuesd volver a mostrar todo
        private List<Persona> lstPersonasOriginal;



        public MainWindow()
        {

            InitializeComponent();
            lstPersonas = new List<Persona>();

            persona = new Persona();

            try
            {
                persona.last();
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }

            lstPersonas = persona.getListaPersonas();
            dgvPersonas.ItemsSource = lstPersonas;
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
            txtEdad.Text = "";
            btnModificar.IsEnabled = false;
        }
        private void dgvPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Persona p = dgvPersonas.SelectedItem as Persona;
            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                txtApellido.Text = p.Apellido;
                txtEdad.Text = p.Edad.ToString();
                btnModificar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            lstPersonas.Remove((Persona)dgvPersonas.SelectedItem);
            dgvPersonas.Items.Refresh();
            start();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            Persona persona = new Persona(txtNombre.Text, txtApellido.Text, int.Parse(txtEdad.Text));
            persona.last();

            persona.insertar();

            lstPersonas.Add(persona);
            dgvPersonas.Items.Refresh();
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Persona p = (Persona)dgvPersonas.SelectedItem;
            if (p == null)
            {
                btnModificar.IsEnabled = false;
            }
            else
            {
                btnModificar.IsEnabled = true;
                p.Nombre = txtNombre.Text;
                p.Apellido = txtApellido.Text;
                p.Edad = int.Parse(txtEdad.Text);
                dgvPersonas.Items.Refresh();
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


    }
}


