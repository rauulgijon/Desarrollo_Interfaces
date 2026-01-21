using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using ApiRest.Models;
using static System.Net.WebRequestMethods;

namespace ApiRest
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;

        // ¡¡IMPORTANTE!! Sustituye esto por tu API Key de Spoonacular
        private const string ApiKey = "PON_AQUI_TU_API_KEY";

        // Endpoint base para búsqueda
        private const string BaseUrl = "https://api.spoonacular.com/recipes/complexSearch";

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        // Carga recetas iniciales (ej. pasta por defecto o populares)
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Podemos llamar a buscar con una query vacía o por defecto
            PerformSearch("number=10");
        }

        // Busca según lo que escriba el usuario
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Please enter a food term to search.");
                return;
            }

            // Construimos la query para la API
            PerformSearch($"query={query}&number=10");
        }

        private async void PerformSearch(string queryParams)
        {
            ObjectsListBox.Items.Clear();
            DetailsTextBox.Clear();

            try
            {
                // Construcción de la URL completa con la API Key
                // Ejemplo final: https://api.spoonacular.com/recipes/complexSearch?query=query&number=10&apiKey=12345
                var url = $"https://api.spoonacular.com/recipes/complexSearch?Pasta&apiKey=9d74a59c23844c38a4da945af42547cc&addRecipeInformation=true";

                // Spoonacular devuelve un objeto wrapper (SpoonacularResponse), no una lista directa
                var response = await _httpClient.GetFromJsonAsync<SpoonacularResponse>(url);

                if (response != null && response.Results != null)
                {
                    foreach (var recipe in response.Results)
                    {
                        // Agregamos al ListBox un objeto anónimo con las propiedades necesarias
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

            // Usamos reflection o dynamic para acceder al objeto anónimo guardado en el ListBox
            var item = ObjectsListBox.SelectedItem; // es un objeto anónimo

            // Accedemos a la propiedad _json que creamos al rellenar la lista
            dynamic selectedItem = item;
            try
            {
                string json = selectedItem._json;
                DetailsTextBox.Text = json;
            }
            catch
            {
                // Fallback por si acaso falla el acceso dinámico
                DetailsTextBox.Text = "Error reading details.";
            }
        }
    }
}