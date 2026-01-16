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

namespace stacknavigation.view
{
    /// <summary>
    /// Lógica de interacción para DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btn_atras(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btn_siguiente(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoForward();
        }
    }
}
