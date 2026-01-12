// View/MainWindow.xaml.cs — CODE-BEHIND de la VISTA
// Para esta actividad usamos code-behind (válido en ejercicios pequeños).
// En proyectos grandes, lo ideal es MVVM (separar lógica de UI en ViewModels).



using System;
using System.Threading.Tasks;               // Para el manejador async del botón
using System.Windows;                       // WPF
using FinalActivity.Wpf_MVC.Controller;     // ObjectController


namespace FinalActivity.Wpf_MVC.View
{
    public partial class MainWindow : Window
    {
        // Instancia del controlador — se encarga de hablar con la API y devolver datos.
        private readonly ObjectController controller = new ObjectController();

        public MainWindow()
        {
            InitializeComponent(); // Carga los elementos definidos en el XAML
        }

        // Manejador del botón. Es async para NO bloquear la interfaz mientras llega la respuesta HTTP.
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Pedimos los datos al controlador (GET /objects)
                var objects = await controller.GetObjectsAsync();

                // 2) Enlazamos la lista de resultados a la ListBox para visualizar los nombres
                ObjectsList.ItemsSource = objects;
            }
            catch (Exception ex)

            // Si fallan la red, la API o la deserialización, mostramos un mensaje informativo.
            {
                MessageBox.Show(ex.Message, "Error al cargar datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
