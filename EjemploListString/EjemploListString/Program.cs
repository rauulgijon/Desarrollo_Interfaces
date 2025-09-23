using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploListString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>() { "Juan", "Manuel", "Alejandra" };
            foreach (string s in list)
            {
                if (s.StartsWith("J"))
                {
                    Console.WriteLine(s);
                }
            }
            Console.ReadKey();
        }
    }
}
