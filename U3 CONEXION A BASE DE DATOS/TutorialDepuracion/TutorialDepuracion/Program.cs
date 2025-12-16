using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialDepuracion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random randomGenerator = new Random();
            double num1 = randomGenerator.NextDouble();
            double num2 = randomGenerator.NextDouble();
            Console.WriteLine("Numero 1: " + num1);
            Console.WriteLine("Numero 2: " + num2);

            CalcularOperaciones(num1, num2);
            Console.ReadLine();
        }

        static void CalcularOperaciones(double num1, double num2)
        {
            double resultadoSuma = SumarNumeros(num1, num2);
            double resultadoDivision = DividirNumeros(num1, num2);
        }

        static double SumarNumeros(double num1, double num2) 
        {
            double resultado = num1 + num2;
            Console.WriteLine("Resultado de la suma: " + resultado);
            return resultado;
        }

        static double DividirNumeros(double num1, double num2)
        {
            double resultado = num1 / num2;
            Console.WriteLine("Resultado de la división: " + (double)resultado);
            return resultado;
        }

        static void MultiplicarNmeros()
        {
            Random r = new Random();
            double num1 = r.NextDouble();
            double num2 = r.NextDouble();
            double result = num1 * num2;
            Console.WriteLine("Resultado de la multiplicacion: " + result);
        }
    }
}
