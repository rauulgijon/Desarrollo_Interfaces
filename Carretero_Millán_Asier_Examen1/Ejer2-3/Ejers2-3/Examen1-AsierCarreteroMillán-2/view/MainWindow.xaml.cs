using Examen1_AsierCarreteroMillán_2.domain;
using Examen1_AsierCarreteroMillán_2.persistence.manage;
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

namespace Minihito2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Jugador> lstJugador;
        JugadorPersistence jp;
        Jugador jugador;

        public MainWindow()
        {
            InitializeComponent();
            lstJugador = new ObservableCollection<Jugador>();
            jugador = new Jugador();
            jp = new JugadorPersistence();
            cargarAlumnos();
        }

        private void cargarAlumnos()
        {
            lstJugador.Clear();
            var jugador = jp.leerJugador();
            foreach (var p in jugador)
            {
                lstJugador.Add(p);
            }
            dgJugador.ItemsSource = lstJugador;
        }

        public void start()
        {
            txtNickname.Text = "";
            txtNivel.Text = "";
            txtPuntuacion.Text = "";
            btnModificar.IsEnabled = false;
            dgJugador.SelectedItem = null;
            dpFechadejuego.SelectedDate = null;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Jugador j = dgJugador.SelectedItem as Jugador;
            if (j != null)
            {
                try
                {
                    // Eliminar de la base de datos
                    j.eliminar();

                    // Eliminar de la lista observable
                    lstJugador.Remove(j);

                    MessageBox.Show("Jugador eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un jugador para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNickname.Text) ||
                string.IsNullOrWhiteSpace(txtNivel.Text) ||
                string.IsNullOrWhiteSpace(txtPuntuacion.Text) ||
                !dpFechadejuego.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtNivel.Text, out int nivel))
            {
                MessageBox.Show("El nivel debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtPuntuacion.Text, out int puntuacion))
            {
                MessageBox.Show("La puntuacin debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Jugador existente = lstJugador.FirstOrDefault(j =>
                j.nickname == txtNickname.Text &&
                j.nivel == nivel &&
                j.puntuacion == puntuacion);

            if (existente != null)
            {
                MessageBox.Show("El Jugador ya existe", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    Jugador jugador = new Jugador(txtNickname.Text, dpFechadejuego.SelectedDate.Value, nivel, puntuacion);
                    jugador.insertar();

                    MessageBox.Show("Jugador agregado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Recargar la lista desde la base de datos para obtener el ID asignado
                    cargarAlumnos();
                    start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Jugador j = dgJugador.SelectedItem as Jugador;
            if (j == null)
            {
                btnModificar.IsEnabled = false;
                MessageBox.Show("Por favor, seleccione un jugador", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNickname.Text) ||
                string.IsNullOrWhiteSpace(txtNivel.Text) ||
                string.IsNullOrWhiteSpace(txtPuntuacion.Text) ||
                !dpFechadejuego.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtNivel.Text, out int nivel))
            {
                MessageBox.Show("El nivel debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtPuntuacion.Text, out int puntuacion))
            {
                MessageBox.Show("La puntuacin debe ser un número válido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Actualizar los valores en el objeto
                j.nickname = txtNickname.Text;
                j.nivel = nivel;
                j.puntuacion = puntuacion;
                j.fechadejuego = dpFechadejuego.SelectedDate.Value;

                // Actualizar en la base de datos
                j.actualizar();

                MessageBox.Show("Jugador modificado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refrescar el DataGrid
                dgJugador.Items.Refresh();
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgJugador_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Jugador j = dgJugador.SelectedItem as Jugador;
            if (j != null)
            {
                txtNickname.Text = j.nickname;
                txtNivel.Text = j.nivel.ToString();
                txtPuntuacion.Text = j.puntuacion.ToString();
                dpFechadejuego.SelectedDate = j.fechadejuego;
                btnModificar.IsEnabled = true;


            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        private void btnMaspared_Click(object sender, RoutedEventArgs e)
        {
            txtPared.Text = (int.Parse(txtPared.Text) + 1).ToString();
        }

        private void btnMenospared_Click(object sender, RoutedEventArgs e)
        {
            if (txtPared.Text != "0")
            {
                txtPared.Text = (int.Parse(txtPared.Text) - 1).ToString();
            }
                
        }

        private void btnMasratones_Click(object sender, RoutedEventArgs e)
        {
            txtRatones.Text = (int.Parse(txtRatones.Text) + 1).ToString();
        }

        private void btnMenosratones_Click(object sender, RoutedEventArgs e)
        {
            if (txtRatones.Text != "0")
            {
                txtRatones.Text = (int.Parse(txtRatones.Text) - 1).ToString();
            }
        }

        private void txtIniciar_Click(object sender, RoutedEventArgs e)
        {
            int paredes = int.Parse(txtPared.Text);
            int ratones = int.Parse(txtRatones.Text);
            Random rand = new Random();
            HashSet<(int, int)> posicionesOcupadas = new HashSet<(int, int)>();
            for (int i = 0; i < paredes; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(0, 6);
                    y = rand.Next(0, 6);
                } while (posicionesOcupadas.Contains((x, y)));
                posicionesOcupadas.Add((x, y));
                Label lblPared = new Label
                {
                    Content = "P",
                    Foreground = Brushes.Green,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(lblPared, x);
                Grid.SetRow(lblPared, y);
                gridJuego.Children.Add(lblPared);
            }
            for (int i = 0; i < ratones; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(0, 6);
                    y = rand.Next(0, 6);
                } while (posicionesOcupadas.Contains((x, y)));
                posicionesOcupadas.Add((x, y));
                Label lblRaton = new Label
                {
                    Content = "R",
                    Foreground = Brushes.Blue,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(lblRaton, x);
                Grid.SetRow(lblRaton, y);
                gridJuego.Children.Add(lblRaton);
            }


        }

    }
}
