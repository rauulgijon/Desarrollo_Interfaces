using System;
using System.Collections.Generic;
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

namespace botonesConFor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    Button btn = new Button();
                    btn.Content = "botón " + i + " . " + j;

                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.HorizontalAlignment = HorizontalAlignment.Left;

                    btn.Width = 100;
                    btn.Height = 25;

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    btn.Margin = new Thickness(20 + i * 110, 10 + j * 55, 0, 0);
                    contenedor.Children.Add(btn);
                }
            }
        }
    }
}
