using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiderman
{
    internal class Aplicacion
    {
        static void Main(string[] args)
        {
            // Crear el juego
            Juego juego = new Juego();

            // Iniciar el juego (bucle principal)
            juego.Iniciar();

            Console.WriteLine("\n Gracias por jugar Spiderman: Misión en Nueva York!");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
