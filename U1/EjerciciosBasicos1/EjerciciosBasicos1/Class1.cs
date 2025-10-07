using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosBasicos1
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            // Ejercicio 1: Convertir una cadena a mayúsculas y obtener su longitud
            string texto = "Hola Mundo";
            string textoMayusculas = texto.ToUpper();
            int longitud = texto.Length;
            bool contieneHola = texto.Contains("Hola");
            Console.WriteLine($"Original: {texto}");
            Console.WriteLine($"Mayúsculas: {textoMayusculas}");
            Console.WriteLine($"Longitud: {longitud}");
            Console.WriteLine($"Contiene hola: {contieneHola}");
            Console.ReadKey();

        }
    }
}
