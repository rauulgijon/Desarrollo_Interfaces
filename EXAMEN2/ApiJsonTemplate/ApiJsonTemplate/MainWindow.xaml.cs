using System;
using System.Windows;
using ApiJsonTemplate.Controllers;

namespace ApiJsonTemplate
{
    public partial class MainWindow : Window
    {
        // Instanciamos nuestro controlador
        private readonly JsonController _controller = new JsonController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TxtStatus.Text = "Leyendo archivo JSON...";

                // Llamamos al método que lee el archivo
                var items = await _controller.GetObjectsFromJsonAsync();

                // Le pasamos la lista a la tabla visual
                DataGridItems.ItemsSource = items;

                TxtStatus.Text = $"Se cargaron {items.Count} elementos del archivo.";
            }
            catch (Exception ex)
            {
                TxtStatus.Text = "Error al cargar.";
                MessageBox.Show(ex.Message, "Error de Archivo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}