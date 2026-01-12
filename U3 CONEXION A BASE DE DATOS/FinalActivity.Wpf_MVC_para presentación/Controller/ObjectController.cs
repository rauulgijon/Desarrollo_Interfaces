// Controller/ObjectController.cs — CONTROLADOR
// Encapsula la comunicación HTTP con la API y la deserialización del JSON.
// En este patrón MVC-lite, el Controller NO conoce la UI: solo devuelve datos preparados.

using System.Collections.Generic;            // List<T>
using System.Net.Http;                       // HttpClient para peticiones HTTP
using System.Text.Json;                      // JsonSerializer para parsear JSON
using System.Threading.Tasks;                // Task, async/await
using FinalActivity.Wpf_MVC.Models;          // ApiObject


namespace FinalActivity.Wpf_MVC.Controller
{
    public class ObjectController
    {
        // Reutilizamos HttpClient (buena práctica para evitar agotar sockets).
        private readonly HttpClient client = new HttpClient();

        // Método asincrónico que obtiene todos los objetos de la API con GET /objects
        public async Task<List<ApiObject>> GetObjectsAsync()
        {
            // 1) Endpoint que vamos a consumir (lista de objetos)
            string url = "https://api.restful-api.dev/objects";
            
            // 2) Hacemos la petición GET y recibimos el cuerpo como string (JSON)
            string json = await client.GetStringAsync(url);

            // 3) Deserializamos el JSON a List<ApiObject>;
            //    PropertyNameCaseInsensitive = true permite emparejar nombres sin distinguir mayúsculas/minúsculas.

            List<ApiObject>? objects = JsonSerializer.Deserialize<List<ApiObject>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            // 4) Devolvemos lista vacía si por algún motivo fuera null(defensivo)
            // Literalmente ?? está diciendo: si lo de la izquierda es null, usa lo de la derecha. Si no tengo nada en objets, devuelve una lista vacía
            return objects ?? new List<ApiObject>();
        }
    }
}
