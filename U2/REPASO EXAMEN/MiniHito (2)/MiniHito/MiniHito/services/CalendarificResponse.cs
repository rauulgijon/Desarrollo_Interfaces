using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace MiniHito.services
{
    // Clases para mapear el JSON de la API
    public class CalendarificResponse
    {
        public ResponseData response { get; set; }
    }

    public class ResponseData
    {
        public List<Holiday> holidays { get; set; }
    }

    public class Holiday
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateInfo date { get; set; }
        public List<string> type { get; set; }
    }

    public class DateInfo
    {
        public string iso { get; set; }
    }

    public class CalendarificService
    {
        // Pega aquí tu API KEY de calendarific.com
        private const string API_KEY = "CCYo3NBtnMMiK8udvGjPi5f902JoetlQ";
        private const string BASE_URL = "https://calendarific.com/api/v2/holidays";

        public async static Task<bool> EsFestivo(DateTime fecha)
        {
            try
            {
                string year = fecha.Year.ToString();
                string country = "ES"; // España
                string month = fecha.Month.ToString();
                string day = fecha.Day.ToString();

                // Añadimos &type=national para cumplir el requisito del examen
                string url = $"{BASE_URL}?api_key={API_KEY}&country={country}&year={year}&month={month}&day={day}&type=national";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<CalendarificResponse>(json);

                        if (data != null && data.response != null && data.response.holidays != null && data.response.holidays.Count > 0)
                        {
                            return true; // Es festivo
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error de conexión, no bloqueamos al usuario (o muestra un MessageBox si prefieres)
                Console.WriteLine("Error API: " + ex.Message);
            }
            return false; // Asumimos laborable si falla o no hay datos
        }
    }
}