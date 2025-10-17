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

namespace Calculadora
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Necesito una calculadora
        public MainWindow()
        {
            InitializeComponent();

            for (int i= 0; i<4; i++)
            {
                for(int j=0; j<3; j++)
                {
                    Button boton = new Button();
                    boton.Width = 50;
                    boton.Height = 40;
                    if(i==3)
                        switch(j)
                        {
                            case 0:
                                boton.Content = "=";
                                break;
                            case 1:
                                boton.Content = "0";
                                break;
                            case 2:
                                boton.Content = "C";
                                break;
                        }
                    else
                        boton.Content = (9 - 3 * i) - (2 - j);
                    boton.HorizontalAlignment = HorizontalAlignment.Center;
                    boton.VerticalAlignment = VerticalAlignment.Center;

                    Grid.SetRow(boton, i);
                    Grid.SetColumn(boton, j);
                    contenedor.Children.Add(boton);

                }
            }
        }
    }
}
