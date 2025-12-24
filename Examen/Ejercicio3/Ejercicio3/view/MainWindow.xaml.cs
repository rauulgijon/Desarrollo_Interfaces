using Ejercicio3.domain;
using Ejercicio3.persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Ejercicio3
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Jugador> lsPersonas;
        Jugador persona;
        private List<Jugador> lstPersonasOriginal;

        // Variables para la serpiente
        private List<Point> serpiente = new List<Point>();
        private int longitudSerpiente = 4; // Puedes cambiar la longitud


        public MainWindow()
        {
            InitializeComponent();
            lsPersonas = new ObservableCollection<Jugador>();
            persona = new Jugador();
            cargarPersonas();
            fillGrid();
            start();
            GenerarSerpiente();

        }
        public void fillGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Label txt = new Label();
                    txt.Content = "(" + i + "," + j+ ")";
                    txt.HorizontalAlignment = HorizontalAlignment.Stretch;
                    txt.VerticalAlignment = VerticalAlignment.Stretch;
                    Grid.SetRow(txt, i);
                    Grid.SetColumn(txt, j);
                    GridTable.Children.Add(txt);
                }
            }

        }

        
        
     
        private void cargarPersonas()
        {
            lsPersonas.Clear();
            var personas = JugadorPersistence.leerPersonas();
            foreach (var p in personas)
            {
                lsPersonas.Add(p);
            }
            dataGridPersonas.ItemsSource = lsPersonas;
        }

        public void start()
        {
            txtNombre.Text = "";
            txtFecha.Text = "";
            txtPuntos.Text = "";
            datePickerFechaNacimiento.SelectedDate = null;
            btnModificar.IsEnabled = false;
            dataGridPersonas.SelectedItem = null;
            cmbNivel.SelectedItem = null;
            btnModificar.IsEnabled = false; 
        }

        private void dataGridPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jugador p = dataGridPersonas.SelectedItem as Jugador;
            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                txtPuntos.Text = p.Puntuacion.ToString();

                foreach (ComboBoxItem item in cmbNivel.Items)
                    if (item.Tag != null && item.Tag.ToString() == p.Nivel.ToString()) { cmbNivel.SelectedItem = item; break; }
                btnModificar.IsEnabled = true;

                // Cargar la fecha de nacimiento si está disponible
                if (!string.IsNullOrWhiteSpace(p.Fechanac))
                {
                    if (DateTime.TryParse(p.Fechanac, out DateTime fecha))
                    {
                        datePickerFechaNacimiento.SelectedDate = fecha;
                    }
                }

                btnModificar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPuntos.Text) ||
                cmbNivel.SelectedItem == null ||
                !datePickerFechaNacimiento.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtPuntos.Text, out int puntuacion))
            {
                MessageBox.Show("La puntuación debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ComboBoxItem selectedNivel = cmbNivel.SelectedItem as ComboBoxItem;
            int nivel = int.Parse(selectedNivel.Tag.ToString());
            string fecha = txtFecha.Text;
            fecha = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");

            Jugador nuevo = new Jugador(txtNombre.Text, puntuacion, fecha, nivel);

            try
            {
                nuevo.insertar();
                MessageBox.Show("Jugador agregado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarPersonas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Jugador seleccionado = dataGridPersonas.SelectedItem as Jugador;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un jugador para modificar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPuntos.Text) ||
                cmbNivel.SelectedItem == null ||
                !datePickerFechaNacimiento.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtPuntos.Text, out int puntuacion))
            {
                MessageBox.Show("La puntuación debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ComboBoxItem selectedNivel = cmbNivel.SelectedItem as ComboBoxItem;
            int nivel = int.Parse(selectedNivel.Tag.ToString());
            string fecha = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");

            seleccionado.Nombre = txtNombre.Text;
            seleccionado.Puntuacion = puntuacion;
            seleccionado.Fechanac = fecha;
            seleccionado.Nivel = nivel;

            try
            {
                seleccionado.actualizar();
                MessageBox.Show("Jugador modificado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarPersonas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Jugador seleccionado = dataGridPersonas.SelectedItem as Jugador;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un jugador para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                seleccionado.eliminar();
                lsPersonas.Remove(seleccionado);
                MessageBox.Show("Jugador eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void masParedes(object sender, RoutedEventArgs e)
        {
            
        }

        private void GenerarSerpiente()
        {
            serpiente.Clear();
            int filaInicial = 2; 
            int columnaInicial = 0;

            for (int i = 0; i < longitudSerpiente; i++)
            {
                serpiente.Add(new Point(columnaInicial + i, filaInicial));
            }

            PintarSerpiente();
        }
        
        private void GenerarRatones()
        {

        }

        private void PintarSerpiente()
        {
            GridTable.Children.Clear();
            fillGrid(); // Dibuja el fondo

            foreach (var punto in serpiente)
            {
                Label lbl = new Label
                {
                    Content = "S",
                    Background = Brushes.Green,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                Grid.SetRow(lbl, (int)punto.Y);
                Grid.SetColumn(lbl, (int)punto.X);
                GridTable.Children.Add(lbl);
            }
        }

        private void btnGenerarSerpiente_Click(object sender, RoutedEventArgs e)
        {
            GenerarSerpiente();
        }

        private void menosParedes(object sender, RoutedEventArgs e)
        {

        }
        
    }
}

