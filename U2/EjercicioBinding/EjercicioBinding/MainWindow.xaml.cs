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

namespace EjercicioBinding
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            lstBoxColorText.SelectedIndex = 0;
            lstBoxFuentes.SelectedIndex = 0;

            btnSizePequenio.Click += (s, e) => fontSlider.Value=12;
            btnSizeNormal.Click += (s, e) => fontSlider.Value=16;
            btnSizeGrande.Click += (s, e) => fontSlider.Value=20;

            txtBoxTamanioExacto.TextChanged += TxtBoxTamanioExacto_TextChanged;
            fontSlider.ValueChanged += FontSlider_ValueChanged;
        }

        private void TxtBoxTamanioExacto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(txtBoxTamanioExacto.Text, out double size))
            {
                fontSlider.Value = size;
            }
        }

        private void FontSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtBoxTamanioExacto.Text = fontSlider.Value.ToString("0");
        }
    }
}
