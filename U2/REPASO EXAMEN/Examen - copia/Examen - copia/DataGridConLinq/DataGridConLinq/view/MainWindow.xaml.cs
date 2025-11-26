using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataGridConLinq;
using Datagrid.persistence;

namespace Datagrid
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Persona> lsPersonas;
        Persona persona;
        
        public MainWindow()
        {
            InitializeComponent();
            lsPersonas = new ObservableCollection<Persona>();
            persona = new Persona();
            cargarPersonas();
        }
        
        private void cargarPersonas()
        {
            lsPersonas.Clear();
            var personas = PersonaPersistence.leerPersonas();
            foreach (var p in personas)
            {
                lsPersonas.Add(p);
            }
            dataGridPersonas.ItemsSource = lsPersonas;
        }
        
        public void start() 
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            datePickerFechaNacimiento.SelectedDate = null;
            btnModificar.IsEnabled = false;
            dataGridPersonas.SelectedItem = null;
        }
        
        private void dataGridPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Persona p = dataGridPersonas.SelectedItem as Persona;
           if (p != null) 
           {
                txtNombre.Text = p.Nombre;
                txtApellido.Text = p.Apellidos;
                txtEdad.Text = p.Edad.ToString();
                
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
        
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Persona p = dataGridPersonas.SelectedItem as Persona;
            if (p != null)
            {
                try
                {
                    // Eliminar de la base de datos
                    p.eliminar();
                    
                    // Eliminar de la lista observable
                    lsPersonas.Remove(p);
                    
                    MessageBox.Show("Persona eliminada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la persona: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una persona para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                string.IsNullOrWhiteSpace(txtApellido.Text) || 
                string.IsNullOrWhiteSpace(txtEdad.Text) ||
                !datePickerFechaNacimiento.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtEdad.Text, out int edad))
            {
                MessageBox.Show("La edad debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Persona existente = lsPersonas.FirstOrDefault(p => 
                p.Nombre == txtNombre.Text && 
                p.Apellidos == txtApellido.Text && 
                p.Edad == edad);

            if (existente != null)
            {
                MessageBox.Show("La persona ya existe", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    Persona psnew = new Persona(txtNombre.Text, txtApellido.Text, edad, datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    psnew.insertar();
                    
                    MessageBox.Show("Persona agregada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    // Recargar la lista desde la base de datos para obtener el ID asignado
                    cargarPersonas();
                    start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar la persona: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
        
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Persona p = dataGridPersonas.SelectedItem as Persona;
            if (p == null)
            {
                btnModificar.IsEnabled = false;
                MessageBox.Show("Por favor, seleccione una persona", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                string.IsNullOrWhiteSpace(txtApellido.Text) || 
                string.IsNullOrWhiteSpace(txtEdad.Text) ||
                !datePickerFechaNacimiento.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtEdad.Text, out int edad))
            {
                MessageBox.Show("La edad debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Actualizar los valores en el objeto
                p.Nombre = txtNombre.Text;
                p.Apellidos = txtApellido.Text;
                p.Edad = edad;
                p.Fechanac = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");
                
                // Actualizar en la base de datos
                p.actualizar();
                
                MessageBox.Show("Persona modificada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Refrescar el DataGrid
                dataGridPersonas.Items.Refresh();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la persona: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}