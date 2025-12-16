using DatagridPersonas.persistence;
using System;
using System.Windows;

namespace DatagridPersonas.view
{
    public partial class LoginWindow : Window
    {
        private PersonaPersistence persistence;

        public LoginWindow()
        {
            InitializeComponent();
            persistence = new PersonaPersistence();
            txtUsuario.Focus();

            // Permitir login con Enter
            txtPassword.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    btnLogin_Click(null, null);
                }
            };
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Ocultar mensaje de error previo
            txtError.Visibility = Visibility.Collapsed;

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarError("Por favor, ingrese su usuario");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                MostrarError("Por favor, ingrese su contraseña");
                txtPassword.Focus();
                return;
            }

            try
            {
                // Validar credenciales
                if (persistence.ValidarUsuario(txtUsuario.Text, txtPassword.Password))
                {
                    // Login exitoso
                    string nombreUsuario = persistence.ObtenerNombreUsuario(txtUsuario.Text);

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Title = $"Sistema de Gestión - {nombreUsuario}";
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MostrarError("Usuario o contraseña incorrectos");
                    txtPassword.Password = "";
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al iniciar sesión: {ex.Message}");
            }
        }

        private void MostrarError(string mensaje)
        {
            txtError.Text = mensaje;
            txtError.Visibility = Visibility.Visible;
        }
    }
}