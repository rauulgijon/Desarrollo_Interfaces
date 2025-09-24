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

        public static void PatternA()
        {
            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
        }
        
        public static void PatternB()
        {
            for (int i = SIZE; i >= 1; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
        }

        public static void PatternC()
        {
            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j > i; j++)
                {
                    Console.Write("  ");
                    Console.Write("* ");
                }
                    
                
                Console.WriteLine();
            }
        }

        public static void PatternD()
        {
            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = SIZE; j > i; j--)
                {
                    Console.Write("  ");
                }
                for (int k = 1; k <= i; k++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            PatternA();
            Console.WriteLine();
            PatternB();
            Console.WriteLine();
            PatternC();
            Console.WriteLine();
            PatternD();
        }
    }
}
