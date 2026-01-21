using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using ApiRest.Models; // Cambio de namespace

namespace ApiRest // Cambio de namespace
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;

        private const string ApiKey = "9d74a59c23844c38a4da945af42547cc";

        private const string BaseUrl = "https://api.spoonacular.com/recipes/complexSearch";

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch("number=10");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Please enter a food term to search.");
                return;
            }
            PerformSearch($"query={query}&number=10");
        }

        private async void PerformSearch(string queryParams)
        {
            ObjectsListBox.Items.Clear();
            DetailsTextBox.Clear();

            try
            {
                var url = $"{BaseUrl}?{queryParams}&apiKey={ApiKey}&addRecipeInformation=true";
                var response = await _httpClient.GetFromJsonAsync<SpoonacularResponse>(url);

                if (response != null && response.Results != null)
                {
                    foreach (var recipe in response.Results)
                    {
                        ObjectsListBox.Items.Add(new
                        {
                            _display = recipe.ToString(),
                            _json = System.Text.Json.JsonSerializer.Serialize(recipe, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }),
                            _obj = recipe
                        });
                    }

                    if (response.Results.Count == 0)
                    {
                        MessageBox.Show("No recipes found.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error fetching recipes: {ex.Message}");
            }
        }

        private void ObjectsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ObjectsListBox.SelectedItem == null) return;

            var item = ObjectsListBox.SelectedItem;
            dynamic selectedItem = item;
            try
            {
                string json = selectedItem._json;
                DetailsTextBox.Text = json;
            }
            catch
            {
                DetailsTextBox.Text = "Error reading details.";
            }
        }
    }
}