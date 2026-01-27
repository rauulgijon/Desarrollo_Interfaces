using System;

namespace Carretero_Millan_Asier_Examen1
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var tablero = new Tablero();
            tablero.Iniciar();
            Console.WriteLine("Fin del juego. Pulsa una tecla para cerrar.");
            Console.ReadKey();
        }
    }
}
