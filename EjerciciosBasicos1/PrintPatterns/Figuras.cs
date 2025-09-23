using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrintPatterns
{
    internal class Class1
    {
        const int SIZE = 6;

        // (a) Triángulo invertido
        public static void PatternA()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE - i; j++)
                    Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (b) Triángulo recto
        public static void PatternB()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j <= i; j++)
                    Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (c) Triángulo invertido alineado a la derecha
        public static void PatternC()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < i; j++) Console.Write("  ");
                for (int j = 0; j < SIZE - i; j++) Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (d) Triángulo recto alineado a la derecha
        public static void PatternD()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE - i - 1; j++) Console.Write("  ");
                for (int j = 0; j <= i; j++) Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (e) Cuadro lleno
        public static void PatternE()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                    Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (f) Cuadro con diagonal principal
        public static void PatternF()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i == j) Console.Write("# ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        // (g) Cuadro con diagonal secundaria
        public static void PatternG()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i + j == SIZE - 1) Console.Write("# ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        // (h) Cuadro con ambas diagonales
        public static void PatternH()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i == j || i + j == SIZE - 1) Console.Write("# ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        // (i) Borde del cuadrado
        public static void PatternI()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i == 0 || i == SIZE - 1 || j == 0 || j == SIZE - 1)
                        Console.Write("# ");
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        // (j) Pirámide invertida
        public static void PatternJ()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < i; j++) Console.Write("  ");
                for (int j = 0; j < (SIZE - i); j++) Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (k) Diamante lleno
        public static void PatternK()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE - i - 1; j++) Console.Write("  ");
                for (int j = 0; j < (2 * i + 1); j++) Console.Write("# ");
                Console.WriteLine();
            }
            for (int i = SIZE - 2; i >= 0; i--)
            {
                for (int j = 0; j < SIZE - i - 1; j++) Console.Write("  ");
                for (int j = 0; j < (2 * i + 1); j++) Console.Write("# ");
                Console.WriteLine();
            }
        }

        // (l) Diamante hueco
        public static void PatternL()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE - i - 1; j++) Console.Write("  ");
                for (int j = 0; j < (2 * i + 1); j++)
                {
                    if (j == 0 || j == 2 * i) Console.Write("# ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
            for (int i = SIZE - 2; i >= 0; i--)
            {
                for (int j = 0; j < SIZE - i - 1; j++) Console.Write("  ");
                for (int j = 0; j < (2 * i + 1); j++)
                {
                    if (j == 0 || j == 2 * i) Console.Write("# ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            PatternA();
            PatternB();
            PatternC();
            PatternD();
            PatternE();
            PatternF();
            PatternG();
            PatternH();
            PatternI();
            PatternJ();
            PatternK();
            PatternL();
        }
    }
}
