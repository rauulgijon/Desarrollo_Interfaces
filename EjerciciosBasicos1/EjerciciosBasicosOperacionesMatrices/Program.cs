using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosBasicosOperacionesMatrices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrizA = new int[2, 3]
            { { 1, 2, 3 }, 
            { 4, 6, 7} };

            for (int i = 0; i < matrizA.GetLength(0); i++)
            {
                for (int j = 0; j < matrizA.GetLength(1); j++)
                {
                    Console.Write(matrizA[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        
    }
}
