using Ejercicio3.domain;
using Ejercicio3.persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ejercicio3
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Jugador> lsPersonas;

        // Variables juego
        private List<Point> serpiente = new List<Point>();
        private List<Point> listaParedes = new List<Point>();
        private List<Point> listaRatones = new List<Point>();
        private Random random = new Random();
        private int longitudSerpiente = 4;

        public MainWindow()
        {
            InitializeComponent();
            lsPersonas = new ObservableCollection<Jugador>();

            cargarPersonas();
            start();

            // Config juego inicial
            GenerarSerpiente();
            PintarTableroCompleto();

            // Iniciar contadores visuales
            txtNParedes.Text = "0";
            txtRatones.Text = "0";
        }

        // -----------------------------------------------------------
        // LÓGICA CRUD (BOTONES Y TABLA)
        // -----------------------------------------------------------

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
            // Resetear formulario
            txtNombre.Text = "";
            txtPuntos.Text = "";
            txtEmail.Text = "";
            chkVip.IsChecked = false;
            rbManana.IsChecked = true;
            datePickerFechaNacimiento.SelectedDate = null;
            cmbNivel.SelectedItem = null;

            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            dataGridPersonas.SelectedItem = null;
        }

        private void dataGridPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jugador p = dataGridPersonas.SelectedItem as Jugador;
            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                txtPuntos.Text = p.Puntuacion.ToString();
                txtEmail.Text = p.Email;
                chkVip.IsChecked = p.EsVip;

                if (p.Turno == "Tarde") rbTarde.IsChecked = true;
                else rbManana.IsChecked = true;

                if (DateTime.TryParse(p.Fechanac, out DateTime f))
                    datePickerFechaNacimiento.SelectedDate = f;

                foreach (ComboBoxItem item in cmbNivel.Items)
                {
                    if (item.Tag.ToString() == p.Nivel.ToString()) { cmbNivel.SelectedItem = item; break; }
                }

                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (DatosValidos())
            {
                try
                {
                    Jugador nuevo = ExtraerDatosFormulario();
                    nuevo.insertar();

                    MessageBox.Show("Jugador agregado.");
                    cargarPersonas();
                    start();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Jugador seleccion = dataGridPersonas.SelectedItem as Jugador;
            if (seleccion == null) return;

            if (DatosValidos())
            {
                try
                {
                    // Actualizamos el objeto existente con los datos nuevos
                    Jugador datosNuevos = ExtraerDatosFormulario();

                    seleccion.Nombre = datosNuevos.Nombre;
                    seleccion.Puntuacion = datosNuevos.Puntuacion;
                    seleccion.Email = datosNuevos.Email;
                    seleccion.EsVip = datosNuevos.EsVip;
                    seleccion.Turno = datosNuevos.Turno;
                    seleccion.Fechanac = datosNuevos.Fechanac;
                    seleccion.Nivel = datosNuevos.Nivel;

                    seleccion.actualizar();
                    MessageBox.Show("Jugador modificado.");
                    cargarPersonas();
                    start();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Jugador seleccion = dataGridPersonas.SelectedItem as Jugador;
            if (seleccion == null) return;

            if (MessageBox.Show("¿Eliminar?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    seleccion.eliminar();
                    cargarPersonas();
                    start();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        // Métodos auxiliares para no repetir código en examen
        private bool DatosValidos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPuntos.Text) ||
                cmbNivel.SelectedItem == null ||
                !datePickerFechaNacimiento.SelectedDate.HasValue)
            {
                MessageBox.Show("Faltan datos obligatorios.");
                return false;
            }
            if (!int.TryParse(txtPuntos.Text, out _))
            {
                MessageBox.Show("La puntuación debe ser número.");
                return false;
            }
            return true;
        }

        private Jugador ExtraerDatosFormulario()
        {
            string nombre = txtNombre.Text;
            int puntos = int.Parse(txtPuntos.Text);
            string email = txtEmail.Text;
            bool vip = chkVip.IsChecked == true;
            string turno = rbManana.IsChecked == true ? "Mañana" : "Tarde";
            string fecha = datePickerFechaNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");
            int nivel = int.Parse(((ComboBoxItem)cmbNivel.SelectedItem).Tag.ToString());

            return new Jugador(nombre, puntos, email, vip, turno, fecha, nivel);
        }


        // -----------------------------------------------------------
        // LÓGICA JUEGO (Tablero, Serpiente, etc.)
        // -----------------------------------------------------------

        private void masParedes(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNParedes.Text, out int num)) txtNParedes.Text = (num + 1).ToString();
        }
        private void menosParedes(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNParedes.Text, out int num) && num > 0) txtNParedes.Text = (num - 1).ToString();
        }
        private void masRatones(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtRatones.Text, out int num)) txtRatones.Text = (num + 1).ToString();
        }
        private void menosRatones(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtRatones.Text, out int num) && num > 0) txtRatones.Text = (num - 1).ToString();
        }

        private void iniciarJuego(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNickJugador.Text))
            {
                MessageBox.Show("Introduce un Nick.");
                return;
            }

            int.TryParse(txtNParedes.Text, out int nParedes);
            int.TryParse(txtRatones.Text, out int nRatones);

            GenerarSerpiente();
            GenerarObstaculosYRatones(nParedes, nRatones);
            PintarTableroCompleto();

            MessageBox.Show($"Juego iniciado para {txtNickJugador.Text}");
        }

        public void fillGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Label txt = new Label();
                    txt.Content = "(" + i + "," + j + ")";
                    txt.HorizontalAlignment = HorizontalAlignment.Stretch;
                    txt.VerticalAlignment = VerticalAlignment.Stretch;
                    Grid.SetRow(txt, i);
                    Grid.SetColumn(txt, j);
                    GridTable.Children.Add(txt);
                }
            }
        }

        private void GenerarSerpiente()
        {
            serpiente.Clear();
            for (int i = 0; i < longitudSerpiente; i++)
                serpiente.Add(new Point(i, 2));
        }

        private void GenerarObstaculosYRatones(int numParedes, int numRatones)
        {
            listaParedes.Clear();
            listaRatones.Clear();
            for (int i = 0; i < numParedes; i++) AñadirElementoUnico(listaParedes);
            for (int i = 0; i < numRatones; i++) AñadirElementoUnico(listaRatones);
        }

        private void AñadirElementoUnico(List<Point> listaDestino)
        {
            Point p = ObtenerPosicionVacia();
            if (p.X != -1) listaDestino.Add(p);
        }

        private Point ObtenerPosicionVacia()
        {
            for (int i = 0; i < 50; i++)
            {
                Point p = new Point(random.Next(0, 6), random.Next(0, 6));
                if (!serpiente.Contains(p) && !listaParedes.Contains(p) && !listaRatones.Contains(p)) return p;
            }
            return new Point(-1, -1);
        }

        private void PintarTableroCompleto()
        {
            GridTable.Children.Clear();
            fillGrid();
            foreach (var p in serpiente) PintarCelda(p, Brushes.Green, "S");
            foreach (var p in listaParedes) PintarCelda(p, Brushes.Gray, "X");
            foreach (var p in listaRatones) PintarCelda(p, Brushes.OrangeRed, "R");
        }

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