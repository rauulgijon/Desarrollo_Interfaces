using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploListT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear uana lista de enteros
            List<int> numeros = new List<int>();
            //Agregar elementos a la lista
            numeros.Add(5);
            numeros.Add(10);
            numeros.Add(15);
            // Recorrer e imprimir los elementos de la lista
            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }
            numeros.Remove(10);
            Console.WriteLine("Después de eliminar 10:");
            foreach (var item in numeros)
            {
                Console.WriteLine(item);
            }


            Console.ReadKey();



        }
    }
}
