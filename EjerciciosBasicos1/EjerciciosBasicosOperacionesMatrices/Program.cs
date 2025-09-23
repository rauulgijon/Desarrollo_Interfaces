using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosBasicosOperacionesMatrices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrizA = new int[2, 3] {
            { 1, 2, 3 }, 
            { 4, 6, 7} };

            int[,] matrizB = new int[2, 3] {
            { 7, 8, 9 },
            { 10, 11, 12 } };

            int[,] resultado = new int[2, 3];

            for (int i = 0; i < matrizA.GetLength(0); i++)
            {
                for (int j = 0; j < matrizA.GetLength(1); j++)
                {
                    resultado[i,j] = matrizA[i, j] + matrizB[i, j];
                    Console.Write(resultado[i, j] + "\t");
                }
                Console.WriteLine();
            }




            Console.ReadKey();
        }
        
    }
}
