
# ApiShowcase.Wpf (Commented)

This version includes **inline comments** in all C# and XAML files to help you explain the code in class.

## What to highlight in class
- **Controllers** encapsulate HTTP calls with `HttpClient` and parse JSON via `System.Text.Json` (case-insensitive mapping).
- **Models** represent just the fields we need (minimal DTOs) to keep the demo simple.
- **MainWindow** handles UI events (button clicks) and binds data with `ItemsSource`.
- **Error handling** uses `EnsureSuccessStatusCode` and `try/catch` to show messages.

## Run
Open `ApiShowcase.Wpf.csproj` in Visual Studio 2022 (or later) and press **F5**.
