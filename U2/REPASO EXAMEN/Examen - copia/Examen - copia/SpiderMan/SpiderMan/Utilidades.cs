using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderMan
{
    internal class Utilidades
    {
        // Generador de números aleatorios, compartido por toda la clase
        public static Random r = new Random();

        // Genera y devuelve un número entero aleatorio entre 0 y 6
        // Este número se usa para determinar el tipo de casilla (NEUTRO, DUENDE, etc.)
        public static int NumeroAleatorio()
        {
            return r.Next(0, 7);
        }
    }
}
