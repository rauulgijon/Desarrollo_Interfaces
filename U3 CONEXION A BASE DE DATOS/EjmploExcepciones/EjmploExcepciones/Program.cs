using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjmploExcepciones
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("=== PARTE 1: SIMULACIÓN DE EXCEPCIONES ===\n");

            try
            {
                Console.WriteLine("--- Cálculo de División ---");

                Console.Write("Introduce el numerador (número a dividir): ");
                int numerador = int.Parse(Console.ReadLine());

                Console.Write("Introduce el divisor (número por el cual dividir): ");
                int divisor = int.Parse(Console.ReadLine());

                int resultado = numerador / divisor;

                Console.WriteLine($"Resultado: {numerador} / {divisor} = {resultado}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: No es posible dividir por cero.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Por favor, introduce solo números enteros.");
            }

            Console.WriteLine(); 

            string[] colores = { "Rojo", "Verde", "Azul", "Amarillo" };

            try
            {
                Console.WriteLine("--- Acceso a Elemento de Array ---");
                Console.WriteLine($"El array tiene {colores.Length} elementos.");

                
                Console.Write("Introduce un índice para ver el color (0-3): ");
                int indice = int.Parse(Console.ReadLine());

                string colorSeleccionado = colores[indice];

                Console.WriteLine($"Elemento seleccionado: {colorSeleccionado}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error: El índice está fuera del rango del array.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Por favor, introduce un número válido.");
            }

            Console.WriteLine("\nPresiona cualquier tecla para finalizar...");
            Console.ReadKey();
        }
    }
}
