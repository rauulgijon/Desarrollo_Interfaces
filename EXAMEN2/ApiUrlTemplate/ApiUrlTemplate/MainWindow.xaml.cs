// En tu MainWindow.xaml.cs
using System;
using System.Windows;
using ApiUrlTemplate.Controllers;

namespace ApiUrlTemplate
{
    public partial class MainWindow : Window
    {
        private readonly ApiController _controller = new ApiController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TxtStatus.Text = "Cargando datos...";
                var items = await _controller.GetObjectsAsync();
                DataGridItems.ItemsSource = items; // Vinculamos los datos a la tabla
                TxtStatus.Text = $"Se cargaron {items.Count} elementos correctamente.";
            }
            catch (Exception ex)
            {
                TxtStatus.Text = "Error al cargar.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}