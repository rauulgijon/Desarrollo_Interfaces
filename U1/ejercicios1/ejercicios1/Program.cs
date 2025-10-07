using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ejercicios1;

namespace ejercicios1
{
    internal class Program
    {
        private const int V = 4;

        static void Main(string[] args)
        {
            //Coche coche1 = new Coche(1, "BMW", "Z4", 100, 54000);

            /**
             * Esto es lo hecho en clase
            Console.WriteLine(coche1.GetMarca());
            Console.WriteLine(coche1.GetModelo());
            Console.WriteLine(coche1.GetKm());
            Console.WriteLine(coche1.GetPrecio());

            coche1.SetPrecio(24000);
            */

            //Creamos 4 coches mas y los mostramos por consola
            Coche coche2 = new Coche(2, "Audi", "A3", 200, 30000);
            Coche coche3 = new Coche(3, "Mercedes", "Clase A", 150, 35000);
            Coche coche4 = new Coche(4, "Ford", "Focus", 250, 20000);
            Coche coche5 = new Coche(5, "Toyota", "Corolla", 300, 22000);
            List<Coche> coches = new List<Coche>();
            //coches.Add(coche1);
            coches.Add(coche2);
            coches.Add(coche3);
            coches.Add(coche4);
            coches.Add(coche5);
            //Recorremos la lista con foreach y mostramos todo
            foreach (Coche coche in coches)
            {
                Console.WriteLine(coche.ToString());
            }


            Utilidades u = new Utilidades();
            Coche[,] c = new Coche[4, 4];

            for (int i = 0; i < c.GetLength(0); i++) {
                for (int j = 0; j < c.GetLength(1); j++) {
                    int num = u.randomNumber(0, coches.Count -1);
                    c [i,j] = coches[num];

                }
            }

            // Mostramos la matriz
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    Console.Write(c[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }

            Producto[] lista = new Producto[2];


            //Generamos aleatoriamente una cantidad y un precio para crear un producto
            for (int i = 0; i < lista.Length; i++)
            {
                int cantidad = u.randomNumber(1, 10);
                double precio = u.randomNumber(100, 1000);
                Producto producto = new Producto(cantidad, precio);
                lista[i] = producto;

            }

            // Mostramos en una tabla la cantidad, el precio y el precio final
            Console.WriteLine("Cantidad\tPrecio\tPrecio Final");
            foreach (Producto p in lista)
            {
                Console.WriteLine($"{p.GetCantidad()}\t\t{p.GetPrecio()}\t{p.precioFinal()}");
            }
           

            Console.ReadKey();
        }
    }
}

