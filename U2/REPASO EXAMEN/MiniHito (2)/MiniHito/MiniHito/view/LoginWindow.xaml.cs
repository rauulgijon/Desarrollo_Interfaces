using System;
using System.Collections.Generic;
using System.Windows;
using ExampleMVCnoDatabase.Persistence; // Importante para usar tu DBBroker
using MiniHito; // Para poder llamar a MainWindow

namespace MiniHito.view
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = txtPass.Password;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Por favor, introduce usuario y contraseña.");
                return;
            }

            try
            {
                // NOTA: Si tu tabla en MySQL se llama diferente, cambia 'Usuarios' aquí.
                // Si tus columnas no son 'NombreUsuario' y 'Password', cámbialas también.
                string sql = $"SELECT * FROM Usuarios WHERE NombreUsuario = '{user}' AND Password = '{pass}';";

                // Usamos tu DBBroker existente
                List<object> resultado = DBBroker.obtenerAgente().leer(sql);

                if (resultado.Count > 0)
                {
                    // Login correcto: Abrimos la ventana principal
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close(); // Cerramos el login
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}