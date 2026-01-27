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
        private List<Point> listaParedes = new List<Point>();
        private List<Point> listaRatones = new List<Point>();
        private Random random = new Random();
        private int longitudSerpiente = 4; // Puedes cambiar la longitud


        public MainWindow()
        {
            InitializeComponent();
            lsPersonas = new ObservableCollection<Jugador>();
            persona = new Jugador();
            cargarPersonas();

            start();

            GenerarSerpiente();

            PintarTableroCompleto();
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
            // Limpieza de campos del formulario
            txtNombre.Text = "";
            txtPuntos.Text = "";
            datePickerFechaNacimiento.SelectedDate = null;

            // Bloqueo de botones y selección
            btnModificar.IsEnabled = false;
            dataGridPersonas.SelectedItem = null;
            cmbNivel.SelectedItem = null;

            // --- CORRECCIÓN PARA LOS BOTONES ---
            // Inicializamos las cajas de texto con "0" para que los botones + y - funcionen
            txtNParedes.Text = "0";
            txtRatones.Text = "0";
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
            String fecha = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");

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



        // Lógica para Paredes
        private void masParedes(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNParedes.Text, out int num))
            {
                txtNParedes.Text = (num + 1).ToString();
            }
        }

        private void menosParedes(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNParedes.Text, out int num))
            {
                if (num > 0) txtNParedes.Text = (num - 1).ToString();
            }
        }

        // Lógica para Ratones
        private void masRatones(object sender, RoutedEventArgs e)
        {
            // OJO: En tu XAML el cuadro se llama txtNatones
            if (int.TryParse(txtRatones.Text, out int num))
            {
                txtRatones.Text = (num + 1).ToString();
            }
        }

        private void menosRatones(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtRatones.Text, out int num))
            {
                if (num > 0) txtRatones.Text = (num - 1).ToString();
            }
        }

        private void iniciarJuego(object sender, RoutedEventArgs e)
        {
            // 1. Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNickJugador.Text))
            {
                MessageBox.Show("Por favor, introduce un Nick para jugar.");
                return;
            }

            // 2. Añadir jugador a la tabla (SOLO EN MEMORIA, SIN BDD)
            // Creamos un jugador con puntuación 0 y fecha actual
            Jugador jugadorActual = new Jugador(txtNickJugador.Text, 0, DateTime.Now.ToString("yyyy-MM-dd"), 1);

            // Lo añadimos a la colección observable para que aparezca en el DataGrid automáticamente
            lsPersonas.Add(jugadorActual);

            // 3. Preparar el Tablero
            int nParedes = 0;
            int nRatones = 0;
            int.TryParse(txtNParedes.Text, out nParedes);
            int.TryParse(txtRatones.Text, out nRatones);

            // Generamos la serpiente (reinicia posición), paredes y ratones
            GenerarSerpiente();
            GenerarObstaculosYRatones(nParedes, nRatones);

            // Pintamos todo
            PintarTableroCompleto();

            MessageBox.Show($"Juego iniciado para {jugadorActual.Nombre} con {nParedes} paredes y {nRatones} ratones.");
        }

        private void GenerarObstaculosYRatones(int numParedes, int numRatones)
        {
            listaParedes.Clear();
            listaRatones.Clear();

            // Generar Paredes
            for (int i = 0; i < numParedes; i++)
            {
                Point p = ObtenerPosicionVacia();
                if (p != new Point(-1, -1)) listaParedes.Add(p);
            }

            // Generar Ratones
            for (int i = 0; i < numRatones; i++)
            {
                Point p = ObtenerPosicionVacia();
                if (p != new Point(-1, -1)) listaRatones.Add(p);
            }
        }

        private Point ObtenerPosicionVacia()
        {
            // Intenta buscar una posición libre (que no sea serpiente, ni pared, ni ratón)
            // NOTA: El Grid es de 6x6 según tu método fillGrid()
            int maxIntentos = 50;
            for (int i = 0; i < maxIntentos; i++)
            {
                int x = random.Next(0, 6);
                int y = random.Next(0, 6);
                Point p = new Point(x, y);

                // Comprobamos que no choque con nada
                if (!serpiente.Contains(p) && !listaParedes.Contains(p) && !listaRatones.Contains(p))
                {
                    return p;
                }
            }
            return new Point(-1, -1); // No encontró sitio
        }

        private void GenerarSerpiente()
        {
            serpiente.Clear();
            int filaInicial = 2;
            int columnaInicial = 0;

            for (int i = 0; i < longitudSerpiente; i++)
            {
                // OJO: Añadimos puntos a la lista 'serpiente' pero NO pintamos aquí.
                // Ya pintamos todo junto en PintarTableroCompleto() después.
                serpiente.Add(new Point(columnaInicial + i, filaInicial));
            }
        }

        private void PintarTableroCompleto()
        {
            GridTable.Children.Clear();
            fillGrid(); // Dibuja el fondo y coordenadas

            // 1. Pintar Serpiente (Verde)
            foreach (var punto in serpiente)
            {
                PintarCelda(punto, Brushes.Green, "S");
            }

            // 2. Pintar Paredes (Gris o Negro)
            foreach (var punto in listaParedes)
            {
                PintarCelda(punto, Brushes.Gray, "X");
            }

            // 3. Pintar Ratones (Rojo o Naranja)
            foreach (var punto in listaRatones)
            {
                PintarCelda(punto, Brushes.OrangeRed, "R");
            }
        }

        // Método auxiliar para no repetir código de Labels
        private void PintarCelda(Point punto, Brush color, string contenido)
        {
            Label lbl = new Label
            {
                Content = contenido,
                Background = color,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White
            };
            Grid.SetColumn(lbl, (int)punto.X);
            Grid.SetRow(lbl, (int)punto.Y);
            GridTable.Children.Add(lbl);
        }





    }
}

