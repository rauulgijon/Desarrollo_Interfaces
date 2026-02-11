// MainWindow.xaml.cs — C# with inline comments
// ------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows;
using ApiShowcase.Wpf.Controller;
using ApiShowcase.Wpf.Models;

namespace ApiShowcase.Wpf // Namespace groups related classes of this project
{
    public partial class MainWindow : Window // MainWindow: UI logic (event handlers for buttons)
    {
        private readonly ObjectController objectsController = new ObjectController();
        private readonly CountriesController countriesController = new CountriesController();
        private readonly PokeController pokeController = new PokeController();

        public MainWindow()
        {
            InitializeComponent(); // Initialize XAML components and wire-up UI elements
        }

        // Objects tab
        private async void Objects_LoadAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObjectsStatus.Text = "Loading list...";
                var items = await objectsController.GetObjectsAsync();
                ObjectsList.ItemsSource = items; // Bind the list/grid to the data collection so it can display
                ObjectsStatus.Text = $"Loaded {items.Count} objects.";
            }
            catch (Exception ex)
            {
                ObjectsStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Objects_SearchById_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = ObjIdBox.Text?.Trim();
                if (string.IsNullOrWhiteSpace(id)) { MessageBox.Show("Please enter an ID."); return; }
                ObjectsStatus.Text = $"Searching id {id}...";
                var obj = await objectsController.GetObjectByIdAsync(id);
                ObjectsList.ItemsSource = obj == null ? null : new[] { obj }; // Bind the list/grid to the data collection so it can display
                ObjectsStatus.Text = obj == null ? "Not found." : "1 object loaded.";
            }
            catch (Exception ex)
            {
                ObjectsStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Countries tab
        private async void Countries_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var q = CountryQueryBox.Text?.Trim();
                if (string.IsNullOrWhiteSpace(q)) { MessageBox.Show("Please enter a country name."); return; }
                CountriesStatus.Text = $"Searching {q}...";
                var items = await countriesController.SearchByNameAsync(q);
                CountriesGrid.ItemsSource = items; // Bind the list/grid to the data collection so it can display
                CountriesStatus.Text = $"Found {items.Count} result(s).";
            }
            catch (Exception ex)
            {
                CountriesStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Poke tab
        private async void Poke_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var q = PokeQueryBox.Text?.Trim();
                if (string.IsNullOrWhiteSpace(q)) { MessageBox.Show("Please enter a name or ID."); return; }
                PokeStatus.Text = $"Searching {q}...";
                var poke = await pokeController.GetPokemonAsync(q);
                if (poke == null)
                {
                    PokeIdText.Text = PokeNameText.Text = PokeHeightText.Text = PokeWeightText.Text = string.Empty;
                    PokeStatsGrid.ItemsSource = null; // Bind the list/grid to the data collection so it can display
                    PokeStatus.Text = "Not found.";
                    return;
                }
                PokeIdText.Text = poke.id.ToString();
                PokeNameText.Text = poke.name;
                PokeHeightText.Text = poke.height.ToString();
                PokeWeightText.Text = poke.weight.ToString();
                PokeStatsGrid.ItemsSource = poke.stats; // Bind the list/grid to the data collection so it can display
                PokeStatus.Text = "Done.";
            }
            catch (Exception ex)
            {
                PokeStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}