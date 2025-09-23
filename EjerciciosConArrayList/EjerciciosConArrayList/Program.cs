using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosConArrayList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayList lista = new ArrayList();
            lista.Add(10);
            lista.Add("Hola");
            lista.Add(3.14);    

            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();

        }
    }
}
