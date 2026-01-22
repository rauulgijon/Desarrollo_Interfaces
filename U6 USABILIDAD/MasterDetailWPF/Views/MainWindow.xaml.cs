using System.Windows;
using MasterDetailWPF.Controllers;

namespace MasterDetailWPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MasterDetailController();
        }
    }
}
