using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GRIDEjercicio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for (int r = 0; r < 15; r++)
                rootGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int c = 0; c < 15; c++)
                rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    var lbl = new Label
                    {
                        Content = $"{r},{c}",
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Background = Brushes.Transparent,
                        BorderBrush = Brushes.LightGray,    // opcional: muestra líneas por celda
                        BorderThickness = new Thickness(0.5) // opcional
                    };
                    Grid.SetRow(lbl, r);
                    Grid.SetColumn(lbl, c);
                    rootGrid.Children.Add(lbl);
                }
            }
        }
    }
}
