using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ApiJsonTemplate.Models;

namespace ApiJsonTemplate.Controllers
{
    public class JsonController
    {
        public async Task<List<ApiObject>> GetObjectsFromJsonAsync()
        {
            // 1. Nombre de tu archivo JSON (debe estar en el proyecto con las propiedades bien puestas)
            string filePath = "datos.json";

            // 2. Comprobamos si el archivo existe para evitar cuelgues
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"No se encontró el archivo: {filePath}");
            }

            // 3. Abrimos el archivo (Versión C# 7.3, a prueba de errores)
            using (FileStream stream = File.OpenRead(filePath))
            {
                // 4. Ignoramos mayúsculas/minúsculas al leer
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // 5. Convertimos el archivo JSON a nuestra lista de objetos en C#
                var objects = await JsonSerializer.DeserializeAsync<List<ApiObject>>(stream, options);

                return objects ?? new List<ApiObject>();
            }
        }
    }
}