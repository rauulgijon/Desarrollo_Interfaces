using Examen.domain;
using Examen.persistence;
using Examen.persistence.manage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Examen
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Cliente> lsClientes;

        Cliente cliente;
        ObservableCollection<Mascota> lsMascotas;

        Mascota mascota;
        ObservableCollection<Consulta> lsConsultas;

        Consulta consulta;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            lsClientes = new ObservableCollection<Cliente>();
            lsMascotas = new ObservableCollection<Mascota>();
            lsConsultas = new ObservableCollection<Consulta>();
            cliente = new Cliente();
            mascota = new Mascota();
            consulta = new Consulta();
            cargarClientes();
            cargarMascotas();
            cargarConsultas();

            start();

        }
        /// <summary>
        /// Cargars the clientes.
        /// </summary>
        private void cargarClientes()
        {
            lsClientes.Clear();
            var personas = ClientePersistence.leerClientes();
            foreach (var p in personas)
            {
                lsClientes.Add(p);
            }
            dataGridClientes.ItemsSource = lsClientes;
        }
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            // Limpieza de campos del formulario
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";

            txtNombreMascota.Text = "";
            txtEspecie.Text = "";
            txtRaza.Text = "";
            datePickerFechaNacimiento.SelectedDate = null;

            txtDiagnostico.Text = "";
            datePickerFechaConsulta.SelectedDate = null;
            txtMascota.Text = "";
            txtVeterinario.Text = "";
            // Bloqueo de botones y selección
            btnModificarCliente.IsEnabled = false;
            dataGridClientes.SelectedItem = null;

            btnModificarMascota.IsEnabled = false;
            dataGridMascotas.SelectedItem = null;

            btnModificarConsulta.IsEnabled = false;
            dataGridConsultas.SelectedItem = null;


           


        }

        //===============================================
        //                  CLIENTES
        //===============================================        
        /// <summary>
        /// Handles the SelectionChanged event of the dataGridClientes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cliente p = dataGridClientes.SelectedItem as Cliente;
            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                txtDireccion.Text = p.Direccion;
                txtTelefono.Text = p.Telefono;
                txtEmail.Text = p.Email;

                btnModificarCliente.IsEnabled = true;
            }
            else
            {
                btnModificarCliente.IsEnabled = false;
            }
        }
        /// <summary>
        /// Handles the Click event of the btnAgregarCliente control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) 
                )
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

    
            Cliente nuevo = new Cliente(txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text);

            try
            {
                nuevo.insertar();
                MessageBox.Show("Cliente agregado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarClientes();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el Cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnModificarCliente control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente seleccionado = dataGridClientes.SelectedItem as Cliente;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente para modificar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text)
                )
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            


            seleccionado.Nombre = txtNombre.Text;
            seleccionado.Direccion = txtDireccion.Text;
            seleccionado.Telefono = txtTelefono.Text;
            seleccionado.Email = txtEmail.Text;

            try
            {
                seleccionado.actualizar();
                MessageBox.Show("Cliente modificado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarClientes();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnEliminarCliente control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente seleccionado = dataGridClientes.SelectedItem as Cliente;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                seleccionado.eliminar();
                lsClientes.Remove(seleccionado);
                MessageBox.Show("Cliente eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //===============================================
        //                  MASCOTAS
        //===============================================        
        /// <summary>
        /// Cargars the mascotas.
        /// </summary>
        private void cargarMascotas()
        {
            lsMascotas.Clear();
            var personas = MascotaPersistence.leerMascotas();
            foreach (var p in personas)
            {
                lsMascotas.Add(p);
            }
            dataGridMascotas.ItemsSource = lsMascotas;
        }
        /// <summary>
        /// Handles the SelectionChanged event of the dataGridMascotas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dataGridMascotas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mascota p = dataGridMascotas.SelectedItem as Mascota;
            if (p != null)
            {
                txtNombreMascota.Text = p.Nombre;
                txtEspecie.Text = p.Especie;
                txtRaza.Text = p.Raza;
                txtCliente.Text = p.Idcliente.ToString();

                // Cargar la fecha de nacimiento si está disponible
                if (!string.IsNullOrWhiteSpace(p.Fechanac))
                {
                    if (DateTime.TryParse(p.Fechanac, out DateTime fecha))
                    {
                        datePickerFechaNacimiento.SelectedDate = fecha;
                    }
                }

                btnModificarMascota.IsEnabled = true;
            }
            else
            {
                btnModificarMascota.IsEnabled = false;
            }
        }
        /// <summary>
        /// Handles the Click event of the btnAgregarMascota control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAgregarMascota_Click(object sender, RoutedEventArgs e) 
        {
            if (string.IsNullOrWhiteSpace(txtNombreMascota.Text) ||
                string.IsNullOrWhiteSpace(txtEspecie.Text) ||
                string.IsNullOrWhiteSpace(txtRaza.Text) ||
                !datePickerFechaNacimiento.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(txtCliente.Text, out int cliente))
            {
                MessageBox.Show("El cliente debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            String fecha = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");

            Mascota nuevo = new Mascota(txtNombreMascota.Text, txtEspecie.Text, txtRaza.Text, fecha, cliente);

            try
            {
                nuevo.insertar();
                MessageBox.Show("Mascota agregada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarMascotas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el mascota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnModificarMascota control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnModificarMascota_Click(object sender, RoutedEventArgs e) 
        {
            Mascota seleccionado = dataGridMascotas.SelectedItem as Mascota;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione una mascota para modificar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreMascota.Text) ||
                string.IsNullOrWhiteSpace(txtEspecie.Text) ||
                string.IsNullOrWhiteSpace(txtRaza.Text) ||
                !datePickerFechaNacimiento.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCliente.Text, out int cliente))
            {
                MessageBox.Show("El cliente debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string fecha = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");

            seleccionado.Nombre = txtNombreMascota.Text;
            seleccionado.Especie = txtEspecie.Text;
            seleccionado.Raza = txtRaza.Text;
            seleccionado.Fechanac = fecha;
            seleccionado.Idcliente = cliente;

            try
            {
                seleccionado.actualizar();
                MessageBox.Show("Mascota modificada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarMascotas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la mascota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEliminarMascota control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEliminarMascota_Click(object sender, RoutedEventArgs e) 
        {
            Mascota seleccionado = dataGridMascotas.SelectedItem as Mascota;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione una mascota para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                seleccionado.eliminar();
                lsMascotas.Remove(seleccionado);
                MessageBox.Show("Mascota eliminada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la mascota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //===============================================
        //                  CONSULTAS
        //===============================================        
        /// <summary>
        /// Cargars the consultas.
        /// </summary>
        private void cargarConsultas()
        {
            lsConsultas.Clear();
            var personas = ConsultaPersistence.leerConsultas();
            foreach (var p in personas)
            {
                lsConsultas.Add(p);
            }
            dataGridConsultas.ItemsSource = lsConsultas;
        }
        /// <summary>
        /// Handles the SelectionChanged event of the dataGridConsultas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dataGridConsultas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Consulta p = dataGridConsultas.SelectedItem as Consulta;
            if (p != null)
            {
                txtDiagnostico.Text = p.Diagnostico;
                txtMascota.Text = p.Idmascota.ToString();
                txtVeterinario.Text = p.Idveterinario.ToString();

                // Cargar la fecha de nacimiento si está disponible
                if (!string.IsNullOrWhiteSpace(p.Fechaconsulta))
                {
                    if (DateTime.TryParse(p.Fechaconsulta, out DateTime fecha))
                    {
                        datePickerFechaNacimiento.SelectedDate = fecha;
                    }
                }

                btnModificarConsulta.IsEnabled = true;
            }
            else
            {
                btnModificarConsulta.IsEnabled = false;
            }
        }
        /// <summary>
        /// Handles the Click event of the btnAgregarConsulta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAgregarConsulta_Click(object sender, RoutedEventArgs e)
        {
            if (!datePickerFechaConsulta.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(txtDiagnostico.Text) ||
                string.IsNullOrWhiteSpace(txtMascota.Text) ||
                string.IsNullOrWhiteSpace(txtVeterinario.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(txtMascota.Text, out int mascota))
            {
                MessageBox.Show("El cliente debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtVeterinario.Text, out int veterinario))
            {
                MessageBox.Show("El cliente debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            String fecha = datePickerFechaConsulta.SelectedDate.Value.ToString("yyyy-MM-dd");

            Consulta nuevo = new Consulta(fecha, txtDiagnostico.Text, mascota, veterinario);

            try
            {
                nuevo.insertar();
                MessageBox.Show("Mascota agregada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarMascotas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el mascota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnModificarConsulta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnModificarConsulta_Click(object sender, RoutedEventArgs e)
        {
            Consulta seleccionado = dataGridConsultas.SelectedItem as Consulta;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione una mascota para modificar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!datePickerFechaConsulta.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(txtDiagnostico.Text) ||
                string.IsNullOrWhiteSpace(txtMascota.Text) ||
                string.IsNullOrWhiteSpace(txtVeterinario.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtMascota.Text, out int mascota))
            {
                MessageBox.Show("El cliente debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtVeterinario.Text, out int veterinario))
            {
                MessageBox.Show("El cliente debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string fecha = datePickerFechaConsulta.SelectedDate.Value.ToString("yyyy-MM-dd");

            seleccionado.Fechaconsulta = fecha;
            seleccionado.Diagnostico = txtDiagnostico.Text;
            seleccionado.Idmascota = mascota;
            seleccionado.Idveterinario = veterinario;

            try
            {
                seleccionado.actualizar();
                MessageBox.Show("Mascota modificada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarMascotas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la mascota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEliminarConsulta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEliminarConsulta_Click(object sender, RoutedEventArgs e)
        {
            Consulta seleccionado = dataGridConsultas.SelectedItem as Consulta;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione una mascota para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                seleccionado.eliminar();
                lsConsultas.Remove(seleccionado);
                MessageBox.Show("Mascota eliminada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la mascota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}
