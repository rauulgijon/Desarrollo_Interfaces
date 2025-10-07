using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosBasicosBuclesControlFlujo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //bucle for
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Número: {i}");
            }
            Console.ReadKey();
            string[] frutas = { "Manzana", "Banana", "Cereza" };
            //bucle foreach
            foreach (string fruta in frutas)
            {
                Console.WriteLine($"Fruta: {fruta}");
            }
            Console.ReadKey();

        }
    }
}
