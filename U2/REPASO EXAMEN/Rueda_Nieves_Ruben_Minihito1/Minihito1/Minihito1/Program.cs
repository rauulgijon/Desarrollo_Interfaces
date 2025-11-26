using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minihito1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Primero inicializaremos tanto las matrices que van a ser nuestro tablero como las variables que vayamos a utilizar
            String[,] tablero = Utilidades.generarMatriz(".", 15, 15);
            
            Console.Write("Cual es el tamaño maximo que pueden tener los barcos (este no puede ser menor que 2 ni mayor que 5): ");
            int tamanioBarcoMax = int.Parse(Console.ReadLine());
            while (tamanioBarcoMax > 5 || tamanioBarcoMax < 2)
            {
                Console.WriteLine("Ese tamaño no es posible introducelo otra vez: ");
                tamanioBarcoMax = int.Parse(Console.ReadLine());
            }

            Utilidades.colocarBarcos(tablero, tamanioBarcoMax);
            Utilidades.imprimirTablero(tablero);
            Console.ReadKey();
        }
    }
}
