using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicios1;

namespace ejercicios1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Coche coche1 = new Coche(1, "BMW", "Z4", 100, 54000);

            Console.WriteLine(coche1.GetMarca());
            Console.WriteLine(coche1.GetModelo());
            Console.WriteLine(coche1.GetKm());
            Console.WriteLine(coche1.GetPrecio());

            coche1.SetPrecio(24000);


            //Creamos 4 coches mas y los mostramos por consola
            Coche coche2 = new Coche(2, "Audi", "A3", 200, 30000);
            Coche coche3 = new Coche(3, "Mercedes", "Clase A", 150, 35000);
            Coche coche4 = new Coche(4, "Ford", "Focus", 250, 20000);
            Coche coche5 = new Coche(5, "Toyota", "Corolla", 300, 22000);
            List<Coche> coches = new List<Coche>();
            coches.Add(coche1);
            coches.Add(coche2);
            coches.Add(coche3);
            coches.Add(coche4);
            coches.Add(coche5);
            //Recorremos la lista con foreach y mostramos todo
            foreach (Coche coche in coches)
            {
                Console.WriteLine(coche.ToString());
            }
            Console.ReadKey();


        }
    }
}

