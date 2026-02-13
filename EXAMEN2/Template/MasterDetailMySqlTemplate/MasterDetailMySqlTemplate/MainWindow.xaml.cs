using System.Windows;
using MasterDetailMySqlTemplate.Controllers;

namespace MasterDetailMySqlTemplate
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