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

namespace _15x15
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Crear 15 filas y 15 columnas
            for (int i = 0; i < 15; i++)
            {
                contenedor.RowDefinitions.Add(new RowDefinition());
                contenedor.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Rellenar el grid con etiquetas usando un bucle
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    var label = new Label
                    {
                        Content = $"{row},{col}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(label, row);
                    Grid.SetColumn(label, col);
                    contenedor.Children.Add(label);
                }
            }
        }
    }
}
