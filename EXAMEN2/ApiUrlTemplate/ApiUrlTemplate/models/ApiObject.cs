using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUrlTemplate.Models
{
    public class ApiObject
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        // Añade aquí más campos si el examen lo pide (ej: public string email { get; set; })
    }
}
