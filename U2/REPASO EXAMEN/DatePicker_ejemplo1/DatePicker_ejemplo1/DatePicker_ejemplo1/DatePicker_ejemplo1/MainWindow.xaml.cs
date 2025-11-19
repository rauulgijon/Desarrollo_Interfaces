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

namespace DatePicker_ejemplo1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Inicializa los componentes definidos en XAML
            InitializeComponent();
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Comprobamos si el usuario ha seleccionado una fecha en el DatePicker
            if (dpFechaJuego.SelectedDate.HasValue)
            {
                // Si hay una fecha seleccionada, la obtenemos
                DateTime fecha = dpFechaJuego.SelectedDate.Value;

                // Mostramos la fecha en formato corto (ej: 19/11/2025)
                MessageBox.Show($"Fecha seleccionada: {fecha.ToShortDateString()}");
            }
            else
            {
                // Si no hay fecha seleccionada, mostramos un mensaje informativo
                MessageBox.Show("No se ha seleccionado ninguna fecha.");
            }
        }

    }
}