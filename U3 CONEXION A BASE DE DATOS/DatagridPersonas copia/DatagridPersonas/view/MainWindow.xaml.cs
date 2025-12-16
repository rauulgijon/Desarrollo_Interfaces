using DatagridPersonas.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DatagridPersonas
{
    public partial class MainWindow : Window
    {
        private List<Persona> lstPersonas;
        private Persona persona;
        private List<Persona> lstPersonasOriginal;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                persona = new Persona();
                CargarPersonas();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Carga todas las personas desde el JSON
        /// </summary>
        private void CargarPersonas()
        {
            try
            {
                lstPersonas = Persona.getPersonas();
                lstPersonasOriginal = new List<Persona>(lstPersonas);
                dgvPersonas.ItemsSource = null;
                dgvPersonas.ItemsSource = lstPersonas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Reinicia el formulario
        /// </summary>
        public void start()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            btnModificar.IsEnabled = false;
            bntEliminar.IsEnabled = false;
            dgvPersonas.SelectedItem = null;
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
                bntEliminar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
                bntEliminar.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    MessageBox.Show("El apellido es obligatorio", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtApellido.Focus();
                    return;
                }

                if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0 || edad > 120)
                {
                    MessageBox.Show("La edad debe ser un número válido entre 1 y 120",
                                  "Validación",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    txtEdad.Focus();
                    return;
                }

                // Crear nueva persona
                Persona nuevaPersona = new Persona(txtNombre.Text, txtApellido.Text, edad);
                nuevaPersona.last(); // Obtener el siguiente ID
                nuevaPersona.insertar(); // Insertar en JSON

                // Actualizar la lista
                CargarPersonas();
                start();

                MessageBox.Show("Persona agregada correctamente", "Éxito",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar persona: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Persona p = dgvPersonas.SelectedItem as Persona;

                if (p == null)
                {
                    MessageBox.Show("Debe seleccionar una persona", "Advertencia",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    MessageBox.Show("El apellido es obligatorio", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0 || edad > 120)
                {
                    MessageBox.Show("La edad debe ser un número válido entre 1 y 120",
                                  "Validación",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                // Actualizar datos
                p.Nombre = txtNombre.Text;
                p.Apellido = txtApellido.Text;
                p.Edad = edad;

                // Guardar cambios en JSON
                p.pm.actualizarPersona(p);

                // Refrescar vista
                CargarPersonas();
                start();

                MessageBox.Show("Persona modificada correctamente", "Éxito",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar persona: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Persona p = dgvPersonas.SelectedItem as Persona;

                if (p == null)
                {
                    MessageBox.Show("Debe seleccionar una persona", "Advertencia",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Confirmar eliminación
                MessageBoxResult result = MessageBox.Show(
                    $"¿Está seguro de eliminar a {p.Nombre} {p.Apellido}?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Eliminar del JSON
                    p.pm.eliminarPersona(p.Id);

                    // Recargar lista
                    CargarPersonas();
                    start();

                    MessageBox.Show("Persona eliminada correctamente", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar persona: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnFiltrarEdad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtEdadFiltro.Text, out int edad))
                {
                    MessageBox.Show("Ingrese una edad válida", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbCondicionEdad.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione una condición", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string condicion = (cmbCondicionEdad.SelectedItem as ComboBoxItem).Content.ToString();

                // Filtrar en memoria
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

                dgvPersonas.ItemsSource = null;
                dgvPersonas.ItemsSource = lstPersonas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    MessageBox.Show("Ingrese un texto para buscar", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                lstPersonas = lstPersonasOriginal
                    .Where(p => p.Nombre.ToLower().Contains(texto.ToLower()) ||
                               p.Apellido.ToLower().Contains(texto.ToLower()))
                    .ToList();

                dgvPersonas.ItemsSource = null;
                dgvPersonas.ItemsSource = lstPersonas;

                if (lstPersonas.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados", "Búsqueda",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbOrdenar.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un campo para ordenar", "Validación",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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

                dgvPersonas.ItemsSource = null;
                dgvPersonas.ItemsSource = lstPersonas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ordenar: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarPersonas();
            txtBuscar.Text = "";
            txtEdadFiltro.Text = "";
            cmbCondicionEdad.SelectedItem = null;
            cmbOrdenar.SelectedItem = null;
        }
    }
}