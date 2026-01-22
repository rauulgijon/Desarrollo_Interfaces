using System;
using System.Collections.Generic;
using System.Windows;
using ExampleMVCnoDatabase.Persistence; // Tu DBBroker
using MiniHito; // Tu MainWindow

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
                // CORRECCIÓN IMPORTANTE BASADA EN TU ARCHIVO SQL:
                // 1. Tabla: 'usuario' (singular)
                // 2. Columnas: 'USERNAME' y 'PASSWORD'
                // 3. Comprobamos también que ACTIVO sea 1 (usuario habilitado)
                string sql = $"SELECT * FROM usuario WHERE USERNAME = '{user}' AND PASSWORD = '{pass}' AND ACTIVO = 1;";

                List<object> resultado = DBBroker.obtenerAgente().leer(sql);

                if (resultado.Count > 0)
                {
                    // Login correcto
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario incorrecto o no activo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}